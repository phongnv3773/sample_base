namespace Ss.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessPermissionDescription = c.String(nullable: false, maxLength: 50),
                        Actflg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleAccessPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScopeView = c.Int(nullable: false),
                        Actflg = c.Int(nullable: false),
                        AccessPermission_Id = c.Int(),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccessPermissions", t => t.AccessPermission_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.AccessPermission_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        RoleDescription = c.String(),
                        IsSysAdmin = c.Boolean(nullable: false),
                        Actflg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        FullName = c.String(),
                        Permission = c.Int(nullable: false),
                        Actflg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupUserName = c.String(nullable: false),
                        Actflg = c.Int(nullable: false),
                        GroupUserParent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroupUsers", t => t.GroupUserParent_Id)
                .Index(t => t.GroupUserParent_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Total = c.Double(nullable: false),
                        Discount = c.Int(nullable: false),
                        ShippingCost = c.Double(nullable: false),
                        Tax = c.String(),
                        GrandTotal = c.Double(nullable: false),
                        Actflg = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                        Shipping_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.ShippingAddres", t => t.Shipping_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Shipping_Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Strength = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnutPrice = c.Double(nullable: false),
                        Discount = c.Int(nullable: false),
                        Total = c.Double(nullable: false),
                        Actflg = c.Int(nullable: false),
                        OrderInfo_Id = c.Int(),
                        ProductInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderInfo_Id)
                .ForeignKey("dbo.Products", t => t.ProductInfo_Id)
                .Index(t => t.OrderInfo_Id)
                .Index(t => t.ProductInfo_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UnitPrice = c.Double(nullable: false),
                        Discount = c.Int(nullable: false),
                        Image = c.String(),
                        ImageDesc = c.String(),
                        Ranking = c.Int(nullable: false),
                        Actflg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categorys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Actflg = c.Int(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.SubCategorys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Actflg = c.Int(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorys", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ShippingAddres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Stresst = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Actflg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupUserUser",
                c => new
                    {
                        GroupUser_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupUser_Id, t.User_Id })
                .ForeignKey("dbo.GroupUsers", t => t.GroupUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.GroupUser_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleAccessPermissions", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRole", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRole", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Orders", "Shipping_Id", "dbo.ShippingAddres");
            DropForeignKey("dbo.OrderDetails", "ProductInfo_Id", "dbo.Products");
            DropForeignKey("dbo.SubCategorys", "Category_Id", "dbo.Categorys");
            DropForeignKey("dbo.Categorys", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderInfo_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Users");
            DropForeignKey("dbo.GroupUserUser", "User_Id", "dbo.Users");
            DropForeignKey("dbo.GroupUserUser", "GroupUser_Id", "dbo.GroupUsers");
            DropForeignKey("dbo.GroupUsers", "GroupUserParent_Id", "dbo.GroupUsers");
            DropForeignKey("dbo.RoleAccessPermissions", "AccessPermission_Id", "dbo.AccessPermissions");
            DropIndex("dbo.UserRole", new[] { "Role_Id" });
            DropIndex("dbo.UserRole", new[] { "User_Id" });
            DropIndex("dbo.GroupUserUser", new[] { "User_Id" });
            DropIndex("dbo.GroupUserUser", new[] { "GroupUser_Id" });
            DropIndex("dbo.SubCategorys", new[] { "Category_Id" });
            DropIndex("dbo.Categorys", new[] { "Product_Id" });
            DropIndex("dbo.OrderDetails", new[] { "ProductInfo_Id" });
            DropIndex("dbo.OrderDetails", new[] { "OrderInfo_Id" });
            DropIndex("dbo.Orders", new[] { "Shipping_Id" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.GroupUsers", new[] { "GroupUserParent_Id" });
            DropIndex("dbo.RoleAccessPermissions", new[] { "Role_Id" });
            DropIndex("dbo.RoleAccessPermissions", new[] { "AccessPermission_Id" });
            DropTable("dbo.UserRole");
            DropTable("dbo.GroupUserUser");
            DropTable("dbo.ShippingAddres");
            DropTable("dbo.SubCategorys");
            DropTable("dbo.Categorys");
            DropTable("dbo.Products");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.GroupUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.RoleAccessPermissions");
            DropTable("dbo.AccessPermissions");
        }
    }
}
