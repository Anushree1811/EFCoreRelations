using EFCoreRelations.Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelations.DB;

public class DemoDbContext:DbContext
{

    public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
    {

    }


    public DbSet<User> User { get; set; }

    public DbSet<Character> Character { get; set; }

    public DbSet<Weapon> Weapon { get; set; }

    public DbSet<Skill> Skill { get; set; }
}
