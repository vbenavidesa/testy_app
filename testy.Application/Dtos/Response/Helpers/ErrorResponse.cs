using testy.Common;

namespace testy.Application.Dtos.Response.Helpers
{
    public class ErrorResponse : BaseDto
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
