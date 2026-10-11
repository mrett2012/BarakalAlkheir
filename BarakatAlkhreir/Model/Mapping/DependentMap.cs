using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class DependentMap : EntityTypeConfiguration<Dependent>
    {
        internal DependentMap()
        {
            ToTable("Dependents", "dbo");
            HasKey(e => e.dependentsID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.depend_religion).HasColumnName("depend_religion").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.depend_nationality).HasColumnName("depend_nationality").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.depend_gender).HasColumnName("depend_gender").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.depend_birthday).HasColumnName("depend_birthday").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.depend_relation).HasColumnName("depend_relation").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.depend_eName).HasColumnName("depend_eName").HasColumnType("nvarchar").IsOptional().HasMaxLength(150).IsUnicode();
            Property(e => e.depend_aName).HasColumnName("depend_aName").HasColumnType("nvarchar").IsOptional().HasMaxLength(150).IsUnicode();
            Property(e => e.employeeID).HasColumnName("employeeID").HasColumnType("int").IsOptional();
            Property(e => e.dependentsID).HasColumnName("dependentsID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Employee).WithMany(e => e.Dependents)
                .HasForeignKey(e => e.employeeID).WillCascadeOnDelete(false);
        }
    }
}
