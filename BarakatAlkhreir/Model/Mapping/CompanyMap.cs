using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CompanyMap : EntityTypeConfiguration<Company>
    {
        internal CompanyMap()
        {
            ToTable("Company", "dbo");
            HasKey(e => e.companyID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.comp_remarks).HasColumnName("comp_remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(500).IsUnicode();
            Property(e => e.comp_contactNo).HasColumnName("comp_contactNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_Designation).HasColumnName("comp_Designation").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_Salary).HasColumnName("comp_Salary").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_nationalityNo).HasColumnName("comp_nationalityNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_nationality).HasColumnName("comp_nationality").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_civilId).HasColumnName("comp_civilId").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_ownerName).HasColumnName("comp_ownerName").HasColumnType("nvarchar").IsOptional().HasMaxLength(100).IsUnicode();
            Property(e => e.comp_uniqueNo).HasColumnName("comp_uniqueNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_fileNo).HasColumnName("comp_fileNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_englishName).HasColumnName("comp_englishName").HasColumnType("nvarchar").IsOptional().HasMaxLength(70).IsUnicode();
            Property(e => e.comp_arabicName).HasColumnName("comp_arabicName").HasColumnType("nvarchar").IsOptional().HasMaxLength(70).IsUnicode();
            Property(e => e.comp_name5).HasColumnName("comp_name5").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_name4).HasColumnName("comp_name4").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_name3).HasColumnName("comp_name3").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_name2).HasColumnName("comp_name2").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_name1).HasColumnName("comp_name1").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_nam5).HasColumnName("comp_nam5").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_nam4).HasColumnName("comp_nam4").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_nam3).HasColumnName("comp_nam3").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_nam2).HasColumnName("comp_nam2").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.comp_nam1).HasColumnName("comp_nam1").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.companyID).HasColumnName("companyID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
