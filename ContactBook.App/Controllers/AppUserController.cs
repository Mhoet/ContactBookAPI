using ContactBook.DTO;
using ContactBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactBook.App.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserServices _appUserService;
        public AppUserController(IAppUserServices appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpPost]
        [Route("add-new")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser(RegistrationDTO userRegistration)
        {
            try
            {
                ResponseDTO result = await _appUserService.AddUser(userRegistration);
                return Created("", result);
            }
            catch (MissingFieldException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch]
        [Route("update")]
        [Authorize(Roles = "Admin, Regular")]
        public async Task<IActionResult> Update(UpdateDTO userUpdate)
        {
            string userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            try
            {
                var result = await _appUserService.UpdateAppUser(userId, userUpdate);
                return NoContent();
            }
            catch (MissingMemberException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch]
        [Route("update-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string userId, UpdateDTO userUpdate)
        {
            try
            {
                var result = await _appUserService.UpdateAppUser(userId, userUpdate);
                return NoContent();
            }
            catch (MissingMemberException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("email")]
        [Authorize(Roles = "Admin, Regular")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            try
            {
                var result = await _appUserService.GetUserByEmail(email);
                return Ok(result);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                var result = await _appUserService.GetUserById(userId);
                return Ok(result);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("all-users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers([FromQuery] PagingDTO paging)
        {
            try
            {
                var result = _appUserService.GetUsers(paging);

                return Ok(result);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                var result = await _appUserService.DeleteAppUser(userId);
                return NoContent();
            }
            catch (MissingMemberException mEx)
            {
                return BadRequest(mEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch]
        [Route("photo")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> UploadPhoto([FromForm] PhotoDTO photo)
        {
            string userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            try
            {
                var result = await _appUserService.UpdatePhoto(userId, photo);
                return NoContent();
            }
            catch (MissingMemberException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = "Admin, Regular")]
        public IActionResult Search([FromQuery] PagingDTO pagination, string searchQuery)
        {
            try
            {
                var result = _appUserService.SearchUsers(searchQuery, pagination);
                return Ok(result);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
