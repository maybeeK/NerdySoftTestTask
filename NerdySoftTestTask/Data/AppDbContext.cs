using Microsoft.EntityFrameworkCore;
using NerdySoftTestTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdySoftTestTask.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
        
        }

        public DbSet<Announcement> Announcements { get; set; }
    }
}
