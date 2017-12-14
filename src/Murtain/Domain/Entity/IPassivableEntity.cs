using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Domain
{
    /// <summary>
    /// This interface is used to make an entity active/passive.
    /// </summary>
    public interface IPassivableEntity
    {
        bool IsActived { get; set; }
    }
}
