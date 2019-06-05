using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Axioma.Models;
using Axioma.Models.RequestModels;
using Axioma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Axioma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
		UniversitiesService us;
		public UniversityController(UniversitiesService us)
		{
			this.us = us;
		}

		[HttpGet("{id}")]
		public ActionResult GetById(string id)
		{
			return new JsonResult(us.Get(id));
		}

		[HttpGet]
		public ActionResult Get()
		{
			return new JsonResult(us.Get());
		}

		[HttpGet]
		[Route("~/api/University/Image/{id}")]
		public ActionResult GetImage(string id)
		{
			string imgid = us.Get(id).Image;
			byte[] bytes = us.GetImage(imgid);
			return new FileContentResult(bytes, "image/png");
		}


		[HttpPost]
		public JsonResult Create(RequestUniversity u)
		{
			University university = new University
			{
				Name = u.Name,
			};
			us.Create(university);
			return new JsonResult(university);
		}

		
		[HttpPost]
		[Route("~/api/University/Image/{id}")]
		public ActionResult CreateImage(string id)
		{			
			IFormFile file = Request.Form.Files[0];
			Stream stream = file.OpenReadStream();
			byte[] b;
			using (BinaryReader br = new BinaryReader(stream))
			{
				b = br.ReadBytes((int)stream.Length);
			}
			string ImageId = us.CreateImage(id, b);
			University uni = us.Get(id);
			uni.Image = ImageId;
			us.Update(uni.Id, uni);
			return new JsonResult(uni);
		}
	}
}