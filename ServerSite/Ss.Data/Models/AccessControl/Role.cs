using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        public Role()
        {
            RoleAccessPermissions = new HashSet<RoleAccessPermission>();
            Users = new HashSet<User>();
        }

        [Required]
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }
        public bool IsSysAdmin { get; set; }

        public virtual ICollection<RoleAccessPermission> RoleAccessPermissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
