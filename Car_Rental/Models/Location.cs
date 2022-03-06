using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    [Table("location")]
    public class Location
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Client")]
        public int id_client { get; set; }
        public Client Client { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Voiture")]
        public int Id_voiture { get; set; }
        public Voiture Voiture { get; set; }

        [Required]
        public string type_location { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime date_debut { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime date_fin { get; set; }

        public double prix_location_total { get; set; }
        public string Etat_location { get; set; }
    }
}