using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class carCustomerRentDetailMap : EntityTypeConfiguration<carCustomerRentDetail>
    {
        internal carCustomerRentDetailMap()
        {
            ToTable("carCustomerRentDetails", "dbo");
            HasKey(e => e.carCustomerRentDetailsID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.rd_isPaid).HasColumnName("rd_isPaid").HasColumnType("bit").IsOptional();
            Property(e => e.rd_amount).HasColumnName("rd_amount").HasColumnType("real").IsOptional();
            Property(e => e.rd_mNo).HasColumnName("rd_mNo").HasColumnType("int").IsOptional();
            Property(e => e.rd_toDate).HasColumnName("rd_toDate").HasColumnType("datetime").IsOptional();
            Property(e => e.rd_fromDate).HasColumnName("rd_fromDate").HasColumnType("datetime").IsOptional();
            Property(e => e.carCustomerRentID).HasColumnName("carCustomerRentID").HasColumnType("int").IsOptional();
            Property(e => e.carCustomerRentDetailsID).HasColumnName("carCustomerRentDetailsID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.CarCustomerRent).WithMany(e => e.carCustomerRentDetails)
                .HasForeignKey(e => e.carCustomerRentID).WillCascadeOnDelete(false);
        }
    }
}
