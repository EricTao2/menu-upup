using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using menu_upup.Database;
using MySql.Data.MySqlClient.X.XDevAPI.Common;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace menu_upup.Entity
{
    [Table("Menu")]
    public class Menu
    {
        [Key] public Guid Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }

        public string UserName { get; set; }
        public ICollection<Food> Foods { get; set; }

        protected bool Equals(Menu other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Menu)obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
