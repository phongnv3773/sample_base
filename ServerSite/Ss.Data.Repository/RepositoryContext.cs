using Ss.Data.Models;
using Ss.Data.Models.Business;
using Ss.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Repository
{
    public class RepositoryContext : IRepositoryContext
    {
        public readonly DatabaseContext context;

        public RepositoryContext(DatabaseContext context)
        {
            this.context = context;
        }

        private static IRepository<User> _userRepository;
        public IRepository<User> UserRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new BaseRepository<User>(context));
            }
        }

        private static IRepository<AccessPermission> _accessPermissionRepository;
        public IRepository<AccessPermission> AccessPermissionRepository
        {
            get
            {
                return _accessPermissionRepository ?? (_accessPermissionRepository = new BaseRepository<AccessPermission>(context));
            }
        }

        private static IRepository<RoleAccessPermission> _roleAccessPermissionRepository;
        public IRepository<RoleAccessPermission> RoleAccessPermissionRepository
        {
            get
            {
                return _roleAccessPermissionRepository ?? (_roleAccessPermissionRepository = new BaseRepository<RoleAccessPermission>(context));
            }
        }

        private static IRepository<Category> _categoryRepository;
        public IRepository<Category> CategoryRepository
        {
            get
            {
                return _categoryRepository ?? (_categoryRepository = new BaseRepository<Category>(context));
            }
        }

        private static IRepository<Order> _orderRepository;
        public IRepository<Order> OrderRepository
        {
            get
            {
                return _orderRepository ?? (_orderRepository = new BaseRepository<Order>(context));
            }
        }

        private static IRepository<OrderDetail> _orderDetailRepository;
        public IRepository<OrderDetail> OrderDetailRepository
        {
            get
            {
                return _orderDetailRepository ?? (_orderDetailRepository = new BaseRepository<OrderDetail>(context));
            }
        }

        private static IRepository<Product> _productRepository;
        public IRepository<Product> ProductRepository
        {
            get
            {
                return _productRepository ?? (_productRepository = new BaseRepository<Product>(context));
            }
        }

        private static IRepository<ShippingAddres> _shippingAddresRepository;
        public IRepository<ShippingAddres> ShippingAddresRepository
        {
            get
            {
                return _shippingAddresRepository ?? (_shippingAddresRepository = new BaseRepository<ShippingAddres>(context));
            }
        }

        private static IRepository<SubCategory> _subCategoryRepository;
        public IRepository<SubCategory> SubCategoryRepository
        {
            get
            {
                return _subCategoryRepository ?? (_subCategoryRepository = new BaseRepository<SubCategory>(context));
            }
        }
    }
}
