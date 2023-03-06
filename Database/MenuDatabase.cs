using System.ComponentModel;
using menu_upup.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationCenter.Config
{
    public class MenuDatabase: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySQL(Config.DatabaseConnectionString);

        public virtual DbSet<User> UserDbSet { get; set; }
        
    }
}