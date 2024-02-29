using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Witcher.Application.Common.Exceptions;
using Witcher.Application.EnvironmentCategories.Commands.CreateEnvironmentCategory;
using Witcher.Application.EnvironmentCategories.Commands.DeleteEnvironmentCategory;
using Witcher.Application.EnvironmentCategories.Commands.UpdateEnvironmentCategory;
using Witcher.Application.EnvironmentCategories.Queries.GetEnvironmentCategoryDetails;
using Witcher.Application.EnvironmentCategories.Queries.GetEnvironmentCategoryList;
using Witcher.WebApi.Models.EnvironmentCategories;

namespace Witcher.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EnvironmentCategoryController : BaseController
    {
        private readonly IMapper _mapper;

        public EnvironmentCategoryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list EnvironmentCategory
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /environmentCategory
        /// </remarks>
        /// <returns>Returns EnvironmentCategoryListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EnvironmentCategoryListVm>> GetAll()
        {
            var query = new GetEnvironmentCategoryListQuery();

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the environmentCategory by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /environmentCategory/80C4D39D-484C-4A06-AC76-90645BF8DCBE
        /// </remarks>
        /// <param name="id">EnvironmentCategory id(guid)</param>
        /// <returns>Returns EnvironmentCategoryVm</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">if environmentCategory not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EnvironmentCategoryVm>> Get(Guid id)
        {
            EnvironmentCategoryVm vm;
            try
            {
                var query = new GetEnvironmentCategoryDetailsQuery { Id = id };
                vm = await Mediator.Send(query);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok(vm);
        }

        /// <summary>
        /// Creates the environmentCategory
        /// </summary>
        /// <param name="createEnvironmentCategoryDto">CreateEnvironmentCategoryDto object</param>
        /// <returns>Returns environmentCategory id (guid)</returns>
        /// <response code="201">Success</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> Create([FromForm] CreateEnvironmentCategoryDto createEnvironmentCategoryDto)
        {
            var command = _mapper.Map<CreateEnvironmentCategoryCommand>(createEnvironmentCategoryDto);

            var environmentCategoryId = await Mediator.Send(command);
            return Ok(environmentCategoryId);
        }

        /// <summary>
        /// Updates the environmentCategory
        /// </summary>
        /// <param name="updateEnvironmentCategoryDto">UpdateEnvironmentCategoryDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] UpdateEnvironmentCategoryDto updateEnvironmentCategoryDto)
        {
            var command = _mapper.Map<UpdateEnvironmentCategoryCommand>(updateEnvironmentCategoryDto);

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the environmentCategory by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /environmentCategory/2B7E86B9-F6B3-4FAC-A17B-A77264D6DAD8
        /// </remarks>
        /// <param name="id">Id of the environmentCategory (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If environmentCategory not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteEnvironmentCategoryCommand { Id = id };
                await Mediator.Send(command);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return NoContent();
        }
    }
}
