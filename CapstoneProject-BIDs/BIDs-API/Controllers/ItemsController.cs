using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data_Access.Entities;
using Business_Logic.Modules.ItemModule.Interface;
using Business_Logic.Modules.ItemModule.Request;
using BIDs_API.SignalR;
using Microsoft.AspNetCore.SignalR;
using Business_Logic.Modules.ItemModule.Request;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _ItemService;
        private readonly IHubContext<ItemHub> _hubItemContext;

        public ItemsController(IItemService ItemService
            , IHubContext<ItemHub> hubItemContext)
        {
            _ItemService = ItemService;
            _hubItemContext = hubItemContext;
        }

        // GET api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsForAdmin()
        {
            try
            {
                var response = await _ItemService.GetAll();
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
        public async Task<ActionResult<Item>> GetItemByID([FromRoute] Guid? id)
        {
            var Item = await _ItemService.GetItemByID(id);

            if (Item == null)
            {
                return NotFound();
            }

            return Item;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_name/{name}")]
        public async Task<ActionResult<Item>> GetItemByName([FromRoute] string name)
        {
            var Item = await _ItemService.GetItemByName(name);

            if (Item == null)
            {
                return NotFound();
            }

            return Item;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_type_name/{name}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemByTypeName([FromRoute] string name)
        {
            try
            {
                var response = await _ItemService.GetItemByTypeName(name);
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

        // GET api/<ValuesController>/abc
        [HttpGet("by_user/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemByTypeName([FromRoute] Guid? id)
        {
            try
            {
                var response = await _ItemService.GetItemByUserID(id);
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

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutItem([FromBody] UpdateItemRequest updateItemRequest)
        {
            try
            {
                var Item = await _ItemService.UpdateItem(updateItemRequest);
                await _hubItemContext.Clients.All.SendAsync("ReceiveItemUpdate", Item);
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
        public async Task<ActionResult<Item>> PostItem([FromBody] CreateItemRequest createItemRequest)
        {
            try
            {
                var Item = await _ItemService.AddNewItem(createItemRequest);
                await _hubItemContext.Clients.All.SendAsync("ReceiveItemAdd", Item);
                return Ok(Item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid? id)
        {
            try
            {
                var Item = await _ItemService.DeleteItem(id);
                await _hubItemContext.Clients.All.SendAsync("ReceiveItemDelete", Item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
