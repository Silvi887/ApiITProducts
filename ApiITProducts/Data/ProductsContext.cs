using ApiITProducts.Datasets;
using ApiITProducts.Models;
using Microsoft.EntityFrameworkCore;
using ApiITProducts.Datasets;

namespace ApiITProducts.Data
{
    public class ProductsContext :DbContext
    {

        public ProductsContext(DbContextOptions options)
         : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CategoryProduct> Categoriesproducts { get; set; }

        public DbSet<Sale> Sales { get; set; }

        internal DbSet<CatMaxPriceDto> CatMaxPricesDto { get; set; }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        object value = optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CatMaxPriceDto>().HasNoKey();

            modelBuilder.Entity<CategoryProduct>(entity =>

            {
                entity.HasKey(cp => new { cp.Productid, cp.Categoryid });




            });

            modelBuilder.Entity<User>(entity =>

            {
                entity.HasKey(u => u.Id);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasMany(p => p.CategoriesProducts)
                .WithOne(c => c.Product)
                .HasForeignKey(p => p.Productid);

            });
            modelBuilder.Entity<CategoryProduct>(entity =>

            {
                entity.HasKey(cp => new { cp.Productid, cp.Categoryid });
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasMany(p => p.CategoriesProducts)
                .WithOne(c => c.Category)
                .HasForeignKey(p => p.Categoryid);

            }
              );


            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasOne(s => s.Product)
               .WithMany(p => p.Sales)
               .HasForeignKey(s => s.ProductId);

                entity.HasOne(s => s.User)
               .WithMany(u => u.Sales)
              .HasForeignKey(s => s.UserId);
            });

        }
    }
    }
