
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Interface.Customer
{
    public interface ICustomerServices 
    {
        Task<object?> DeleteCustomer(int id);
        Task<object?> GetCustomerByCustomerID(int id);
        Task<object?> GetCustomerList(CustomerData param);
        Task<object?> CreateCustomer(CustomerModel data,List<AttachedFile> files);
        Task<object?> UpdateCustomer(CustomerModel data,List<AttachedFile> files);
        Task<int> GetCustomerTotal();
        Task<object?> GetRoleList();
        Task<byte[]?> GetProfilePicture(string fileName);
        Task<string> GetFileMimeType(string fileName);
    }
}
