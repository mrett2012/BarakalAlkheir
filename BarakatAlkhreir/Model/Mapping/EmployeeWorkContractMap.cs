using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class EmployeeWorkContractMap : EntityTypeConfiguration<EmployeeWorkContract>
    {
        internal EmployeeWorkContractMap()
        {
            ToTable("EmployeeWorkContract", "dbo");
            HasKey(e => e.workContractID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.wc_payEvery).HasColumnName("wc_payEvery").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.wc_duration).HasColumnName("wc_duration").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.wc_durationType).HasColumnName("wc_durationType").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.wc_startingContract).HasColumnName("wc_startingContract").HasColumnType("datetime").IsOptional();
            Property(e => e.wc_date).HasColumnName("wc_date").HasColumnType("datetime").IsOptional();
            Property(e => e.employeeID).HasColumnName("employeeID").HasColumnType("int").IsOptional();
            Property(e => e.workContractID).HasColumnName("workContractID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Employee).WithMany(e => e.EmployeeWorkContracts)
                .HasForeignKey(e => e.employeeID).WillCascadeOnDelete(false);
        }
    }
}
