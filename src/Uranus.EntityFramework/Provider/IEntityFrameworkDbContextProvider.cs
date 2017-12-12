using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uranus.EntityFramework.Provider
{
    public interface IEntityFrameworkDbContextProvider<out TDbContext> : IEntityFrameworkDbContextProvider
        where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }

    public interface IEntityFrameworkDbContextProvider
    {

    }
}
