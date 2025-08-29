using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class LoginSuccessDTO
    {
        [Required]
        public string Token { get; set; } = "";
    }
}
