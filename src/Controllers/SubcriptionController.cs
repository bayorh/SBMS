using Microsoft.AspNetCore.Mvc;
using SBMS.src.Contracts;
using SBMS.src.Dtos;
using System.Threading.Tasks;

namespace SBMS.src.Controllers
{
    [ApiController]
    [Route("api/subscription")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _subscriptionService.Subscribe(request);

            if (!result.Success)
            {
                // Differentiate between a bad request and a conflict
                if (result.Message.Contains("already subscribed"))
                {
                    return Conflict(result);
                }
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("unsubscribe")]
        public async Task<IActionResult> Unsubscribe([FromBody] UnSubscriptionRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _subscriptionService.UnSubscribe(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("status")]
        public async Task<IActionResult> CheckStatus([FromBody] StatusRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _subscriptionService.CheckStatus(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}