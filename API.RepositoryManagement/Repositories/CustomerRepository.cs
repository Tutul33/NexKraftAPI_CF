using API.RepositoryManagement.Base;
using API.RepositoryManagement.Base.Interfaces;
using API.RepositoryManagement.Repositories.Interfaces;
//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using API.ViewModel.ViewModels.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysqlx.Crud;
using Azure;
using System.Drawing.Printing;
using API.Settings;
using API.ViewModel.ViewModels.Common;

namespace API.RepositoryManagement.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private NexKraftDbContextCF? NexKraftDbContextCF => _dbContext as NexKraftDbContextCF;
        public CustomerRepository(NexKraftDbContextCF dbContext) : base(dbContext)
        {
        }
        public async Task<Customer?> GetCustomerInfo(int customerId)
        {
            //return (await GetManyAsync(filter: u => u.CustomerId == customerId)).FirstOrDefault();
            return await GetByIdAsync(customerId);
        }
        public async Task<Customer?> GetCustomerInfo(string email)
        {
            return (await GetManyAsync(filter: u => u.Email == email)).FirstOrDefault();
        }
        public async Task<List<Customer>> GetCustomerList(CustomerData param)
        {
            CommonData commonData=new CommonData(){ 
             PageNumber= param.PageNumber,
             PageSize= param.PageSize,
            };
            return (await GetManyAsync(
                filter: x => (                
                !string.IsNullOrEmpty(param.Search)? (x.FirstName.Contains(param.Search) || x.LastName.Contains(param.Search) || x.Email.Contains(param.Search) || x.Phone.Contains(param.Search)) :true
                ),
                orderBy: x => x.OrderBy(t => t.CustomerId),
                top: param.PageSize,
                skip: Common.Skip(commonData)
                )).ToList();
        }
        public async Task<List<Customer>> GetCustomerList()
        {
            return (await GetAllAsync()).ToList();
        }
        public async Task<int> GetCustomerTotal()
        {
            return (await GetAllAsync()).Count();
        }

        public async Task<bool> DeleteCustomer(int id)
        {           
            return Convert.ToBoolean(DeleteAsync(await GetByIdAsync(id)).IsCompleted);
        }

        public async Task<Customer> CreateCustomer(CreateCustomerModel data,string fileName,string fileExtension)
        {
            Customer obj = new Customer() { 
                FullName= data.FirstName+" "+data.LastName,
                FirstName= data.FirstName,
                LastName =data.LastName,
                Country= data.Country,
                Email= data.Email,
                Phone= data.Phone,
                FilePath=fileName,
                FileExtension=fileExtension
            };
            return await AddAsync(obj);
        }

    }
}
