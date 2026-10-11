using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CompanyLicencesEndMap : EntityTypeConfiguration<CompanyLicencesEnd>
    {
        internal CompanyLicencesEndMap()
        {
            ToTable("CompanyLicencesEnd", "dbo");
            HasKey(e => e.companyLicencesEndID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.companyLicencesEnd_isActive).HasColumnName("companyLicencesEnd_isActive").HasColumnType("bit").IsOptional();
            Property(e => e.companyLicencesEnd_reasons).HasColumnName("companyLicencesEnd_reasons").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.companyLicencesEnd_Date).HasColumnName("companyLicencesEnd_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.companyLicencesID).HasColumnName("companyLicencesID").HasColumnType("int").IsRequired();
            Property(e => e.companyLicencesEndID).HasColumnName("companyLicencesEndID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(e => e.CompanyLicence).WithMany(e => e.CompanyLicencesEnds)
                .HasForeignKey(e => e.companyLicencesID).WillCascadeOnDelete(false);
        }
    }
}
