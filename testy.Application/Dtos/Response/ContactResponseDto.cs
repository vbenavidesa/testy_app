using testy.Common;

namespace testy.Application.Dtos.Response
{
    public class ContactResponseDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
