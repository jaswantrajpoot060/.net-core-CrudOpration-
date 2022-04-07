using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TimeSheet
{
    public class JwtAuthenticationManager
    {
        //private readonly IConfiguration _configuration;
        //public static JwtAuthenticationManager(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //private  List<UserLogin> Login(string Email, string Password)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConncetion"));
        //    SqlCommand cmd = new SqlCommand
        //    {
        //        CommandType = CommandType.StoredProcedure,
        //        CommandText = "UserLogin"
        //    };
        //    cmd.Parameters.AddWithValue("@userid", Email);
        //    cmd.Parameters.AddWithValue("@password", Password);
        //    cmd.Connection = con;
        //    cmd.Connection.Open();
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    List<UserLogin> EmailList = new List<UserLogin>();
        //    while (reader.Read())
        //    {
        //        UserLogin Obj = new UserLogin(reader);
        //        EmailList.Add(Obj);
        //    }
        //    reader.Close();
        //    cmd.Connection.Close();
        //    return EmailList;
        //}

        public JwtAuthResponse Authenticate(string userName, string password)
        {
            //Validating the User Name and Password
            if(userName != "jaswant" || password != "jaswant")
            {
                return null;
            }

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(Constants.JWT_TOKEN_VALIDITY_MINS);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(Constants.JWT_SECURITY_KEY);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("username", userName),
                    new Claim(ClaimTypes.PrimaryGroupSid, "User Group 01")
                }),
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new JwtAuthResponse
            {
                token = token,
                user_name = userName,
                expires_in = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
        }
    }
}
