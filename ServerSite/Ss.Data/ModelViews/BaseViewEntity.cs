using Ss.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.ModelViews
{
    public class BaseViewEntity : IViewEntity
    {
        public int Id { get; set; }
    }
}
