using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace menu_upup.Entity
{
    [Table("User")]
    public class User
    {
        [Key]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        protected bool Equals(User other)
        {
            return Name == other.Name && Email == other.Email && Password == other.Password;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Email, Password);
        }
    }
}
