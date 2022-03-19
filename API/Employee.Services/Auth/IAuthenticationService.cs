using Employee.Models.ApiModelsDto.AuthenticationDto;
namespace Employee.Services.Auth
{
    public interface IAuthenticationService
    {

        public UserTokenDto Authenticate(LoginDto _credentials);
    }
}
