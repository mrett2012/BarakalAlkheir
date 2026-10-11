using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class RentShopEndMap : EntityTypeConfiguration<RentShopEnd>
    {
        internal RentShopEndMap()
        {
            ToTable("RentShopEnd", "dbo");
            HasKey(e => e.rentShopEndID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.rentShopEnd_isActive).HasColumnName("rentShopEnd_isActive").HasColumnType("bit").IsOptional();
            Property(e => e.rentShopEnd_reasons).HasColumnName("rentShopEnd_reasons").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.rentShopEnd_Date).HasColumnName("rentShopEnd_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.rentShopID).HasColumnName("rentShopID").HasColumnType("int").IsOptional();
            Property(e => e.rentShopEndID).HasColumnName("rentShopEndID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.RentShop).WithMany(e => e.RentShopEnds)
                .HasForeignKey(e => e.rentShopID).WillCascadeOnDelete(false);
        }
    }
}
