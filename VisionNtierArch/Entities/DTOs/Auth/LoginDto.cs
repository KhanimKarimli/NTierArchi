using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs.Auth
{
    public class LoginDto:IDto
    {
		public string Password { get; set; }
		public string UserName { get; set; }
	}
}
