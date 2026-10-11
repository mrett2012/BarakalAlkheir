using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class RentLicenseEndMap : EntityTypeConfiguration<RentLicenseEnd>
    {
        internal RentLicenseEndMap()
        {
            ToTable("RentLicenseEnd", "dbo");
            HasKey(e => e.rentLicenseEndID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.rentLicEnd_reasons).HasColumnName("rentLicEnd_reasons").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.rentLicEnd_Date).HasColumnName("rentLicEnd_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.rentLicenseID).HasColumnName("rentLicenseID").HasColumnType("int").IsOptional();
            Property(e => e.rentLicenseEndID).HasColumnName("rentLicenseEndID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.RentLicens).WithMany(e => e.RentLicenseEnds)
                .HasForeignKey(e => e.rentLicenseID).WillCascadeOnDelete(false);
        }
    }
}
