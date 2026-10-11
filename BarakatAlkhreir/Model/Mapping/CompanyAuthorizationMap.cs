using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CompanyAuthorizationMap : EntityTypeConfiguration<CompanyAuthorization>
    {
        internal CompanyAuthorizationMap()
        {
            ToTable("CompanyAuthorization", "dbo");
            HasKey(e => e.companyAuthorizationID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.auth_isCurrent).HasColumnName("auth_isCurrent").HasColumnType("bit").IsOptional();
            Property(e => e.authorizationID).HasColumnName("authorizationID").HasColumnType("int").IsOptional();
            Property(e => e.companyID).HasColumnName("companyID").HasColumnType("int").IsOptional();
            Property(e => e.companyAuthorizationID).HasColumnName("companyAuthorizationID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.AuthorizationPerson).WithMany(e => e.CompanyAuthorizations)
                .HasForeignKey(e => e.authorizationID).WillCascadeOnDelete(false);

            HasOptional(e => e.Company).WithMany(e => e.CompanyAuthorizations)
                .HasForeignKey(e => e.companyID).WillCascadeOnDelete(false);
        }
    }
}
