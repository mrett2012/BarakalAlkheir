using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class DocumentTransactionTasksPaymentMap : EntityTypeConfiguration<DocumentTransactionTasksPayment>
    {
        internal DocumentTransactionTasksPaymentMap()
        {
            ToTable("DocumentTransactionTasksPayment", "dbo");
            HasKey(e => e.DocumentTransactionTasksPaymentID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.voucherAmount).HasColumnName("voucherAmount").HasColumnType("real").IsOptional();
            Property(e => e.voucherNo).HasColumnName("voucherNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.documentTransTaskID).HasColumnName("documentTransTaskID").HasColumnType("int").IsOptional();
            Property(e => e.DocumentTransactionTasksPaymentID).HasColumnName("DocumentTransactionTasksPaymentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.DocumentTransactionTask).WithMany(e => e.DocumentTransactionTasksPayments)
                .HasForeignKey(e => e.documentTransTaskID).WillCascadeOnDelete(false);
        }
    }
}
