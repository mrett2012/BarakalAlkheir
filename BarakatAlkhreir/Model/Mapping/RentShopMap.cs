using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class RentShopMap : EntityTypeConfiguration<RentShop>
    {
        internal RentShopMap()
        {
            ToTable("RentShop", "dbo");
            HasKey(e => e.rentShopID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.rentShop_active).HasColumnName("rentShop_active").HasColumnType("bit").IsOptional();
            Property(e => e.rentShop_DamanAmount).HasColumnName("rentShop_DamanAmount").HasColumnType("real").IsOptional();
            Property(e => e.rentShop_RentalAmount).HasColumnName("rentShop_RentalAmount").HasColumnType("real").IsOptional();
            Property(e => e.rentShop_to).HasColumnName("rentShop_to").HasColumnType("datetime").IsOptional();
            Property(e => e.rentShop_from).HasColumnName("rentShop_from").HasColumnType("datetime").IsOptional();
            Property(e => e.rentShop_date).HasColumnName("rentShop_date").HasColumnType("datetime").IsOptional();
            Property(e => e.licencesShopID).HasColumnName("licencesShopID").HasColumnType("int").IsOptional();
            Property(e => e.customerID).HasColumnName("customerID").HasColumnType("int").IsOptional();
            Property(e => e.rentShopNo).HasColumnName("rentShopNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.rentShopID).HasColumnName("rentShopID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Customer).WithMany(e => e.RentShops)
                .HasForeignKey(e => e.customerID).WillCascadeOnDelete(false);

            HasOptional(e => e.LicensesShop).WithMany(e => e.RentShops)
                .HasForeignKey(e => e.licencesShopID).WillCascadeOnDelete(false);
        }
    }
}
