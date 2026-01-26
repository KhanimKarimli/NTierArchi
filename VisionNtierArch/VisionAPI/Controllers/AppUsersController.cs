using AutoMapper;
using Core.Entities.Concrete.Auth;
using Core.Utilities.Results.Concrete;
using Entities.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VisionAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AppUsersController : ControllerBase
	{
		readonly IAuthService _service;

		public AppUsersController(IAuthService service)
		{
			_service=service;
		}
		[HttpPost]
		public async Task<IActionResult> CreateRegister(RegisterDto register)
		{
			var result = await _service.CreateRegister(register);

			if (result.Success)
			{
				return Ok(new
				{
					Message = "User registered successfully"
				});
			}
				return BadRequest();

		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginDto login)
		{
			var result = await _service.Login(login);

			if (result!=null)
				return Ok(result);
			return BadRequest(new ErrorResult("Tapilmadi"));
		}
	}
}
