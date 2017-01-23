namespace Company1.Department1.Project1.AuthenticationService.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequestLogTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.RequestLogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RequestLogs",
                c => new
                    {
                        Random = c.String(nullable: false, maxLength: 128),
                        TimeStamp = c.String(),
                    })
                .PrimaryKey(t => t.Random);
            
        }
    }
}
