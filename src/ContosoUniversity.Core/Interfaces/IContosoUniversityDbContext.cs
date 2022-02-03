using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace ContosoUniversity.Core.Interfaces
{
    public interface IContosoUniversityDbContext
    {
        DbSet<Student> Students { get; }
        DbSet<Course> Courses { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
