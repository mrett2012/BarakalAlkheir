using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class AuthorizationPersonMap : EntityTypeConfiguration<AuthorizationPerson>
    {
        internal AuthorizationPersonMap()
        {
            ToTable("AuthorizationPerson", "dbo");
            HasKey(e => e.authorizationID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.auth_isCurrent).HasColumnName("auth_isCurrent").HasColumnType("bit").IsOptional();
            Property(e => e.auth_remarks).HasColumnName("auth_remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(75).IsUnicode();
            Property(e => e.auth_ContactNo).HasColumnName("auth_ContactNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_Designation).HasColumnName("auth_Designation").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_CivilID).HasColumnName("auth_CivilID").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.fauth_ullEnglishName).HasColumnName("fauth_ullEnglishName").HasColumnType("nvarchar").IsOptional().HasMaxLength(70).IsUnicode();
            Property(e => e.fauth_ullArabicName).HasColumnName("fauth_ullArabicName").HasColumnType("nvarchar").IsOptional().HasMaxLength(70).IsUnicode();
            Property(e => e.auth_namee5).HasColumnName("auth_namee5").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_namee4).HasColumnName("auth_namee4").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_namee3).HasColumnName("auth_namee3").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_namee2).HasColumnName("auth_namee2").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_namee1).HasColumnName("auth_namee1").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_namea5).HasColumnName("auth_namea5").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_namea4).HasColumnName("auth_namea4").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_namea3).HasColumnName("auth_namea3").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_namea2).HasColumnName("auth_namea2").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.auth_namea1).HasColumnName("auth_namea1").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.authorizationID).HasColumnName("authorizationID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
