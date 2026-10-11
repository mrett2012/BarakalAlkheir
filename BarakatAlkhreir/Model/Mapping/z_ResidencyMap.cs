using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class z_ResidencyMap : EntityTypeConfiguration<z_Residency>
    {
        internal z_ResidencyMap()
        {
            ToTable("z-Residency", "dbo");
            HasKey(e => e.residencyID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.residency_Jawazat).HasColumnName("residency_Jawazat").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.residency_Shou2oun).HasColumnName("residency_Shou2oun").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.residency_WorkPermitNo).HasColumnName("residency_WorkPermitNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.residency_useDuration).HasColumnName("residency_useDuration").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.residency_salary).HasColumnName("residency_salary").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.residency_entryDate).HasColumnName("residency_entryDate").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.residency_type).HasColumnName("residency_type").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.residency_endDate).HasColumnName("residency_endDate").HasColumnType("datetime").IsOptional();
            Property(e => e.residency_startDate).HasColumnName("residency_startDate").HasColumnType("datetime").IsOptional();
            Property(e => e.residency_No).HasColumnName("residency_No").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.designationID).HasColumnName("designationID").HasColumnType("int").IsOptional();
            Property(e => e.employeeID).HasColumnName("employeeID").HasColumnType("int").IsOptional();
            Property(e => e.residencyID).HasColumnName("residencyID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
