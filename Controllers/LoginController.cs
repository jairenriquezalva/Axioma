using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axioma.Models;
using Axioma.Models.RequestModels;
using Axioma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Axioma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginStudentController : ControllerBase
    {
		private TokenService tokenService;
		public LoginStudentController(TokenService tokenService)
		{
			this.tokenService = tokenService;
		}

		[HttpPost]
		public ActionResult Login(User user)
		{
			string token = tokenService.getToken(user.Username, user.Password);
			if (token != null)
				return new JsonResult(new { Token = token });
			return new JsonResult(new { Result = "usuario o contraseña incorrecta" });
		}

	}
}
