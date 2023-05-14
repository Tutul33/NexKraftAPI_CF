using API.ViewModel.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Interface.ILogin
{
    public interface ILoginServices
    {
        Task<object> LoginUser(Login credential, string userAgent, string remoteIpAddress);
        Task<string> GenerateNewToken(LoginModel userInfo, string userAgent, string remoteIpAddress);
        Task<object> ChangePassword(LoginModel userInfo);
        Task<object> SendEmailToChangePassword(LoginModel userInfo, string userAgent, string remoteIpAddress);
        Task<object> DeycryptLoginPasswordKey(string ChangePassKey);
        Task<object?> UserRegistration(CustomerModel data);
    }
}
