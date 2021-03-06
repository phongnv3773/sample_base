﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models.Business
{
    [Table("Categorys")]
    public class Category : BaseEntity
    {
        public Category()
        {
            Sub_categorys = new HashSet<SubCategory>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<SubCategory> Sub_categorys { get; set; }
        public virtual Product Product { get; set; }
    }
}
