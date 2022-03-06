using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Car_Rental.Models
{
    [Table("voiture")]
    public class Voiture
    {
        [Key]
        public int Id_voiture { get; set; }
        [Required]
        public string Image { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date_mise_en_circulation { get; set; }

        [Required]
        public string Type_carburant { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Prix_location { get; set; }

        [ForeignKey("Catégorie")]
        public int Id_categorie { get; set; }
        public Catégorie Catégorie { get; set; }

        [ForeignKey("Modèle")]
        public int Id_modele { get; set; }
        public Modèle Modèle { get; set; }
    }
}