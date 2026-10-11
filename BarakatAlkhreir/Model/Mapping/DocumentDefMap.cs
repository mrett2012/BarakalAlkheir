using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class DocumentDefMap : EntityTypeConfiguration<DocumentDef>
    {
        internal DocumentDefMap()
        {
            ToTable("DocumentDef", "dbo");
            HasKey(e => e.documentDefID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.documentDef_WarningDays).HasColumnName("documentDef_WarningDays").HasColumnType("int").IsOptional();
            Property(e => e.documentDef_Name).HasColumnName("documentDef_Name").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.documentTypeDefID).HasColumnName("documentTypeDefID").HasColumnType("int").IsOptional();
            Property(e => e.documentDefCode).HasColumnName("documentDefCode").HasColumnType("nvarchar").IsOptional().HasMaxLength(5).IsUnicode();
            Property(e => e.documentDefID).HasColumnName("documentDefID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.DocumentTypeDefinition).WithMany(e => e.DocumentDefs)
                .HasForeignKey(e => e.documentTypeDefID).WillCascadeOnDelete(false);
        }
    }
}
