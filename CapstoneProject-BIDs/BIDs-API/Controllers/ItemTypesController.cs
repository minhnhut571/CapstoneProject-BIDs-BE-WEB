using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data_Access.Entities;
using Business_Logic.Modules.ItemTypeModule.Interface;
using Business_Logic.Modules.ItemTypeModule.Request;
using BIDs_API.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypesController : ControllerBase
    {
        private readonly IItemTypeService _ItemTypeService;
        private readonly IHubContext<ItemTypeHub> _hubItemTypeContext;
        public ItemTypesController(IItemTypeService ItemTypeService
            , IHubContext<ItemTypeHub> hubItemTypeContext)
        {
            _ItemTypeService = ItemTypeService;
            _hubItemTypeContext = hubItemTypeContext;
        }

        // GET api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemType>>> GetItemTypesForAdmin()
        {
            try
            {
                var response = await _ItemTypeService.GetAll();
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
        public async Task<ActionResult<ItemType>> GetItemTypeByID([FromRoute] Guid? id)
        {
            var ItemType = await _ItemTypeService.GetItemTypeByID(id);

            if (ItemType == null)
            {
                return NotFound();
            }

            return ItemType;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_name/{name}")]
        public async Task<ActionResult<ItemType>> GetItemTypeByName([FromRoute] string name)
        {
            var ItemType = await _ItemTypeService.GetItemTypeByName(name);

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
                await _hubItemTypeContext.Clients.All.SendAsync("ReceiveItemTypeUpdate", ItemType);
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
        public async Task<ActionResult<ItemType>> PostItemType([FromBody] CreateItemTypeRequest createItemTypeRequest)
        {
            try
            {
                var ItemType = await _ItemTypeService.AddNewItemType(createItemTypeRequest);
                await _hubItemTypeContext.Clients.All.SendAsync("ReceiveItemTypeAdd", ItemType);
                return Ok(ItemType);
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
                await _hubItemTypeContext.Clients.All.SendAsync("ReceiveItemTypeDelete", ItemType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
