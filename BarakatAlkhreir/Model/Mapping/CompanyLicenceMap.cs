using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CompanyLicenceMap : EntityTypeConfiguration<CompanyLicence>
    {
        internal CompanyLicenceMap()
        {
            ToTable("CompanyLicences", "dbo");
            HasKey(e => e.companyLicencesID);
            Property(e => e.licenseActivityID).HasColumnName("licenseActivityID").HasColumnType("int").IsOptional();
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.fulladdress).HasColumnName("fulladdress").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.contactNo).HasColumnName("contactNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(100).IsUnicode();
            Property(e => e.email).HasColumnName("email").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.poboxOffice).HasColumnName("poboxOffice").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.pobox).HasColumnName("pobox").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.appartment).HasColumnName("appartment").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.floor).HasColumnName("floor").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.BuilduingType).HasColumnName("BuilduingType").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.Share3).HasColumnName("Share3").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.Quet3a).HasColumnName("Quet3a").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.Mantaqa).HasColumnName("Mantaqa").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.goverareaID).HasColumnName("goverareaID").HasColumnType("int").IsOptional();
            Property(e => e.Mohafaza).HasColumnName("Mohafaza").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.governorateID).HasColumnName("governorateID").HasColumnType("int").IsOptional();
            Property(e => e.mandoubID).HasColumnName("mandoubID").HasColumnType("int").IsOptional();
            Property(e => e.isFack).HasColumnName("isFack").HasColumnType("bit").IsOptional();
            Property(e => e.isClose).HasColumnName("isClose").HasColumnType("bit").IsOptional();
            Property(e => e.isActive).HasColumnName("isActive").HasColumnType("bit").IsOptional();
            Property(e => e.isMainLicences).HasColumnName("isMainLicences").HasColumnType("bit").IsOptional();
            Property(e => e.noOfSeats).HasColumnName("noOfSeats").HasColumnType("int").IsOptional();
            Property(e => e.endDate).HasColumnName("endDate").HasColumnType("datetime").IsOptional();
            Property(e => e.startDate).HasColumnName("startDate").HasColumnType("datetime").IsOptional();
            Property(e => e.licensecivilID).HasColumnName("licensecivilID").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.commercialName).HasColumnName("commercialName").HasColumnType("nvarchar").IsOptional().HasMaxLength(100).IsUnicode();
            Property(e => e.licenseLinkCode).HasColumnName("licenseLinkCode").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.licenseUniqueNo).HasColumnName("licenseUniqueNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.licenseAutomatedNumber).HasColumnName("licenseAutomatedNumber").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.licenseNo).HasColumnName("licenseNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.companyID).HasColumnName("companyID").HasColumnType("int").IsOptional();
            Property(e => e.companyLicencesID).HasColumnName("companyLicencesID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Company).WithMany(e => e.CompanyLicences)
                .HasForeignKey(e => e.companyID).WillCascadeOnDelete(false);
        }
    }
}
