using Ss.Data.Models;
using Ss.Data.Models.Business;
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

        IRepository<Category> CategoryRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<OrderDetail> OrderDetailRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<ShippingAddres> ShippingAddresRepository { get; }
        IRepository<SubCategory> SubCategoryRepository { get; }

    }
}
