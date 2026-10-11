using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class DocumentTransactionTaskMap : EntityTypeConfiguration<DocumentTransactionTask>
    {
        internal DocumentTransactionTaskMap()
        {
            ToTable("DocumentTransactionTasks", "dbo");
            HasKey(e => e.documentTransTaskID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.documentTransTask_isClose).HasColumnName("documentTransTask_isClose").HasColumnType("bit").IsOptional();
            Property(e => e.documentTransTask_isPaid).HasColumnName("documentTransTask_isPaid").HasColumnType("bit").IsOptional();
            Property(e => e.documentTransTask_ApprovedDate).HasColumnName("documentTransTask_ApprovedDate").HasColumnType("datetime").IsOptional();
            Property(e => e.documentTransTask_Approved).HasColumnName("documentTransTask_Approved").HasColumnType("bit").IsOptional();
            Property(e => e.documentTransTask_Remarks).HasColumnName("documentTransTask_Remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.documentTransTask_Status).HasColumnName("documentTransTask_Status").HasColumnType("bit").IsOptional();
            Property(e => e.documentTransTask_NoOfDays).HasColumnName("documentTransTask_NoOfDays").HasColumnType("int").IsOptional();
            Property(e => e.documentTransTask_date).HasColumnName("documentTransTask_date").HasColumnType("datetime").IsOptional();
            Property(e => e.documentTransID).HasColumnName("documentTransID").HasColumnType("int").IsOptional();
            Property(e => e.documentID).HasColumnName("documentID").HasColumnType("int").IsOptional();
            Property(e => e.documentTransTaskID).HasColumnName("documentTransTaskID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Document).WithMany(e => e.DocumentTransactionTasks)
                .HasForeignKey(e => e.documentID).WillCascadeOnDelete(false);

            HasOptional(e => e.DocumentTransaction).WithMany(e => e.DocumentTransactionTasks)
                .HasForeignKey(e => e.documentTransID).WillCascadeOnDelete(false);
        }
    }
}
