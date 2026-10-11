using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BarakalAlkheir.Model.Mapping
{
    internal sealed class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        internal EmployeeMap()
        {
            ToTable("Employees", "dbo");
            HasKey(e => e.employeeID);
            Property(e => e.editedBy).HasColumnName("editedBy").HasColumnType("int").IsOptional();
            Property(e => e.addedBy).HasColumnName("addedBy").HasColumnType("int").IsOptional();
            Property(e => e.oldoldGoveContract).HasColumnName("oldoldGoveContract").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.oldoldLicenceNO).HasColumnName("oldoldLicenceNO").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.oldoldFileNo).HasColumnName("oldoldFileNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.oldoldCommercialName).HasColumnName("oldoldCommercialName").HasColumnType("nvarchar").IsOptional().HasMaxLength(100).IsUnicode();
            Property(e => e.oldoldAuthName).HasColumnName("oldoldAuthName").HasColumnType("nvarchar").IsOptional().HasMaxLength(100).IsUnicode();
            Property(e => e.oldoladOwnerName).HasColumnName("oldoladOwnerName").HasColumnType("nvarchar").IsOptional().HasMaxLength(100).IsUnicode();
            Property(e => e.emp_OldDesignation).HasColumnName("emp_OldDesignation").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.emp_OldNoOfYears).HasColumnName("emp_OldNoOfYears").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.emp_OldSalary).HasColumnName("emp_OldSalary").HasColumnType("nvarchar").IsOptional().HasMaxLength(10).IsUnicode();
            Property(e => e.emp_type).HasColumnName("emp_type").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.emp_res28).HasColumnName("emp_res28").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res27).HasColumnName("emp_res27").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res26).HasColumnName("emp_res26").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res25).HasColumnName("emp_res25").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res24).HasColumnName("emp_res24").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res23).HasColumnName("emp_res23").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res22).HasColumnName("emp_res22").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res21).HasColumnName("emp_res21").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res20).HasColumnName("emp_res20").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res19).HasColumnName("emp_res19").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res18).HasColumnName("emp_res18").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res17).HasColumnName("emp_res17").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res16).HasColumnName("emp_res16").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res15).HasColumnName("emp_res15").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res14).HasColumnName("emp_res14").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res13).HasColumnName("emp_res13").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res12).HasColumnName("emp_res12").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res11).HasColumnName("emp_res11").HasColumnType("nvarchar").IsOptional().HasMaxLength(3).IsUnicode();
            Property(e => e.emp_res10).HasColumnName("emp_res10").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_res9).HasColumnName("emp_res9").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_res8).HasColumnName("emp_res8").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_res7).HasColumnName("emp_res7").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_res6).HasColumnName("emp_res6").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_res5).HasColumnName("emp_res5").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_res4).HasColumnName("emp_res4").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_res3).HasColumnName("emp_res3").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_res2).HasColumnName("emp_res2").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_res1).HasColumnName("emp_res1").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_appType).HasColumnName("emp_appType").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.emp_appDate).HasColumnName("emp_appDate").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.edited_Date).HasColumnName("edited_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.added_Date).HasColumnName("added_Date").HasColumnType("datetime").IsOptional();
            Property(e => e.fulladdress).HasColumnName("fulladdress").HasColumnType("nvarchar").IsOptional().HasMaxLength(250).IsUnicode();
            Property(e => e.phoneNo).HasColumnName("phoneNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.homeNo).HasColumnName("homeNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.email).HasColumnName("email").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.poboxOffice).HasColumnName("poboxOffice").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.pobox).HasColumnName("pobox").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.appartment).HasColumnName("appartment").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.floor).HasColumnName("floor").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.BuilduingType).HasColumnName("BuilduingType").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.Jada).HasColumnName("Jada").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.Share3).HasColumnName("Share3").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.Quet3a).HasColumnName("Quet3a").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.Mohafaza).HasColumnName("Mohafaza").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.companyID).HasColumnName("companyID").HasColumnType("int").IsOptional();
            Property(e => e.employeeStatusID).HasColumnName("employeeStatusID").HasColumnType("int").IsOptional();
            Property(e => e.emp_isMandoub).HasColumnName("emp_isMandoub").HasColumnType("bit").IsOptional();
            Property(e => e.emp_active).HasColumnName("emp_active").HasColumnType("bit").IsOptional();
            Property(e => e.emp_bloodType).HasColumnName("emp_bloodType").HasColumnType("nvarchar").IsOptional().HasMaxLength(15).IsUnicode();
            Property(e => e.departmentID).HasColumnName("departmentID").HasColumnType("int").IsOptional();
            Property(e => e.maritalID).HasColumnName("maritalID").HasColumnType("int").IsOptional();
            Property(e => e.educationID).HasColumnName("educationID").HasColumnType("int").IsOptional();
            Property(e => e.religionID).HasColumnName("religionID").HasColumnType("int").IsOptional();
            Property(e => e.nationalityID).HasColumnName("nationalityID").HasColumnType("int").IsOptional();
            Property(e => e.genderID).HasColumnName("genderID").HasColumnType("int").IsOptional();
            Property(e => e.nationality_placeOfBirthID).HasColumnName("nationality_placeOfBirthID").HasColumnType("int").IsOptional();
            Property(e => e.emp_year).HasColumnName("emp_year").HasColumnType("nvarchar").IsOptional().HasMaxLength(4).IsUnicode();
            Property(e => e.emp_month).HasColumnName("emp_month").HasColumnType("nvarchar").IsOptional().HasMaxLength(2).IsUnicode();
            Property(e => e.emp_day).HasColumnName("emp_day").HasColumnType("nvarchar").IsOptional().HasMaxLength(2).IsUnicode();
            Property(e => e.emp_namefullen).HasColumnName("emp_namefullen").HasColumnType("nvarchar").IsOptional().HasMaxLength(75).IsUnicode();
            Property(e => e.emp_namefullar).HasColumnName("emp_namefullar").HasColumnType("nvarchar").IsOptional().HasMaxLength(75).IsUnicode();
            Property(e => e.emp_namee5).HasColumnName("emp_namee5").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.emp_namee4).HasColumnName("emp_namee4").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.emp_namee3).HasColumnName("emp_namee3").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.emp_namee2).HasColumnName("emp_namee2").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.emp_namee1).HasColumnName("emp_namee1").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.emp_namea5).HasColumnName("emp_namea5").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.emp_namea4).HasColumnName("emp_namea4").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.emp_namea3).HasColumnName("emp_namea3").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.emp_namea2).HasColumnName("emp_namea2").HasColumnType("nvarchar").IsOptional().HasMaxLength(30).IsUnicode();
            Property(e => e.emp_namea1).HasColumnName("emp_namea1").HasColumnType("nvarchar").IsOptional().HasMaxLength(50).IsUnicode();
            Property(e => e.emp_civilID).HasColumnName("emp_civilID").HasColumnType("nvarchar").IsOptional().HasMaxLength(20).IsUnicode();
            Property(e => e.emp_fileNo).HasColumnName("emp_fileNo").HasColumnType("nvarchar").IsOptional().HasMaxLength(10).IsUnicode();
            Property(e => e.emp_No).HasColumnName("emp_No").HasColumnType("nvarchar").IsOptional().HasMaxLength(10).IsUnicode();
            Property(e => e.employeeID).HasColumnName("employeeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(e => e.Education).WithMany(e => e.Employees)
                .HasForeignKey(e => e.educationID).WillCascadeOnDelete(false);

            HasOptional(e => e.EmployeeStatusDef).WithMany(e => e.Employees)
                .HasForeignKey(e => e.employeeStatusID).WillCascadeOnDelete(false);

            HasOptional(e => e.Gender).WithMany(e => e.Employees)
                .HasForeignKey(e => e.genderID).WillCascadeOnDelete(false);

            HasOptional(e => e.MaritalStatu).WithMany(e => e.Employees)
                .HasForeignKey(e => e.maritalID).WillCascadeOnDelete(false);

            HasOptional(e => e.Nationality).WithMany(e => e.Employees)
                .HasForeignKey(e => e.nationalityID).WillCascadeOnDelete(false);

            HasOptional(e => e.Nationality1).WithMany(e => e.Employees1)
                .HasForeignKey(e => e.nationality_placeOfBirthID).WillCascadeOnDelete(false);

            HasOptional(e => e.Religion).WithMany(e => e.Employees)
                .HasForeignKey(e => e.religionID).WillCascadeOnDelete(false);
        }
    }
}
