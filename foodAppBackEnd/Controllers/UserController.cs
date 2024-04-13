using BAL.IBAL;
using Microsoft.AspNetCore.Mvc;
using Models.ResponseModel;
using Models.ViewModels;

namespace foodAppBackEnd.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private IUserBAL _user;
        public UserController(IUserBAL bAL)
        {
            this._user = bAL;
        }
        [HttpPost("UserRegister")]
        public async Task<IActionResult> post([FromBody] User user)
        {
            Response response = new Response();
            try
            {
                User u = new User();
                u = await _user.UserRegister(user);
                if (u != null)
                {
                    response.success = true;
                    response.message = "User Save successfully";
                    response.objectResponse = u;
                }
                else
                {
                    response.success = false;
                    response.message = "something went wrong";
                }
            }
            catch(Exception e)
            {
                response.success = false;
                response.message = "something went wrong";
                response.objectResponse = e;
            }
            
            return Ok(response);
        }
        [HttpPost("UserLogin")]
        public IActionResult post([FromBody] string userName, string password)
        {
            Response r = new Response();
            try 
            {
                
            }
            catch(Exception e)
            {
                r.success = false;
                r.message = "Something Went wron";
                r.objectResponse = e;
            }
            return Ok();
        }
    }
}
