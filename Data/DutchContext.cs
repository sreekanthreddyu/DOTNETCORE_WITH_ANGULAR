using DutchTreat.Data.Entities;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchContext : DbContext
    {
        public DutchContext(DbContextOptions<DutchContext> options):base(options)
        {
        }

        public DbSet<Product> products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
