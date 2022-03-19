namespace Employee.Models.ApiModelsDto.AuthenticationDto
{
    public class UserTokenDto
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public string RoleName { get; set; }
        public int EmployeeId { get; set; }
    }
}
