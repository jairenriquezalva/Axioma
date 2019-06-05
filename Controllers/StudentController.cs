using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Axioma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Axioma.Models;

namespace Axioma.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentController : ControllerBase
  {
      StudentService studentService;
      public StudentController(StudentService studentService){
          this.studentService = studentService;
      }

      [HttpPost]
      [Route("~/api/student/university")]
      public ActionResult UpdateUniversity(Student student){
          string userId = this.User.Claims.FirstOrDefault(i => i.Type == "userid").Value;
          string university = student.University;
          student = studentService.Get(userId);
          student.University = university;
          studentService.Update(userId,student);
          return new JsonResult(student);
      }
  }
}