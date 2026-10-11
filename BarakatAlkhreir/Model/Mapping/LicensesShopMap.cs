using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class LicensesShopMap : EntityTypeConfiguration<LicensesShop>
    {
        internal LicensesShopMap()
        {
            ToTable("LicensesShop", "dbo");
            HasKey(e => e.licencesShopID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.goverareaID).HasColumnName("goverareaID").HasColumnType("int").IsOptional();
            Property(e => e.governorateID).HasColumnName("governorateID").HasColumnType("int").IsOptional();
            Property(e => e.shop_isCurrent).HasColumnName("shop_isCurrent").HasColumnType("bit").IsOptional();
            Property(e => e.shop_contact2).HasColumnName("shop_contact2").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.shop_contact1).HasColumnName("shop_contact1").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.shop_Name).HasColumnName("shop_Name").HasColumnType("nvarchar").IsOptional().HasMaxLength(70).IsUnicode();
            Property(e => e.shop_Code).HasColumnName("shop_Code").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.companyLicencesID).HasColumnName("companyLicencesID").HasColumnType("int").IsOptional();
            Property(e => e.licencesShopID).HasColumnName("licencesShopID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasOptional(e => e.CompanyLicence).WithMany(e => e.LicensesShops)
                .HasForeignKey(e => e.companyLicencesID).WillCascadeOnDelete(false);
        }
    }
}
