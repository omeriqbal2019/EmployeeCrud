using Employee.Services.Auth;
using Employee.Services.EmployeeService;
using Microsoft.Extensions.DependencyInjection;
namespace Employee.Services
{
    public static class ServicesModule
    {
        //public static DateTime CurrentDateTime()
        //{
        //    var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        //    return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternZone);
        //}

        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEmployeeService,  EmployeeService.EmployeeService>();
           

            

        }
    }
}
