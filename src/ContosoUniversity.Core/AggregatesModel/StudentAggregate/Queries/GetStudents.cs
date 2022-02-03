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

    public class GetStudentsRequest: IRequest<GetStudentsResponse> { }
    public class GetStudentsResponse: ResponseBase
    {
        public List<StudentDto> Students { get; set; }
    }
    public class GetStudentsHandler: IRequestHandler<GetStudentsRequest, GetStudentsResponse>
    {
        private readonly IContosoUniversityDbContext _context;
        private readonly ILogger<GetStudentsHandler> _logger;
    
        public GetStudentsHandler(IContosoUniversityDbContext context, ILogger<GetStudentsHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetStudentsResponse> Handle(GetStudentsRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Students = await _context.Students.ToDtosAsync(cancellationToken)
            };
        }
        
    }

}
