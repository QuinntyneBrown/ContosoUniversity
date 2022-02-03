using ContosoUniversity.Core.Interfaces;
using ContosoUniversity.SharedKernal;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversity.Core
{

    public class RemoveCourseRequest: IRequest<RemoveCourseResponse>
    {
        public Guid CourseId { get; set; }
    }
    public class RemoveCourseResponse: ResponseBase
    {
        public CourseDto Course { get; set; }
    }
    public class RemoveCourseHandler: IRequestHandler<RemoveCourseRequest, RemoveCourseResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<RemoveCourseHandler> _logger;
    
        public RemoveCourseHandler(IContosoUniversityDbContext context, ILogger<RemoveCourseHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<RemoveCourseResponse> Handle(RemoveCourseRequest request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.SingleAsync(x => x.CourseId == request.CourseId);
            
            _context.Courses.Remove(course);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Course = course.ToDto()
            };
        }
        
    }

}
