namespace SocialStorytelling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixChange : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        username = c.String(nullable: false, maxLength: 128),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.username);
            
        }
    }
}
