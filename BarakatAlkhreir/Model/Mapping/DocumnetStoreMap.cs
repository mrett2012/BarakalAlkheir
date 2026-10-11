using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class DocumnetStoreMap : EntityTypeConfiguration<DocumnetStore>
    {
        internal DocumnetStoreMap()
        {
            ToTable("DocumnetStore", "dbo");
            HasKey(e => e.documentStoreID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.documentStore_Description).HasColumnName("documentStore_Description").HasColumnType("nvarchar").IsOptional().HasMaxLength(100).IsUnicode();
            Property(e => e.documentStore_Date).HasColumnName("documentStore_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.documentStore_doc).HasColumnName("documentStore_doc").HasColumnType("varbinary(max)").IsOptional();
            Property(e => e.documentID).HasColumnName("documentID").HasColumnType("int").IsOptional();
            Property(e => e.documentStoreID).HasColumnName("documentStoreID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Document).WithMany(e => e.DocumnetStores)
                .HasForeignKey(e => e.documentID).WillCascadeOnDelete(false);
        }
    }
}
