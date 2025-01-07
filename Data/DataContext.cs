using Microsoft.EntityFrameworkCore;

namespace bakery.api.Data;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions options) : base(options)
    {
    }
}
