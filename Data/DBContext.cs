using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using API.Models;

namespace API.Data
{
    public partial class DBContext : DbContext
    {
         public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountCategory> AccountCategories { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<ArticleMainCategory> ArticleMainCategories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientCategory> ClientCategories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactCategory> ContactCategories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<PictureCategory> PictureCategories { get; set; }
        public virtual DbSet<PictureMainCategory> PictureMainCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductMainCategory> ProductMainCategories { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffCategory> StaffCategories { get; set; }
        public virtual DbSet<StaffMainCategory> StaffMainCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_ACCOUNT")
                    .IsClustered(false);

                entity.HasOne(d => d.AccountCategory)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountCategoryID)
                    .HasConstraintName("FK_ACCOUNT_ACCOUNTTY_ACCOUNTT");
            });

            modelBuilder.Entity<AccountCategory>(entity =>
            {
                entity.HasKey(e => e.AccountCategoryID)
                    .HasName("PK_ACCOUNTTYPE")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.ArticleID)
                    .HasName("PK_ARTICLE")
                    .IsClustered(false);

                entity.HasOne(d => d.ArticleCategory)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.ArticleCategoryID)
                    .HasConstraintName("FK_ARTICLE_ARTICLECA_ARTICLEC");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ARTICLE_ACCOUNT_A_ACCOUNT");
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.HasKey(e => e.ArticleCategoryID)
                    .HasName("PK_ARTICLECATEGORY")
                    .IsClustered(false);

                entity.HasOne(d => d.ArticleMainCategory)
                    .WithMany(p => p.ArticleCategories)
                    .HasForeignKey(d => d.ArticleMainCategoryID)
                    .HasConstraintName("FK_ARTICLEC_ARTICLEMA_ARTICLEM");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ArticleCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ARTICLEC_ACCOUNT_A_ACCOUNT");
            });

            modelBuilder.Entity<ArticleMainCategory>(entity =>
            {
                entity.HasKey(e => e.ArticleMainCategoryID)
                    .HasName("PK_ARTICLEMAINCATEGORY")
                    .IsClustered(false);

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ArticleMainCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ARTICLEM_ACCOUNT_A_ACCOUNT");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasOne(d => d.ApproveByNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.ApproveBy)
                    .HasConstraintName("FK_Client_Account");

                entity.HasOne(d => d.ClientCategory)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.ClientCategoryID)
                    .HasConstraintName("FK_Client_ClientCategory");
            });

            modelBuilder.Entity<ClientCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ClientCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ClientCategory_Account");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasOne(d => d.ApproveByNavigation)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ApproveBy)
                    .HasConstraintName("FK_Contact_Account");

                entity.HasOne(d => d.ContactCategory)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactCategoryID)
                    .HasConstraintName("FK_Contact_ContactCategory");
            });

            modelBuilder.Entity<ContactCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ContactCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ContactCategory_Account");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientID)
                    .HasConstraintName("FK_Order_Client");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StaffID)
                    .HasConstraintName("FK_Order_Staff");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderID, e.ProductID });

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Product");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.Pictures)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK__Picture__CreateB__33D4B598");

                entity.HasOne(d => d.PictureCategory)
                    .WithMany(p => p.Pictures)
                    .HasForeignKey(d => d.PictureCategoryID)
                    .HasConstraintName("FK__Picture__Picture__32E0915F");
            });

            modelBuilder.Entity<PictureCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.PictureCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK__PictureCa__Creat__300424B4");

                entity.HasOne(d => d.PictureMainCategory)
                    .WithMany(p => p.PictureCategories)
                    .HasForeignKey(d => d.PictureMainCategoryID)
                    .HasConstraintName("FK__PictureCa__Pictu__2F10007B");
            });

            modelBuilder.Entity<PictureMainCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.PictureMainCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK__PictureMa__Creat__2C3393D0");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_Product_Account");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryID)
                    .HasConstraintName("FK_Product_ProductCategory");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ProductCategory_Account");

                entity.HasOne(d => d.ProductMainCategory)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.ProductMainCategoryID)
                    .HasConstraintName("FK_ProductCategory_ProductMainCategory");
            });

            modelBuilder.Entity<ProductMainCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ProductMainCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ProductMainCategory_Account");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_Staff_Account");

                entity.HasOne(d => d.StaffCategory)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.StaffCategoryID)
                    .HasConstraintName("FK_Staff_StaffCategory");
            });

            modelBuilder.Entity<StaffCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.StaffCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_StaffCategory_Account");

                entity.HasOne(d => d.StaffMainCategory)
                    .WithMany(p => p.StaffCategories)
                    .HasForeignKey(d => d.StaffMainCategoryID)
                    .HasConstraintName("FK_StaffCategory_StaffMainCategory");
            });

            modelBuilder.Entity<StaffMainCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.StaffMainCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_StaffMainCategory_Account");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
