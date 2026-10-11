using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class GovernorateMap : EntityTypeConfiguration<Governorate>
    {
        internal GovernorateMap()
        {
            ToTable("Governorate", "dbo");
            HasKey(e => e.governorateID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.gover_namee).HasColumnName("gover_namee").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.gover_namea).HasColumnName("gover_namea").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.gover_Code).HasColumnName("gover_Code").HasColumnType("nvarchar").IsOptional().HasMaxLength(10).IsUnicode();
            Property(e => e.governorateID).HasColumnName("governorateID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
