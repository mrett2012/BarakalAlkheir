using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CarSupplierRentDetailMap : EntityTypeConfiguration<CarSupplierRentDetail>
    {
        internal CarSupplierRentDetailMap()
        {
            ToTable("CarSupplierRentDetails", "dbo");
            HasKey(e => e.carSupplierRentDetailsID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.rd_isMonthlyPayment).HasColumnName("rd_isMonthlyPayment").HasColumnType("bit").IsOptional();
            Property(e => e.rd_isLastPayment).HasColumnName("rd_isLastPayment").HasColumnType("bit").IsOptional();
            Property(e => e.rd_isFirstPayment).HasColumnName("rd_isFirstPayment").HasColumnType("bit").IsOptional();
            Property(e => e.rd_isPaid).HasColumnName("rd_isPaid").HasColumnType("bit").IsOptional();
            Property(e => e.rd_amount).HasColumnName("rd_amount").HasColumnType("real").IsOptional();
            Property(e => e.rd_year).HasColumnName("rd_year").HasColumnType("int").IsOptional();
            Property(e => e.rd_mNo).HasColumnName("rd_mNo").HasColumnType("int").IsOptional();
            Property(e => e.rd_toDate).HasColumnName("rd_toDate").HasColumnType("datetime").IsOptional();
            Property(e => e.rd_fromDate).HasColumnName("rd_fromDate").HasColumnType("datetime").IsOptional();
            Property(e => e.carSupplierRentID).HasColumnName("carSupplierRentID").HasColumnType("int").IsOptional();
            Property(e => e.carSupplierRentDetailsID).HasColumnName("carSupplierRentDetailsID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.CarSupplierRent).WithMany(e => e.CarSupplierRentDetails)
                .HasForeignKey(e => e.carSupplierRentID).WillCascadeOnDelete(false);
        }
    }
}
