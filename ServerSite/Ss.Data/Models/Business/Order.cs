using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models.Business
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        public Order()
        {
            Order_details = new HashSet<OrderDetail>();
        }

        [Required]
        public virtual User Customer { get; set; }
        public virtual ShippingAddres Shipping { get; set; }

        public double Total { get; set; }
        public int Discount { get; set; }
        public double ShippingCost { get; set; }
        public string Tax { get; set; }
        public double GrandTotal { get; set; }

        public virtual ICollection<OrderDetail> Order_details { get; set; }
    }
}
