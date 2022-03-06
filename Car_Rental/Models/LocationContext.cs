using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Car_Rental.Models
{
    public class LocationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<Catégorie> Catégories { get; set; }
        public DbSet<Modèle> Modèles { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}