using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class EmployeeStatusDefMap : EntityTypeConfiguration<EmployeeStatusDef>
    {
        internal EmployeeStatusDefMap()
        {
            ToTable("EmployeeStatusDef", "dbo");
            HasKey(e => e.employeeStatusID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.employeeStatus_namee).HasColumnName("employeeStatus_namee").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.employeeStatus_namea).HasColumnName("employeeStatus_namea").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.employeeStatus_Code).HasColumnName("employeeStatus_Code").HasColumnType("nvarchar").IsOptional().HasMaxLength(10).IsUnicode();
            Property(e => e.employeeStatusID).HasColumnName("employeeStatusID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
