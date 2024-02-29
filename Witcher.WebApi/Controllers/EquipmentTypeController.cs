using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Witcher.Application.Common.Exceptions;
using Witcher.Application.EquipmentTypes.Commands.CreateEquipmentType;
using Witcher.Application.EquipmentTypes.Commands.DeleteEquipmentType;
using Witcher.Application.EquipmentTypes.Commands.UpdateEquipmentType;
using Witcher.Application.EquipmentTypes.Queries.GetEquipmentTypeDetails;
using Witcher.Application.EquipmentTypes.Queries.GetEquipmentTypeList;
using Witcher.WebApi.Models.EquipmentTypes;

namespace Witcher.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EquipmentTypeController : BaseController
    {
        private readonly IMapper _mapper;

        public Guid Id { get; private set; }

        public EquipmentTypeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list equipmentTypes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /equipmentType
        /// </remarks>
        /// <returns>Returns EquipmentTypeListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EquipmentTypeListVm>> GetAllS()
        {
            var query = new GetEquipmentTypeListQuery();

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the equipmentType by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /equipmentType/80C4D39D-484C-4A06-AC76-90645BF8DCBE
        /// </remarks>
        /// <param name="id">EquipmentType id(guid)</param>
        /// <returns>Returns EquipmentTypeVm</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">if equipmentType not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EquipmentTypeVm>> Get(Guid id)
        {
            EquipmentTypeVm vm;
            try
            {
                var query = new GetEquipmentTypeDetailsQuery { Id = id };
                vm = await Mediator.Send(query);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(vm);
        }

        /// <summary>
        /// Creates the equipmentType
        /// </summary>
        /// <param name="createEquipmentTypeDto">CreateEquipmentTypeDto object</param>
        /// <returns>Returns equipmentType id (guid)</returns>
        /// <response code="201">Success</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> Create([FromForm] CreateEquipmentTypeDto createEquipmentTypeDto)
        {
            var command = _mapper.Map<CreateEquipmentTypeCommand>(createEquipmentTypeDto);

            var equipmentTypeId = await Mediator.Send(command);
            return Ok(equipmentTypeId);
        }

        /// <summary>
        /// Updates the equipmentType
        /// </summary>
        /// <param name="updateEquipmentTypeDto">equipmentTypeDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] UpdateEquipmentTypeDto updateEquipmentTypeDto)
        {
            var command = _mapper.Map<UpdateEquipmentTypeCommand>(updateEquipmentTypeDto);

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the equipmentType by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /equipmentType/2B7E86B9-F6B3-4FAC-A17B-A77264D6DAD8
        /// </remarks>
        /// <param name="id">Id of the equipmentType (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If equipmentType not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteEquipmentTypeCommand { Id = id };
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
