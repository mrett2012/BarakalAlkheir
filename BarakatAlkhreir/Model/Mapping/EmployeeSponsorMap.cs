using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class EmployeeSponsorMap : EntityTypeConfiguration<EmployeeSponsor>
    {
        internal EmployeeSponsorMap()
        {
            ToTable("EmployeeSponsor", "dbo");
            HasKey(e => e.employeeSponsorID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.employeeSponsor_Jawazat).HasColumnName("employeeSponsor_Jawazat").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.employeeSponsor_Shou2oun).HasColumnName("employeeSponsor_Shou2oun").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.employeeSponsor_WorkPermitNo).HasColumnName("employeeSponsor_WorkPermitNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.employeeSponsor_useDuration).HasColumnName("employeeSponsor_useDuration").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.employeeSponsor_salary).HasColumnName("employeeSponsor_salary").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.employeeSponsor_entryDate).HasColumnName("employeeSponsor_entryDate").HasColumnType("datetime").IsOptional();
            Property(e => e.employeeSponsor_residencyEndDate).HasColumnName("employeeSponsor_residencyEndDate").HasColumnType("datetime").IsOptional();
            Property(e => e.employeeSponsor_residencyStartDate).HasColumnName("employeeSponsor_residencyStartDate").HasColumnType("datetime").IsOptional();
            Property(e => e.employeeSponsor_residencyNo).HasColumnName("employeeSponsor_residencyNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.designationID).HasColumnName("designationID").HasColumnType("int").IsOptional();
            Property(e => e.govermentContractID).HasColumnName("govermentContractID").HasColumnType("int").IsOptional();
            Property(e => e.companyLicencesID).HasColumnName("companyLicencesID").HasColumnType("int").IsOptional();
            Property(e => e.companyID).HasColumnName("companyID").HasColumnType("int").IsOptional();
            Property(e => e.employeeID).HasColumnName("employeeID").HasColumnType("int").IsOptional();
            Property(e => e.employeeSponsorID).HasColumnName("employeeSponsorID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Company).WithMany(e => e.EmployeeSponsors)
                .HasForeignKey(e => e.companyID).WillCascadeOnDelete(false);

            HasOptional(e => e.CompanyLicence).WithMany(e => e.EmployeeSponsors)
                .HasForeignKey(e => e.companyLicencesID).WillCascadeOnDelete(false);

            HasOptional(e => e.Designation).WithMany(e => e.EmployeeSponsors)
                .HasForeignKey(e => e.designationID).WillCascadeOnDelete(false);

            HasOptional(e => e.Employee).WithMany(e => e.EmployeeSponsors)
                .HasForeignKey(e => e.employeeID).WillCascadeOnDelete(false);
        }
    }
}
