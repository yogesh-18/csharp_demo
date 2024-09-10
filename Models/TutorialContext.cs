using Microsoft.EntityFrameworkCore;

namespace csharpwebapi.Models
{
    public class TutorialContext : DbContext
    {
        public TutorialContext(DbContextOptions<TutorialContext> options)
            : base(options)
        {
        }

        public DbSet<Tutorial> Tutorials { get; set; } = null!;
    }
}
