

using Axioma.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class ModulesController : ControllerBase {

    ModulesService modulesService;

    public ModulesController(ModulesService modulesService){
        this.modulesService = modulesService;
    }
    
    [HttpGet("{courseId}")]
    public ActionResult Get(string courseId){
        return new JsonResult(modulesService.Get(courseId));
    }

    [HttpPost]
    public ActionResult Create(Module module){
        return new JsonResult(modulesService.Create(module));
    }

}