using BeelineMicroService.Provider;
using Microsoft.AspNetCore.Mvc;

namespace BeelineMicroService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountProvider _provider;

        public UserAccountController(IUserAccountProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("{id}/add-balance")]
        public async Task<IActionResult> AddBalance(int id, [FromBody] decimal amount)
        {
            var userAccount = await _provider.GetAggregateAsync(id);
            var result = await userAccount.AddBalanceAsync(amount);
                                                                    // сохранение отключено в провайдере
            if (result.Success)
            {
                return Ok(new { result.Version, result.Message });
            }
            return BadRequest(new { result.Version, result.Message });
        }

        [HttpPost("{id}/block")]
        public async Task<IActionResult> BlockUser(int id)
        {
            var userAccount = await _provider.GetAggregateAsync(id);
            var result = await userAccount.BlockUserAsync();
                                                             // cохранение отключено в провайдере
            if (result.Success)
            {
                return Ok(new { result.Version, result.Message });
            }
            return BadRequest(new { result.Version, result.Message });
        }
    }
}
