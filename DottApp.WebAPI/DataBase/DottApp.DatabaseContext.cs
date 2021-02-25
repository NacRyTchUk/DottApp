using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DottApp.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DottApp.WebAPI
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
