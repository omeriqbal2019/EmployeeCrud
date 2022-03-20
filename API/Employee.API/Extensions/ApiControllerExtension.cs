using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Employee.API.Extensions
{
    public abstract class ApiControllerExtension: ControllerBase
    {

        [NonAction]
        public HeaderData GetProfile()
        {
            string response = Request.Headers["Authorization"];
            if (response == null)
                return null;
            response = response.Replace("Bearer ", "");
            var res = GetTokenData(response);
            HeaderData header = new HeaderData()
            {
                EmployeeId = Convert.ToInt32(res["groupsid"]),
                EmployeeName = res["unique_name"],
                UserName=res["nameid"]

              
            };
            return header;
        }

        public static Dictionary<string, string> GetTokenData(string tokenHeader)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtInput = tokenHeader;
            var readableToken = jwtHandler.CanReadToken(jwtInput);
            Dictionary<string, string> headerData = new Dictionary<string, string>();

            if (readableToken)
            {
                var token = jwtHandler.ReadJwtToken(jwtInput);
                var claims = token.Claims;

                foreach (Claim c in claims)
                {
                    headerData.Add(c.Type, c.Value);
                }
            }
            return headerData;
        }


    }

    public class HeaderData
    {
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string EmployeeName { get; set; }
    }
}
