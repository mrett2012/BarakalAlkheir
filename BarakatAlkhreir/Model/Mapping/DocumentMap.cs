using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class DocumentMap : EntityTypeConfiguration<Document>
    {
        internal DocumentMap()
        {
            ToTable("Documents", "dbo");
            HasKey(e => e.documentID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.document_text5).HasColumnName("document_text5").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.document_text4).HasColumnName("document_text4").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.document_text3).HasColumnName("document_text3").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.document_text2).HasColumnName("document_text2").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.document_text1).HasColumnName("document_text1").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.document_TypeID).HasColumnName("document_TypeID").HasColumnType("int").IsOptional();
            Property(e => e.document_Remarks).HasColumnName("document_Remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(100).IsUnicode();
            Property(e => e.document_Status).HasColumnName("document_Status").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.document_placeOfIssue).HasColumnName("document_placeOfIssue").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.document_Type).HasColumnName("document_Type").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.document_No).HasColumnName("document_No").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.document_endDate).HasColumnName("document_endDate").HasColumnType("datetime").IsOptional();
            Property(e => e.document_startDate).HasColumnName("document_startDate").HasColumnType("datetime").IsOptional();
            Property(e => e.documentDefID).HasColumnName("documentDefID").HasColumnType("int").IsOptional();
            Property(e => e.documentID).HasColumnName("documentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.DocumentDef).WithMany(e => e.Documents)
                .HasForeignKey(e => e.documentDefID).WillCascadeOnDelete(false);
        }
    }
}
