using authAPIpoc.Models;
using authAPIpoc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace authAPIpoc.Controllers.auth
{
    //[Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthController(IUserService svcUser, IConfiguration config)
        {
            userService = svcUser;
        }



 
        [HttpPost(CONSTANTS.ROUTES.USER.AUTHENTICATION_POST)]
        public IActionResult Post([FromBody] LoginData value)
        {
            Response res = null;
            bool rtnVal = false;
            try
            {
                res = new Response();
                if(value != null)
                {
                    res = userService.loginUser(value);
                    if (res.status == "SUCCESS")
                    {
                        /******************** set coockie ********************/
                        //var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions()
                        //{
                        //    Path = "/",
                        //    HttpOnly = true,
                        //    IsEssential = true, //<- there
                        //    Expires = DateTime.Now.AddMinutes(20),
                        //    Domain = ".byrebarn.com",
                        //};
                        //Response.Cookies.Append("X-Access-Token", tokenString, cookieOptions);
                        /******************** set coockie ********************/
                        rtnVal = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                res.message = CONSTANTS.MESSAGES.ERROR;
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }

            if (rtnVal == false)
                return BadRequest(res);
            return Ok(res);
        }



        [HttpPost(CONSTANTS.ROUTES.USER.REGISTRATION_POST)]
        public IActionResult Post([FromBody] UserRegistrationData data)
        {

            Response res = null;
            bool rtnVal = false;

            try
            {
                res = new Response();


                if (userService.IsUserExists(data.registrationDetails.email))
                    res.message = CONSTANTS.MESSAGES.REGISTRATION.REGISTRATION_EXISTS;
                else
                {
                    User userData = new User();
                    userData.firstName = data.registrationDetails.firstName;
                    userData.lastName = data.registrationDetails.lastName;
                    userData.email = data.registrationDetails.email;

                    rtnVal = userService.insertUser(userData, data.password);

                    if (rtnVal)
                    {
                        res.status = CONSTANTS.MESSAGES.STATUS_SUCCESS;
                        res.message = CONSTANTS.MESSAGES.REGISTRATION.REGISTRATION_SUCCESS;
                    }
                    else
                        res.message = CONSTANTS.MESSAGES.REGISTRATION.REGISTRATION_FAILED;
                }
                

            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                res.message = CONSTANTS.MESSAGES.ERROR;
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }

            if (rtnVal == false)
                return BadRequest(res);
            return Ok(res);
        }


    
    }
}
