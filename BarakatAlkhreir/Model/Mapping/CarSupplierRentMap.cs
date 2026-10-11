using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CarSupplierRentMap : EntityTypeConfiguration<CarSupplierRent>
    {
        internal CarSupplierRentMap()
        {
            ToTable("CarSupplierRent", "dbo");
            HasKey(e => e.carSupplierRentID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.carSupplierRent_isactive).HasColumnName("carSupplierRent_isactive").HasColumnType("bit").IsOptional();
            Property(e => e.carSupplierRent_isBuyed).HasColumnName("carSupplierRent_isBuyed").HasColumnType("bit").IsOptional();
            Property(e => e.carSupplierRent_isRented).HasColumnName("carSupplierRent_isRented").HasColumnType("bit").IsOptional();
            Property(e => e.carSupplierRent_lastPaymentAmount).HasColumnName("carSupplierRent_lastPaymentAmount").HasColumnType("real").IsOptional();
            Property(e => e.carSupplierRent_firstPaymentAmount).HasColumnName("carSupplierRent_firstPaymentAmount").HasColumnType("real").IsOptional();
            Property(e => e.carSupplierRent_firstPaymentDate).HasColumnName("carSupplierRent_firstPaymentDate").HasColumnType("datetime").IsOptional();
            Property(e => e.carSupplierRent_noOfMonths).HasColumnName("carSupplierRent_noOfMonths").HasColumnType("int").IsOptional();
            Property(e => e.carSupplierRent_MonthlyAmount).HasColumnName("carSupplierRent_MonthlyAmount").HasColumnType("real").IsOptional();
            Property(e => e.carSupplierRent_totalAmount).HasColumnName("carSupplierRent_totalAmount").HasColumnType("real").IsOptional();
            Property(e => e.carSupplierRent_to).HasColumnName("carSupplierRent_to").HasColumnType("datetime").IsOptional();
            Property(e => e.carSupplierRent_from).HasColumnName("carSupplierRent_from").HasColumnType("datetime").IsOptional();
            Property(e => e.carSupplierRent_Date).HasColumnName("carSupplierRent_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.carRentNo).HasColumnName("carRentNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.carSuppliserID).HasColumnName("carSuppliserID").HasColumnType("int").IsOptional();
            Property(e => e.carID).HasColumnName("carID").HasColumnType("int").IsOptional();
            Property(e => e.carSupplierRentID).HasColumnName("carSupplierRentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Car).WithMany(e => e.CarSupplierRents)
                .HasForeignKey(e => e.carID).WillCascadeOnDelete(false);

            HasOptional(e => e.CarSupllier).WithMany(e => e.CarSupplierRents)
                .HasForeignKey(e => e.carSuppliserID).WillCascadeOnDelete(false);
        }
    }
}
