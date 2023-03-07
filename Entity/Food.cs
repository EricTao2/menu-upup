using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu_upup.Database
{
    [Table("Food")]
    public class Food
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; }
        [Unicode(false)]
        public string FoodType { get; set;}
        public Guid MenuId { get; set; }
    }
}
