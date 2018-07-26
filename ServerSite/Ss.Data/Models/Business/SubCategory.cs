using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models.Business
{
    [Table("SubCategorys")]
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Category Category { get; set; }
    }
}
