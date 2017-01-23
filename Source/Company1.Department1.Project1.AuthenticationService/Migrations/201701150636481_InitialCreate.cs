namespace Company1.Department1.Project1.AuthenticationService.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        AppId = c.String(nullable: false, maxLength: 128),
                        AppKey = c.String(),
                    })
                .PrimaryKey(t => t.AppId);

            CreateTable(
                "dbo.RequestLogs",
                c => new
                {
                    Random = c.String(nullable: false, maxLength: 128),
                    TimeStamp = c.String(),
                })
                .PrimaryKey(t => t.Random);
            
            CreateTable(
                "dbo.UserLogs",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        LatestAppId = c.String(),
                    })
                .PrimaryKey(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserLogs");
            DropTable("dbo.RequestLogs");
            DropTable("dbo.Applications");
        }
    }
}
