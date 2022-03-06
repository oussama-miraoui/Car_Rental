using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Car_Rental.Models
{
    [Table("catégorie")]
    public class Catégorie
    {
        [Key]
        public int Id_categorie { get; set; }

        [Required]
        public string Nom { get; set; }
    }
}