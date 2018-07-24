using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models.Business
{
    [Table("Order_details")]
    public class Order_detail : BaseEntity
    {
        public virtual Order OrderInfo { get; set; }
        public virtual Product ProductInfo { get; set; }

        public string Strength { get; set; }
        public int Quantity { get; set; }
        public double UnutPrice { get; set; }
        public int Discount { get; set; }
        public double Total { get; set; }

    }
}
