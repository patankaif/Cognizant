using Microsoft.EntityFrameworkCore;

namespace Module5.Data;

public class LazyAppDbContext : AppDbContext
{
    public LazyAppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}
