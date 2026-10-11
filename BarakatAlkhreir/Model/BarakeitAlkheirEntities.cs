using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BarakalAlkheir.Model.Mapping;

namespace BarakalAlkheir.Model
{
    [DbConfigurationType(typeof(BarakeitAlkheirDbConfiguration))]
    public partial class BarakeitAlkheirEntities : DbContext
    {
        static BarakeitAlkheirEntities()
        {
            // Program applies versioned migrations before login; forms never initialize schema.
            Database.SetInitializer<BarakeitAlkheirEntities>(null);
        }

        public BarakeitAlkheirEntities() : this("name=BarakeitAlkheirEntities") { }

        public BarakeitAlkheirEntities(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            // Retain the legacy database-enforced validation behavior.
            Configuration.ValidateOnSaveEnabled = false;
        }

        // Preserve the legacy post-save transaction calls. SaveChanges already
        // accepts tracked changes; this has the same explicit EF behavior as before.
        public void AcceptAllChanges()
        {
            ((IObjectContextAdapter)this).ObjectContext.AcceptAllChanges();
        }

        public virtual DbSet<AssignDocumentTransactionTask> AssignDocumentTransactionTasks { get; set; }
        public virtual DbSet<AuthorizationPerson> AuthorizationPersons { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<CarCustomerRent> CarCustomerRents { get; set; }
        public virtual DbSet<carCustomerRentDetail> carCustomerRentDetails { get; set; }
        public virtual DbSet<CarCustomerRentPayment> CarCustomerRentPayments { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarSupllier> CarSuplliers { get; set; }
        public virtual DbSet<CarSupplierRent> CarSupplierRents { get; set; }
        public virtual DbSet<CarSupplierRentDetail> CarSupplierRentDetails { get; set; }
        public virtual DbSet<CarSupplierRentPayment> CarSupplierRentPayments { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyAuthorization> CompanyAuthorizations { get; set; }
        public virtual DbSet<companyGovermentContract> companyGovermentContracts { get; set; }
        public virtual DbSet<CompanyLicence> CompanyLicences { get; set; }
        public virtual DbSet<CompanyLicencesEnd> CompanyLicencesEnds { get; set; }
        public virtual DbSet<CompanyLicencesRemark> CompanyLicencesRemarks { get; set; }
        public virtual DbSet<CompanyLicenseDesignation> CompanyLicenseDesignations { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Dependent> Dependents { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<DocumentDef> DocumentDefs { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentTransaction> DocumentTransactions { get; set; }
        public virtual DbSet<DocumentTransactionStep> DocumentTransactionSteps { get; set; }
        public virtual DbSet<DocumentTransactionTask> DocumentTransactionTasks { get; set; }
        public virtual DbSet<DocumentTransactionTasksPayment> DocumentTransactionTasksPayments { get; set; }
        public virtual DbSet<documentTransTaskDetail> documentTransTaskDetails { get; set; }
        public virtual DbSet<DocumentTypeDefinition> DocumentTypeDefinitions { get; set; }
        public virtual DbSet<DocumnetStore> DocumnetStores { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<EmployeeLicenseShop> EmployeeLicenseShops { get; set; }
        public virtual DbSet<EmployeePassport> EmployeePassports { get; set; }
        public virtual DbSet<EmployeeRabet> EmployeeRabets { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public virtual DbSet<EmployeeSponsor> EmployeeSponsors { get; set; }
        public virtual DbSet<EmployeeStatusDef> EmployeeStatusDefs { get; set; }
        public virtual DbSet<EmployeeWorkContract> EmployeeWorkContracts { get; set; }
        public virtual DbSet<EventsDefinition> EventsDefinitions { get; set; }
        public virtual DbSet<FormEvent> FormEvents { get; set; }
        public virtual DbSet<FormScrean> FormScreans { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Governorate> Governorates { get; set; }
        public virtual DbSet<GovernorateArea> GovernorateAreas { get; set; }
        public virtual DbSet<LicenseActivity> LicenseActivities { get; set; }
        public virtual DbSet<LicensesShop> LicensesShops { get; set; }
        public virtual DbSet<MaritalStatu> MaritalStatus { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<RentLicenseEnd> RentLicenseEnds { get; set; }
        public virtual DbSet<RentLicens> RentLicenses { get; set; }
        public virtual DbSet<RentLicensesDetail> RentLicensesDetails { get; set; }
        public virtual DbSet<RentShop> RentShops { get; set; }
        public virtual DbSet<RentShopEnd> RentShopEnds { get; set; }
        public virtual DbSet<RentShopPayment> RentShopPayments { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TransactionDef> TransactionDefs { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserGroupEvent> UserGroupEvents { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<z_Residency> z_Residency { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AssignDocumentTransactionTaskMap());
            modelBuilder.Configurations.Add(new AuthorizationPersonMap());
            modelBuilder.Configurations.Add(new BankMap());
            modelBuilder.Configurations.Add(new CarCustomerRentMap());
            modelBuilder.Configurations.Add(new carCustomerRentDetailMap());
            modelBuilder.Configurations.Add(new CarCustomerRentPaymentMap());
            modelBuilder.Configurations.Add(new CarMap());
            modelBuilder.Configurations.Add(new CarSupllierMap());
            modelBuilder.Configurations.Add(new CarSupplierRentMap());
            modelBuilder.Configurations.Add(new CarSupplierRentDetailMap());
            modelBuilder.Configurations.Add(new CarSupplierRentPaymentMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyAuthorizationMap());
            modelBuilder.Configurations.Add(new companyGovermentContractMap());
            modelBuilder.Configurations.Add(new CompanyLicenceMap());
            modelBuilder.Configurations.Add(new CompanyLicencesEndMap());
            modelBuilder.Configurations.Add(new CompanyLicencesRemarkMap());
            modelBuilder.Configurations.Add(new CompanyLicenseDesignationMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DependentMap());
            modelBuilder.Configurations.Add(new DesignationMap());
            modelBuilder.Configurations.Add(new DocumentDefMap());
            modelBuilder.Configurations.Add(new DocumentMap());
            modelBuilder.Configurations.Add(new DocumentTransactionMap());
            modelBuilder.Configurations.Add(new DocumentTransactionStepMap());
            modelBuilder.Configurations.Add(new DocumentTransactionTaskMap());
            modelBuilder.Configurations.Add(new DocumentTransactionTasksPaymentMap());
            modelBuilder.Configurations.Add(new documentTransTaskDetailMap());
            modelBuilder.Configurations.Add(new DocumentTypeDefinitionMap());
            modelBuilder.Configurations.Add(new DocumnetStoreMap());
            modelBuilder.Configurations.Add(new EducationMap());
            modelBuilder.Configurations.Add(new EmployeeLicenseShopMap());
            modelBuilder.Configurations.Add(new EmployeePassportMap());
            modelBuilder.Configurations.Add(new EmployeeRabetMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new EmployeeSalaryMap());
            modelBuilder.Configurations.Add(new EmployeeSponsorMap());
            modelBuilder.Configurations.Add(new EmployeeStatusDefMap());
            modelBuilder.Configurations.Add(new EmployeeWorkContractMap());
            modelBuilder.Configurations.Add(new EventsDefinitionMap());
            modelBuilder.Configurations.Add(new FormEventMap());
            modelBuilder.Configurations.Add(new FormScreanMap());
            modelBuilder.Configurations.Add(new GenderMap());
            modelBuilder.Configurations.Add(new GovernorateMap());
            modelBuilder.Configurations.Add(new GovernorateAreaMap());
            modelBuilder.Configurations.Add(new LicenseActivityMap());
            modelBuilder.Configurations.Add(new LicensesShopMap());
            modelBuilder.Configurations.Add(new MaritalStatuMap());
            modelBuilder.Configurations.Add(new NationalityMap());
            modelBuilder.Configurations.Add(new ReligionMap());
            modelBuilder.Configurations.Add(new RentLicenseEndMap());
            modelBuilder.Configurations.Add(new RentLicensMap());
            modelBuilder.Configurations.Add(new RentLicensesDetailMap());
            modelBuilder.Configurations.Add(new RentShopMap());
            modelBuilder.Configurations.Add(new RentShopEndMap());
            modelBuilder.Configurations.Add(new RentShopPaymentMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TransactionDefMap());
            modelBuilder.Configurations.Add(new UserGroupMap());
            modelBuilder.Configurations.Add(new UserGroupEventMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new z_ResidencyMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
