using API.BusinessLogic.Interface.Customer;
using API.Filters;
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Customers;
using Google.Protobuf;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;
using System.Net.Http.Headers;

namespace API.Controllers
{
    [Route("api/[controller]"), Produces("application/json"), EnableCors()]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly ICustomerServices serivce;
        public CustomerController(ICustomerServices service,IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            this.serivce= service;
        }

        [HttpGet("[action]"), Authorizations]
        public async Task<object?> GetCustomerList([FromQuery] CustomerData param)
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

                    data = await serivce.GetCustomerList(param);

                    memoryCache.Set("CacheTime", data, cacheEntryOptions);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }
        [HttpGet("[action]"), Authorizations]
        public async Task<object?> GetCustomerTotal()
        {
            object? data = null;
            try
            {
                data = await serivce.GetCustomerTotal();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }
        [HttpGet("[action]"), Authorizations]
        public async Task<object?> GetRoleList()
        {
            object? data = null;
            try
            {
                data = await serivce.GetRoleList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }
        [HttpGet("GetCustomerByCustomerID/{id:int}"), Authorizations]
        public async Task<object?> GetCustomerByCustomerID(int id)
        {
            object? data = null;
            try
            {
                data = await serivce.GetCustomerByCustomerID(id);
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
        public async Task<object?> CreateCustomer()
        {
            object? resdata = null;
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var ReqFiles = formCollection.Files;
                var listAttachment = new List<AttachedFile>();
                foreach (var attachedFile in ReqFiles)
                {
                    if (attachedFile != null)
                    {
                        if (attachedFile.Length > 0)
                        {
                            string fileName = ContentDispositionHeaderValue.Parse(attachedFile.ContentDisposition).FileName.Trim('"');
                            var newFileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                            var arrayExtens = fileName.Split(".");
                            var exten = arrayExtens[arrayExtens.Length - 1];
                            fileName = fileName.Substring(0, fileName.Length - (exten.Length + 1)) + "_" + newFileName + "." + exten;
                            string base64String = "";
                            using (var ms = new MemoryStream())
                            {
                                attachedFile.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                base64String = Convert.ToBase64String(fileBytes);
                                // act on the Base64 data
                            }
                            var objAttachment = new AttachedFile();
                            objAttachment.FileName = fileName;
                            objAttachment.FileData = base64String;
                            objAttachment.Extension = exten;
                            listAttachment.Add(objAttachment);
                        }
                    }
                }
                var data = Request.Form["postModel"].ToString();
                CustomerModel model = JsonConvert.DeserializeObject<CustomerModel>(data.ToString());               
                resdata = await serivce.CreateCustomer(model,listAttachment);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;

        }

        [HttpPut("[action]"), Authorizations]
        public async Task<object?> UpdateCustomer()
        {
            object? resdata = null;
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var ReqFiles = formCollection.Files;
                var listAttachment = new List<AttachedFile>();
                foreach (var attachedFile in ReqFiles)
                {
                    if (attachedFile != null)
                    {
                        if (attachedFile.Length > 0)
                        {
                            string fileName = ContentDispositionHeaderValue.Parse(attachedFile.ContentDisposition).FileName.Trim('"');
                            var newFileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                            var arrayExtens = fileName.Split(".");
                            var exten = arrayExtens[arrayExtens.Length - 1];
                            fileName = fileName.Substring(0, fileName.Length - (exten.Length + 1)) + "_" + newFileName + "." + exten;
                            string base64String = "";
                            using (var ms = new MemoryStream())
                            {
                                attachedFile.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                base64String = Convert.ToBase64String(fileBytes);
                                // act on the Base64 data
                            }
                            var objAttachment = new AttachedFile();
                            objAttachment.FileName = fileName;
                            objAttachment.FileData = base64String;
                            objAttachment.Extension = exten;
                            listAttachment.Add(objAttachment);
                        }
                    }
                }
                var data = Request.Form["postModel"].ToString();
                CustomerModel model = JsonConvert.DeserializeObject<CustomerModel>(data.ToString());
                resdata = await serivce.UpdateCustomer(model,listAttachment);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }

        [HttpDelete("DeleteCustomer/{id:int}"), Authorizations]
        public async Task<object?> DeleteCustomer(int id)
        {
            object? resdata = null;
            try
            {
                resdata = await serivce.DeleteCustomer(id);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return resdata;
        }
        [HttpGet("ProfilePic/{fileName}"),Authorizations]
        public async Task<object> file(string fileName)
        {
            byte[]? file = null; string returnType = "";
            try
            {               
                if (!string.IsNullOrEmpty(fileName))
                {
                    returnType = await serivce.GetFileMimeType(fileName);
                    if (!string.IsNullOrEmpty(returnType))
                    {
                        file = await serivce.GetProfilePicture(fileName);
                    }
                    
                }
            }
            catch (Exception ex)
            {
               ex.ToString();
            }
            return File(file, returnType);
        }
    }
}
