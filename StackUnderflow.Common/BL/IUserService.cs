using StackUnderflow.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackUnderflow.Common.BL
{
    public interface IUserService
    {
        IQueryable<User> GetAllUsers(int page);
    }
}
