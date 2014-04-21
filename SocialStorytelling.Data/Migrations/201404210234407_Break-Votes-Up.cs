namespace SocialStorytelling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BreakVotesUp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PendingEntryDatas", "VotesCastForMeFromSite", c => c.Int(nullable: false));
            AddColumn("dbo.PendingEntryDatas", "VotesCastForMeFromTwitter", c => c.Int(nullable: false));
            DropColumn("dbo.PendingEntryDatas", "VotesCastForMe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PendingEntryDatas", "VotesCastForMe", c => c.Int(nullable: false));
            DropColumn("dbo.PendingEntryDatas", "VotesCastForMeFromTwitter");
            DropColumn("dbo.PendingEntryDatas", "VotesCastForMeFromSite");
        }
    }
}
