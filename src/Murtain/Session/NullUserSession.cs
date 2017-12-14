using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Session
{
    public class NullUserSession : IUserSession
    {
        public static NullUserSession Instance { get { return SingletonInstance; } }
        private static readonly NullUserSession SingletonInstance = new NullUserSession();

        public Task<string> GetUserIdAsync()
        {
            return null;
        }
    }
}
