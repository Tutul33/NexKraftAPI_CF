using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Interface.IRole;
using API.DataAccess.ORM.CodeFirst;
using API.Filters;
using API.ViewModel.ViewModels.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IRoleCommands cmndSerivce;
        private readonly IRoleQueries qrySerivce;
        public RoleController(IRoleCommands cmndSerivce,IRoleQueries qrySerivce, IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            this.cmndSerivce = cmndSerivce;
            this.qrySerivce = qrySerivce;
        }

        [HttpGet("[action]"), Authorizations]
        public async Task<object> GetRoleList([FromQuery] vmRole role)
        {
            object result = null;
            try
            {
                result=await qrySerivce.GetRoleList(role);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                result
            };
        }
        [HttpPost("[action]"), Authorizations]
        public async Task<object> AddRole(vmRole role)
        {
            object result = null;
            try
            {
                result = await cmndSerivce.AddRole(role);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                result
            };
        }
        [HttpPut("[action]"), Authorizations]
        public async Task<object> UpdateRole(vmRole role)
        {
            object result = null;
            try
            {
                result = await cmndSerivce.UpdateRole(role);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                result
            };
        }
        [HttpDelete("DeleteRole/{id:int}"), Authorizations]
        public async Task<object> DeleteRole(int id)
        {
            object result = null;
            try
            {
                result = await cmndSerivce.DeleteRole(id);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                result
            };
        }
    }
}
