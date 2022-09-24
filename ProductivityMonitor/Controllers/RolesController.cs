using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductivityMonitor.Models;
using ProductivityMonitor.Models.Input;

namespace ProductivityMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private RoleManager<CustomRole> roleManager;
        private UserManager<CustomUser> userManager;

        public RolesController(RoleManager<CustomRole> roleManager, UserManager<CustomUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        //create new role
        [HttpPost]
        public async Task<ActionResult<RoleModel>> CreateRole(RoleModel roleData)
        {
            if (!await roleManager.RoleExistsAsync(roleData.RoleName))
            {
                //map the rolemodel to customrole indentity model
                CustomRole role = new CustomRole()
                {
                    Name = roleData.RoleName,
                    Role_Desc = roleData.RoleDescription
                };
                //create the role
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(roleData);
                }
                else
                {
                    var message = string.Join("\n", result.Errors.Select(x => "Code : " + x.Code + " Description : " + x.Description));
                    return BadRequest(message);
                }
            }
            else
            {
                return BadRequest("role already exists");
            }
        }

        //add user to role
        [HttpPost("Users")]

        public async Task<ActionResult<UserRoleModel>> AddUserToRole([FromBody]UserRoleModel userRoleData)
        {
            //get user identity modal using user id
            CustomUser user = await userManager.FindByIdAsync(userRoleData.UserId);
        
            //check if user already had the role
            if(await userManager.IsInRoleAsync(user,userRoleData.RoleName))
            {
                return BadRequest("User already has the role");
            }
            else
            {
                //add the user to the role
                IdentityResult result = await userManager.AddToRoleAsync(user, userRoleData.RoleName);
                if (result.Succeeded)
                {
                    return Ok(userRoleData);
                }
                else
                {
                    //return the error message
                    var message = string.Join("\n", result.Errors.Select(x => "Code : " + x.Code + " Description : " + x.Description));
                    return BadRequest(message);
                }
            }
        }
    }
}
