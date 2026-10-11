using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CarSupplierRentPaymentMap : EntityTypeConfiguration<CarSupplierRentPayment>
    {
        internal CarSupplierRentPaymentMap()
        {
            ToTable("CarSupplierRentPayment", "dbo");
            HasKey(e => e.carSupplierRentPaymentID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.carSupplierRentPayment_Remarks).HasColumnName("carSupplierRentPayment_Remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.carSupplierRentPayment_PayDate).HasColumnName("carSupplierRentPayment_PayDate").HasColumnType("datetime").IsOptional();
            Property(e => e.carSupplierRentPayment_VoucherNo).HasColumnName("carSupplierRentPayment_VoucherNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.carSupplierRentPayment_amount).HasColumnName("carSupplierRentPayment_amount").HasColumnType("real").IsOptional();
            Property(e => e.carSupplierRentPayment_ForDate).HasColumnName("carSupplierRentPayment_ForDate").HasColumnType("datetime").IsOptional();
            Property(e => e.carSupplierRentPayment_mandoubID).HasColumnName("carSupplierRentPayment_mandoubID").HasColumnType("int").IsOptional();
            Property(e => e.carSupplierRentDetailsID).HasColumnName("carSupplierRentDetailsID").HasColumnType("int").IsOptional();
            Property(e => e.carSupplierRentID).HasColumnName("carSupplierRentID").HasColumnType("int").IsOptional();
            Property(e => e.carSupplierRentPaymentID).HasColumnName("carSupplierRentPaymentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.CarSupplierRent).WithMany(e => e.CarSupplierRentPayments)
                .HasForeignKey(e => e.carSupplierRentID).WillCascadeOnDelete(false);
        }
    }
}
