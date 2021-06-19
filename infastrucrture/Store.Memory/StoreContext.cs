using Microsoft.EntityFrameworkCore;

namespace Store.Memory
{
    class StoreContext : DbContext
    {
        public DbSet<Maker> Makers { get; set; }

        public DbSet<Product> Products { get; set; }

        public StoreContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Maker>(
                eb =>
                {
                    eb.HasKey(u => u.Id);
                    eb.ToTable("ProductMaker");

                    eb.Property(v => v.Title).HasColumnName("TitleMaker");
                    eb.Property(v => v.NumberPhone).HasColumnName("NumberPhone");
                    eb.Property(v => v.Email).HasColumnName("Email");
                    eb.Property(v => v.Addres).HasColumnName("AddresOffic");
                    eb.Property(v => v.Description).HasColumnName("Description");
                });

            modelBuilder.Entity<Product>(
                eb =>
                {
                    eb.HasKey(u => u.Id);
                    eb.ToTable("ProductItem");

                    eb.Property(v => v.Title).HasColumnName("NameProduct");
                    eb.Property(v => v.Category).HasColumnName("Categoty");
                    eb.Property(v => v.IdMaker).HasColumnName("Manufacture");
                    eb.Property(v => v.Price).HasColumnName("PriceProduct");
                    eb.Property(v => v.Description).HasColumnName("Description");

                    eb.Ignore(v => v.Manufacture);
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ProductStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
