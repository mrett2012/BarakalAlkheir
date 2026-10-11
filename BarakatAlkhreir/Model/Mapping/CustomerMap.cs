using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class CustomerMap : EntityTypeConfiguration<Customer>
    {
        internal CustomerMap()
        {
            ToTable("Customers", "dbo");
            HasKey(e => e.customerID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.customer_active).HasColumnName("customer_active").HasColumnType("bit").IsOptional();
            Property(e => e.customer_Address).HasColumnName("customer_Address").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.customer_phoneNumber2).HasColumnName("customer_phoneNumber2").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.customer_phoneNumber1).HasColumnName("customer_phoneNumber1").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.customer_BirthDate).HasColumnName("customer_BirthDate").HasColumnType("datetime").IsOptional();
            Property(e => e.customer_nationalityID).HasColumnName("customer_nationalityID").HasColumnType("int").IsOptional();
            Property(e => e.customer_CivilID).HasColumnName("customer_CivilID").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.customer_Name).HasColumnName("customer_Name").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.customerID).HasColumnName("customerID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Nationality).WithMany(e => e.Customers)
                .HasForeignKey(e => e.customer_nationalityID).WillCascadeOnDelete(false);
        }
    }
}
