using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Murtain.AutoMapper;
using Murtain.Domain;
using Murtain.Angela.Domain.Repositories;

namespace Murtain.Angela.ApplicationService
{
    public class ServiceBarApplicationService : IServiceBarApplicationService
    {
        private readonly IServiceBarRepository serviceBarRepository;
        public ServiceBarApplicationService(IServiceBarRepository serviceBarRepository)
        {
            this.serviceBarRepository = serviceBarRepository;
        }


        public async Task<IEnumerable<SDK.ServiceBar>> ServiceBarGetCollectionAsync(string name, string ip)
        {
            int total = 0;
            var models = await serviceBarRepository.Get(x => true)
                                             .WhereIf(x => x.Name == name, string.IsNullOrEmpty(name))
                                             .WhereIf(x => x.IP == name, string.IsNullOrEmpty(ip))
                                             .OrderByDescending(x => x.Name)
                                             .ThenBy(x => x.IP)
                                             .Paging(1, 10, out total)
                                             .ToListAsync();

            return models.MapTo<IEnumerable<SDK.ServiceBar>>();
        }

    }
}
