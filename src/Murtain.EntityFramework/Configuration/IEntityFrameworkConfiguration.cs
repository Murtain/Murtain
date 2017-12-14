using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.EntityFramework.Configuration
{
    public interface IEntityFrameworkConfiguration
    {
        DbContextOptions DbContextOptions { get; set; }
    }
}
