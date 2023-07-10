using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Interface.IChat;
using API.Filters;
using API.signalr_hub;
using API.ViewModel.ViewModels.Chat;
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System.Net.Http.Headers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IHubContext<ChatHub, IMessageHubClient> _hubContext;
        private readonly IChatServices _serivce;
        public ChatController(IChatServices serivce, IHubContext<ChatHub, IMessageHubClient> hubContext)
        {
            this._hubContext = hubContext;
            this._serivce = serivce;
        }

        [HttpPost("[action]"), Authorizations]
        public async Task<object> sendMessage()
        {
            object resdata = null;
            try
            {
                string jsonStr = "[]";
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
                var data = Request.Form["chat"].ToString();
                ChatModel model = JsonConvert.DeserializeObject<ChatModel>(data.ToString());
                resdata = await _serivce.SendMessages(model, listAttachment);
                await _hubContext.Clients.All.BroadcastMessage(jsonStr);
            }
            catch (Exception ex)
            {

            }

            return new
            {
                resdata
            };
        }
    }
}
