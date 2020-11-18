using Microsoft.AspNetCore.Identity;
using GymBuddyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
        }
        /// <summary>
        /// Log in user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Successfully logged in</response>   
        /// <response code="400">Wasn't able to log in</response>   
        [HttpPost("Login")]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        public async Task<ActionResult> LoginAsync(AuthAccount request)
        {
            return Ok();
        }
        /// <summary>
        /// Refresh user token
        /// </summary>
        /// <returns></returns>
        [HttpPost("Refresh")]
        public async Task<ActionResult> RefreshAsync(RefreshTokenRequest request)
        {
            return Ok();
        }
        /// <summary>
        /// Register user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Registration")]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(IdentityError), 400)]
        public async Task<ActionResult> RegisterAsync(AuthAccount request)
        {
            return Ok();
        }
    }
}