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

    public class CreateStudentValidator: AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentValidator()
        {
            RuleFor(request => request.Student).NotNull();
            RuleFor(request => request.Student).SetValidator(new StudentValidator());
        }
    
    }
    public class CreateStudentRequest: IRequest<CreateStudentResponse>
    {
        public StudentDto Student { get; set; }
    }
    public class CreateStudentResponse: ResponseBase
    {
        public StudentDto Student { get; set; }
    }
    public class CreateStudentHandler: IRequestHandler<CreateStudentRequest, CreateStudentResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<CreateStudentHandler> _logger;
    
        public CreateStudentHandler(IContosoUniversityDbContext context, ILogger<CreateStudentHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<CreateStudentResponse> Handle(CreateStudentRequest request, CancellationToken cancellationToken)
        {
            var student = new Student();
            
            _context.Students.Add(student);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Student = student.ToDto()
            };
        }
        
    }

}
