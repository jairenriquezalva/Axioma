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
  public class SubscriptionsController : ControllerBase
  {
      SubscriptionsService subscriptionsService;
      StudentService studentService;
      public SubscriptionsController(SubscriptionsService subscriptionsService, StudentService studentService){
          this.subscriptionsService = subscriptionsService;
          this.studentService = studentService;
      }

      [HttpPost]
      public ActionResult Create([FromBody]Subscription subscription){
          subscription.Student = studentService.Get(this.User.Claims.FirstOrDefault(i => i.Type == "userid").Value).Id;
          return new JsonResult(subscriptionsService.Create(subscription));
      }

      [HttpGet]
      public ActionResult Get(){
          string userId = this.User.Claims.FirstOrDefault(i => i.Type == "userid").Value;
          return new JsonResult(subscriptionsService.Get(userId));
      }

      [HttpDelete("{courseId}")]
      public ActionResult Remove(string courseId){
          string userId = this.User.Claims.FirstOrDefault(i => i.Type == "userid").Value;
          subscriptionsService.Delete(userId,courseId);
          return new StatusCodeResult(200);
      }
  }
}