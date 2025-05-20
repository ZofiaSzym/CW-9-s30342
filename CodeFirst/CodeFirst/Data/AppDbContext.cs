using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Data;

public class  AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}