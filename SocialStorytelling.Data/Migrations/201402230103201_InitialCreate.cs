namespace SocialStorytelling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoryDatas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Prompt = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.EntryDatas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Author = c.String(),
                        SubmissionDate = c.DateTime(nullable: false),
                        StoryData_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.StoryDatas", t => t.StoryData_id)
                .Index(t => t.StoryData_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EntryDatas", "StoryData_id", "dbo.StoryDatas");
            DropIndex("dbo.EntryDatas", new[] { "StoryData_id" });
            DropTable("dbo.EntryDatas");
            DropTable("dbo.StoryDatas");
        }
    }
}
