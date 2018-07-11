using Ss.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.ModelViews
{
    public class UserView : BaseViewEntity
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public Permission Permission { get; set; }
    }
}
