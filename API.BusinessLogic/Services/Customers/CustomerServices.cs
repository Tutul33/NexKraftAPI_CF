using API.ViewModel.ViewModels.Customers;
using API.BusinessLogic.Interface.Customer;
using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.RepositoryManagement.UnityOfWork;
//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using API.Settings;
using API.ViewModel.ViewModels.Common;
using System.Security.Policy;
using Google.Protobuf.WellKnownTypes;
using API.Utility;
using API.ViewModel.ViewModels.UserRole;

namespace API.BusinessLogic.Services.Customers
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerServices(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
        public async Task<object?> GetCustomerList(CustomerData cmnParam)
        {
            object list = new object(); int total = 0;
            CommonData common = new CommonData()
            {
                PageNumber = cmnParam.PageNumber,
                PageSize = cmnParam.PageSize,
            };
            try
            {
                total = (from c in await _unitOfWork.CustomerRepository.GetCustomerList()
                         join l in await _unitOfWork.UserLoginRepository.GetUserLoginList() on c.CustomerId equals l.CustomerId
                         join ur in await _unitOfWork.UserRoleRepository.GetUserRoleList() on l.LoginId equals ur.LoginId
                         join r in await _unitOfWork.RoleRepository.GetRoleList() on ur.RoleId equals r.RoleId
                         where
                         (string.IsNullOrEmpty(cmnParam.Search) ? true
                         :
                         (
                          (c.Email == null ? false : c.Email.Contains(cmnParam.Search)) ||
                           (c.FirstName == null ? false : c.FirstName.Contains(cmnParam.Search)) ||
                           (c.LastName == null ? false : c.LastName.Contains(cmnParam.Search)) ||
                           (c.Phone == null ? false : c.Phone.Contains(cmnParam.Search)) ||
                           (r.RoleName == null ? false : r.RoleName.Contains(cmnParam.Search))
                         )) &&
                         (cmnParam.roleId == 0 ? true : r.RoleId == cmnParam.roleId)
                         select c).ToList().Count();
                list = (from c in await _unitOfWork.CustomerRepository.GetCustomerList()
                        join l in await _unitOfWork.UserLoginRepository.GetUserLoginList() on c.CustomerId equals l.CustomerId
                        join ur in await _unitOfWork.UserRoleRepository.GetUserRoleList() on l.LoginId equals ur.LoginId
                        join r in await _unitOfWork.RoleRepository.GetRoleList() on ur.RoleId equals r.RoleId
                        where
                        (string.IsNullOrEmpty(cmnParam.Search) ? true
                        : (
                            (c.Email == null ? false : c.Email.Contains(cmnParam.Search)) ||
                           (c.FirstName == null ? false : c.FirstName.Contains(cmnParam.Search)) ||
                           (c.LastName == null ? false : c.LastName.Contains(cmnParam.Search)) ||
                           (c.Phone == null ? false : c.Phone.Contains(cmnParam.Search)) ||
                            (r.RoleName == null ? false : r.RoleName.Contains(cmnParam.Search))
                         )) &&
                         (cmnParam.roleId == 0 ? true : r.RoleId == cmnParam.roleId)
                        select new
                        {
                            firstName = c.FirstName,
                            lastName = c.LastName,
                            fullName = c.FullName,
                            customerId = c.CustomerId,
                            country = c.Country,
                            email = c.Email,
                            password = l.Password,
                            filePath = c.FilePath,
                            phone = c.Phone,
                            roleId = r.RoleId,
                            roleName = r.RoleName,
                            isDeleting = false,
                            total
                        }
                                ).OrderByDescending(x => x.customerId).Skip(Common.Skip(common)).Take(common.PageSize).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                list,
                total
            };
        }
        public async Task<int> GetCustomerTotal()
        {
            int total = 0;
            total = await _unitOfWork.CustomerRepository.GetCustomerTotal();
            return total;
        }
        public async Task<object?> GetCustomerByCustomerID(int id)
        {
            Customer? customer = new Customer();
            try
            {
                var objCustomer = await _unitOfWork.CustomerRepository.GetCustomerInfo(id);
                if (objCustomer != null)
                {
                    customer.CustomerId = objCustomer.CustomerId;
                    customer.LastName = objCustomer?.LastName;
                    customer.FirstName = objCustomer?.FirstName;
                    customer.Country = objCustomer?.Country;
                    customer.Email = objCustomer?.Email;
                    customer.Phone = objCustomer?.Phone;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return customer;
        }
        public async Task<object?> DeleteCustomer(int id)
        {
            string message = string.Empty; bool resstate = false; int total = 0;
            try
            {
                var objUserLogin = await _unitOfWork.UserLoginRepository.GetUserInfo(id);
                if (objUserLogin != null)
                {
                    await _unitOfWork.UserLoginRepository.DeleteUserLogin(objUserLogin.LoginId);
                }
                resstate = await _unitOfWork.CustomerRepository.DeleteCustomer(id);
                await _unitOfWork.CompleteAsync();
                message = "Deleted Successfully.";
                total = await _unitOfWork.CustomerRepository.GetCustomerTotal();
            }
            catch (Exception ex)
            {
                message = "Failed."; resstate = false;
            }
            return new
            {
                message,
                isSuccess = resstate,
                total
            };
        }
        public async Task<object?> CreateCustomer(CustomerModel objCtomer, List<AttachedFile> files)
        {

            string message = string.Empty; bool resstate = false; Customer objCustomer = new(); string fileName = "", fileExt = ""; int loginId = 0;
            try
            {
                fileName = files.Count > 0 ? files[0].FileName : "";
                fileExt = files.Count > 0 ? files[0].Extension : "";
                objCustomer = await _unitOfWork.CustomerRepository.CreateCustomer(objCtomer, fileName, fileExt);
                await _unitOfWork.CompleteAsync();
                if (objCustomer != null)
                {
                    objCtomer.CustomerID = objCustomer.CustomerId;
                    objCtomer.RoleId = objCtomer.RoleId;
                    objCtomer.Password = objCtomer.Password;
                    string hassPassword = CommonExt.Encryptdata(objCtomer.Password);
                    var login = await _unitOfWork.UserLoginRepository.CreateUserLogin(objCtomer, hassPassword);
                    await _unitOfWork.CompleteAsync();
                    if (login != null)
                    {
                        CreateUserRole uRole = new CreateUserRole() { 
                        RoleId=Convert.ToInt32(objCtomer.RoleId),
                        LoginId=login.LoginId
                        };
                        var ulogin = await _unitOfWork.UserRoleRepository.CreateUserRole(uRole);
                        await _unitOfWork.CompleteAsync();
                        loginId = login.LoginId;
                        foreach (var file in files)
                        {
                            Common.FileSave(file, "/uploadedFile/ProfilePic");
                        }
                    }
                    message = "Created Successfully.";
                    resstate = true;
                }
                else
                {
                    message = "Failed."; resstate = false;
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
                loginId
            };
        }
        public async Task<object?> UpdateCustomer(CustomerModel? objCtomer, List<AttachedFile> files)
        {
            string message = string.Empty; bool resstate = false;
            try
            {
                var objCustomer = await _unitOfWork.CustomerRepository.GetCustomerInfo((int)objCtomer.CustomerID);
                if (objCustomer != null)
                {
                    objCustomer.FirstName = objCtomer.FirstName;
                    objCustomer.LastName = objCtomer.LastName;
                    objCustomer.Email = objCtomer.Email;
                    objCustomer.Phone = objCtomer.Phone;
                    objCustomer.Country = objCtomer.Country;
                    objCustomer.FilePath = files.Count > 0 ? files[0].FileName : null;
                    objCustomer.FileExtension = files.Count > 0 ? files[0].Extension : null;
                    var objUserLogin = await _unitOfWork.UserLoginRepository.GetUserByCustomerID((int)objCtomer.CustomerID);
                    if (objUserLogin != null)
                        objUserLogin.UserName = objCtomer.Email;
                    await _unitOfWork.CompleteAsync();
                    foreach (var file in files)
                    {
                        Common.FileSave(file, "/uploadedFile/ProfilePic");
                    }
                }
                message = "Updated Successfully.";
                resstate = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                message = "Failed.";
                resstate = false;
            }
            return new { message, isSuccess = resstate };
        }
        public async Task<object?> GetRoleList()
        {
            object? list = null;
            try
            {
                list = await _unitOfWork.RoleRepository.GetRoleList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new { list };
        }

        public async Task<byte[]?> GetProfilePicture(string fileName)
        {
            await Task.Yield();
            return Common.getFile("/uploadedFile/ProfilePic", fileName);
        }
        public async Task<string> GetFileMimeType(string fileName)
        {
            await Task.Yield();
            return Common.fileMimeType(fileName);
        }
    }
}
