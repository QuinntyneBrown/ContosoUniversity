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

    public class UpdateStudentValidator: AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentValidator()
        {
            RuleFor(request => request.Student).NotNull();
            RuleFor(request => request.Student).SetValidator(new StudentValidator());
        }
    
    }
    public class UpdateStudentRequest: IRequest<UpdateStudentResponse>
    {
        public StudentDto Student { get; set; }
    }
    public class UpdateStudentResponse: ResponseBase
    {
        public StudentDto Student { get; set; }
    }
    public class UpdateStudentHandler: IRequestHandler<UpdateStudentRequest, UpdateStudentResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<UpdateStudentHandler> _logger;
    
        public UpdateStudentHandler(IContosoUniversityDbContext context, ILogger<UpdateStudentHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<UpdateStudentResponse> Handle(UpdateStudentRequest request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.SingleAsync(x => x.StudentId == request.Student.StudentId);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Student = student.ToDto()
            };
        }
        
    }

}
