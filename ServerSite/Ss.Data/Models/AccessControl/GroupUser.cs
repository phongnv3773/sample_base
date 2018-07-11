using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models
{
    [Table("GroupUsers")]
    public class GroupUser : BaseEntity
    {
        public GroupUser()
        {
            Users = new HashSet<User>();
        }

        [Required]
        public string GroupUserName { get; set; }

        public virtual GroupUser GroupUserParent { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
