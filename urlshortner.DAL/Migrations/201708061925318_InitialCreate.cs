namespace urlshortner.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LongURL = c.String(nullable: false),
                        ShortURL = c.String(),
                        ClickCount = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        UserCreator_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserCreator_ID)
                .Index(t => t.UserCreator_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nickname = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "UserCreator_ID", "dbo.Users");
            DropIndex("dbo.Links", new[] { "UserCreator_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Links");
        }
    }
}
