using API.BusinessLogic.Interface.IChat;
using API.DataAccess.ORM.CodeFirst;
using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.Settings;
using API.Utility;
using API.ViewModel.ViewModels.Chat;
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Customers;
using API.ViewModel.ViewModels.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Services.Chats
{
    public class ChatsServices : IChatServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChatsServices(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
        public async Task<object?> SendMessages(ChatModel objChat, List<AttachedFile> files)
        {

            string message = string.Empty; bool resstate = false;
            Chat objChatNew = new(); string fileName = "", fileExt = "";
            int loginId = 0, roleId = 0; string roleName = "";
            try
            {
                fileName = files.Count > 0 ? files[0].FileName : "";
                fileExt = files.Count > 0 ? files[0].Extension : "";
                objChatNew = await _unitOfWork.ChatRepository.SendMessages(objChat, fileName, fileExt);
                await _unitOfWork.CompleteAsync();
                if (objChatNew != null)
                {
                    foreach (var file in files)
                    {
                        Common.FileSave(file, "/uploadedFile/ChatFiles");
                    }

                    message = "Send Successfully.";
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
                message
            };
        }

    }
}
