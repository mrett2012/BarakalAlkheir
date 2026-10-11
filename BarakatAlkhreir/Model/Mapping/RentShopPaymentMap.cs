using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class RentShopPaymentMap : EntityTypeConfiguration<RentShopPayment>
    {
        internal RentShopPaymentMap()
        {
            ToTable("RentShopPayment", "dbo");
            HasKey(e => e.rentLicensePaymentID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.rentShopPay_mandoubID).HasColumnName("rentShopPay_mandoubID").HasColumnType("int").IsOptional();
            Property(e => e.rentShopPay_ForDate).HasColumnName("rentShopPay_ForDate").HasColumnType("date").IsOptional();
            Property(e => e.rentShopPay_PaymentDate).HasColumnName("rentShopPay_PaymentDate").HasColumnType("datetime").IsOptional();
            Property(e => e.rentShopPay_Amount).HasColumnName("rentShopPay_Amount").HasColumnType("real").IsOptional();
            Property(e => e.rentShopPay_VouNo).HasColumnName("rentShopPay_VouNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.rentShopID).HasColumnName("rentShopID").HasColumnType("int").IsOptional();
            Property(e => e.rentLicensePaymentID).HasColumnName("rentLicensePaymentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.RentShop).WithMany(e => e.RentShopPayments)
                .HasForeignKey(e => e.rentShopID).WillCascadeOnDelete(false);
        }
    }
}
