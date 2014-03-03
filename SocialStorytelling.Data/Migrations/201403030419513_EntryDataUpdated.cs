namespace SocialStorytelling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntryDataUpdated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PendingEntryDatas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Author = c.String(),
                        SubmissionDate = c.DateTime(nullable: false),
                        StoryIBelongTo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PendingEntryDatas");
        }
    }
}
