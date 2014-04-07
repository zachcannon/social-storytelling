namespace SocialStorytelling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoryClosed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoryDatas", "StoryClosed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoryDatas", "StoryClosed");
        }
    }
}
