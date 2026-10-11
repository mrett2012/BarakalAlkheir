using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class EmployeeSalaryMap : EntityTypeConfiguration<EmployeeSalary>
    {
        internal EmployeeSalaryMap()
        {
            ToTable("EmployeeSalary", "dbo");
            HasKey(e => e.employeeSalaryID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.VoucherNo).HasColumnName("VoucherNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.paidFromSalary).HasColumnName("paidFromSalary").HasColumnType("real").IsOptional();
            Property(e => e.branchName).HasColumnName("branchName").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.accountNo).HasColumnName("accountNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.bankID).HasColumnName("bankID").HasColumnType("int").IsOptional();
            Property(e => e.employeeID).HasColumnName("employeeID").HasColumnType("int").IsOptional();
            Property(e => e.employeeSalaryID).HasColumnName("employeeSalaryID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Bank).WithMany(e => e.EmployeeSalaries)
                .HasForeignKey(e => e.bankID).WillCascadeOnDelete(false);

            HasOptional(e => e.Employee).WithMany(e => e.EmployeeSalaries)
                .HasForeignKey(e => e.employeeID).WillCascadeOnDelete(false);
        }
    }
}
