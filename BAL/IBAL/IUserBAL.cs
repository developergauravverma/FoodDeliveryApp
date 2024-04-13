using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IBAL
{
    public interface IUserBAL
    {
        Task<User> UserRegister(User user);
    }
}
