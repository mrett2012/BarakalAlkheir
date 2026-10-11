using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class MaritalStatuMap : EntityTypeConfiguration<MaritalStatu>
    {
        internal MaritalStatuMap()
        {
            ToTable("MaritalStatus", "dbo");
            HasKey(e => e.maritalStatusID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.marital_namee).HasColumnName("marital_namee").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.marital_namea).HasColumnName("marital_namea").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.marital_Code).HasColumnName("marital_Code").HasColumnType("nvarchar").IsOptional().HasMaxLength(10).IsUnicode();
            Property(e => e.maritalStatusID).HasColumnName("maritalStatusID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
