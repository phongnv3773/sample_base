using Ss.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models
{
    [Table("Users")]
    public class User : BaseEntity
    {

        public User()
        {
            Roles = new HashSet<Role>();
            GroupUsers = new HashSet<GroupUser>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Permission Permission { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
    }
}
