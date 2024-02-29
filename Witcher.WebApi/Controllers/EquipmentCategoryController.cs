using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Witcher.Application.Common.Exceptions;
using Witcher.Application.EquipmentCategories.Commands.CreateEquipmentCategory;
using Witcher.Application.EquipmentCategories.Commands.DeleteEquipmentCategory;
using Witcher.Application.EquipmentCategories.Commands.UpdateEquipmentCategory;
using Witcher.Application.EquipmentCategories.Queries.GetEquipmentCategoryDetails;
using Witcher.Application.EquipmentCategories.Queries.GetEquipmentCategoryList;
using Witcher.WebApi.Models.EqiupmentCategories;

namespace Witcher.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EquipmentCategoryController : BaseController
    {
        private readonly IMapper _mapper;

        public EquipmentCategoryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list equipmentCategories
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /equipmentCategory
        /// </remarks>
        /// <returns>Returns EquipmentCategoryListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EquipmentCategoryListVm>> GetAll()
        {
            var query = new GetEquipmentCategoryListQuery();

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the equipmentCategory by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /equipmentCategory/80C4D39D-484C-4A06-AC76-90645BF8DCBE
        /// </remarks>
        /// <param name="id">EquipmentCategory id(guid)</param>
        /// <returns>Returns EquipmentCategoryVm</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">if equipmentCategory not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EquipmentCategoryVm>> Get(Guid id)
        {
            EquipmentCategoryVm vm;
            try
            {
                var query = new GetEquipmentCategoryDetailsQuery { Id = id };
                vm = await Mediator.Send(query);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(vm);
        }

        /// <summary>
        /// Creates the equipmentCategory
        /// </summary>
        /// <param name="createEquipmentCategoryDto">CreateEquipmentCategoryDto object</param>
        /// <returns>Returns equipmentCategory id (guid)</returns>
        /// <response code="201">Success</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> Create([FromForm] CreateEquipmentCategoryDto createEquipmentCategoryDto)
        {
            var command = _mapper.Map<CreateEquipmentCategoryCommand>(createEquipmentCategoryDto);

            var equipmentCategoryId = await Mediator.Send(command);
            return Ok(equipmentCategoryId);
        }

        /// <summary>
        /// Updates the equipmentCategory
        /// </summary>
        /// <param name="updateEquipmentCategoryDto">UpdateEquipmentCategoryDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] UpdateEquipmentCategoryDto updateEquipmentCategoryDto)
        {
            var command = _mapper.Map<UpdateEquipmentCategoryCommand>(updateEquipmentCategoryDto);

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the equipmentCategory by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /equipmentCategory/2B7E86B9-F6B3-4FAC-A17B-A77264D6DAD8
        /// </remarks>
        /// <param name="id">Id of the equipmentCategory (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If equipmentCategory not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteEquipmentCategoryCommand { Id = id };
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
