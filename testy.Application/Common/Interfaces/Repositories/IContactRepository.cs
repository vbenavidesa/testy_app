using System.Collections.Generic;
using System.Threading.Tasks;
using testy.Application.Dtos.Request.Helpers;
using testy.Domain.Entities;

namespace testy.Application.Common.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<ContactMaster> GetContactByIdAsync(string Id);
        Task<List<ContactMaster>> GetContactsAsync();
        Task<List<ContactMaster>> GetContactsPaginatedAsync(PageRequest Request);
        Task<ContactMaster> CreateContactAsync(ContactMaster Repository);
        Task<ContactMaster> UpdateContactAsync(ContactMaster Repository);
        Task<ContactMaster> DeleteContactAsync(ContactMaster Repository);
        int CountRecords();
    }
}
