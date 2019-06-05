using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axioma.Models;
using Axioma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Axioma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupStudentController : ControllerBase
    {
		private UserService userService;
		private StudentService studentService;
		public SignupStudentController(UserService userService, StudentService studentService)
		{
			this.userService = userService;
			this.studentService = studentService;
		}

		[HttpPost]
		public ActionResult SignUp(User usuario)
		{
			bool validUsername = false, validEmail = false;
			if(userService.Get().FirstOrDefault(us => us.Username == usuario.Username) == null)
			{
				validUsername = true;
			}
			if (userService.Get().FirstOrDefault(us => us.Email == usuario.Email) == null)
			{
				validEmail = true;
			}
			if(validUsername && validEmail)
			{
				usuario.Role = "Student";
				User user = userService.Create(usuario);
				
				Student student = new Student()
				{
					User = user.Id
				};
				studentService.Create(student);
				return new JsonResult(user);
			}
			return new JsonResult(new { Result = "operacion no permitida", Details = new { validUsername = validUsername, validEmail = validEmail } });
		}
	}
}