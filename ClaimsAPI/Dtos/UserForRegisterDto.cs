using System.ComponentModel.DataAnnotations;

namespace ClaimsAPI.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8,ErrorMessage="You must specify password 8 characters")]
        public string Password { get; set; }
    }
}