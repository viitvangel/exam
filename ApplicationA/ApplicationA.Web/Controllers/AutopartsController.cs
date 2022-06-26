using ApplicationA.BL.Interfaces;
using ApplicationA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationA.Controllers
{
    [ApiController]
    [Route("/api/autoparts")]
    public class AutopartsController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMq;
        private readonly IAutopartService autopartService;

        public AutopartsController(IRabbitMqService rabbitMq, IAutopartService autopartService)
        {
            _rabbitMq = rabbitMq;
            this.autopartService = autopartService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(autopartService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> ProduceAutoparts([FromBody] Autopart autopart)
        {
            autopartService.Create(autopart);
            await _rabbitMq.PublishAutopartAsync(autopart);

            return Ok();
        }
    }
}
