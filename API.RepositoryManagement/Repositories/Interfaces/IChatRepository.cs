using API.DataAccess.ORM.CodeFirst;
using API.ViewModel.ViewModels.Chat;
using API.ViewModel.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> SendMessages(ChatModel data, string? fileName, string? extension);
    }
}
