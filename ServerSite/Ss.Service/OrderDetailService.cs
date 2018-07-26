using Ss.Data.Models.Business;
using Ss.Data.ModelViews.Business;
using Ss.Data.Repository.Interfaces;
using Ss.Service.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Service
{
    public class OrderDetailService : BaseService<OrderDetail, OrderDetailView>, IOrderDetailService
    {
        protected readonly IRepositoryContext _context;
        public OrderDetailService(IRepositoryContext context) : base(context.OrderDetailRepository)
        {
            _context = context;
        }
    }
}
