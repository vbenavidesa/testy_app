using testy.Common;

namespace testy.Application.Dtos.Response.Helpers
{
    public class ServiceResult
    {
        public int StatusCode { get; set; }
        public BaseDto Result { get; set; }
    }
}
