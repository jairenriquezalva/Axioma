using Axioma.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class LessonsController : ControllerBase {
    
    LessonsService lessonsService;

    public LessonsController(LessonsService lessonsService){
        this.lessonsService = lessonsService;
    }

    [HttpPost]
    public ActionResult Create([FromBody]Lesson lesson){
        return new JsonResult(lessonsService.Create(lesson));
    }

    [HttpGet("{moduleId}")]
    public ActionResult GetByModuleId(string moduleId){
        return new JsonResult(lessonsService.Get(moduleId));
    }

}