using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Models;

namespace Project.DAL.Data;

public class APIContext:IdentityDbContext<User>
{
    public APIContext(DbContextOptions option) : base(option) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CategoriesImages>()
            .HasIndex(sci => sci.imageURL)
            .IsUnique();

        modelBuilder.Entity<SubCategoryImages>()
            .HasIndex(sci => sci.imageURL)
            .IsUnique();
    }

    public DbSet<Models.Attributes> attributes { get; set; }
    public DbSet<VariantGroupAttributes> variantGroupAttributes { get; set; }
    public DbSet<Brand> brands { get; set; }
    public DbSet<Cart> carts { get; set; }
    public DbSet<CartProducts> cartProducts { get; set; }
    public DbSet<CategoriesImages> categoriesImages { get; set; }
    public DbSet<Category> category { get; set; }
    public DbSet<EAVProducts> EAVProducts { get; set; }
    public DbSet<PTA> PTA { get; set; }
    public DbSet<Order> order { get; set; }
    public DbSet<OrderItem> orderItems { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<ProductsImages> productsImages { get; set; }
    public DbSet<Rating> rating { get; set; }
    public DbSet<SubCategory> subCategories { get; set; }
    public DbSet<SubCategoryImages> subCategoryImages { get; set; }
    public DbSet<User> user { get; set; }
    public DbSet<Values> values { get; set; }
    public DbSet<VariantGroup> variantGroups { get; set; }
    public DbSet<WishList> WishList { get; set; }
}
