namespace Car_Rental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.catégorie",
                c => new
                    {
                        Id_categorie = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_categorie);
            
            CreateTable(
                "dbo.client",
                c => new
                    {
                        Id_client = c.Int(nullable: false, identity: true),
                        Nom_complet = c.String(),
                        Date_naissance = c.DateTime(nullable: false),
                        Telephone = c.String(),
                        Adresse_mail = c.String(nullable: false, maxLength: 100),
                        Mots_de_passe = c.String(nullable: false, maxLength: 100),
                        Image_cin = c.String(nullable: false),
                        Image_permis = c.String(nullable: false),
                        Etat_compte = c.String(),
                    })
                .PrimaryKey(t => t.Id_client)
                .Index(t => t.Adresse_mail, unique: true);
            
            CreateTable(
                "dbo.location",
                c => new
                    {
                        id_client = c.Int(nullable: false),
                        Id_voiture = c.Int(nullable: false),
                        type_location = c.String(nullable: false),
                        date_debut = c.DateTime(nullable: false),
                        date_fin = c.DateTime(nullable: false),
                        prix_location_total = c.Double(nullable: false),
                        Etat_location = c.String(),
                    })
                .PrimaryKey(t => new { t.id_client, t.Id_voiture })
                .ForeignKey("dbo.client", t => t.id_client, cascadeDelete: true)
                .ForeignKey("dbo.voiture", t => t.Id_voiture, cascadeDelete: true)
                .Index(t => t.id_client)
                .Index(t => t.Id_voiture);
            
            CreateTable(
                "dbo.voiture",
                c => new
                    {
                        Id_voiture = c.Int(nullable: false, identity: true),
                        Image = c.String(nullable: false),
                        Date_mise_en_circulation = c.DateTime(nullable: false),
                        Type_carburant = c.String(nullable: false),
                        Prix_location = c.Double(nullable: false),
                        Id_categorie = c.Int(nullable: false),
                        Id_modele = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_voiture)
                .ForeignKey("dbo.catégorie", t => t.Id_categorie, cascadeDelete: true)
                .ForeignKey("dbo.modèle", t => t.Id_modele, cascadeDelete: true)
                .Index(t => t.Id_categorie)
                .Index(t => t.Id_modele);
            
            CreateTable(
                "dbo.modèle",
                c => new
                    {
                        Id_modele = c.Int(nullable: false, identity: true),
                        Marque = c.String(nullable: false),
                        Serie = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_modele);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.location", "Id_voiture", "dbo.voiture");
            DropForeignKey("dbo.voiture", "Id_modele", "dbo.modèle");
            DropForeignKey("dbo.voiture", "Id_categorie", "dbo.catégorie");
            DropForeignKey("dbo.location", "id_client", "dbo.client");
            DropIndex("dbo.voiture", new[] { "Id_modele" });
            DropIndex("dbo.voiture", new[] { "Id_categorie" });
            DropIndex("dbo.location", new[] { "Id_voiture" });
            DropIndex("dbo.location", new[] { "id_client" });
            DropIndex("dbo.client", new[] { "Adresse_mail" });
            DropTable("dbo.modèle");
            DropTable("dbo.voiture");
            DropTable("dbo.location");
            DropTable("dbo.client");
            DropTable("dbo.catégorie");
        }
    }
}
