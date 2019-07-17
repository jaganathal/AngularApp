using System.ComponentModel.DataAnnotations;

namespace ClaimsAPI.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        public string  Username{ get; set; }
        
        [Required]   
        public string  Password{ get; set; }

    }
}