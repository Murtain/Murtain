using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Murtain.EntityFramework;
using Murtain.EntityFramework.Provider;
using Murtain.Angela.Domain.Repositories;

namespace Murtain.Angela.Infrastructure.Repositories
{
    public class ServiceBarRepository : Repository<ModelsContainer, Domain.Entities.ServiceBar>, IServiceBarRepository
    {
        public ServiceBarRepository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
