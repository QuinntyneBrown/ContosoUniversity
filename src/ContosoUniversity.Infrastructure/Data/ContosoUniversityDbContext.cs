using ContosoUniversity.Core;
using ContosoUniversity.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace ContosoUniversity.Infrastructure.Data
{
    public class ContosoUniversityDbContext: DbContext, IContosoUniversityDbContext
    {
        public DbSet<Student> Students { get; private set; }
        public ContosoUniversityDbContext(DbContextOptions options)
            :base(options) { }

    }
}
