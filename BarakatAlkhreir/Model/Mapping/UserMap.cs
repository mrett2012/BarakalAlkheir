using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class UserMap : EntityTypeConfiguration<User>
    {
        internal UserMap()
        {
            ToTable("Users", "dbo");
            HasKey(e => e.userID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.isActive).HasColumnName("isActive").HasColumnType("bit").IsOptional();
            Property(e => e.isSuperUser).HasColumnName("isSuperUser").HasColumnType("bit").IsOptional();
            Property(e => e.password).HasColumnName("password").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.userName).HasColumnName("userName").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.displayName).HasColumnName("displayName").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.userGroupID).HasColumnName("userGroupID").HasColumnType("int").IsOptional();
            Property(e => e.userID).HasColumnName("userID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.UserGroup).WithMany(e => e.Users)
                .HasForeignKey(e => e.userGroupID).WillCascadeOnDelete(false);
        }
    }
}
