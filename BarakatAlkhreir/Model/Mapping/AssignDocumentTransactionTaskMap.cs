using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class AssignDocumentTransactionTaskMap : EntityTypeConfiguration<AssignDocumentTransactionTask>
    {
        internal AssignDocumentTransactionTaskMap()
        {
            ToTable("AssignDocumentTransactionTask", "dbo");
            HasKey(e => e.assignDocTransTaskID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.assignDocTransTask_IsClose).HasColumnName("assignDocTransTask_IsClose").HasColumnType("bit").IsOptional();
            Property(e => e.assignDocTransTask_FinishDate).HasColumnName("assignDocTransTask_FinishDate").HasColumnType("datetime").IsOptional();
            Property(e => e.assignDocTransTask_Remarks).HasColumnName("assignDocTransTask_Remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.assignDocTransTask_noOfDays).HasColumnName("assignDocTransTask_noOfDays").HasColumnType("int").IsOptional();
            Property(e => e.assignDocTransTask_date).HasColumnName("assignDocTransTask_date").HasColumnType("datetime").IsOptional();
            Property(e => e.assignDocTransTask_Name).HasColumnName("assignDocTransTask_Name").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.documentTransTaskID).HasColumnName("documentTransTaskID").HasColumnType("int").IsOptional();
            Property(e => e.employeeID).HasColumnName("employeeID").HasColumnType("int").IsOptional();
            Property(e => e.assignDocTransTaskID).HasColumnName("assignDocTransTaskID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.DocumentTransactionTask).WithMany(e => e.AssignDocumentTransactionTasks)
                .HasForeignKey(e => e.documentTransTaskID).WillCascadeOnDelete(false);
        }
    }
}
