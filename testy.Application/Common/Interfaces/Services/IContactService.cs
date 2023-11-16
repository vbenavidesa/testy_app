using System.Collections.Generic;
using System.Threading.Tasks;
using testy.Application.Dtos.Request;
using testy.Application.Dtos.Request.Helpers;
using testy.Application.Dtos.Response;
using testy.Application.Dtos.Response.Helpers;

namespace testy.Application.Common.Interfaces.Services
{
    public interface IContactService
    {
        Task<ContactResponseDto> GetContactByIdAsync(string Id);
        Task<List<ContactResponseDto>> GetContactsAsync();
        Task<PaginatedList<ContactResponseDto>> GetContactsPaginatedAsync(PageRequest Request);
        Task<ServiceResult> CreateContactAsync(ContactRequestDto Contact);
        Task<ServiceResult> UpdateContactAsync(ContactRequestDto Contact);
        Task<ServiceResult> DeleteContactAsync(string Id);
    }
}
