using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Murtain.Session;

namespace Murtain.EntityFramework
{
    public abstract class EntityFrameworkDbContext : DbContext
    {
        public IUserSession UserSession { get; set; }

        protected EntityFrameworkDbContext()
        {
            UserSession = NullUserSession.Instance;
        }
        protected EntityFrameworkDbContext(DbContextOptions contextOptions)
            :base(contextOptions)
        {
            UserSession = NullUserSession.Instance;
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
