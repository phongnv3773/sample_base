using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models.Business
{
    [Table("Shipping_addres")]
    public class Shipping_addres
    {
        public Shipping_addres()
        {
            Orders = new HashSet<Order>();
        }
        public string Name { get; set; }
        public string Stresst { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
