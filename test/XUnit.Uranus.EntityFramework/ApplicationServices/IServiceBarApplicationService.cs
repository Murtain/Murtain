using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uranus.Domain;

namespace XUnit.Uranus.EntityFramework.ApplicationServices
{
    public interface IServiceBarApplicationService : IApplicationService
    {
        Task<IEnumerable<SDK.ServiceBar>> ServiceBarGetCollectionAsync(string name, string ip);
    }
}
