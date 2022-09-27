using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductivityMonitor.Contracts;
using ProductivityMonitor.Models;
using ProductivityMonitor.Models.Input;
using ProductivityMonitor.Models.Resource;
using ProductivityMonitor.Utilities;

namespace ProductivityMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private UserManager<CustomUser> userManager;
        private RoleManager<CustomRole> roleManager;
        private AppDbContext appDbContext;
        private IJwtTokenManager jwtTokenManager;

        public AuthenticationController(UserManager<CustomUser> userManager,
            RoleManager<CustomRole> roleManager,
            AppDbContext appDbContext, IJwtTokenManager jwtTokenManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.appDbContext = appDbContext;
            this.jwtTokenManager = jwtTokenManager;
        }

        //register new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel)
        {
            //check if user already exists
            var userExists = await userManager.FindByEmailAsync(registerModel.Email);
            if(userExists != null)
            {
                return BadRequest($"user with email {registerModel.Email} already exists");
            }

            //create new customuser class
            CustomUser newUser = new CustomUser()
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                User_Empl_Id = registerModel.EmployeeId,
                User_CDate = DateTime.UtcNow,
                User_LuDate = DateTime.UtcNow,
                User_Status = "av"
            };
            //create the user
            var result = await userManager.CreateAsync(newUser, registerModel.Password);
            if (result.Succeeded)
            {
                return Ok("user created!");
            }
            else
            {
                var message = string.Join("\n", result.Errors.Select(x => "Code : " + x.Code + " Description : " + x.Description));
                return BadRequest(message);
            }
            
        }

        //authenticate user and return jwt token
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery]LoginModel loginModel)
        {
            var userExists = await userManager.FindByEmailAsync(loginModel.Email);
            if (userExists != null && await userManager.CheckPasswordAsync(userExists, loginModel.Password))
            {
                IList<string> roles = await userManager.GetRolesAsync(userExists);
                //generate token
                var token = jwtTokenManager.GenerateToken(userExists.UserName, roles);
                TokenRes tokenRes = new TokenRes()
                {
                    Token = token
                };
                return Ok(tokenRes);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
