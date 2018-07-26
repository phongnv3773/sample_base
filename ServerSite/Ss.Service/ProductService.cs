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
    public class ProductService : BaseService<Product, ProductView>, IProductService
    {
        protected readonly IRepositoryContext _context;
        public ProductService(IRepositoryContext context) : base(context.ProductRepository)
        {
            _context = context;
        }
    }
}
