using System.Data.Entity.Migrations;

namespace BarakalAlkheir.Model.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<BarakeitAlkheirEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "BarakalAlkheir.Model.BarakeitAlkheirEntities";
            MigrationsDirectory = "Model\\Migrations";
        }
    }
}
