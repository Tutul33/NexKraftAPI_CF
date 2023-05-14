//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using API.RepositoryManagement.Base;
using API.RepositoryManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private NexKraftDbContextCF? NexKraftDbContext => _dbContext as NexKraftDbContextCF;
        public RoleRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Role>> GetRoleList()
        {
            List<Role> list=new List<Role>();
            list = (await GetAllAsync()).ToList().Count() > 0 ? (await GetAllAsync()).ToList() : list;
            return list;
        }
    }
}
