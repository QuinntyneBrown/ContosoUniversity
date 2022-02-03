using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ContosoUniversity.Core;
using MediatR;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace ContosoUniversity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class StudentController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IMediator mediator, ILogger<StudentController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [SwaggerOperation(
            Summary = "Get Student by id.",
            Description = @"Get Student by id."
        )]
        [HttpGet("{studentId:guid}", Name = "getStudentById")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetStudentByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetStudentByIdResponse>> GetById([FromRoute]Guid studentId, CancellationToken cancellationToken)
        {
            var request = new GetStudentByIdRequest() { StudentId = studentId };
        
            var response = await _mediator.Send(request, cancellationToken);
        
            if (response.Student == null)
            {
                return new NotFoundObjectResult(request.StudentId);
            }
        
            return response;
        }
        
        [SwaggerOperation(
            Summary = "Get Students.",
            Description = @"Get Students."
        )]
        [HttpGet(Name = "getStudents")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetStudentsResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetStudentsResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetStudentsRequest(), cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Create Student.",
            Description = @"Create Student."
        )]
        [HttpPost(Name = "createStudent")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateStudentResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateStudentResponse>> Create([FromBody]CreateStudentRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName}: ({@Command})",
                nameof(CreateStudentRequest),
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Get Student Page.",
            Description = @"Get Student Page."
        )]
        [HttpGet("page/{pageSize}/{index}", Name = "getStudentsPage")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetStudentsPageResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetStudentsPageResponse>> Page([FromRoute]int pageSize, [FromRoute]int index, CancellationToken cancellationToken)
        {
            var request = new GetStudentsPageRequest { Index = index, PageSize = pageSize };
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Update Student.",
            Description = @"Update Student."
        )]
        [HttpPut(Name = "updateStudent")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateStudentResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateStudentResponse>> Update([FromBody]UpdateStudentRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(UpdateStudentRequest),
                nameof(request.Student.StudentId),
                request.Student.StudentId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Delete Student.",
            Description = @"Delete Student."
        )]
        [HttpDelete("{studentId:guid}", Name = "removeStudent")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveStudentResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveStudentResponse>> Remove([FromRoute]Guid studentId, CancellationToken cancellationToken)
        {
            var request = new RemoveStudentRequest() { StudentId = studentId };
        
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(RemoveStudentRequest),
                nameof(request.StudentId),
                request.StudentId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
    }
}
