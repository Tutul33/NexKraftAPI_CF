using API.DataAccess.ORM.CodeFirst;
using API.RepositoryManagement.Base;
using API.RepositoryManagement.Repositories.Interfaces;
using API.ViewModel.ViewModels.Chat;
using API.ViewModel.ViewModels.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        private NexKraftDbContextCF? NexKraftDbContextCF => _dbContext as NexKraftDbContextCF;
        public ChatRepository(NexKraftDbContextCF dbContext) : base(dbContext)
        {
        }

        public async Task<Chat> SendMessages(ChatModel data, string? fileName, string? extension)
        {
            Chat obj = new Chat()
            {
                groupId=data.groupId,
                messages= data.messages,
                mediaUrl=data.mediaUrl,
                mediaExt=data.mediaExt,
                fromUserId=data.fromUserId,
                toUserId=data.toUserId,
                CreateDate=DateTime.Now,
                isActive=true
            };
            return await AddAsync(obj);
        }
    }
}
