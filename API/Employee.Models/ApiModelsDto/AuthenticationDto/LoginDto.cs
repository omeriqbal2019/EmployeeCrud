using System.ComponentModel.DataAnnotations;
namespace Employee.Models.ApiModelsDto.AuthenticationDto
{
    public class LoginDto
    {
       
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
