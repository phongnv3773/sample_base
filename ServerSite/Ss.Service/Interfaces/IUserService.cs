using Ss.Data.Interfaces;
using Ss.Data.Models;
using Ss.Data.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Service.Interfaces
{
    public interface IUserService : IService<User, UserView>
    {
        User Login(string username, string password);
    }
}
