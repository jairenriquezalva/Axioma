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
  public class CoursesController : ControllerBase
  {
    private readonly CoursesService coursesService;

    public CoursesController(CoursesService coursesService)
    {
      this.coursesService = coursesService;
    }

    [Authorize( Roles = "Student")]
    [HttpGet]
    public ActionResult Get(){
      string userId = this.User.Claims.FirstOrDefault(i => i.Type == "userid").Value;
      return new JsonResult(coursesService.GetAll(userId));
    }

    [HttpGet("{id}")]
    public ActionResult Get(string id){
      
      return new JsonResult(coursesService.Get(id));
    }

    [HttpGet]
    [Route("~/api/courses/Image/{id}")]
    public ActionResult GetImage(string id){
      string imgid = coursesService.Get(id).Image;
			byte[] bytes = coursesService.GetImage(imgid);
			return new FileContentResult(bytes, "image/png");
    }


    [HttpPost]
    public ActionResult Create(Course course){
      course = coursesService.Create(course);
      return new JsonResult(course);
    }

    [HttpPost]
    [Route("~/api/courses/image/{id}")]
    public ActionResult CreateImage(string id){
      IFormFile file = Request.Form.Files[0];
			Stream stream = file.OpenReadStream();
			byte[] b;
			using (BinaryReader br = new BinaryReader(stream))
			{
				b = br.ReadBytes((int)stream.Length);
			}
			string ImageId = coursesService.CreateImage(id, b);
			Course course = coursesService.Get(id);
			course.Image = ImageId;
			coursesService.Update(course.Id, course);
			return new JsonResult(course);
    }
  }
}