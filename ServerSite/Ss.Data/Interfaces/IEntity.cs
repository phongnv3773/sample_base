using Ss.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        //string Code { get; }
        //DateTime CreatedDate { get; set; }
        //DateTime? ModifiedDate { get; set; }
        Actflg Actflg { get; set; }
    }
}
