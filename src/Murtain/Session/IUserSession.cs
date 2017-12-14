using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Session
{
    public interface IUserSession
    {
        Task<string> GetUserIdAsync();
    }
}
