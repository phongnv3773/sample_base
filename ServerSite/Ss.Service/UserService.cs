using Ss.Data.Models;
using Ss.Data.ModelViews;
using Ss.Data.Repository.Interfaces;
using Ss.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Service
{
    public class UserService : BaseService<User, UserView>, IUserService
    {
        protected readonly IRepositoryContext _context;
        public UserService(IRepositoryContext context) : base(context.UserRepository)
        {
            _context = context;
        }


        public override UserView ConvertToView(User model)
        {
            var view = new UserView();
            view.Id = model.Id;
            view.UserName = model.UserName;
            view.FullName = model.FullName;
            view.Permission = model.Permission;
            return view;
        }


        public User Login(string username, string password)
        {
            return BaseRepository.GetSingle(x => x.UserName == username && x.Password == password);
        }
    }
}
