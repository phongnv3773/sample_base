using Ss.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models
{
    [Table("RoleAccessPermissions")]
    public class RoleAccessPermission : BaseEntity
    {

        [Required]
        public virtual Role Role { get; set; }
        public virtual AccessPermission AccessPermission { get; set; }


        public ScopeView ScopeView { get; set; }
    }
}
