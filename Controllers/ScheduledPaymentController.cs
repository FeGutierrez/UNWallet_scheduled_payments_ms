using System;
using Microsoft.AspNetCore.Mvc;
using unwallet.Models;
using unwallet.Services;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace unwallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Route("api/[controller]")]
    public class ScheduledPaymentController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public ScheduledPaymentController(MongoDBService mongoDBService){
            _mongoDBService = mongoDBService;
        }


        [HttpGet]
        [Route("list")]
        public async Task<List<ScheduledPayment>> GetScheduledPayments()
        {
            return await _mongoDBService.GetAsync();
        }

        //[NonAction]
        [HttpGet("list/{_id}", Name="GetById")]
        //[Route("list")]
        public async Task<ActionResult<ScheduledPayment>> GetScheduledPaymentById(string _id)
        {
            var payment = await _mongoDBService.GetByIdAsync(_id);
            if (payment is null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ScheduledPayment>> CreateScheduledPayment(ScheduledPayment scheduledPayment)
        {
            scheduledPayment._id = ObjectId.GenerateNewId().ToString();
            await _mongoDBService.CreateAsync(scheduledPayment);
            var response = await _mongoDBService.GetByIdAsync(scheduledPayment._id);
            return Ok(response);
            //return CreatedAtRoute("GetById", new { id = scheduledPayment._id}, scheduledPayment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScheduledPayment(string id, ScheduledPayment scheduledPayment)
        {
            var existingScheduledPayment = await _mongoDBService.GetByIdAsync(id);
            
            if(existingScheduledPayment is null){
                return NotFound();
            }

            await _mongoDBService.UpdateAsync(id, scheduledPayment);
            var response = await _mongoDBService.GetByIdAsync(id);
            return Ok(response);
        }
        
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteScheduledPayment(string id){
            var sPayment = await _mongoDBService.GetByIdAsync(id);
            var success = await _mongoDBService.DeleteAsync(id);
            if(!success){
                return NotFound();
            }
            return Ok(sPayment);
        }

    }
}