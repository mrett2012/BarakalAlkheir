using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class UserGroupEventMap : EntityTypeConfiguration<UserGroupEvent>
    {
        internal UserGroupEventMap()
        {
            ToTable("UserGroupEvent", "dbo");
            HasKey(e => e.userGroupEventID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.isEnable).HasColumnName("isEnable").HasColumnType("bit").IsOptional();
            Property(e => e.formEventID).HasColumnName("formEventID").HasColumnType("int").IsOptional();
            Property(e => e.userGroupID).HasColumnName("userGroupID").HasColumnType("int").IsOptional();
            Property(e => e.userGroupEventID).HasColumnName("userGroupEventID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.FormEvent).WithMany(e => e.UserGroupEvents)
                .HasForeignKey(e => e.formEventID).WillCascadeOnDelete(true);

            HasOptional(e => e.UserGroup).WithMany(e => e.UserGroupEvents)
                .HasForeignKey(e => e.userGroupID).WillCascadeOnDelete(false);
        }
    }
}
