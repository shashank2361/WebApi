using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL.Models
{
    public partial class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext()
        {
        }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetSqlCacheTablesForChangeNotification> AspNetSqlCacheTablesForChangeNotifications { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<TblCity> TblCities { get; set; }
        public virtual DbSet<TblCity1> TblCities1 { get; set; }
        public virtual DbSet<TblContinent> TblContinents { get; set; }
        public virtual DbSet<TblCountry> TblCountries { get; set; }
        public virtual DbSet<TblCountry1> TblCountries1 { get; set; }
        public virtual DbSet<TblEmployee> TblEmployees { get; set; }
        public virtual DbSet<TblImage> TblImages { get; set; }
        public virtual DbSet<TblLog> TblLogs { get; set; }
        public virtual DbSet<TblMenuItemsLevel1> TblMenuItemsLevel1s { get; set; }
        public virtual DbSet<TblMenuItemsLevel2> TblMenuItemsLevel2s { get; set; }
        public virtual DbSet<TblProduct> TblProducts { get; set; }
        public virtual DbSet<TblProduct1> TblProducts1 { get; set; }
        public virtual DbSet<TblRate> TblRates { get; set; }
        public virtual DbSet<TblResetPasswordRequest> TblResetPasswordRequests { get; set; }
        public virtual DbSet<TblStudent> TblStudents { get; set; }
        public virtual DbSet<TblTreeViewItem> TblTreeViewItems { get; set; }
        public virtual DbSet<Tbluser> Tblusers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=EmployeeDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.Name, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetSqlCacheTablesForChangeNotification>(entity =>
            {
                entity.HasKey(e => e.TableName)
                    .HasName("PK__AspNet_S__93F7AC69C912B55A");

                entity.ToTable("AspNet_SqlCacheTablesForChangeNotification");

                entity.Property(e => e.TableName).HasColumnName("tableName");

                entity.Property(e => e.ChangeId).HasColumnName("changeId");

                entity.Property(e => e.NotificationCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("notificationCreated")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.UserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.HasIndex(e => e.RoleId, "IX_RoleId");

                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DateOfJoining).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<TblCity>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__tblCitie__F2D21B7694288B5B");

                entity.ToTable("tblCities");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TblCities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__tblCities__Count__74AE54BC");
            });

            modelBuilder.Entity<TblCity1>(entity =>
            {
                entity.ToTable("tblCity");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TblCity1s)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__tblCity__Country__5FB337D6");
            });

            modelBuilder.Entity<TblContinent>(entity =>
            {
                entity.HasKey(e => e.ContinentId)
                    .HasName("PK__tblConti__7E5220E1B2B31A34");

                entity.ToTable("tblContinents");

                entity.Property(e => e.ContinentName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__tblCount__10D1609F6916E2C8");

                entity.ToTable("tblCountries");

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.HasOne(d => d.Continent)
                    .WithMany(p => p.TblCountries)
                    .HasForeignKey(d => d.ContinentId)
                    .HasConstraintName("FK__tblCountr__Conti__71D1E811");
            });

            modelBuilder.Entity<TblCountry1>(entity =>
            {
                entity.ToTable("tblCountry");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__tblEmplo__7AD04F11DCEF0CD7");

                entity.ToTable("tblEmployee");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TblImage>(entity =>
            {
                entity.ToTable("tblImages");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<TblLog>(entity =>
            {
                entity.ToTable("tblLog");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMenuItemsLevel1>(entity =>
            {
                entity.ToTable("tblMenuItemsLevel1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MenuText).HasMaxLength(50);

                entity.Property(e => e.NavigateUrl)
                    .HasMaxLength(50)
                    .HasColumnName("NavigateURL");
            });

            modelBuilder.Entity<TblMenuItemsLevel2>(entity =>
            {
                entity.ToTable("tblMenuItemsLevel2");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MenuText).HasMaxLength(50);

                entity.Property(e => e.NavigateUrl)
                    .HasMaxLength(50)
                    .HasColumnName("NavigateURL");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.TblMenuItemsLevel2s)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__tblMenuIt__Paren__2739D489");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.ProdcutId)
                    .HasName("PK__tblProdu__F5DFFBFDC76DF6A5");

                entity.ToTable("tblProduct");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TblProduct1>(entity =>
            {
                entity.ToTable("tblProducts");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ProductDescription).HasMaxLength(100);
            });

            modelBuilder.Entity<TblRate>(entity =>
            {
                entity.ToTable("tblRates");

                entity.Property(e => e.Currency).HasMaxLength(5);

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<TblResetPasswordRequest>(entity =>
            {
                entity.ToTable("tblResetPasswordRequests");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ResetRequestDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblResetPasswordRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__tblResetP__UserI__0A9D95DB");
            });

            modelBuilder.Entity<TblStudent>(entity =>
            {
                entity.ToTable("tblStudents");

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TblTreeViewItem>(entity =>
            {
                entity.ToTable("tblTreeViewItems");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NavigateUrl)
                    .HasMaxLength(50)
                    .HasColumnName("NavigateURL");

                entity.Property(e => e.TreeViewText).HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__tblTreeVi__Paren__2B0A656D");
            });

            modelBuilder.Entity<Tbluser>(entity =>
            {
                entity.ToTable("tblusers");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.LockedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Username).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
