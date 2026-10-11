using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class RentLicensMap : EntityTypeConfiguration<RentLicens>
    {
        internal RentLicensMap()
        {
            ToTable("RentLicenses", "dbo");
            HasKey(e => e.rentLicenseID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.rentLicense_NoOfSeats).HasColumnName("rentLicense_NoOfSeats").HasColumnType("int").IsOptional();
            Property(e => e.rentLicense_active).HasColumnName("rentLicense_active").HasColumnType("bit").IsOptional();
            Property(e => e.rentLicense_DamanAmount).HasColumnName("rentLicense_DamanAmount").HasColumnType("real").IsOptional();
            Property(e => e.rentLicense_RentalAmount).HasColumnName("rentLicense_RentalAmount").HasColumnType("real").IsOptional();
            Property(e => e.rentLicense_to).HasColumnName("rentLicense_to").HasColumnType("datetime").IsOptional();
            Property(e => e.rentLicense_from).HasColumnName("rentLicense_from").HasColumnType("datetime").IsOptional();
            Property(e => e.rentLicense_noOfMonths).HasColumnName("rentLicense_noOfMonths").HasColumnType("int").IsOptional();
            Property(e => e.rentLicense_date).HasColumnName("rentLicense_date").HasColumnType("datetime").IsOptional();
            Property(e => e.licencesShopID).HasColumnName("licencesShopID").HasColumnType("int").IsOptional();
            Property(e => e.companyLicencesID).HasColumnName("companyLicencesID").HasColumnType("int").IsOptional();
            Property(e => e.customerID).HasColumnName("customerID").HasColumnType("int").IsOptional();
            Property(e => e.rentLicenseNo).HasColumnName("rentLicenseNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.rentLicenseID).HasColumnName("rentLicenseID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.CompanyLicence).WithMany(e => e.RentLicenses)
                .HasForeignKey(e => e.companyLicencesID).WillCascadeOnDelete(false);

            HasOptional(e => e.Customer).WithMany(e => e.RentLicenses)
                .HasForeignKey(e => e.customerID).WillCascadeOnDelete(false);
        }
    }
}
