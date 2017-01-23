namespace Company1.Department1.Project1.AuthenticationService.Migrations
{
    using Company1.Department1.Project1.AuthenticationService.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Company1.Department1.Project1.AuthenticationService.Context.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Company1.Department1.Project1.AuthenticationService.Context.UserContext";
        }

        protected override void Seed(Company1.Department1.Project1.AuthenticationService.Context.UserContext context)
        {
            context.Applications.AddOrUpdate(a=> a.AppId, 
                new Application(){ AppId = "b95ad1af86ff4c92b31139e1972110c1", AppKey= "dcWXo0/G0nTzNp9TF9Fl96//rQyWPogPesw6StKmvXA="}, 
                new Application(){ AppId = "b95ad1af86ff4c92b31139e1972110c2", AppKey= "dcWXo0/G0nTzNp9TF9Fl96//rQyWPogPesw6StKmvXA="});

        }
    }
}
