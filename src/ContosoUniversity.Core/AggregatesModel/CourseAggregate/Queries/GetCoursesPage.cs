using ContosoUniversity.Core.Interfaces;
using ContosoUniversity.SharedKernal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversity.Core
{

    public class GetCoursesPageRequest: IRequest<GetCoursesPageResponse>
    {
        public int PageSize { get; set; }
        public int Index { get; set; }
    }
    public class GetCoursesPageResponse: ResponseBase
    {
        public int Length { get; set; }
        public List<CourseDto> Entities { get; set; }
    }
    public class GetCoursesPageHandler: IRequestHandler<GetCoursesPageRequest, GetCoursesPageResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<GetCoursesPageHandler> _logger;
    
        public GetCoursesPageHandler(IContosoUniversityDbContext context, ILogger<GetCoursesPageHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetCoursesPageResponse> Handle(GetCoursesPageRequest request, CancellationToken cancellationToken)
        {
            var query = from course in _context.Courses
                select course;
            
            var length = await _context.Courses.CountAsync();
            
            var courses = await query.Page(request.Index, request.PageSize)
                .Select(x => x.ToDto()).ToListAsync();
            
            return new ()
            {
                Length = length,
                Entities = courses
            };
        }
        
    }

}
