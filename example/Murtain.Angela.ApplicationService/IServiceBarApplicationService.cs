using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Murtain.Domain;

namespace Murtain.Angela.ApplicationService
{
    public interface IServiceBarApplicationService : IApplicationService
    {
        Task<IEnumerable<SDK.ServiceBar>> ServiceBarGetCollectionAsync(string name, string ip);
    }
}
