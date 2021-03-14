using Microsoft.AspNetCore.Identity;
using GymBuddyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GymBuddyAPI;
using GymBuddyAPI.Services;
using GymBuddyAPI.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GymBuddyContext _context;
        private readonly AuthServices service;
        public AuthController(UserManager<IdentityUser> identityService, GymBuddyContext gymBuddyContext, AuthServices authservices
            )
        {
            _userManager = identityService;
            _context = gymBuddyContext;
            service = authservices;
        }
        /// <summary>
        /// Log in user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Successfully logged in</response>   
        /// <response code="400">Wasn't able to log in</response>   
        [HttpPost("Login")]
        [EnableCors("AllowAll")]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        public async Task<ActionResult> LoginAsync(AuthAccount request)
        {
            var authResponse = await service.LogIn(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return Unauthorized(new AuthFailedResponse
                {
                    Error = authResponse.Error
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
        /// <summary>
        /// Refresh user token
        /// </summary>
        /// <returns></returns>
        [HttpPost("Refresh")]
        public async Task<ActionResult> RefreshAsync(RefreshTokenRequest request)
        {
            var authResponse = await service.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Error = authResponse.Error
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
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
            var result = await _userManager.CreateAsync(new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
            }, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    result.Errors,
                });
            }
            else
            {
                _context.UserData.Add(new UserData
                {
                    User = request.Email,
                    UserSchedule = new UserSchedule()
                });
                _context.SaveChanges();

            }
            return Ok();
        }
    }
}