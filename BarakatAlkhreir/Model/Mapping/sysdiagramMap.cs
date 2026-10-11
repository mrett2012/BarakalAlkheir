using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class sysdiagramMap : EntityTypeConfiguration<sysdiagram>
    {
        internal sysdiagramMap()
        {
            ToTable("sysdiagrams", "dbo");
            HasKey(e => e.diagram_id);
            Property(e => e.definition).HasColumnName("definition").HasColumnType("varbinary(max)").IsOptional();
            Property(e => e.version).HasColumnName("version").HasColumnType("int").IsOptional();
            Property(e => e.diagram_id).HasColumnName("diagram_id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.principal_id).HasColumnName("principal_id").HasColumnType("int").IsRequired();
            Property(e => e.name).HasColumnName("name").HasColumnType("nvarchar").IsRequired().HasMaxLength(128).IsUnicode();
        }
    }
}
