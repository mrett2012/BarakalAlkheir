using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class DocumentTransactionStepMap : EntityTypeConfiguration<DocumentTransactionStep>
    {
        internal DocumentTransactionStepMap()
        {
            ToTable("DocumentTransactionSteps", "dbo");
            HasKey(e => e.documentTransSetpID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.documentTransSetp_Order).HasColumnName("documentTransSetp_Order").HasColumnType("int").IsOptional();
            Property(e => e.documentTransSetp_Period).HasColumnName("documentTransSetp_Period").HasColumnType("int").IsOptional();
            Property(e => e.documentTransSetp_name).HasColumnName("documentTransSetp_name").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.documentTransID).HasColumnName("documentTransID").HasColumnType("int").IsOptional();
            Property(e => e.documentTransSetp_pCode).HasColumnName("documentTransSetp_pCode").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.documentTransSetpID).HasColumnName("documentTransSetpID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.DocumentTransaction).WithMany(e => e.DocumentTransactionSteps)
                .HasForeignKey(e => e.documentTransID).WillCascadeOnDelete(false);
        }
    }
}
