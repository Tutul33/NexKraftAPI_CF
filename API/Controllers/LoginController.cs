using API.BusinessLogic.Interface.ILogin;
using API.ServiceRegister;
using API.Utility;
using API.ViewModel.ViewModels.Customers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]"), Produces("application/json"), EnableCors()]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ILoginServices loginServices;
        public LoginController(ILoginServices _loginServices)
        {
            loginServices = _loginServices;
        }
        [HttpPost("[action]")]
        public async Task<object> UserLogin(Login credential)
        {
            object result = new object();
            try
            {
                string userAgent = Request.Headers["User-Agent"].ToString();
                string? RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                result = await loginServices.LoginUser(credential, userAgent,RemoteIpAddress);
                if (result == null)
                {
                    result = new { message = "User not foud.", resstate = false };
                }
                Logs.WriteLogFile("Success");
            }
            catch (Exception ex)
            {
                ex.ToString();
                Logs.WriteLogFile(ex.ToString());
            }
            return result;
        }
        [HttpPost("[action]")]
        public async Task<object?> UserRegistration([FromBody] CustomerModel data)
        {
            object? resdata = null;
            try
            {
                resdata = await loginServices.UserRegistration(data);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;

        }
        [HttpPost("[action]")]
        public async Task<object> GenerateNewToken(LoginModel model)
        {
            string token = string.Empty;object result = new object();
            try
            {
                string userAgent = Request.Headers["User-Agent"].ToString();
                string RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                token = await loginServices.GenerateNewToken(model,userAgent,RemoteIpAddress);
                if (string.IsNullOrEmpty(token))
                {
                    result = new { message = "User is not exist.", resstate = false };
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new { token };
        }
        [HttpPost("[action]")]
        public async Task<object> ChangePassword(LoginModel model)
        {
            object result = new object();
            try
            {
                string userAgent = Request.Headers["User-Agent"].ToString();
                string RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                result = await loginServices.ChangePassword(model);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }
        [HttpPost("[action]")]
        public async Task<object> SendEmailToChangePassword(LoginModel model)
        {
            object result = new object();
            try
            {
                string userAgent = Request.Headers["User-Agent"].ToString();
                string RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                result = await loginServices.SendEmailToChangePassword(model, userAgent, RemoteIpAddress);                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }

        [HttpGet("DeycryptLoginPasswordKey/{passKey}")]//, Authorizations
        public async Task<object> DeycryptLoginPasswordKey(string passKey)
        {
            object result = new object();
            try
            {
                string userAgent = Request.Headers["User-Agent"].ToString();
                string RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                result = await loginServices.DeycryptLoginPasswordKey(passKey);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }

    }
}
