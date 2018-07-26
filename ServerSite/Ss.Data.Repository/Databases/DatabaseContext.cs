using Ss.Data.Models;
using Ss.Data.Models.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=DatabaseServerSite")
        {

        }

        public virtual DbSet<AccessPermission> AccessPermissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RoleAccessPermission> RoleAccessPermissions { get; set; }
        public virtual DbSet<GroupUser> GroupUsers { get; set; }

        public virtual DbSet<SubCategory> Sub_categorys { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<ShippingAddres> Shipping_addres { get; set; }
        public virtual DbSet<OrderDetail> Order_details { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Role>()
            //   .HasMany(e => e.Users)
            //   .WithMany(e => e.Roles)
            //   .Map(m => m.ToTable("UserRole").MapLeftKey("Id").MapRightKey("Id"));

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
