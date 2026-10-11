using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CompanyLicenseDesignationMap : EntityTypeConfiguration<CompanyLicenseDesignation>
    {
        internal CompanyLicenseDesignationMap()
        {
            ToTable("CompanyLicenseDesignation", "dbo");
            HasKey(e => e.companyLicencesDesignationID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.noOfSeats).HasColumnName("noOfSeats").HasColumnType("int").IsOptional();
            Property(e => e.designationID).HasColumnName("designationID").HasColumnType("int").IsOptional();
            Property(e => e.companyLicencesID).HasColumnName("companyLicencesID").HasColumnType("int").IsOptional();
            Property(e => e.companyLicencesDesignationID).HasColumnName("companyLicencesDesignationID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.CompanyLicence).WithMany(e => e.CompanyLicenseDesignations)
                .HasForeignKey(e => e.companyLicencesID).WillCascadeOnDelete(false);

            HasOptional(e => e.Designation).WithMany(e => e.CompanyLicenseDesignations)
                .HasForeignKey(e => e.designationID).WillCascadeOnDelete(false);
        }
    }
}
