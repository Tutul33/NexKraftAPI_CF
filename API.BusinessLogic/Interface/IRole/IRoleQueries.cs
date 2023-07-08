using API.ViewModel.ViewModels.Customers;
using API.ViewModel.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Interface.IRole
{
    public interface IRoleQueries
    {
        Task<object?> GetRoleList(CustomerData search);
    }
}
