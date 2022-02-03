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

    public class RemoveStudentRequest: IRequest<RemoveStudentResponse>
    {
        public Guid StudentId { get; set; }
    }
    public class RemoveStudentResponse: ResponseBase
    {
        public StudentDto Student { get; set; }
    }
    public class RemoveStudentHandler: IRequestHandler<RemoveStudentRequest, RemoveStudentResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<RemoveStudentHandler> _logger;
    
        public RemoveStudentHandler(IContosoUniversityDbContext context, ILogger<RemoveStudentHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<RemoveStudentResponse> Handle(RemoveStudentRequest request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.SingleAsync(x => x.StudentId == request.StudentId);
            
            _context.Students.Remove(student);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Student = student.ToDto()
            };
        }
        
    }

}
