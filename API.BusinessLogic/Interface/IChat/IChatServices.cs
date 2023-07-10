using API.ViewModel.ViewModels.Chat;
using API.ViewModel.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Interface.IChat
{
    public interface IChatServices
    {
        Task<object?> SendMessages(ChatModel objChat, List<AttachedFile> files);
    }
}
