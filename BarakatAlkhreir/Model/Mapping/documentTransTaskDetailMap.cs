using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class documentTransTaskDetailMap : EntityTypeConfiguration<documentTransTaskDetail>
    {
        internal documentTransTaskDetailMap()
        {
            ToTable("documentTransTaskDetails", "dbo");
            HasKey(e => e.documentTransTaskDetailsID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.documentTransSetp_Remarks).HasColumnName("documentTransSetp_Remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(500).IsUnicode();
            Property(e => e.documentTransSetp_isClose).HasColumnName("documentTransSetp_isClose").HasColumnType("bit").IsOptional();
            Property(e => e.employeeID).HasColumnName("employeeID").HasColumnType("int").IsOptional();
            Property(e => e.documentTransSetp_pCode).HasColumnName("documentTransSetp_pCode").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.documentTransSetp_endDate).HasColumnName("documentTransSetp_endDate").HasColumnType("datetime").IsOptional();
            Property(e => e.documentTransSetp_startDate).HasColumnName("documentTransSetp_startDate").HasColumnType("datetime").IsOptional();
            Property(e => e.documentTransSetp_Period).HasColumnName("documentTransSetp_Period").HasColumnType("int").IsOptional();
            Property(e => e.documentTransSetp_name).HasColumnName("documentTransSetp_name").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.documentTransTaskID).HasColumnName("documentTransTaskID").HasColumnType("int").IsOptional();
            Property(e => e.documentTransTaskDetailsID).HasColumnName("documentTransTaskDetailsID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.DocumentTransactionTask).WithMany(e => e.documentTransTaskDetails)
                .HasForeignKey(e => e.documentTransTaskID).WillCascadeOnDelete(false);
        }
    }
}
