using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VSTT.Models;

namespace VSTT.Data
{
    public class VSTTContext : DbContext
    {
        public VSTTContext (DbContextOptions<VSTTContext> options)
            : base(options)
        {
        }

        public DbSet<VSTT.Models.User> User { get; set; } = default!;
    }
}
