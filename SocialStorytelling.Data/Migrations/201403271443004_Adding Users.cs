namespace SocialStorytelling.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUsers : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.UserDatas");
        }
    }
}
