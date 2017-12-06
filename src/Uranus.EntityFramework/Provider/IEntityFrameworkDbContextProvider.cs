using System;
using System.Collections.Generic;
using System.Text;

namespace Uranus.EntityFramework.Provider
{
    /// <summary>
    /// IDbContextProvider
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IEntityFrameworkDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
