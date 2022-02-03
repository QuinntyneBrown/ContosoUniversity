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

    public class GetStudentByIdRequest: IRequest<GetStudentByIdResponse>
    {
        public Guid StudentId { get; set; }
    }
    public class GetStudentByIdResponse: ResponseBase
    {
        public StudentDto Student { get; set; }
    }
    public class GetStudentByIdHandler: IRequestHandler<GetStudentByIdRequest, GetStudentByIdResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<GetStudentByIdHandler> _logger;
    
        public GetStudentByIdHandler(IContosoUniversityDbContext context, ILogger<GetStudentByIdHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetStudentByIdResponse> Handle(GetStudentByIdRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Student = (await _context.Students.SingleOrDefaultAsync(x => x.StudentId == request.StudentId)).ToDto()
            };
        }
        
    }

}
