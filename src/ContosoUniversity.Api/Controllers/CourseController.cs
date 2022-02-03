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
    public class CourseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CourseController> _logger;

        public CourseController(IMediator mediator, ILogger<CourseController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [SwaggerOperation(
            Summary = "Get Course by id.",
            Description = @"Get Course by id."
        )]
        [HttpGet("{courseId:guid}", Name = "getCourseById")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCourseByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCourseByIdResponse>> GetById([FromRoute]Guid courseId, CancellationToken cancellationToken)
        {
            var request = new GetCourseByIdRequest() { CourseId = courseId };
        
            var response = await _mediator.Send(request, cancellationToken);
        
            if (response.Course == null)
            {
                return new NotFoundObjectResult(request.CourseId);
            }
        
            return response;
        }
        
        [SwaggerOperation(
            Summary = "Get Courses.",
            Description = @"Get Courses."
        )]
        [HttpGet(Name = "getCourses")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCoursesResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCoursesResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetCoursesRequest(), cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Create Course.",
            Description = @"Create Course."
        )]
        [HttpPost(Name = "createCourse")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateCourseResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateCourseResponse>> Create([FromBody]CreateCourseRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName}: ({@Command})",
                nameof(CreateCourseRequest),
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Get Course Page.",
            Description = @"Get Course Page."
        )]
        [HttpGet("page/{pageSize}/{index}", Name = "getCoursesPage")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCoursesPageResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCoursesPageResponse>> Page([FromRoute]int pageSize, [FromRoute]int index, CancellationToken cancellationToken)
        {
            var request = new GetCoursesPageRequest { Index = index, PageSize = pageSize };
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Update Course.",
            Description = @"Update Course."
        )]
        [HttpPut(Name = "updateCourse")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateCourseResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateCourseResponse>> Update([FromBody]UpdateCourseRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(UpdateCourseRequest),
                nameof(request.Course.CourseId),
                request.Course.CourseId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Delete Course.",
            Description = @"Delete Course."
        )]
        [HttpDelete("{courseId:guid}", Name = "removeCourse")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveCourseResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveCourseResponse>> Remove([FromRoute]Guid courseId, CancellationToken cancellationToken)
        {
            var request = new RemoveCourseRequest() { CourseId = courseId };
        
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(RemoveCourseRequest),
                nameof(request.CourseId),
                request.CourseId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
    }
}
