using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data_Access.Entities;
using Business_Logic.Modules.PaymentModule.Interface;
using Business_Logic.Modules.PaymentModule.Request;

namespace BIDs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _PaymentService;

        public PaymentsController(IPaymentService PaymentService)
        {
            _PaymentService = PaymentService;
        }

        // GET api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentsForAdmin()
        {
            try
            {
                var response = await _PaymentService.GetAll();
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
        public async Task<ActionResult<Payment>> GetPaymentByID([FromRoute] Guid? id)
        {
            var Payment = await _PaymentService.GetPaymentByID(id);

            if (Payment == null)
            {
                return NotFound();
            }

            return Payment;
        }

        // GET api/<ValuesController>/abc
        [HttpGet("by_user_id/{name}")]
        public async Task<ActionResult<Payment>> GetPaymentByName([FromRoute] Guid? id)
        {
            var Payment = await _PaymentService.GetPaymentByUserID(id);

            if (Payment == null)
            {
                return NotFound();
            }

            return Payment;
        }

        // PUT api/<ValuesController>/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutPayment([FromBody] UpdatePaymentRequest updatePaymentRequest)
        {
            try
            {
                await _PaymentService.UpdatePayment(updatePaymentRequest);
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
        public async Task<ActionResult<Payment>> PostPayment([FromBody] CreatePaymentRequest createPaymentRequest)
        {
            try
            {
                return Ok(await _PaymentService.AddNewPayment(createPaymentRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment([FromRoute] Guid? id)
        {
            try
            {
                await _PaymentService.DeletePayment(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
