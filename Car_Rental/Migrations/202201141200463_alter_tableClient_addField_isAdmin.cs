namespace Car_Rental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tableClient_addField_isAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.client", "isAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.client", "isAdmin");
        }
    }
}
