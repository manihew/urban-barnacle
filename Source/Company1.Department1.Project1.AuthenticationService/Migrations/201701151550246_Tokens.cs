namespace Company1.Department1.Project1.AuthenticationService.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Tokens : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        AuthToken = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.AuthToken);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tokens");
        }
    }
}
