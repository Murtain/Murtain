using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uranus.EntityFramework;
using XUnit.Uranus.EntityFramework.Entities;

namespace XUnit.Uranus.EntityFramework
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
