using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.IdentityModels;
using microsoft_lms_backend.ViewModels;

namespace microsoft_lms_backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public readonly JwtSettings appSettings;

        public AuthenticationController(ApplicationDbContext context, IOptions<JwtSettings> settings)
        {
            _context = context;
            appSettings = settings.Value;
        }

        [HttpPost]

        public ActionResult<GenericResponse<string>> Login([FromBody] LoginViewModel User)
        {
            try
            {
                if (User == null)
                {
                    return new GenericResponse<string>
                    {
                        Data = null,
                        Message = "Invalid Request",
                        Success = false

                    };
                }

                if (User.Email == appSettings.Email && User.Password == appSettings.Password)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: appSettings.Issuer,
                        audience: appSettings.Audience,
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials
                   );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                    return Ok(new GenericResponse<string>
                    {
                        Data = tokenString,
                        Message = "Token generated, Successfully!",
                        Success = true
                    });
                }
                else
                {
                    return Unauthorized(new GenericResponse<string>
                    {
                        Data = null,
                        Message = "Unauthorized, Please get a Token",
                        Success = false
                    });
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<string>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }
    }
}
