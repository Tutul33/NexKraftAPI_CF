using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [HttpGet]
        public async Task<object> GetRoleList()
        {
            object result = null;
            try
            {

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
        [HttpPost]
        public async Task<object> AddRole()
        {
            object result = null;
            try
            {

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
        [HttpPut]
        public async Task<object> UpdateRole()
        {
            object result = null;
            try
            {

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
        [HttpDelete]
        public async Task<object> DeleteRole()
        {
            object result = null;
            try
            {

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
