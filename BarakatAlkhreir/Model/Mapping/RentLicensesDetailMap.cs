using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class RentLicensesDetailMap : EntityTypeConfiguration<RentLicensesDetail>
    {
        internal RentLicensesDetailMap()
        {
            ToTable("RentLicensesDetails", "dbo");
            HasKey(e => e.rentLicencesDetailsID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.rd_isPaid).HasColumnName("rd_isPaid").HasColumnType("bit").IsOptional();
            Property(e => e.rd_amount).HasColumnName("rd_amount").HasColumnType("real").IsOptional();
            Property(e => e.rd_mNo).HasColumnName("rd_mNo").HasColumnType("int").IsOptional();
            Property(e => e.rd_toDate).HasColumnName("rd_toDate").HasColumnType("datetime").IsOptional();
            Property(e => e.rd_fromDate).HasColumnName("rd_fromDate").HasColumnType("datetime").IsOptional();
            Property(e => e.rentLicenseID).HasColumnName("rentLicenseID").HasColumnType("int").IsOptional();
            Property(e => e.rentLicencesDetailsID).HasColumnName("rentLicencesDetailsID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.rentLicPay_VouNo).HasColumnType("nvarchar").HasMaxLength(50).IsUnicode().IsOptional();
            Property(e => e.rentLicPay_PaymentDate).HasColumnType("datetime").IsOptional();
            Property(e => e.rentLicPay_mandoubID).HasColumnType("int").IsOptional();
            Property(e => e.payment_added_Date).HasColumnType("datetime").IsOptional();
            Property(e => e.payment_edited_Date).HasColumnType("datetime").IsOptional();
            Property(e => e.payment_addedBy).HasColumnType("int").IsOptional();
            Property(e => e.payment_editedBy).HasColumnType("int").IsOptional();
            Property(e => e.legacyPaymentID).HasColumnType("int").IsOptional();

            HasOptional(e => e.RentLicens).WithMany(e => e.RentLicensesDetails)
                .HasForeignKey(e => e.rentLicenseID).WillCascadeOnDelete(false);
        }
    }
}
