using System.ComponentModel;
using menu_upup.Database;
using menu_upup.Entity;
using menu_upup.Entity.Enum;
using Microsoft.EntityFrameworkCore;

namespace menu_upup.Config
{
    public class MenuDatabase: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL(Config.DatabaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("utf8_general_ci");
        }

        public virtual DbSet<User> UserDbSet { get; set; }
        public virtual DbSet<Menu> MenuDbSet { get; set; }
        public virtual DbSet<Food> FoodDbSet { get; set; }

    }
}