
using API.RepositoryManagement.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.RepositoryManagement.Repositories;
using API.DataAccess.ORM.CodeFirst;

namespace API.RepositoryManagement.UnityOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties-[Register repository here]

        private CustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository => _customerRepository ?? (_customerRepository = new CustomerRepository(_dbContext));

        private UserLoginRepository _loginRepository;
        public ILoginRepository UserLoginRepository => _loginRepository ?? (_loginRepository = new UserLoginRepository(_dbContext));
        private MenuPermissionRepository _menuPermissionRepository;
        public IMenuPermissionRepository MenuPermissionRepository => _menuPermissionRepository ?? (_menuPermissionRepository = new MenuPermissionRepository(_dbContext));
        private ModuleRepository _moduleRepository;
        public IModuleRepository ModuleRepository => _moduleRepository??(_moduleRepository=new ModuleRepository(_dbContext));
        private MenuRepository _menuRepository;
        public IMenuRepository MenuRepository => _menuRepository??(_menuRepository=new MenuRepository(_dbContext));
        private RoleRepository _roleRepository;
        public IRoleRepository RoleRepository => _roleRepository ?? (_roleRepository = new RoleRepository(_dbContext));
        private UserRoleRepository _userRoleRepository;
        public IUserRoleRepository UserRoleRepository => _userRoleRepository ?? (_userRoleRepository = new UserRoleRepository(_dbContext));
        private ChatRepository _chatRepository;
        public IChatRepository ChatRepository => _chatRepository ?? (_chatRepository = new ChatRepository(_dbContext));
        #endregion

        #region Readonlys

        private readonly NexKraftDbContextCF _dbContext;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">The Database Context</param>
        public UnitOfWork(NexKraftDbContextCF dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region Methods

        /// <summary>
        /// Completes the unit of work, saving all repository changes to the underlying data-store.
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task CompleteAsync() => await _dbContext.SaveChangesAsync();

        #endregion

        #region Implements IDisposable

        #region Private Dispose Fields

        private bool _disposed;

        #endregion

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        /// <returns><see cref="ValueTask"/></returns>
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);

            // Take this object off the finalization queue to prevent 
            // finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        /// <param name="disposing">Whether or not we are disposing</param>
        /// <returns><see cref="ValueTask"/></returns>
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    await _dbContext.DisposeAsync();
                }

                // Dispose any unmanaged resources here...

                _disposed = true;
            }
        }

        #endregion
    }
}
