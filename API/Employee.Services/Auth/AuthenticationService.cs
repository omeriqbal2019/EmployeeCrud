using Employee.DBCore.Uow;
using Employee.Models.ApiModelsDto.AuthenticationDto;
using Employee.Models.Entities.EmployeeEntity;
using Employee.Utilities.Hashing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Linq;

namespace Employee.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public UserTokenDto Authenticate(LoginDto _credentials)
        {
            var Loginrepository = _unitOfWork.GetRepository<EmployeeEntity>();

            _credentials.Password = Sha.GenerateSha512String(_credentials.Password);
         
            var EmpData = Loginrepository.GetAll().SingleOrDefault(x => x.Username == _credentials.UserName && x.Password== _credentials.Password && x.IsDeleted!=true);
          
           
            if (EmpData == null)
                return null;

            string token = JwtToken.GenerateToken(EmpData.FirstName+' '+EmpData.LastName, EmpData.Username, EmpData.EmployeeId.ToString(), _appSettings.Secret);
            var userObj = new UserTokenDto
            {
                Token = "Bearer " + token,
                FirstName = EmpData.FirstName,
                LastName = EmpData.LastName,
                EmployeeId=EmpData.EmployeeId
            };
            return userObj;
        }
    }
}
