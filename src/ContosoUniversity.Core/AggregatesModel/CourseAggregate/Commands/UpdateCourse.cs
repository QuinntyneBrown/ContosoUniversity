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

    public class UpdateCourseValidator: AbstractValidator<UpdateCourseRequest>
    {
        public UpdateCourseValidator()
        {
            RuleFor(request => request.Course).NotNull();
            RuleFor(request => request.Course).SetValidator(new CourseValidator());
        }
    
    }
    public class UpdateCourseRequest: IRequest<UpdateCourseResponse>
    {
        public CourseDto Course { get; set; }
    }
    public class UpdateCourseResponse: ResponseBase
    {
        public CourseDto Course { get; set; }
    }
    public class UpdateCourseHandler: IRequestHandler<UpdateCourseRequest, UpdateCourseResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<UpdateCourseHandler> _logger;
    
        public UpdateCourseHandler(IContosoUniversityDbContext context, ILogger<UpdateCourseHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<UpdateCourseResponse> Handle(UpdateCourseRequest request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.SingleAsync(x => x.CourseId == request.Course.CourseId);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Course = course.ToDto()
            };
        }
        
    }

}
