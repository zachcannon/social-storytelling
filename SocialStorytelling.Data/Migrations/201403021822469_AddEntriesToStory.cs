namespace SocialStorytelling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntriesToStory : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.EntryDatas", name: "StoryData_id", newName: "Story_id");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.EntryDatas", name: "Story_id", newName: "StoryData_id");
        }
    }
}
