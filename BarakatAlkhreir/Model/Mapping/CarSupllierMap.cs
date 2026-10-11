using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CarSupllierMap : EntityTypeConfiguration<CarSupllier>
    {
        internal CarSupllierMap()
        {
            ToTable("CarSuplliers", "dbo");
            HasKey(e => e.carSuppliserID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.carSuppliser_remarks).HasColumnName("carSuppliser_remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.carSuppliser_Address).HasColumnName("carSuppliser_Address").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.carSuppliser_Contact).HasColumnName("carSuppliser_Contact").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.carSuppliser_PersonInCharge).HasColumnName("carSuppliser_PersonInCharge").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.carSuppliser_Name).HasColumnName("carSuppliser_Name").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.carSuppliserID).HasColumnName("carSuppliserID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
