using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CarCustomerRentPaymentMap : EntityTypeConfiguration<CarCustomerRentPayment>
    {
        internal CarCustomerRentPaymentMap()
        {
            ToTable("CarCustomerRentPayment", "dbo");
            HasKey(e => e.carCustomerRentPaymentID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.carCustomerPayment_mandoubID).HasColumnName("carCustomerPayment_mandoubID").HasColumnType("int").IsOptional();
            Property(e => e.carCustomerPayment_Remarks).HasColumnName("carCustomerPayment_Remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.carCustomerPayment_PaymentDate).HasColumnName("carCustomerPayment_PaymentDate").HasColumnType("datetime").IsOptional();
            Property(e => e.carCustomerPayment_VoucherNo).HasColumnName("carCustomerPayment_VoucherNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.carCustomerPayment_amount).HasColumnName("carCustomerPayment_amount").HasColumnType("real").IsOptional();
            Property(e => e.carCustomerPayment_ForDate).HasColumnName("carCustomerPayment_ForDate").HasColumnType("datetime").IsOptional();
            Property(e => e.carCustomerRentDetailsID).HasColumnName("carCustomerRentDetailsID").HasColumnType("int").IsOptional();
            Property(e => e.carCustomerRentID).HasColumnName("carCustomerRentID").HasColumnType("int").IsOptional();
            Property(e => e.carCustomerRentPaymentID).HasColumnName("carCustomerRentPaymentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.CarCustomerRent).WithMany(e => e.CarCustomerRentPayments)
                .HasForeignKey(e => e.carCustomerRentID).WillCascadeOnDelete(false);
        }
    }
}
