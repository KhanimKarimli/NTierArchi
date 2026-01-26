using Core.Entities.Concrete.Auth;
using Entities.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Profiles
{
    internal class AuthProfile : Profile
	{
		public AuthProfile()
		{
			CreateMap<RegisterDto, AppUser>();
		}
    }
}
