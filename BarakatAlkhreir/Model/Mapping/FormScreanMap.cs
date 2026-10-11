using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class FormScreanMap : EntityTypeConfiguration<FormScrean>
    {
        internal FormScreanMap()
        {
            ToTable("FormScrean", "dbo");
            HasKey(e => e.formID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.form_Descriptions).HasColumnName("form_Descriptions").HasColumnType("nvarchar").IsOptional().HasMaxLength(150).IsUnicode();
            Property(e => e.form_DisplayName).HasColumnName("form_DisplayName").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.form_IDName).HasColumnName("form_IDName").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.formID).HasColumnName("formID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
