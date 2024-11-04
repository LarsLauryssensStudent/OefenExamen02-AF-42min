using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using OefenExamen02.Models;
using OefenExamen02.Services;
using System.Collections;

namespace OefenExamen02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ILogger<PropertiesController> logger;
        private readonly IRealEstateData data;

        public PropertiesController(IRealEstateData DIdata, ILogger<PropertiesController> DIlogger)
        {
            this.logger = DIlogger;
            this.data = DIdata;

        }

        [EnableRateLimiting(policyName: "ThreeRequestThirty")]
        [HttpGet("listforsale")]
        public async Task<ActionResult<IEnumerable>> GetForSale()
        {
            var results = data.GetForSale();
            if (results == null)
            {
                logger.LogInformation("Niets gevonden in for sale");

                return NotFound();
            }
            if (results != null)
            {
                return Ok(results);

            }
            return BadRequest();
        }
        [EnableRateLimiting(policyName: "TenReq20")]
        [HttpGet("listsold")]
        public async Task<ActionResult<IEnumerable>> GetSold()
        {
            
            var results = data.GetSold();
            if (results == null)
            {
                logger.LogInformation("Niets gevonden in sold");

                return NotFound();
            }
            if (results != null)
            {
                return Ok(results);

            }
            return BadRequest();
        }
        [EnableRateLimiting(policyName: "OneRequestFifty")]
        [HttpGet("Details/{id:int}")]
        public async Task<ActionResult<Property?>> GetById([FromRoute (Name = "id")] int id)
        {
            if (!ModelState.IsValid) {
                logger.LogInformation("Er ging iets mis in GetById");

                return BadRequest();
            }
            var result = data.Get(id);
            if (result == null)
            {
                logger.LogInformation($"Niets gevonden met dit id {id}");

                return NotFound("Geen Property met dit ID");
            }
            return Ok(result);
        }
        [EnableRateLimiting(policyName: "OneRequestFifty")]
        [HttpPost("create")]
        public async Task<ActionResult<Property?>> AddProp([FromBody] Property property)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Er ging iets mis in create");

                    return BadRequest();
                }
                data.Add(property);
                logger.LogInformation("Nieuwe property toegevoegd");

                return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
            } catch (ArgumentException ex)
            {
                logger.LogCritical($"Exception opgevangen: {ex.Message}");
                return BadRequest();
            }
            
        }


        [HttpPut("update")]
        [EnableRateLimiting(policyName: "OneRequestFifty")]

        public async Task<ActionResult> UpdateProp([FromBody] Property property)
        {
            if (!ModelState.IsValid)
            {
                logger.LogInformation("Er ging iets mis in update");
                return BadRequest();
            }
            var result = data.Get(property.Id);
            if (result == null)
            {
                return NotFound("Geen property met deze ID");

            }
            data.Update(property);
            return NoContent();

        }

    }
}
