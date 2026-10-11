using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class TransactionDefMap : EntityTypeConfiguration<TransactionDef>
    {
        internal TransactionDefMap()
        {
            ToTable("TransactionDef", "dbo");
            HasKey(e => e.transDefID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.transDef_namee).HasColumnName("transDef_namee").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.transDef_namea).HasColumnName("transDef_namea").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.transDef_code).HasColumnName("transDef_code").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.transDefID).HasColumnName("transDefID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
