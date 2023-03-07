using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient.X.XDevAPI.Common;
using MySql.EntityFrameworkCore.DataAnnotations;
using Newtonsoft.Json;

namespace menu_upup.Entity
{
    [Table("User")]
    public class User
    {
        [Key]
        public string Name { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Email { get; set; }
        [JsonIgnore]
        [Column(TypeName = "varchar(200)")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Token { get; set; }

        public ICollection<Menu> Menus { get; set; }
        public string GenerateToken()
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{Name}:{Password}:{DateTime.UtcNow}");
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            Token = Convert.ToHexString(hashBytes);
            return Token;
        }
        

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
