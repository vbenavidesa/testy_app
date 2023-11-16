using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using testy.Application.Common.Interfaces;
using testy.Common;
using testy.Domain.Entities;

namespace testy.Infrastructure.Persistence
{
    public partial class TestyDbContext : DbContext, ITestyDbContext
    {
        public IConfiguration Configuration { get; }
        private readonly ICurrentUserService _currentUserService;
        private readonly IDomainEventService _domainEventService;
        public TestyDbContext(DbContextOptions<TestyDbContext> options, IConfiguration configuration, ICurrentUserService currentUserService, IDomainEventService domainEventService) : base(options)
        {
            Configuration = configuration;
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
        }

        #region Implementations for DbSets
        public virtual DbSet<ContactMaster> ContactMasters { get; set;}
        #endregion

        public async Task<int> ExecuteSQLRawAsync(string sql, params object[] parameters)
        {
            return await Database.ExecuteSqlRawAsync(sql, parameters);
        }
        

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                // This middleware grabs the user from the jwt token that is associated with the call
                // for the purpose of the challenge I removed this as it will not be a secured request
                var entity = entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.Status = "A";
                        // entity.CreatedBy = _currentUserService.Email;
                        entity.CreatedBy = "Test user";
                        entity.CreatedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entity.Status = "U";
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        // entity.UpdatedBy = _currentUserService.Email;
                        entity.UpdatedBy = "Test user";
                        entity.UpdatedDate = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        // https://www.ryansouthgate.com/2019/01/07/entity-framework-core-soft-delete/
                        // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
                        entry.State = EntityState.Unchanged;

                        // Only update the IsDeleted flag - only this will get sent to the Db
                        entity.Status = "D";
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entry.Property(x => x.UpdatedBy).IsModified = false;
                        entry.Property(x => x.UpdatedDate).IsModified = false;
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEvents();
            return result;
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();

                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;

                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
