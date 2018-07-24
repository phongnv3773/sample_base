using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models.Business
{
    [Table("Sub_categorys")]
    public class Sub_category
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Category Category { get; set; }
    }
}
