using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ContosoUniversity.Core
{
    public static class CourseExtensions
    {
        public static CourseDto ToDto(this Course course)
        {
            return new ()
            {
                CourseId = course.CourseId
            };
        }
        
        public static async Task<List<CourseDto>> ToDtosAsync(this IQueryable<Course> courses, CancellationToken cancellationToken)
        {
            return await courses.Select(x => x.ToDto()).ToListAsync(cancellationToken);
        }
        
    }
}
