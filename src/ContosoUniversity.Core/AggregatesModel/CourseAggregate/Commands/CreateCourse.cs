using ContosoUniversity.Core.Interfaces;
using ContosoUniversity.SharedKernal;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversity.Core
{

    public class CreateCourseValidator: AbstractValidator<CreateCourseRequest>
    {
        public CreateCourseValidator()
        {
            RuleFor(request => request.Course).NotNull();
            RuleFor(request => request.Course).SetValidator(new CourseValidator());
        }
    
    }
    public class CreateCourseRequest: IRequest<CreateCourseResponse>
    {
        public CourseDto Course { get; set; }
    }
    public class CreateCourseResponse: ResponseBase
    {
        public CourseDto Course { get; set; }
    }
    public class CreateCourseHandler: IRequestHandler<CreateCourseRequest, CreateCourseResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<CreateCourseHandler> _logger;
    
        public CreateCourseHandler(IContosoUniversityDbContext context, ILogger<CreateCourseHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<CreateCourseResponse> Handle(CreateCourseRequest request, CancellationToken cancellationToken)
        {
            var course = new Course();
            
            _context.Courses.Add(course);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Course = course.ToDto()
            };
        }
        
    }

}
