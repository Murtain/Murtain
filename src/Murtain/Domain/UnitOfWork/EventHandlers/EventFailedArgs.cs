using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Domain.UnitOfWork.EventHandlers
{
    public class EventFailedArgs : EventArgs
    {
        public Exception Exception { get; set; }

        public EventFailedArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }
}
