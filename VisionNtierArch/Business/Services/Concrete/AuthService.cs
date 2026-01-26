using Core.Entities.Concrete.Auth;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Services.Concrete
{
    public class AuthService:IAuthService
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		IMapper _mapper;
		private readonly IConfiguration _config;
		private readonly TokenOption _tokenOption;

		public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration config)
		{
			_userManager=userManager;
			_roleManager=roleManager;
			_mapper=mapper;
			_config=config;
			_tokenOption=_config.GetSection("TokenOptions").Get<TokenOption>();
		}
		[HttpPost]
		public async Task<IResult> CreateRegister(RegisterDto register)
		{
			var user = _mapper.Map<AppUser>(register);
			var resultuser = await _userManager.CreateAsync(user, register.Password);
			if (!resultuser.Succeeded)
				return new ErrorResult("Qeydiyyat alinmadi");

			await _roleManager.CreateAsync(new IdentityRole("User"));

			var resultrole = await _userManager.AddToRoleAsync(user, "User");

			if (!resultrole.Succeeded)
				return new ErrorResult("Qeydiyyat alinmadi");

			return new SuccessResult("Qeydiyyat ugurla tamamlandi");

		}

		[HttpPost]
		public async Task<IDataResult<TokenDto>> Login(LoginDto login)
		{
			AppUser user = await _userManager.FindByNameAsync(login.UserName);
			if (user is null)
			{
				return new ErrorDataResult<TokenDto>("tapilmadi");
			}
			bool isValidPassword = await _userManager.CheckPasswordAsync(user, login.Password);
			if (!isValidPassword)
			{
				return new ErrorDataResult<TokenDto>("tapilmadi");
			}
			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));
			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
			JwtHeader header = new JwtHeader(signingCredentials);
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, login.UserName)
			};
			foreach (var userRole in await _userManager.GetRolesAsync(user))
			{
				claims.Add(new Claim(ClaimTypes.Role, userRole));
			}

			JwtPayload payload = new JwtPayload(issuer: _tokenOption.Issuer, audience: _tokenOption.Audience, claims: claims, notBefore: DateTime.UtcNow, expires: DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration));
			JwtSecurityToken token = new JwtSecurityToken(header, payload);
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			string jwt = tokenHandler.WriteToken(token);
			TokenDto dto=new TokenDto();
			dto.Token = jwt;
			return new SuccessDataResult<TokenDto>(dto, "Login ugurlu oldu");
		}
	}
}
