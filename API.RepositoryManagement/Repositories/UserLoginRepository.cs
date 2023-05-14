using API.RepositoryManagement.Base;
using API.RepositoryManagement.Repositories.Interfaces;
//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Customers;
using API.Settings;

namespace API.RepositoryManagement.Repositories
{
    public class UserLoginRepository : BaseRepository<UserLogin>,ILoginRepository
    {
        private NexKraftDbContextCF? NexKraftDbContext => _dbContext as NexKraftDbContextCF;
        public UserLoginRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public async Task<UserLogin?> GetUserInfo(string userName,string password)
        {
            return (await GetManyAsync(filter: u => u.UserName == userName && u.HashPassword==password)).FirstOrDefault();
        }
        public async Task<UserLogin?> GetUserByCustomerID(int customerID)
        {
            return (await GetManyAsync(filter: u => u.CustomerId==customerID)).FirstOrDefault();
        }
        public async Task<UserLogin?> GetUserInfo(int loginID)
        {
            return await GetByIdAsync(loginID);
        }
        public async Task<List<UserLogin>> GetUserLoginList(CustomerData param)
        {
            CommonData commonData = new CommonData()
            {
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
            };
            return (await GetManyAsync(
                filter: x => (

                !string.IsNullOrEmpty(param.Search) ? (x.UserName.Contains(param.Search)) : true

                ),
                orderBy: x => x.OrderBy(t => t.CustomerId),
                top: param.PageSize,
                skip: Common.Skip(commonData)
                )).ToList();
        }

        public async Task<List<UserLogin>> GetUserLoginList()
        {
            return (await GetAllAsync()).ToList();
        }        

        public async Task<int> GetTotalUserLogin()
        {
            return (await GetAllAsync()).Count();
        }
        public async Task<bool> DeleteUserLogin(int id)
        {
            return Convert.ToBoolean(DeleteAsync(await GetByIdAsync(id)).IsCompleted);
        }
        public async Task<UserLogin> CreateUserLogin(CustomerModel data,string hassPassword)
        {
            UserLogin obj = new UserLogin()
            {
                UserName = data.Email,
                CustomerId = data.CustomerID,
                Password = data.Password,
                HashPassword= hassPassword,
                //RoleId=data.RoleId
            };
            return await AddAsync(obj);
        }

        public async Task<UserLogin?> GetUserInfo(string userName)
        {
            return (await GetManyAsync(filter: x => x.UserName==userName)).FirstOrDefault();
        }
    }
}
