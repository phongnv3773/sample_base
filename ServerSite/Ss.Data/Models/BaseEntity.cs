using Ss.Data.Enums;
using Ss.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models
{
    public class BaseEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        public Actflg Actflg { get; set; }
    }
}
