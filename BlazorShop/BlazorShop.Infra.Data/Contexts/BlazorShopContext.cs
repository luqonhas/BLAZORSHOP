using BlazorShop.Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Infra.Data.Contexts
{
    public class BlazorShopContext : DbContext
    {
        public BlazorShopContext(DbContextOptions<BlazorShopContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ItemCart> ItemsCart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();

            #region Users
            // Adding Table Name
            modelBuilder.Entity<User>().ToTable("Users");

            // Adding Id
            modelBuilder.Entity<User>().Property(x => x.Id);

            // Adding Name
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnType("VARCHAR(50)");
            modelBuilder.Entity<User>().Property(x => x.UserName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.UserName).IsRequired();
            
            // Adding Email
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnType("VARCHAR(250)");
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(250);
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();

            // Adding Password
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnType("VARCHAR(60)");
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(60);
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();

            // Adding InsertDate
            modelBuilder.Entity<User>().Property(x => x.InsertDate).HasColumnType("DATETIME");
            modelBuilder.Entity<User>().Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");

            // Adding ModifyDate
            modelBuilder.Entity<User>().Property(x => x.ModifyDate).HasColumnType("DATETIME");
            modelBuilder.Entity<User>().Property(x => x.ModifyDate).HasDefaultValueSql("GETDATE()");
            #endregion



            #region Categories
            // Adding Table Name
            modelBuilder.Entity<Category>().ToTable("Categories");

            // Adding Id
            modelBuilder.Entity<Category>().Property(x => x.Id);

            // Adding Name
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnType("VARCHAR(50)");
            modelBuilder.Entity<Category>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired();

            // Adding IconCSS
            modelBuilder.Entity<Category>().Property(x => x.IconCSS).HasColumnType("VARCHAR(150)");
            modelBuilder.Entity<Category>().Property(x => x.IconCSS).HasMaxLength(150);
            modelBuilder.Entity<Category>().Property(x => x.IconCSS).IsRequired();

            // Adding InsertDate
            modelBuilder.Entity<Category>().Property(x => x.InsertDate).HasColumnType("DATETIME");
            modelBuilder.Entity<Category>().Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");

            // Adding ModifyDate
            modelBuilder.Entity<Category>().Property(x => x.ModifyDate).HasColumnType("DATETIME");
            modelBuilder.Entity<Category>().Property(x => x.ModifyDate).HasDefaultValueSql("GETDATE()");
            #endregion



            #region Products
            // Adding Table Name
            modelBuilder.Entity<Product>().ToTable("Products");

            // Adding Id
            modelBuilder.Entity<Product>().Property(x => x.Id);

            // Adding Category FK
            modelBuilder.Entity<Product>()
                .HasOne<Category>(x => x.Categories)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.IdCategory);

            // Adding Name
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnType("VARCHAR(80)");
            modelBuilder.Entity<Product>().Property(x => x.Name).HasMaxLength(80);
            modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired();

            // Adding Description
            modelBuilder.Entity<Product>().Property(x => x.Description).HasColumnType("VARCHAR(100)");
            modelBuilder.Entity<Product>().Property(x => x.Description).HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(x => x.Description).IsRequired();

            // Adding ImageURL
            modelBuilder.Entity<Product>().Property(x => x.ImageURL).HasColumnType("VARCHAR(255)");
            modelBuilder.Entity<Product>().Property(x => x.ImageURL).HasMaxLength(255);
            modelBuilder.Entity<Product>().Property(x => x.ImageURL).HasDefaultValue("https://firebasestorage.googleapis.com/v0/b/podtv-5700.appspot.com/o/upload-image-icon.jpg?alt=media&token=129b5561-51a8-4c2f-81f0-264dcd2397d1");

            // Adding Price
            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("NUMERIC(8,2)");
            modelBuilder.Entity<Product>().Property(x => x.Price).IsRequired();

            // Adding Quantity
            modelBuilder.Entity<Product>().Property(x => x.Quantity).HasColumnType("INT");
            modelBuilder.Entity<Product>().Property(x => x.Quantity).IsRequired();

            // Adding InsertDate
            modelBuilder.Entity<Product>().Property(x => x.InsertDate).HasColumnType("DATETIME");
            modelBuilder.Entity<Product>().Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");

            // Adding ModifyDate
            modelBuilder.Entity<Product>().Property(x => x.ModifyDate).HasColumnType("DATETIME");
            modelBuilder.Entity<Product>().Property(x => x.ModifyDate).HasDefaultValueSql("GETDATE()");
            #endregion



            #region Carts
            // Adding Table Name
            modelBuilder.Entity<Cart>().ToTable("Carts");

            // Adding Id
            modelBuilder.Entity<Cart>().Property(x => x.Id);

            //Adding Cart FK
            modelBuilder.Entity<Cart>()
                .HasOne<User>(x => x.Users)
                .WithMany(x => x.Carts)
                .HasForeignKey(x => x.IdUser);

            // Adding InsertDate
            modelBuilder.Entity<Cart>().Property(x => x.InsertDate).HasColumnType("DATETIME");
            modelBuilder.Entity<Cart>().Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");

            // Adding ModifyDate
            modelBuilder.Entity<Cart>().Property(x => x.ModifyDate).HasColumnType("DATETIME");
            modelBuilder.Entity<Cart>().Property(x => x.ModifyDate).HasDefaultValueSql("GETDATE()");
            #endregion



            #region ItemsCart
            // Adding Table Name
            modelBuilder.Entity<ItemCart>().ToTable("ItemsCart");

            // Adding Id
            modelBuilder.Entity<ItemCart>().Property(x => x.Id);

            //Adding Cart FK
            modelBuilder.Entity<ItemCart>()
                        .HasOne<Cart>(x => x.Carts)
                        .WithMany(x => x.ItemsCart)
                        .HasForeignKey(x => x.IdCart);

            //Adding Product FK
            modelBuilder.Entity<ItemCart>()
                        .HasOne<Product>(x => x.Products)
                        .WithMany(x => x.ItemsCart)
                        .HasForeignKey(x => x.IdProduct);

            // Adding Quantity
            modelBuilder.Entity<ItemCart>().Property(x => x.Quantity).HasColumnType("INT");
            modelBuilder.Entity<ItemCart>().Property(x => x.Quantity).IsRequired();

            // Adding InsertDate
            modelBuilder.Entity<ItemCart>().Property(x => x.InsertDate).HasColumnType("DATETIME");
            modelBuilder.Entity<ItemCart>().Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");

            // Adding ModifyDate
            modelBuilder.Entity<ItemCart>().Property(x => x.ModifyDate).HasColumnType("DATETIME");
            modelBuilder.Entity<ItemCart>().Property(x => x.ModifyDate).HasDefaultValueSql("GETDATE()");
            #endregion


            base.OnModelCreating(modelBuilder);
        }

        // Add-Migration BlazorV1
        // Update-Database
    }
}
