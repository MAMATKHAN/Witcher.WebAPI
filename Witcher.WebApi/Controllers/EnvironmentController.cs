using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Witcher.Application.Common.Exceptions;
using Witcher.Application.Environments.Commands.CreateEnvironment;
using Witcher.Application.Environments.Commands.DeleteEnvironment;
using Witcher.Application.Environments.Commands.UpdateEnvironment;
using Witcher.Application.Environments.Queries.GetEnvironmentDetails;
using Witcher.Application.Environments.Queries.GetEnvironmentList;
using Witcher.WebApi.Models.Environments;

namespace Witcher.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EnvironmentController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly Uploader _uploader;

        public EnvironmentController(IMapper mapper, Uploader uploader)
        {
            _mapper = mapper;
            _uploader = uploader;
        }

        /// <summary>
        /// Gets the list environments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /environment
        /// </remarks>
        /// <returns>Returns EnvironmentListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnvironmentListVm))]
        public async Task<ActionResult<EnvironmentListVm>> GetAll()
        {
            var query = new GetEnvironmentListQuery();

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the environment by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /environment/80C4D39D-484C-4A06-AC76-90645BF8DCBE
        /// </remarks>
        /// <param name="id">Environment id(guid)</param>
        /// <returns>Returns EnvironmentVm</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">if environment not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EnvironmentVm>> Get(Guid id)
        {
            EnvironmentVm vm;
            try
            {
                var query = new GetEnvironmentDetailsQuery { Id = id };
                vm = await Mediator.Send(query);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok(vm);
        }

        /// <summary>
        /// Creates the environment
        /// </summary>
        /// <param name="createEnvironmentDto">CreateEnvironmentDto object</param>
        /// <returns>Returns environment id (guid)</returns>
        /// <response code="201">Success</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> Create([FromForm] CreateEnvironmentDto createEnvironmentDto)
        {
            var command = _mapper.Map<CreateEnvironmentCommand>(createEnvironmentDto);
            var filePath = _uploader.UploadFile(createEnvironmentDto.ImageFile, "environments");

            command.ImageName = filePath?.Split('/').Last();
            command.ImageSource = (filePath != null)?
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{filePath}"
                : null;

            var environmentId = await Mediator.Send(command);
            return Ok(environmentId);
        }

        /// <summary>
        /// Updates the environment
        /// </summary>
        /// <param name="updateEnvironmentDto">UpdateEnvironmentDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] UpdateEnvironmentDto updateEnvironmentDto)
        {
            var command = _mapper.Map<UpdateEnvironmentCommand>(updateEnvironmentDto);
            var filePath = _uploader.UploadFile(updateEnvironmentDto.ImageFile, "environments");

            command.ImageName = filePath?.Split('/').Last();
            command.ImageSource = (filePath != null)?
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{filePath}"
                : null;

            var oldFileName = await Mediator.Send(command);

            _uploader.DeleteFile(oldFileName, "environments");
            return NoContent();
        }

        /// <summary>
        /// Deletes the environment by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /environment/2B7E86B9-F6B3-4FAC-A17B-A77264D6DAD8
        /// </remarks>
        /// <param name="id">Id of the environment (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If environment not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteEnvironmentCommand { Id = id };
                var oldFileName = await Mediator.Send(command);

                _uploader.DeleteFile(oldFileName, "environments");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return NoContent();
        }
    }
}
