using System.ComponentModel.DataAnnotations;
namespace Employee.Models.ApiModelsDto.AuthenticationDto
{
    public class AuthenticateUserDto
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}
