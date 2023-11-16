using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using testy.Domain.Entities;

namespace testy.Application.Common.Interfaces
{
    public interface ITestyDbContext
    {
        #region Interface DbSets
        DbSet<ContactMaster> ContactMasters { get; set;}
        #endregion

        Task<int> ExecuteSQLRawAsync(string sql, params object[] parameters);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
