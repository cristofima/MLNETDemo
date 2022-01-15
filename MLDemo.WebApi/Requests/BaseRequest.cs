using System.ComponentModel.DataAnnotations;

namespace MLDemo.WebApi.Requests
{
    public class BaseRequest
    {
        [Required]
        [StringLength(200)]
        public string Message { get; set; }
    }
}
