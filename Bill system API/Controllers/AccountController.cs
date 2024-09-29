using Bill_system_API.DTOs;
using Bill_system_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bill_system_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(R_NewUserDTO user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = user.userName,
                    Email = user.email
                };
                IdentityResult result = await _userManager.CreateAsync(applicationUser, user.password);
                if (result.Succeeded)
                {
                    return Ok("Succeeded Regestration");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid) // Corrected from !ModelState.IsValid to ModelState.IsValid
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, login.Password))
                    {
                        #region Clamis
                        List<Claim> userData = new List<Claim>();
                        userData.Add(new Claim(ClaimTypes.Name, user.UserName));      // User Name
                        userData.Add(new Claim(ClaimTypes.Email, user.Email));       // Email
                        userData.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //id
                        userData.Add(new Claim(ClaimTypes.Role, "Admin"));          // Role
                        #endregion

                        #region secritKey
                        string key = "Welcom to my secrit key in Bill System";
                        var secritKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                        var signingCredentials = new SigningCredentials(secritKey, SecurityAlgorithms.HmacSha256);
                        #endregion
                        #region Generte Token
                        var token = new JwtSecurityToken(
                            //PayLoad
                            claims: userData, expires: DateTime.Now.AddDays(1),
                            //Signature => SecurityKey + HahAlgorithm
                            signingCredentials: signingCredentials
                            );
                        //TokenObj Encoding to String
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(new { token = tokenString }); // Return token as part of a JSON object
                        #endregion
                    }
                    else
                    {
                        return Unauthorized(); // Password mismatch
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email is Invalid");
                }
            }
            else
                return Unauthorized();
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Authorize]
        public IActionResult add()
        {
            return Ok("No");
        }
    }
}
