using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Murtain.EntityFramework;

using Murtain.Angela.Domain.Entities;

namespace Murtain.Angela.Infrastructure
{
    public class ModelsContainer : EntityFrameworkDbContext
    {
        public ModelsContainer(DbContextOptions<ModelsContainer> options)
            :base(options)
        {
        }

        public virtual DbSet<ServiceBar> ServiceBar { get; set; }
    }
}
