using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CompanyLicencesRemarkMap : EntityTypeConfiguration<CompanyLicencesRemark>
    {
        internal CompanyLicencesRemarkMap()
        {
            ToTable("CompanyLicencesRemarks", "dbo");
            HasKey(e => e.comLicenseRemID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.comLicenseRem_isActive).HasColumnName("comLicenseRem_isActive").HasColumnType("bit").IsOptional();
            Property(e => e.comLicenseRem_Action).HasColumnName("comLicenseRem_Action").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.comLicenseRem_Remarks).HasColumnName("comLicenseRem_Remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.comLicenseRem_Date).HasColumnName("comLicenseRem_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.companyLicencesID).HasColumnName("companyLicencesID").HasColumnType("int").IsOptional();
            Property(e => e.comLicenseRemID).HasColumnName("comLicenseRemID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.CompanyLicence).WithMany(e => e.CompanyLicencesRemarks)
                .HasForeignKey(e => e.companyLicencesID).WillCascadeOnDelete(false);
        }
    }
}
