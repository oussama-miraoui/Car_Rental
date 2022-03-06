using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    [Table("client")]
    public class Client
    {
        [Key]
        public int Id_client { get; set; }

        public string Nom_complet { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire.")]
        [DataType(DataType.Date)]
        public DateTime Date_naissance { get; set; }

        public string Telephone { get; set; }

        [Required(ErrorMessage ="L'adresse email est obligatoire.")]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        [EmailAddress]
        public string Adresse_mail { get; set; }

        [Required(ErrorMessage = "Le mots de passe est obligatoire.")]
        [MaxLength(100)]
        public string Mots_de_passe { get; set; }

        [Required(ErrorMessage = "La CIN est obligatoire.")]
        public string Image_cin { get; set; }

        [Required(ErrorMessage = "Le permis est obligatoire.")]
        public string Image_permis { get; set; }

        public string Etat_compte { get; set; }

        public bool isAdmin { get; set; }
    }
}