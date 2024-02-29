using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Witcher.Application.Builds.Commands.CreateBuild;
using Witcher.Application.Builds.Commands.DeleteBuild;
using Witcher.Application.Builds.Commands.UpdateBuild;
using Witcher.Application.Builds.Queries.GetBuildDetails;
using Witcher.Application.Builds.Queries.GetBuildList;
using Witcher.Application.Common.Exceptions;
using Witcher.WebApi.Models.Builds;

namespace Witcher.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BuildController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly Uploader _uploader;

        public BuildController(IMapper mapper, Uploader uploader)
        {
            _mapper = mapper;
            _uploader = uploader;
        }

        /// <summary>
        /// Gets the list builds
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /build
        /// </remarks>
        /// <returns>Returns BuildListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BuildListVm>> GetAll()
        {
            var query = new GetBuildListQuery();

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the build by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /build/80C4D39D-484C-4A06-AC76-90645BF8DCBE
        /// </remarks>
        /// <param name="id">Build id(guid)</param>
        /// <returns>Returns BuildVm</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">if build not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BuildVm>> Get(Guid id)
        {
            BuildVm vm;
            try
            {
                var query = new GetBuildDetailsQuery { Id = id };
                vm = await Mediator.Send(query);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(vm);
        }

        /// <summary>
        /// Creates the build
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /note
        /// {
        ///     name: "build name"(NOT NULL),
        ///     text: "build text"(NULL),
        ///     imageFile: "build image file"(NULL)
        /// }
        /// </remarks>
        /// <param name="createBuildDto">CreateBuildDto object</param>
        /// <returns>Returns build id (guid)</returns>
        /// <response code="201">Success</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> Create([FromForm] CreateBuildDto createBuildDto)
        {
            var filePath = _uploader.UploadFile(createBuildDto.ImageFile, "builds");
            var command = _mapper.Map<CreateBuildCommand>(createBuildDto);

            command.ImageName = filePath?.Split('/').Last();
            command.ImageSource = (filePath != null) ?
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{filePath}"
                : null;

            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

        /// <summary>
        /// Updates the build
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /build
        /// {
        ///     id: "build id"(NOT NULL),
        ///     name: "build name"(NOT NULL),
        ///     text: "build text"(NULL),
        ///     imageFile: "build image file"(NULL)
        /// }
        /// </remarks>
        /// <param name="updateBuildDto">UpdateBuildDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] UpdateBuildDto updateBuildDto)
        {
            var command = _mapper.Map<UpdateBuildCommand>(updateBuildDto);
            var filePath = _uploader.UploadFile(updateBuildDto.ImageFile, "builds");

            command.ImageName = filePath?.Split('/').Last();
            command.ImageSource = (filePath != null) ?
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{filePath}"
                : null;

            var oldFileName = await Mediator.Send(command);

            _uploader.DeleteFile(oldFileName, "builds");
            return NoContent();
        }

        /// <summary>
        /// Deletes the build by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /build/2B7E86B9-F6B3-4FAC-A17B-A77264D6DAD8
        /// </remarks>
        /// <param name="id">Id of the build (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If build not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteBuildCommand { Id = id };
                var oldFileName = await Mediator.Send(command);

                _uploader.DeleteFile(oldFileName, "builds");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }
    }
}
