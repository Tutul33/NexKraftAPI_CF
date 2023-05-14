using API.BusinessLogic.Interface.IModuleAndMenu;
using API.Filters;
using API.ViewModel.ViewModels.Customers;
using API.ViewModel.ViewModels.Menu;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [Route("api/[controller]"), Produces("application/json"), EnableCors()]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IModuleAndMenuServiceCommands _svcCommand;
        private readonly IModuleAndMenuServiceQueries _svcQueries;
        public ModuleController(
            IMemoryCache memoryCache, 
            IModuleAndMenuServiceCommands svcCommand,
            IModuleAndMenuServiceQueries svcQueries)
        {
            this.memoryCache = memoryCache;
            _svcCommand = svcCommand;
            _svcQueries = svcQueries;
        }

        #region Module
        [HttpGet("[action]"), Authorizations]
        public async Task<object?> GetModuleList([FromQuery] vmModuleSearch param)
        {
            object? data = null;
            try
            {
                //DateTime currentTime;
                bool isExist = memoryCache.TryGetValue("CacheTime", out data);
                if (!isExist)
                {
                    //currentTime = DateTime.Now;
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMilliseconds(30));

                    data = await _svcQueries.GetModuleList(param);

                    memoryCache.Set("CacheTime", data, cacheEntryOptions);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }

        [HttpGet("GetModuleByModuleId/{id:int}"), Authorizations]
        public async Task<object?> GetModuleByModuleId(int id)
        {
            object? data = null;
            try
            {
                data = await _svcQueries.GetModuleByModuleID(id);
                if (data == null)
                {
                    data = new { message = "No data found" };
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }

        [HttpPost("[action]"), Authorizations]
        public async Task<object?> CreateModule([FromBody] vmModule data)
        {
            object? resdata = null;
            try
            {
                resdata = await _svcCommand.CreateModule(data);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;

        }

        [HttpPut("[action]"), Authorizations]
        public async Task<object?> UpdateModule([FromBody] vmModule data)
        {
            object? resdata = null;
            try
            {
                resdata = await _svcCommand.UpdateModule(data);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }

        [HttpDelete("DeleteModule/{id:int}"), Authorizations]
        public async Task<object?> DeleteModule(int id)
        {
            object? resdata = null;
            try
            {
                resdata = await _svcCommand.DeleteModule(id);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }
        #endregion

        #region Menu

        [HttpGet("[action]"), Authorizations]
        public async Task<object?> GetMenuList([FromQuery] vmMenus param)
        {
            object? data = null;
            try
            {
                //DateTime currentTime;
                bool isExist = memoryCache.TryGetValue("CacheTime", out data);
                if (!isExist)
                {
                    //currentTime = DateTime.Now;
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMilliseconds(30));

                    data = await _svcQueries.GetMenuList(param);

                    memoryCache.Set("CacheTime", data, cacheEntryOptions);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }

        [HttpGet("GetMenuByMenuId/{id:int}"), Authorizations]
        public async Task<object?> GetMenuByMenuId(int id)
        {
            object? data = null;
            try
            {
                data = await _svcQueries.GetModuleByModuleID(id);
                if (data == null)
                {
                    data = new { message = "No data found" };
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }

        [HttpPost("[action]"), Authorizations]
        public async Task<object?> CreateMenu([FromBody] vmMenu data)
        {
            object? resdata = null;
            try
            {
                resdata = await _svcCommand.CreateMenu(data);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;

        }

        [HttpPut("[action]"), Authorizations]
        public async Task<object?> UpdateMenu([FromBody] vmMenu data)
        {
            object? resdata = null;
            try
            {
                resdata = await _svcCommand.UpdateMenu(data);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }

        [HttpDelete("DeleteMenu/{id:int}"), Authorizations]
        public async Task<object?> DeleteMenu(int id)
        {
            object? resdata = null;
            try
            {
                resdata = await _svcCommand.DeleteMenu(id);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }
        #endregion
        #region Menu Permission List
        [HttpGet("[action]"), Authorizations]
        public async Task<object?> GetMenuPermissionList([FromQuery] vmMenuSearch param)
        {
            object? data = null;
            try
            {
                //DateTime currentTime;
                bool isExist = memoryCache.TryGetValue("CacheTime", out data);
                if (!isExist)
                {
                    //currentTime = DateTime.Now;
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMilliseconds(30));

                    data = await _svcQueries.GetMenuPermissionList(param);

                    memoryCache.Set("CacheTime", data, cacheEntryOptions);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }
        [HttpPut("[action]"), Authorizations]
        public async Task<object?> UpdateMenuPermission([FromBody] vmMenuPermission data)
        {
            object? resdata = null;
            try
            {
                resdata = await _svcCommand.UpdateMenuMermission(data);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }

        [HttpDelete("DeleteMenuPermission/{id:int}"), Authorizations]
        public async Task<object?> DeleteMenuPermission(int id)
        {
            object? resdata = null;
            try
            {
                resdata = await _svcCommand.DeleteMenuPermission(id);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }
        #endregion

    }
}
