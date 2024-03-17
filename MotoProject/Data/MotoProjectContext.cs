using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MotoProject.Models;

namespace MotoProject.Data
{
    public class MotoProjectContext : DbContext
    {
        public MotoProjectContext (DbContextOptions<MotoProjectContext> options)
            : base(options)
        {
        }

        public DbSet<MotoProject.Models.Motors> Motors { get; set; } = default!;

        public DbSet<MotoProject.Models.Clients> Clients { get; set; } = default!;
    }
}
