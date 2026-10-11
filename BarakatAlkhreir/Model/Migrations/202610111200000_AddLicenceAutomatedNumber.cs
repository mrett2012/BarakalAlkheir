using System.Data.Entity.Migrations;

namespace BarakalAlkheir.Model.Migrations
{
    public sealed partial class AddLicenceAutomatedNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyLicences", "licenseAutomatedNumber", c => c.String(maxLength: 50));
        }

        public override void Down()
        {
            DropColumn("dbo.CompanyLicences", "licenseAutomatedNumber");
        }
    }
}
