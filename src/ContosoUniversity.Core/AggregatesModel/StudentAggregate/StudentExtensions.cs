using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ContosoUniversity.Core
{
    public static class StudentExtensions
    {
        public static StudentDto ToDto(this Student student)
        {
            return new ()
            {
                StudentId = student.StudentId
            };
        }
        
        public static async Task<List<StudentDto>> ToDtosAsync(this IQueryable<Student> students, CancellationToken cancellationToken)
        {
            return await students.Select(x => x.ToDto()).ToListAsync(cancellationToken);
        }
        
    }
}
