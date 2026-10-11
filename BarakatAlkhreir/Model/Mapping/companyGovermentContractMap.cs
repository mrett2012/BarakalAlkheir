using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class companyGovermentContractMap : EntityTypeConfiguration<companyGovermentContract>
    {
        internal companyGovermentContractMap()
        {
            ToTable("companyGovermentContract", "dbo");
            HasKey(e => e.govermentContractID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.endDate).HasColumnName("endDate").HasColumnType("datetime").IsOptional();
            Property(e => e.startDate).HasColumnName("startDate").HasColumnType("datetime").IsOptional();
            Property(e => e.gov_contractNo).HasColumnName("gov_contractNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.companyLicencesID).HasColumnName("companyLicencesID").HasColumnType("int").IsOptional();
            Property(e => e.govermentContractID).HasColumnName("govermentContractID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.CompanyLicence).WithMany(e => e.companyGovermentContracts)
                .HasForeignKey(e => e.companyLicencesID).WillCascadeOnDelete(false);
        }
    }
}
