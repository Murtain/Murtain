using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Murtain.EntityFramework.Configuration
{
    public class EntityFrameworkConfiguration : IEntityFrameworkConfiguration
    {

        public EntityFrameworkConfiguration()
        {
        }

        public DbContextOptions DbContextOptions { get; set; }

    }
}
