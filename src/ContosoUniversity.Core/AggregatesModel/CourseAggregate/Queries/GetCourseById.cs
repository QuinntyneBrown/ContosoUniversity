using ContosoUniversity.Core.Interfaces;
using ContosoUniversity.SharedKernal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversity.Core
{

    public class GetCourseByIdRequest: IRequest<GetCourseByIdResponse>
    {
        public Guid CourseId { get; set; }
    }
    public class GetCourseByIdResponse: ResponseBase
    {
        public CourseDto Course { get; set; }
    }
    public class GetCourseByIdHandler: IRequestHandler<GetCourseByIdRequest, GetCourseByIdResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<GetCourseByIdHandler> _logger;
    
        public GetCourseByIdHandler(IContosoUniversityDbContext context, ILogger<GetCourseByIdHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetCourseByIdResponse> Handle(GetCourseByIdRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Course = (await _context.Courses.SingleOrDefaultAsync(x => x.CourseId == request.CourseId)).ToDto()
            };
        }
        
    }

}
