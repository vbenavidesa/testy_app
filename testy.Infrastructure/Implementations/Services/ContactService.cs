using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;
using testy.Application.Common.Interfaces.Repositories;
using testy.Application.Common.Interfaces.Services;
using testy.Application.Dtos.Request;
using testy.Application.Dtos.Request.Helpers;
using testy.Application.Dtos.Response;
using testy.Application.Dtos.Response.Helpers;
using testy.Domain.Entities;
using testy.Infrastructure.Attributes;

namespace testy.Infrastructure.Implementations.Services
{
    [ScopedService]
    public class ContactService : IContactService
    {
        #region Constructor
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        #endregion

        #region public async Task<ContactResponseDto> GetContactByIdAsync(string Id)
        public async Task<ContactResponseDto> GetContactByIdAsync(string Id)
        {
            var _contact = await _contactRepository.GetContactByIdAsync(Id);
            return _contact.Adapt<ContactResponseDto>();
        }
        #endregion

        #region public async Task<List<ContactResponseDto>> GetContactsAsync()
        public async Task<List<ContactResponseDto>> GetContactsAsync()
        {
            var _contacts = await _contactRepository.GetContactsAsync();
            return _contacts.Adapt<List<ContactResponseDto>>();
        }
        #endregion

        #region public async Task<PaginatedList<ContactResponseDto>> GetContactsAsync(PageRequest Request)
        public async Task<PaginatedList<ContactResponseDto>> GetContactsPaginatedAsync(PageRequest Request)
        {
            var _contacts = await _contactRepository.GetContactsPaginatedAsync(Request);
            var _dtos = _contacts.Adapt<List<ContactResponseDto>>();
            var _count = _contactRepository.CountRecords();
            return new PaginatedList<ContactResponseDto>(_dtos, _count, Request.PageNumber, Request.PageSize);
        }
        #endregion

        #region public async Task<ServiceResult> CreateContactAsync(ContactRequestDto Contact)
        public async Task<ServiceResult> CreateContactAsync(ContactRequestDto Contact)
        {
            var _contact = Contact.Adapt<ContactMaster>();
            _contact = await _contactRepository.CreateContactAsync(_contact);
            return new ServiceResult { StatusCode = 200, Result = _contact.Adapt<ContactResponseDto>() };
        }
        #endregion

        #region public async Task<ServiceResult> UpdateContactAsync(ContactRequestDto Contact)
        public async Task<ServiceResult> UpdateContactAsync(ContactRequestDto Contact)
        {
            var _update = Contact.Adapt<ContactMaster>();
            var _result = await _contactRepository.UpdateContactAsync(_update);
            return new ServiceResult { StatusCode = 200, Result = _result.Adapt<ContactResponseDto>() };
        }
        #endregion

        #region public async Task<ServiceResult> DeleteContactAsync(string Id)
        public async Task<ServiceResult> DeleteContactAsync(string Id)
        {
            var _contact = await _contactRepository.GetContactByIdAsync(Id);
            _contact = await _contactRepository.DeleteContactAsync(_contact);
            return new ServiceResult { StatusCode = 200, Result = _contact.Adapt<ContactResponseDto>() };
        }
        #endregion
    }
}
