using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web3.Models;

namespace web3.Data
{
    public class web3Context : DbContext
    {
        public web3Context (DbContextOptions<web3Context> options)
            : base(options)
        {
        }

        public DbSet<web3.Models.Category> Category { get; set; } = default!;

        public DbSet<Course> Corses { get; set; }
    }
}
