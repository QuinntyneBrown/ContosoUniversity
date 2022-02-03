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

    public class GetStudentsPageRequest: IRequest<GetStudentsPageResponse>
    {
        public int PageSize { get; set; }
        public int Index { get; set; }
    }
    public class GetStudentsPageResponse: ResponseBase
    {
        public int Length { get; set; }
        public List<StudentDto> Entities { get; set; }
    }
    public class GetStudentsPageHandler: IRequestHandler<GetStudentsPageRequest, GetStudentsPageResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<GetStudentsPageHandler> _logger;
    
        public GetStudentsPageHandler(IContosoUniversityDbContext context, ILogger<GetStudentsPageHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetStudentsPageResponse> Handle(GetStudentsPageRequest request, CancellationToken cancellationToken)
        {
            var query = from student in _context.Students
                select student;
            
            var length = await _context.Students.CountAsync();
            
            var students = await query.Page(request.Index, request.PageSize)
                .Select(x => x.ToDto()).ToListAsync();
            
            return new ()
            {
                Length = length,
                Entities = students
            };
        }
        
    }

}
