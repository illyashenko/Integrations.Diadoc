using Microsoft.EntityFrameworkCore;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt;

public class AptContext : AptContext<AptContext>
{

    public AptContext(DbContextOptions<AptContext> context) : base(context)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
