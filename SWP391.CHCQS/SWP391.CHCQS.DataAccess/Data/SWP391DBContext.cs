using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SWP391.CHCQS.Model;
using Task = SWP391.CHCQS.Model.Task;

namespace SWP391.CHCQS.DataAccess.Data
{
    public partial class SWP391DBContext : DbContext
    {
        public SWP391DBContext()
        {
        }

        public SWP391DBContext(DbContextOptions<SWP391DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<BasementType> BasementTypes { get; set; } = null!;
        public virtual DbSet<ConstructDetail> ConstructDetails { get; set; } = null!;
        public virtual DbSet<ConstructionType> ConstructionTypes { get; set; } = null!;
        public virtual DbSet<CustomQuotaionTask> CustomQuotaionTasks { get; set; } = null!;
        public virtual DbSet<CustomQuotation> CustomQuotations { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<FoundationType> FoundationTypes { get; set; } = null!;
        public virtual DbSet<InvestmentType> InvestmentTypes { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialCategory> MaterialCategories { get; set; } = null!;
        public virtual DbSet<MaterialDetail> MaterialDetails { get; set; } = null!;
        public virtual DbSet<Pricing> Pricings { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<RequestForm> RequestForms { get; set; } = null!;
        public virtual DbSet<RooftopType> RooftopTypes { get; set; } = null!;
        public virtual DbSet<Staff> Staff { get; set; } = null!;
        public virtual DbSet<StandardQuotation> StandardQuotations { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskCategory> TaskCategories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Account__F3DBC5731230846A");

                entity.ToTable("Account");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<BasementType>(entity =>
            {
                entity.ToTable("BasementType");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unitPrice");
            });

            modelBuilder.Entity<ConstructDetail>(entity =>
            {
                entity.HasKey(e => e.QuotationId)
                    .HasName("PK__Construc__7536E3527BF2F7DA");

                entity.ToTable("ConstructDetail");

                entity.Property(e => e.QuotationId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("quotationId")
                    .IsFixedLength();

                entity.Property(e => e.Alley)
                    .HasMaxLength(20)
                    .HasColumnName("alley");

                entity.Property(e => e.Balcony).HasColumnName("balcony");

                entity.Property(e => e.BasementId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("basementId")
                    .IsFixedLength();

                entity.Property(e => e.ConstructionId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("constructionId")
                    .IsFixedLength();

                entity.Property(e => e.Facade).HasColumnName("facade");

                entity.Property(e => e.Floor).HasColumnName("floor");

                entity.Property(e => e.FoundationId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("foundationId")
                    .IsFixedLength();

                entity.Property(e => e.Garden)
                    .HasColumnType("decimal(6, 1)")
                    .HasColumnName("garden");

                entity.Property(e => e.InvestmentId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("investmentId")
                    .IsFixedLength();

                entity.Property(e => e.Length)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("length");

                entity.Property(e => e.Mezzanine)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("mezzanine");

                entity.Property(e => e.RooftopFloor)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("rooftopFloor");

                entity.Property(e => e.RooftopId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("rooftopId")
                    .IsFixedLength();

                entity.Property(e => e.Room).HasColumnName("room");

                entity.Property(e => e.Width)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("width");

                entity.HasOne(d => d.Basement)
                    .WithMany(p => p.ConstructDetails)
                    .HasForeignKey(d => d.BasementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Construct__basem__4D94879B");

                entity.HasOne(d => d.Construction)
                    .WithMany(p => p.ConstructDetails)
                    .HasForeignKey(d => d.ConstructionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Construct__const__4E88ABD4");

                entity.HasOne(d => d.Foundation)
                    .WithMany(p => p.ConstructDetails)
                    .HasForeignKey(d => d.FoundationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Construct__found__4D94879B");

                entity.HasOne(d => d.Investment)
                    .WithMany(p => p.ConstructDetails)
                    .HasForeignKey(d => d.InvestmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Construct__inves__5070F446");

                entity.HasOne(d => d.Quotation)
                    .WithOne(p => p.ConstructDetail)
                    .HasForeignKey<ConstructDetail>(d => d.QuotationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Construct__quota__5165187F");

                entity.HasOne(d => d.Rooftop)
                    .WithMany(p => p.ConstructDetails)
                    .HasForeignKey(d => d.RooftopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Construct__rooft__52593CB8");
            });

            modelBuilder.Entity<ConstructionType>(entity =>
            {
                entity.ToTable("ConstructionType");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CustomQuotaionTask>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.QuotationId })
                    .HasName("PK__CustomQu__EA0E34779FFE6727");

                entity.ToTable("CustomQuotaionTask");

                entity.Property(e => e.TaskId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("taskId")
                    .IsFixedLength();

                entity.Property(e => e.QuotationId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("quotationId")
                    .IsFixedLength();

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.HasOne(d => d.Quotation)
                    .WithMany(p => p.CustomQuotaionTasks)
                    .HasForeignKey(d => d.QuotationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomQuo__quota__5441852A");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.CustomQuotaionTasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomQuo__taskI__534D60F1");
            });

            modelBuilder.Entity<CustomQuotation>(entity =>
            {
                entity.ToTable("CustomQuotation");

                entity.Property(e => e.Id)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Acreage)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("acreage");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.EngineerId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("engineerId")
                    .IsFixedLength();

                entity.Property(e => e.Location)
                    .HasMaxLength(30)
                    .HasColumnName("location");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("managerId")
                    .IsFixedLength();

                entity.Property(e => e.RequestId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("requestId")
                    .IsFixedLength();

                entity.Property(e => e.SellerId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("sellerId")
                    .IsFixedLength();

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.HasOne(d => d.Engineer)
                    .WithMany(p => p.CustomQuotationEngineers)
                    .HasForeignKey(d => d.EngineerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomQuo__engin__5629CD9C");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.CustomQuotationManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomQuo__manag__571DF1D5");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.CustomQuotations)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomQuo__reque__5812160E");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.CustomQuotationSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomQuo__selle__59063A47");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Gender)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNum)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phoneNum");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__userna__534D60F1");
            });

            modelBuilder.Entity<FoundationType>(entity =>
            {
                entity.ToTable("FoundationType");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.AreaRatio)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("areaRatio");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unitPrice");
            });

            modelBuilder.Entity<InvestmentType>(entity =>
            {
                entity.ToTable("InvestmentType");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("categoryId")
                    .IsFixedLength();

                entity.Property(e => e.InventoryQuantity).HasColumnName("inventoryQuantity");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Unit)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("unit");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unitPrice");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Material__catego__5812160E");
            });

            modelBuilder.Entity<MaterialCategory>(entity =>
            {
                entity.ToTable("MaterialCategory");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<MaterialDetail>(entity =>
            {
                entity.HasKey(e => new { e.QuotationId, e.MaterialId })
                    .HasName("PK__Material__BCAD866D29D0C3FC");

                entity.ToTable("MaterialDetail");

                entity.Property(e => e.QuotationId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("quotationId")
                    .IsFixedLength();

                entity.Property(e => e.MaterialId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("materialId")
                    .IsFixedLength();

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.MaterialDetails)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MaterialD__mater__59063A47");

                entity.HasOne(d => d.Quotation)
                    .WithMany(p => p.MaterialDetails)
                    .HasForeignKey(d => d.QuotationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MaterialD__quota__5BE2A6F2");
            });

            modelBuilder.Entity<Pricing>(entity =>
            {
                entity.HasKey(e => new { e.ConstructTypeId, e.InvestmentTypeId })
                    .HasName("PK__Pricing__82221887E948CB0C");

                entity.ToTable("Pricing");

                entity.Property(e => e.ConstructTypeId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ConstructTypeID")
                    .IsFixedLength();

                entity.Property(e => e.InvestmentTypeId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("InvestmentTypeID")
                    .IsFixedLength();

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.ConstructType)
                    .WithMany(p => p.Pricings)
                    .HasForeignKey(d => d.ConstructTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pricing__Constru__5CD6CB2B");

                entity.HasOne(d => d.InvestmentType)
                    .WithMany(p => p.Pricings)
                    .HasForeignKey(d => d.InvestmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pricing__Investm__5DCAEF64");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("customerId")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .HasColumnName("location");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Scale)
                    .HasMaxLength(50)
                    .HasColumnName("scale");

                entity.Property(e => e.Size)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("size");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project__custome__5AEE82B9");
            });

            modelBuilder.Entity<RequestForm>(entity =>
            {
                entity.ToTable("RequestForm");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Acreage)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("acreage");

                entity.Property(e => e.ConstructType)
                    .HasMaxLength(50)
                    .HasColumnName("constructType");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("customerId")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.GenerateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("generateDate");

                entity.Property(e => e.Location)
                    .HasMaxLength(30)
                    .HasColumnName("location");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RequestForms)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RequestFo__custo__5FB337D6");

                entity.HasMany(d => d.Materials)
                    .WithMany(p => p.Requests)
                    .UsingEntity<Dictionary<string, object>>(
                        "RequestFormMaterial",
                        l => l.HasOne<Material>().WithMany().HasForeignKey("MaterialId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__RequestFo__mater__5CD6CB2B"),
                        r => r.HasOne<RequestForm>().WithMany().HasForeignKey("RequestId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__RequestFo__reque__619B8048"),
                        j =>
                        {
                            j.HasKey("RequestId", "MaterialId").HasName("PK__RequestF__2A5EBB0EE35F5BBE");

                            j.ToTable("RequestFormMaterial");

                            j.IndexerProperty<string>("RequestId").HasMaxLength(5).IsUnicode(false).HasColumnName("requestId").IsFixedLength();

                            j.IndexerProperty<string>("MaterialId").HasMaxLength(5).IsUnicode(false).HasColumnName("materialId").IsFixedLength();
                        });
            });

            modelBuilder.Entity<RooftopType>(entity =>
            {
                entity.ToTable("RooftopType");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unitPrice");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Gender)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("managerId")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNum)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phoneNum");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__Staff__managerId__628FA481");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Staff__username__6383C8BA");
            });

            modelBuilder.Entity<StandardQuotation>(entity =>
            {
                entity.ToTable("StandardQuotation");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.ConstructionId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("constructionId")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Construction)
                    .WithMany(p => p.StandardQuotations)
                    .HasForeignKey(d => d.ConstructionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StandardQ__const__6477ECF3");

                entity.HasMany(d => d.Materials)
                    .WithMany(p => p.Quotations)
                    .UsingEntity<Dictionary<string, object>>(
                        "StandardQuotationMaterial",
                        l => l.HasOne<Material>().WithMany().HasForeignKey("MaterialId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__StandardQ__mater__619B8048"),
                        r => r.HasOne<StandardQuotation>().WithMany().HasForeignKey("QuotationId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__StandardQ__quota__66603565"),
                        j =>
                        {
                            j.HasKey("QuotationId", "MaterialId").HasName("PK__Standard__BCAD866D662F5E93");

                            j.ToTable("StandardQuotationMaterial");

                            j.IndexerProperty<string>("QuotationId").HasMaxLength(5).IsUnicode(false).HasColumnName("quotationId").IsFixedLength();

                            j.IndexerProperty<string>("MaterialId").HasMaxLength(5).IsUnicode(false).HasColumnName("materialId").IsFixedLength();
                        });

                entity.HasMany(d => d.Tasks)
                    .WithMany(p => p.Quotations)
                    .UsingEntity<Dictionary<string, object>>(
                        "StandardQuotationTask",
                        l => l.HasOne<Task>().WithMany().HasForeignKey("TaskId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__StandardQ__taskI__6477ECF3"),
                        r => r.HasOne<StandardQuotation>().WithMany().HasForeignKey("QuotationId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__StandardQ__quota__6754599E"),
                        j =>
                        {
                            j.HasKey("QuotationId", "TaskId").HasName("PK__Standard__48E336F665A8BFCB");

                            j.ToTable("StandardQuotationTask");

                            j.IndexerProperty<string>("QuotationId").HasMaxLength(5).IsUnicode(false).HasColumnName("quotationId").IsFixedLength();

                            j.IndexerProperty<string>("TaskId").HasMaxLength(5).IsUnicode(false).HasColumnName("taskId").IsFixedLength();
                        });
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("categoryId")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unitPrice");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Task__categoryId__656C112C");
            });

            modelBuilder.Entity<TaskCategory>(entity =>
            {
                entity.ToTable("TaskCategory");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            //dumpling data

            modelBuilder.Entity<Account>().HasData(
                new Account { Username = "thao123", Password = "1", Role = "customer" },
                new Account { Username = "maitran1", Password = "1", Role = "customer" },
                new Account { Username = "lvm123", Password = "1", Role = "customer" },
                new Account { Username = "ngocanh85", Password = "1", Role = "customer" },
                new Account { Username = "dtuan", Password = "1", Role = "customer" },
                new Account { Username = "datnt", Password = "1", Role = "engineer" },
                new Account { Username = "datnx", Password = "1", Role = "manager" },
                new Account { Username = "duclm", Password = "1", Role = "seller" }
            );
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = "ID001", Name = "Nguyễn Trần Phương Thảo", Username = "thao123" },
                new Customer { Id = "ID002", Name = "Trần Thị Mai", Username = "maitran1" },
                new Customer { Id = "ID003", Name = "Lê Văn Minh", Username = "lvm123" },
                new Customer { Id = "ID004", Name = "Ngọc Anh Nguyễn", Username = "ngocanh85" },
                new Customer { Id = "ID005", Name = "Đỗ Minh Tuấn", Username = "dtuan" }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = "PRJ01",
                    Name = "NHÀ PHỐ CHỊ THẢO TẠI ĐỒNG NAI",
                    CustomerId = "ID001",
                    Description = "Nhà ở gia đình",
                    Location = "Phường Hố Nai, thành phố Biên Hòa, tỉnh Đồng Nai",
                    Scale = "1 trệt, 2 lầu",
                    Size = "5x12",
                    Status = true
                },
                new Project
                {
                    Id = "PRJ02",
                    Name = "NHÀ PHỐ CHỊ MAI",
                    CustomerId = "ID002",
                    Description = "Nhà ở gia đình",
                    Location = "huyện Bến Lức, tỉnh Long An",
                    Scale = "1 trệt, 2 lầu, sân thượng",
                    Size = "5x21",
                    Status = true
                },
                new Project
                {
                    Id = "PRJ03",
                    Name = "NHÀ PHỐ HIỆN ĐẠI 5 TẦNG CỦA ANH MINH",
                    CustomerId = "ID003",
                    Description = "Nhà ở gia đình",
                    Location = "Phường An Phú Đông, Quận 12",
                    Scale = "1 trệt + 1 lửng + 2 lầu + 1 tum, sân thượng",
                    Size = "4.5x18",
                    Status = true
                },
                new Project
                {
                    Id = "PRJ04",
                    Name = "NHÀ CHỊ NGỌC ANH",
                    CustomerId = "ID004",
                    Description = "Nhà ở gia đình",
                    Location = "Phường Hiệp Bình Chánh, TP. Thủ Đức",
                    Scale = "1 trệt + 2 lầu + 1 tum, sân thượng",
                    Size = "4.35x19.5",
                    Status = true
                },
                new Project
                {
                    Id = "PRJ05",
                    Name = "NHÀ 1 TRỆT 3 LẦU ANH TUẤN ",
                    CustomerId = "ID005",
                    Description = "Nhà ở gia đình",
                    Location = "Quận 5, TP. HCM",
                    Scale = "Nhà 1 trệt 3 lầu có sân thượng",
                    Size = "4x17",
                    Status = true
                }
            );

            //Dmpling data for Staff
            modelBuilder.Entity<Staff>().HasData(
                //This one represent a Manager
                new Staff
                {
                    Id = "MG001",
                    Name = "Nguyen Xuan Dat",
                    PhoneNum = "0987654321",
                    Email = "datnx@gmail.com",
                    Gender = "Men",
                    Username = "datnx",
                    ManagerId = null,
                    Status = true
                },

                //This one represent a Seller
                new Staff
                {
                    Id = "SL001",
                    Name = "Le Minh Duc",
                    PhoneNum = "0987654321",
                    Email = "duclm@gmail.com",
                    Gender = "Men",
                    Username = "duclm",
                    ManagerId = "MG001",
                    Status = true
                },

                //This one represent a Engineer
                new Staff
                {
                    Id = "EN001",
                    Name = "Nguyen Thanh Dat",
                    PhoneNum = "0987654321",
                    Email = "datnt@gmail.com",
                    Gender = "Men",
                    Username = "datnt",
                    ManagerId = "MG001",
                    Status = true
                }
            );

            modelBuilder.Entity<RequestForm>().HasData(
                new RequestForm
                {
                    Id = "RF001",
                    GenerateDate = DateTime.Now,
                    Description = "Customer said that this project must be finished on 3 month",
                    ConstructType = "CT2",
                    Acreage = "240m2",
                    Location = "Dĩ An, Bình Dương",
                    Status = true,
                    CustomerId = "ID001",
                }
            );

            //Dumpling data for CustomQuotation
            modelBuilder.Entity<CustomQuotation>().HasData(
                new CustomQuotation
                {
                    Id = "CQ001",
                    Date = DateTime.Now,
                    Acreage = "240m2",
                    Location = "Dĩ An, Bình Dương",
                    Status = true,
                    Description = "I want to build this house for my son and his wife, so i can live with them",
                    Total = 0,
                    SellerId = "SL001",
                    EngineerId = "EN001",
                    ManagerId = "MG001",
                    RequestId = "RF001"
                }

            );;

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}