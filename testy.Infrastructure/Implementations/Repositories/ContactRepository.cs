using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testy.Application.Common.Interfaces;
using testy.Application.Common.Interfaces.Repositories;
using testy.Application.Dtos.Request.Helpers;
using testy.Domain.Entities;
using testy.Infrastructure.Attributes;

namespace testy.Infrastructure.Implementations.Repositories
{
    [ScopedService]
    public class ContactRepository : IContactRepository
    {
        #region Constructor
        private readonly ITestyDbContext _context;
        public ContactRepository(ITestyDbContext context)
        {
            _context = context;
        }
        #endregion

        #region public async Task<ContactMaster> GetContactByIdAsync(string Id)
        public async Task<ContactMaster> GetContactByIdAsync(string Id)
        {
            return await _context.ContactMasters
                .Where(x => x.Status != "D")
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
        #endregion

        #region public async Task<List<ContactMaster>> GetContactsAsync()
        public async Task<List<ContactMaster>> GetContactsAsync()
        {
            return await _context.ContactMasters
                .Where(x => x.Status != "D")
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion

        #region public async Task<List<ContactMaster>> GetContactsPaginatedAsync(PageRequest Request)
        public async Task<List<ContactMaster>> GetContactsPaginatedAsync(PageRequest Request)
        {
            var skip = (Request.PageNumber - 1) * Request.PageSize;
            return await _context.ContactMasters
                .Where(x => x.Status != "D")
                .Skip(skip)
                .Take(Request.PageSize)
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion

        #region public async Task<List<ContactMaster>> GetContactsForCompanyAsync(int CompanyId)
        public async Task<List<ContactMaster>> GetContactsForCompanyAsync(int CompanyId)
        {
            return await _context.ContactMasters
                .Where(x => x.Status != "D")
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion

        #region public async Task<ContactMaster> CreateContactAsync(ContactMaster Contact)
        public async Task<ContactMaster> CreateContactAsync(ContactMaster Contact)
        {
            await _context.ContactMasters.AddAsync(Contact);
            await _context.SaveChangesAsync();
            return Contact;
        }
        #endregion

        #region public async Task<ContactMaster> UpdateContactAsync(ContactMaster Contact)
        public async Task<ContactMaster> UpdateContactAsync(ContactMaster Contact)
        {
            _context.ContactMasters.Update(Contact);
            await _context.SaveChangesAsync();
            return Contact;
        }
        #endregion

        #region public async Task<ContactMaster> DeleteContactAsync(ContactMaster Contact)
        public async Task<ContactMaster> DeleteContactAsync(ContactMaster Contact)
        {
            _context.ContactMasters.Remove(Contact);
            await _context.SaveChangesAsync();
            return Contact;
        }
        #endregion

        #region public int CountRecords()
        public int CountRecords()
        {
            return _context.ContactMasters.Count();
        }
        #endregion
    }
}
