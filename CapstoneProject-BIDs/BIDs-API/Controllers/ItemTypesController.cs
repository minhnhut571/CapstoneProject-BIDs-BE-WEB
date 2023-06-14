using AutoMapper;
using BIDs_API.SignalR;
using Business_Logic.Modules.ItemTypeModule.Interface;
using Business_Logic.Modules.ItemTypeModule.Request;
using Business_Logic.Modules.ItemTypeModule.Response;
using Data_Access.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypesController : ControllerBase
    {
        private readonly IItemTypeService _ItemTypeService;
        public readonly IMapper _mapper;
        private readonly IHubContext<ItemTypeHub> _hubContext;

        public ItemTypesController(IItemTypeService ItemTypeService
            , IMapper mapper
            , IHubContext<ItemTypeHub> hubContext)
        {
            _ItemTypeService = ItemTypeService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        // GET api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemTypeResponseStaff>>> GetItemTypesForAdmin()
        {
            try
            {
                var list = await _ItemTypeService.GetAll();
                var response = list.Select
                           (
                             emp => _mapper.Map<ItemType, ItemTypeResponseStaff>(emp)
                           );
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemTypeResponseStaff>> GetItemTypeByID([FromRoute] Guid? id)
        {
            var ItemType = _mapper.Map<ItemTypeResponseStaff>(await _ItemTypeService.GetItemTypeByID(id));

            if (ItemType == null)
            {
                return NotFound();
            }

            return ItemType;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_name/{name}")]
        public async Task<ActionResult<ItemTypeResponseStaff>> GetItemTypeByName([FromRoute] string name)
        {
            var ItemType = _mapper.Map<ItemTypeResponseStaff>(await _ItemTypeService.GetItemTypeByName(name));

            if (ItemType == null)
            {
                return NotFound();
            }

            return ItemType;
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutItemType([FromBody] UpdateItemTypeRequest updateItemTypeRequest)
        {
            try
            {
                var ItemType = await _ItemTypeService.UpdateItemType(updateItemTypeRequest);
                await _hubContext.Clients.All.SendAsync("ReceiveItemTypeUpdate", ItemType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemTypeResponseStaff>> PostItemType([FromBody] CreateItemTypeRequest createItemTypeRequest)
        {
            try
            {
                var ItemType = await _ItemTypeService.AddNewItemType(createItemTypeRequest);
                await _hubContext.Clients.All.SendAsync("ReceiveItemTypeAdd", ItemType);
                return Ok(_mapper.Map<ItemTypeResponseStaff>(ItemType));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemType([FromRoute] Guid? id)
        {
            try
            {
                var ItemType = await _ItemTypeService.DeleteItemType(id);
                await _hubContext.Clients.All.SendAsync("ReceiveItemTypeDelete", ItemType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
