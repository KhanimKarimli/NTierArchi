using Core.Utilities.Results.Abstract;
using Entities.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Abstract
{
    public interface IAuthService
    {
		public Task<IResult> CreateRegister(RegisterDto register);
		public Task<IDataResult<TokenDto>> Login(LoginDto login);

	}
}
