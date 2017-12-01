using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Uranus.Session
{
    public interface IUserSession
    {
        Task<string> GetUserIdAsync();
    }
}
