using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class DocumentTypeDefinitionMap : EntityTypeConfiguration<DocumentTypeDefinition>
    {
        internal DocumentTypeDefinitionMap()
        {
            ToTable("DocumentTypeDefinition", "dbo");
            HasKey(e => e.documentTypeDefID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.documentType_Name).HasColumnName("documentType_Name").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.documentType_Code).HasColumnName("documentType_Code").HasColumnType("nvarchar").IsOptional().HasMaxLength(5).IsUnicode();
            Property(e => e.documentTypeDefID).HasColumnName("documentTypeDefID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
