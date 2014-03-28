namespace SocialStorytelling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recordwhohasvotedonastory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PendingEntryDatas", "Voters", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PendingEntryDatas", "Voters");
        }
    }
}
