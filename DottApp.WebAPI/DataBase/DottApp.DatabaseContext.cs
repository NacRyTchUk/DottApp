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

        public ActiveConnection GetSession(string sid) => ActiveConnections.Where(a => a.SessionId == sid).ToArray()[0];

        public DbSet<User> Users { get; set; }
        public DbSet<ActiveConnection> ActiveConnections { get; set; }
        public DbSet<HistoryMessage> HistoryMessages { get; set; }
        public DbSet<UnreachedMessage> UnreachedMessages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set; }
    }
}
