using Microsoft.EntityFrameworkCore;

namespace TestB.Entities;

public partial class FormulaTrixSqlServerDbContext : DbContext
{
    public FormulaTrixSqlServerDbContext()
    {
    }

    public FormulaTrixSqlServerDbContext(DbContextOptions<FormulaTrixSqlServerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=te;MultipleActiveResultSets=true;User ID=sa;Password=123456;Pooling=true;Max Pool Size=1024; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey("ItemName");
            entity.ToTable("Item");

            entity.Property(e => e.ItemName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ItemContent).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
