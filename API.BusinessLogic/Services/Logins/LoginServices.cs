using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.DataAccess.ORM.CodeFirst;
//using API.DataAccess.ORM.MsSQLDataModels;
using API.ViewModel.ViewModels.Customers;
using API.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Utility;
using API.BusinessLogic.Interface.ILogin;
using API.ViewModel.ViewModels.Menu;
using static API.Settings.StaticInfos;
using API.ViewModel.ViewModels.UserRole;

namespace API.BusinessLogic.Services.Logins
{
    public class LoginServices : ILoginServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> LoginUser(Login credential, string userAgent, string remoteIpAddress)
        {
            bool resstate = false; string token = string.Empty; string roleName = "";
            LoginModel? loginModel = null;
            UserLogin? loggedUser = null;
            Customer? customer = null;
            DateTime? expireDate = new DateTime();
            List<vmModule> modules = new();

            try
            {
                string encryptedPass = CommonExt.Encryptdata(credential.Password);
                loggedUser = await _unitOfWork.UserLoginRepository.GetUserInfo(credential.UserName, encryptedPass);
                if (loggedUser != null)
                {
                    var userRole=(await _unitOfWork.UserRoleRepository.GetUserRoleList()).Where(x=>x.LoginId==loggedUser.LoginId).FirstOrDefault();
                    //var role = (await _unitOfWork.RoleRepository.GetRoleList()).Where(x => x.RoleId == loggedUser.RoleId).FirstOrDefault();
                    var role = (await _unitOfWork.RoleRepository.GetRoleList()).Where(x => x.RoleId == userRole?.RoleId).FirstOrDefault();
                    if (role != null)
                    {
                        roleName = role.RoleName;
                    }
                    customer = (await _unitOfWork.CustomerRepository.GetCustomerInfo((int)loggedUser.CustomerId));
                    loginModel = new LoginModel()
                    {
                        UserName = loggedUser.UserName,
                        Email = customer?.Email,
                        MachineName = userAgent,
                        RemoteIpAddress = remoteIpAddress,
                        CustomerID = Convert.ToInt32(loggedUser.CustomerId)
                    };
                    expireDate = DateTime.UtcNow.AddMinutes(JwtKeyExpireIn);
                    token = await GenerateJSONWebToken(loginModel, JwtKeyExpireIn);
                    
                    modules = await LoadModules(loggedUser.LoginId, Convert.ToInt32(userRole?.RoleId));
                    resstate = true;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                token,
                id = loggedUser?.LoginId,
                customerId = customer?.CustomerId,
                userName = loggedUser?.UserName,
                password = "",
                firstName = customer?.FirstName,
                lastName = customer?.LastName,
                fullName = customer?.FullName,
                roleName,
                email = customer?.Email,
                phone = customer?.Phone,
                profilePicName = customer?.FilePath,
                isSuccess = resstate,
                expireDate,
                modules
            };
        }
        private async Task<List<vmModule>> LoadModules(int loginId, int roleId)
        {
            List<vmModule> modules = new List<vmModule>();
            List<vmMenu> menuList = new List<vmMenu>();
            try
            {
                menuList = await GetMenus();
                modules = (from ml in await _unitOfWork.ModuleRepository.GetModuleList()
                           join mn in await _unitOfWork.MenuRepository.GetMenuList() on ml.ModuleId equals mn.ModuleId
                           join mp in await _unitOfWork.MenuPermissionRepository.GetMenuPermissionList() on mn?.MenuId equals mp?.MenuId
                           into tmp
                           from mp in tmp.DefaultIfEmpty()
                           where
                           (mp != null ? (mp?.RoleId == roleId) : true) && Convert.ToInt32(mn.ParentId) == 0
                           select new vmModule
                           {
                               ModuleId = ml.ModuleId,
                               ModuleName = ml.ModuleName,
                               ModuleColor = ml.ModuleColor,
                               ModuleIcon = ml.ModuleIcon,
                               ModulePath = ml.ModulePath,
                               ModuleSequence = ml.ModuleSequence,
                               Description = ml.Description,
                               IsActive = ml.IsActive,
                               menus = menuList.Where(m => m.ModuleId == ml.ModuleId && (Convert.ToInt32(m.RoleId) != 0 ? m.RoleId == roleId : true)).ToList()
                           }


                           ).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return modules;
        }
        public async Task<List<vmMenu>> GetMenus()
        {
            List<vmMenu> ChildMenu = await GetChildMenus();
            var MenuList = await _unitOfWork.MenuRepository.GetMenuList();
            var MenuPermissionList = await _unitOfWork.MenuPermissionRepository.GetMenuPermissionList();

            var tempList = (from mnl in MenuList
                            join mpl in MenuPermissionList on mnl?.MenuId equals mpl?.MenuId
                            into tmp
                            from mpl in tmp.DefaultIfEmpty()
                            where
                            Convert.ToInt32(mnl.ParentId) == 0
                            select new vmMenu
                            {
                                MenuId = mnl.MenuId,
                                MenuName = mnl.MenuName,
                                MenuIcon = mnl.MenuIcon,
                                MenuPath = mnl.MenuPath,
                                MenuSequence = mnl.MenuSequence,
                                ModuleId = mnl.ModuleId,
                                IsSubParent = mnl.IsSubParent,
                                ParentId = mnl.ParentId,
                                SubParentId = mnl.SubParentId,
                                CanCreate = mpl?.CanCreate,
                                CanDelete = mpl?.CanDelete,
                                CanEdit = mpl?.CanEdit,
                                CanView = mpl?.CanView,
                                UserId = mpl?.UserId,
                                RoleId = mpl?.RoleId,
                                IsActive = mnl.IsActive,
                                menuList = ChildMenu.Where(c => c.ParentId == mnl.MenuId).ToList(),
                            }).ToList();
            return tempList;
        }
        public async Task<List<vmMenu>> GetChildMenus()
        {
            return (from mnl in await _unitOfWork.MenuRepository.GetMenuList()
                    join mpl in await _unitOfWork.MenuPermissionRepository.GetMenuPermissionList() on mnl?.MenuId equals mpl?.MenuId
                    into tmp
                    from mpl in tmp.DefaultIfEmpty()
                    where
                    Convert.ToInt32(mnl.ParentId) != 0
                    select new vmMenu
                    {
                        MenuId = mnl.MenuId,
                        MenuName = mnl.MenuName,
                        MenuIcon = mnl.MenuIcon,
                        MenuPath = mnl.MenuPath,
                        MenuSequence = mnl.MenuSequence,
                        ModuleId = mnl.ModuleId,
                        IsSubParent = mnl.IsSubParent,
                        ParentId = mnl.ParentId,
                        SubParentId = mnl.SubParentId,
                        CanCreate = mpl?.CanCreate,
                        CanDelete = mpl?.CanDelete,
                        CanEdit = mpl?.CanEdit,
                        CanView = mpl?.CanView,
                        UserId = mpl?.UserId,
                        RoleId = mpl?.RoleId,
                        IsActive = mnl.IsActive,
                    }).ToList();
        }
        public async Task<string> GenerateNewToken(LoginModel userInfo, string userAgent, string remoteIpAddress)
        {
            string generatedToken = "";
            try
            {
                UserLogin? loggedUser = await _unitOfWork.UserLoginRepository.GetUserInfo(userInfo.UserName, userInfo.Password);

                if (loggedUser != null)
                {
                    LoginModel loginModel = new LoginModel()
                    {
                        UserName = loggedUser.UserName,
                        Password = loggedUser.Password,
                        Email = (await _unitOfWork.CustomerRepository.GetCustomerInfo((int)loggedUser.CustomerId))?.Email,
                        MachineName = userAgent,
                        RemoteIpAddress = remoteIpAddress,
                        CustomerID = Convert.ToInt32(loggedUser.CustomerId),

                    };
                    generatedToken = await GenerateJSONWebToken(userInfo, JwtKeyExpireIn);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return generatedToken;
        }

        private async Task<string> GenerateJSONWebToken(LoginModel userInfo, int ExpireIn)
        {
            await Task.Yield();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticInfos.JwtKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
             {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email, ClaimValueTypes.String),
                new Claim("ipaddress", userInfo.RemoteIpAddress, ClaimValueTypes.String),
                new Claim("machinename", userInfo.MachineName, ClaimValueTypes.String),
                new Claim("userId", Convert.ToString(userInfo.CustomerID), ClaimValueTypes.String),
                new Claim("roleId", Convert.ToString(userInfo.RoleID), ClaimValueTypes.String)
            };

            // Create the JWT and write it to a string
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: JwtIssuer,
                audience: JwtAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(ExpireIn),//This token will expire after 45 minutes
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<object> ChangePassword(LoginModel userInfo)
        {
            string message = ""; bool resstate = false;
            try
            {
                UserLogin? objLogin = await _unitOfWork.UserLoginRepository.GetUserInfo(userInfo.CustomerID);
                if (objLogin != null)
                {
                    objLogin.Password = userInfo.Password;
                    await _unitOfWork.CompleteAsync();
                    resstate = true;
                    message = "Password is changed successfully.";
                }
                else
                {
                    message = "Password is not changed.Please try again.";
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new { message = message, isSuccess = resstate };
        }

        public async Task<object> SendEmailToChangePassword(LoginModel userInfo, string userAgent, string remoteIpAddress)
        {
            string message = ""; bool resstate = false; string token = "";
            try
            {
                var customer = (await _unitOfWork.CustomerRepository.GetCustomerList()).Where(x => x.Email == userInfo.Email).FirstOrDefault();
                var loggedUser = (await _unitOfWork.UserLoginRepository.GetUserLoginList()).Where(x => x.CustomerId == customer?.CustomerId).FirstOrDefault();
                if (loggedUser != null)
                {
                    var loginModel = new LoginModel()
                    {
                        UserName = loggedUser.UserName,
                        Password = loggedUser.Password,
                        Email = customer?.Email,
                        MachineName = userAgent,
                        RemoteIpAddress = remoteIpAddress,
                        CustomerID = Convert.ToInt32(loggedUser.CustomerId),
                        RoleID = (await _unitOfWork.UserRoleRepository.GetUserRoleList()).Where(x => x.LoginId == loggedUser?.LoginId).FirstOrDefault().RoleId
                };
                    token = await GenerateJSONWebToken(loginModel, StaticInfos.JwtKeyExpireIn);
                    resstate = true;
                    string body = "http://localhost:4200/auth/changepassword?key=" + token;
                    resstate = await new EmailManagement().Sendemail(userInfo.Email, "Send Email", "Password Change", body);

                }
                else
                {
                    message = "Wrong email addrress.Please provide correct email address.";
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new { message = message, isSuccess = resstate };
        }

        public async Task<object> DeycryptLoginPasswordKey(string ChangePassKey)
        {
            LoginModel model = new LoginModel(); string message = ""; bool isSuccess = false;
            if (!CommonExt.IsEmptyOrInvalid(ChangePassKey))
            {
                var JwtToken = new JwtSecurityToken(ChangePassKey);
                foreach (var claim in JwtToken.Claims)
                {
                    switch (claim.Type)
                    {
                        case "email":
                            model.Email = claim.Value;
                            break;
                        case "userId":
                            model.CustomerID = Convert.ToInt32(claim.Value);
                            break;
                        default:
                            break;
                    }
                }

                var customer = (await _unitOfWork.CustomerRepository.GetCustomerList()).Where(x => x.Email == model.Email).FirstOrDefault();
                var loggedUser = (await _unitOfWork.UserLoginRepository.GetUserLoginList()).Where(x => x.CustomerId == model.CustomerID).FirstOrDefault();
                if (loggedUser != null)
                {
                    model.LoginID = Convert.ToInt32(loggedUser.LoginId);
                    message = "You are authenticated as valid user.";
                    isSuccess = true;
                }
                else
                {
                    message = "You are authenticated as valid user.Please try again.";
                }

            }
            else
            {
                message = "Your Change Password link is expired.Please try again.";
            }

            return new { user = model, message, isSuccess };
        }
        public async Task<object?> UserRegistration(CustomerModel objCtomer)
        {

            string message = string.Empty; bool resstate = false; Customer objCustomer = new(); int loginId = 0; string roleName = "";
            try
            {
                var objCustomerEx = await _unitOfWork.CustomerRepository.GetCustomerInfo(objCtomer.Email);
                if (objCustomerEx == null)
                {
                    objCustomer = await _unitOfWork.CustomerRepository.CreateCustomer(objCtomer, "", "");
                    await _unitOfWork.CompleteAsync();
                    if (objCustomer != null)
                    {
                        objCtomer.CustomerID = objCustomer.CustomerId;
                        objCtomer.RoleId = (int)RoleType.User;//User role
                        objCtomer.Password = objCtomer.Password;
                        string hassPassword = CommonExt.Encryptdata(objCtomer.Password);
                        var login = await _unitOfWork.UserLoginRepository.CreateUserLogin(objCtomer, hassPassword);
                        await _unitOfWork.CompleteAsync();
                        if (login != null)
                        {
                            loginId = login.LoginId;
                            CreateUserRole uRole = new CreateUserRole()
                            {
                                RoleId=Convert.ToInt32(objCtomer.RoleId),
                                LoginId=login.LoginId,
                            };
                            var userRole = await _unitOfWork.UserRoleRepository.CreateUserRole(uRole);
                            await _unitOfWork.CompleteAsync();
                            roleName = (await _unitOfWork.RoleRepository.GetRoleList()).Where(x => x.RoleId == objCtomer.RoleId).FirstOrDefault()?.RoleName;
                        }
                        message = "Created Successfully.";
                        resstate = true;
                    }
                    else
                    {
                        message = "Failed."; resstate = false;
                    }
                }
                else
                {
                    message = "Email is already exist.Please use unique email.";
                    resstate = false;
                }

            }
            catch (Exception ex)
            {
                message = "Failed."; resstate = false;
            }
            return new
            {
                message,
                isSuccess = resstate,
                customerId = objCustomer?.CustomerId,
                userName = objCustomer?.Email,
                firstName = objCustomer?.FirstName,
                lastName = objCustomer?.LastName,
                fullName = objCustomer?.FullName,
                email = objCustomer?.Email,
                phone = objCustomer?.Phone,
                roleName,
                loginId
            };
        }
    }
}
