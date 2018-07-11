using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models
{
    [Table("AccessPermissions")]
    public class AccessPermission : BaseEntity
    {
        public AccessPermission()
        {
            RoleAccessPermissions = new HashSet<RoleAccessPermission>();
        }

        [Required]
        [StringLength(50)]
        public string AccessPermissionDescription { get; set; }

        public virtual ICollection<RoleAccessPermission> RoleAccessPermissions { get; set; }
    }
}
