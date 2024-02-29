using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Witcher.Application.Common.Exceptions;
using Witcher.Application.Equipments.Commands.CreateEquipment;
using Witcher.Application.Equipments.Commands.DeleteEquipment;
using Witcher.Application.Equipments.Commands.UpdateEquipment;
using Witcher.Application.Equipments.Quieries.GetEquipmentDetails;
using Witcher.Application.Equipments.Quieries.GetEquipmentList;
using Witcher.WebApi.Models.Equipments;

namespace Witcher.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EquipmentController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly Uploader _uploader;

        public EquipmentController(IMapper mapper, Uploader uploader)
        {
            _mapper = mapper;
            _uploader = uploader;
        }

        /// <summary>
        /// Gets the list equipments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /equipment
        /// </remarks>
        /// <returns>Returns EquipmentListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EquipmentListVm>> GetAll()
        {
            var query = new GetEquipmentListQuery();

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the equipment by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /equipment/80C4D39D-484C-4A06-AC76-90645BF8DCBE
        /// </remarks>
        /// <param name="id">Equipment id(guid)</param>
        /// <returns>Returns EquipmentVm</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">if equipment not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EquipmentVm>> Get(Guid id)
        {
            EquipmentVm vm;
            try
            {
                var query = new GetEquipmentDetailsQeury { Id = id };
                vm = await Mediator.Send(query);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok(vm);
        }

        /// <summary>
        /// Creates the equipment
        /// </summary>
        /// <param name="createEquipmentDto">CreateEquipmentTypeDto object</param>
        /// <returns>Returns equipment id (guid)</returns>
        /// <response code="201">Success</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> Create([FromForm] CreateEquipmentDto createEquipmentDto)
        {
            var command = _mapper.Map<CreateEquipmentCommand>(createEquipmentDto);
            var filePath = _uploader.UploadFile(createEquipmentDto.ImageFile, "equipments");

            command.ImageName = filePath?.Split('/').Last();
            command.ImageSource = (filePath != null) ?
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{filePath}"
                : null;

            var equipmentId = await Mediator.Send(command);
            return Ok(equipmentId);
        }

        /// <summary>
        /// Updates the equpment
        /// </summary>
        /// <param name="updateEquipmentDto">UpdateEquipmentDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] UpdateEquipmentDto updateEquipmentDto)
        {
            var command = _mapper.Map<UpdateEquipmentCommand>(updateEquipmentDto);
            var filePath = _uploader.UploadFile(updateEquipmentDto.ImageFile, "equipments");

            command.ImageName = filePath?.Split('/').Last();
            command.ImageSource = (filePath != null) ?
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{filePath}"
                : null;

            var oldFileName = await Mediator.Send(command);

            _uploader.DeleteFile(oldFileName, "equipments");
            return NoContent();

        }

        /// <summary>
        /// Deletes the equipment by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /equipment/2B7E86B9-F6B3-4FAC-A17B-A77264D6DAD8
        /// </remarks>
        /// <param name="id">Id of the equipment (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If equipment not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteEquipmentCommand { Id = id };
                var oldFileName = await Mediator.Send(command);

                _uploader.DeleteFile(oldFileName, "equipments");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
            return NoContent();
        }
    }
}
