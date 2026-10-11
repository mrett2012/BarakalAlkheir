using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class GovernorateAreaMap : EntityTypeConfiguration<GovernorateArea>
    {
        internal GovernorateAreaMap()
        {
            ToTable("GovernorateArea", "dbo");
            HasKey(e => e.goverareaID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.goverarea_namee).HasColumnName("goverarea_namee").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.goverarea_namea).HasColumnName("goverarea_namea").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.goverarea_Code).HasColumnName("goverarea_Code").HasColumnType("nvarchar").IsOptional().HasMaxLength(10).IsUnicode();
            Property(e => e.governorateID).HasColumnName("governorateID").HasColumnType("int").IsRequired();
            Property(e => e.goverareaID).HasColumnName("goverareaID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(e => e.Governorate).WithMany(e => e.GovernorateAreas)
                .HasForeignKey(e => e.governorateID).WillCascadeOnDelete(false);
        }
    }
}
