using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class EmployeePassportMap : EntityTypeConfiguration<EmployeePassport>
    {
        internal EmployeePassportMap()
        {
            ToTable("EmployeePassports", "dbo");
            HasKey(e => e.passportID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.pass_InUsed).HasColumnName("pass_InUsed").HasColumnType("bit").IsOptional();
            Property(e => e.pass_type).HasColumnName("pass_type").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.pass_placeOfIssue).HasColumnName("pass_placeOfIssue").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.pass_expiryDate).HasColumnName("pass_expiryDate").HasColumnType("datetime").IsOptional();
            Property(e => e.pass_issueDate).HasColumnName("pass_issueDate").HasColumnType("datetime").IsOptional();
            Property(e => e.pass_no).HasColumnName("pass_no").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.employeeID).HasColumnName("employeeID").HasColumnType("int").IsOptional();
            Property(e => e.passportID).HasColumnName("passportID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Employee).WithMany(e => e.EmployeePassports)
                .HasForeignKey(e => e.employeeID).WillCascadeOnDelete(false);
        }
    }
}
