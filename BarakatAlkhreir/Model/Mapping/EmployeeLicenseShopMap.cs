using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class EmployeeLicenseShopMap : EntityTypeConfiguration<EmployeeLicenseShop>
    {
        internal EmployeeLicenseShopMap()
        {
            ToTable("EmployeeLicenseShop", "dbo");
            HasKey(e => e.employeeLicShopID);
            Property(e => e.carID).HasColumnName("carID").HasColumnType("int").IsOptional();
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.employeeLink).HasColumnName("employeeLink").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.employeeID).HasColumnName("employeeID").HasColumnType("int").IsOptional();
            Property(e => e.licencesShopID).HasColumnName("licencesShopID").HasColumnType("int").IsOptional();
            Property(e => e.employeeLicShopID).HasColumnName("employeeLicShopID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Employee).WithMany(e => e.EmployeeLicenseShops)
                .HasForeignKey(e => e.employeeID).WillCascadeOnDelete(false);

            HasOptional(e => e.LicensesShop).WithMany(e => e.EmployeeLicenseShops)
                .HasForeignKey(e => e.licencesShopID).WillCascadeOnDelete(false);
        }
    }
}
