using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uranus.EntityFramework;
using Uranus.EntityFramework.Provider;

namespace XUnit.Uranus.EntityFramework.Repositories
{
    public class ServiceBarRepository : Repository<ModelsContainer,Entities.ServiceBar>, IServiceBarRepository
    {
        public ServiceBarRepository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
