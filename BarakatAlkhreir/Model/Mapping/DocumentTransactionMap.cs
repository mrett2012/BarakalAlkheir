using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class DocumentTransactionMap : EntityTypeConfiguration<DocumentTransaction>
    {
        internal DocumentTransactionMap()
        {
            ToTable("DocumentTransactions", "dbo");
            HasKey(e => e.documentTransID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.documentTrans_Description).HasColumnName("documentTrans_Description").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.documentTrans_Amount).HasColumnName("documentTrans_Amount").HasColumnType("real").IsOptional();
            Property(e => e.transDefID).HasColumnName("transDefID").HasColumnType("int").IsOptional();
            Property(e => e.documentDefID).HasColumnName("documentDefID").HasColumnType("int").IsOptional();
            Property(e => e.documentTransID).HasColumnName("documentTransID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.DocumentDef).WithMany(e => e.DocumentTransactions)
                .HasForeignKey(e => e.documentDefID).WillCascadeOnDelete(false);

            HasOptional(e => e.TransactionDef).WithMany(e => e.DocumentTransactions)
                .HasForeignKey(e => e.transDefID).WillCascadeOnDelete(false);
        }
    }
}
