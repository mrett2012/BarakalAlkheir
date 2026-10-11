using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CarCustomerRentMap : EntityTypeConfiguration<CarCustomerRent>
    {
        internal CarCustomerRentMap()
        {
            ToTable("CarCustomerRent", "dbo");
            HasKey(e => e.carCustomerRentID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.carCustomerRent_active).HasColumnName("carCustomerRent_active").HasColumnType("bit").IsOptional();
            Property(e => e.carCustomerRent_Remarks).HasColumnName("carCustomerRent_Remarks").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.carCustomerRent_DriverName).HasColumnName("carCustomerRent_DriverName").HasColumnType("nvarchar").IsOptional().HasMaxLength(100).IsUnicode();
            Property(e => e.carCustomerRent_DriverID).HasColumnName("carCustomerRent_DriverID").HasColumnType("int").IsOptional();
            Property(e => e.carCustomerRent_DamanAmount).HasColumnName("carCustomerRent_DamanAmount").HasColumnType("real").IsOptional();
            Property(e => e.carCustomerRent_RentalAmount).HasColumnName("carCustomerRent_RentalAmount").HasColumnType("real").IsOptional();
            Property(e => e.carCustomer_to).HasColumnName("carCustomer_to").HasColumnType("datetime").IsOptional();
            Property(e => e.carCustomer_noOfMonths).HasColumnName("carCustomer_noOfMonths").HasColumnType("int").IsOptional();
            Property(e => e.carCustomer_from).HasColumnName("carCustomer_from").HasColumnType("datetime").IsOptional();
            Property(e => e.carCustomer_Date).HasColumnName("carCustomer_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.carCustomer_rentNo).HasColumnName("carCustomer_rentNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.customerID).HasColumnName("customerID").HasColumnType("int").IsOptional();
            Property(e => e.carID).HasColumnName("carID").HasColumnType("int").IsOptional();
            Property(e => e.carCustomerRentID).HasColumnName("carCustomerRentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Car).WithMany(e => e.CarCustomerRents)
                .HasForeignKey(e => e.carID).WillCascadeOnDelete(false);

            HasOptional(e => e.Customer).WithMany(e => e.CarCustomerRents)
                .HasForeignKey(e => e.customerID).WillCascadeOnDelete(false);
        }
    }
}
