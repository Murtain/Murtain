using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Uranus.Domain.UnitOfWork;
using Uranus.Extensions;

namespace Uranus.EntityFramework.Provider
{

    public class EntityFrameworkDbContextProvider<TDbContext> : IEntityFrameworkDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {

        private readonly IUnitOfWorkManager unitOfWorkManager;

        public EntityFrameworkDbContextProvider(IUnitOfWorkManager unitOfWorkManager)
        {
            this.unitOfWorkManager = unitOfWorkManager;
        }


        public TDbContext GetDbContext()
        {
            return unitOfWorkManager.Current.TryAs<EntityFrameworkUnitOfWork>()?.GetOrCreateDbContext<TDbContext>();
        }
    }
}
