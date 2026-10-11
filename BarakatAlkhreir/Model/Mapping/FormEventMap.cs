using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class FormEventMap : EntityTypeConfiguration<FormEvent>
    {
        internal FormEventMap()
        {
            ToTable("FormEvent", "dbo");
            HasKey(e => e.formEventID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.eventID).HasColumnName("eventID").HasColumnType("int").IsOptional();
            Property(e => e.formID).HasColumnName("formID").HasColumnType("int").IsOptional();
            Property(e => e.formEventID).HasColumnName("formEventID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.EventsDefinition).WithMany(e => e.FormEvents)
                .HasForeignKey(e => e.eventID).WillCascadeOnDelete(false);

            HasOptional(e => e.FormScrean).WithMany(e => e.FormEvents)
                .HasForeignKey(e => e.formID).WillCascadeOnDelete(false);
        }
    }
}
