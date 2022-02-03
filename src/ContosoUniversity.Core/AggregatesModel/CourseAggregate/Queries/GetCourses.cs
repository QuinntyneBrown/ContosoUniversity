using ContosoUniversity.Core.Interfaces;
using ContosoUniversity.SharedKernal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversity.Core
{

    public class GetCoursesRequest: IRequest<GetCoursesResponse> { }
    public class GetCoursesResponse: ResponseBase
    {
        public List<CourseDto> Courses { get; set; }
    }
    public class GetCoursesHandler: IRequestHandler<GetCoursesRequest, GetCoursesResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<GetCoursesHandler> _logger;
    
        public GetCoursesHandler(IContosoUniversityDbContext context, ILogger<GetCoursesHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetCoursesResponse> Handle(GetCoursesRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Courses = await _context.Courses.ToDtosAsync(cancellationToken)
            };
        }
        
    }

}
