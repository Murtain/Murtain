using Murtain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Exceptions
{

    public class UserFriendlyException : Exception
    {
        public Enum ReturnCode { get; set; }

        public UserFriendlyException(Enum returnCode) :
            base(returnCode.TryGetDescription())
        {
            this.ReturnCode = returnCode;
        }
        public UserFriendlyException(Enum code, string message)
            : base(message)
        {
            this.ReturnCode = code;
        }
    }
}
