using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using testy.Application.Common.Interfaces.Services;
using testy.Application.Dtos.Request;
using testy.Application.Dtos.Request.Helpers;

namespace testy.Presentation.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("/api/contacts")]
    public class ContactsController : ControllerBase
    {
        #region Constructor
        private readonly IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }
        #endregion

        #region public async Task<IActionResult> GetContactById(int id)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            return Ok(await _contactService.GetContactByIdAsync(id));
        }
        #endregion

        #region public async Task<IActionResult> GetContacts()
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var _contacts = await _contactService.GetContactsAsync();
            return Ok(_contacts);
        }
        #endregion

        #region public async Task<IActionResult> GetContactsPaginated([FromQuery] PageRequest request)
        [HttpGet("paginated")]
        public async Task<IActionResult> GetContactsPaginated([FromQuery] PageRequest request)
        {
            var _contacts = await _contactService.GetContactsPaginatedAsync(request);
            return Ok(_contacts);
        }
        #endregion

        #region public async Task<IActionResult> CreateContact([FromBody] ContactRequestDto Contact)
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactRequestDto Contact)
        {
            var _result = await _contactService.CreateContactAsync(Contact);
            switch (_result.StatusCode)
            {
                case 200:
                    return CreatedAtAction(nameof(GetContactById), new { id = _result.Result }, _result.Result);

                case 400:
                    return BadRequest(_result.Result);

                default:
                    return BadRequest(_result.Result);
            }
        }
        #endregion

        #region public async Task<IActionResult> UpdateContact([FromBody] ContactRequestDto Contact)
        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] ContactRequestDto Contact)
        {
            var _result = await _contactService.UpdateContactAsync(Contact);
            switch (_result.StatusCode)
            {
                case 200:
                    return Ok(_result.Result);

                case 400:
                    return BadRequest(_result.Result);

                default:
                    return BadRequest(_result.Result);
            }
        }
        #endregion

        #region public async Task<IActionResult> DeleteContact(int id)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var _result = await _contactService.DeleteContactAsync(id);
            switch (_result.StatusCode)
            {
                case 200:
                    return Ok(_result.Result);

                case 400:
                    return BadRequest(_result.Result);

                default:
                    return BadRequest(_result.Result);
            }
        }
        #endregion
    }
}
