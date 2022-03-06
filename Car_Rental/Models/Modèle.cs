using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    [Table("modèle")]
    public class Modèle
    {
        [Key]
        public int Id_modele { get; set; }

        [Required]
        public string Marque { get; set; }

        [Required]
        public string Serie { get; set; }
    }
}