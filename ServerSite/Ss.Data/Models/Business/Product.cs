using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models.Business
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public Product()
        {
            Order_details = new HashSet<Order_detail>();
            Categorys = new HashSet<Category>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public int Discount { get; set; }
        public string Image { get; set; }
        public string ImageDesc { get; set; }
        public int Ranking { get; set; }
        public virtual ICollection<Order_detail> Order_details { get; set; }
        public virtual ICollection<Category> Categorys { get; set; }
    }
}
