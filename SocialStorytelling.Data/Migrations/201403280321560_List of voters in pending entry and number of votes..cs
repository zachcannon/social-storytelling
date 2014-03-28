namespace SocialStorytelling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Listofvotersinpendingentryandnumberofvotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PendingEntryDatas", "VotesCastForMe", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PendingEntryDatas", "VotesCastForMe");
        }
    }
}
