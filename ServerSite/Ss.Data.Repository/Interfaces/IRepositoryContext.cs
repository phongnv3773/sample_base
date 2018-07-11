using Ss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Repository.Interfaces
{
    public interface IRepositoryContext
    {
        IRepository<User> UserRepository { get; }
        IRepository<AccessPermission> AccessPermissionRepository { get; }
        IRepository<RoleAccessPermission> RoleAccessPermissionRepository { get; }
    }
}
