using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivityMonitor.Contracts;
using ProductivityMonitor.Models.Input;
using ProductivityMonitor.Models.Resource;

namespace ProductivityMonitor.Controllers
{
    //[Authorize(Roles="Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private INLoggerManager logger;
        private IManagerRepository repo;
        //for auto mapping
        private IMapper mapper;

        //controller
        public ManagerController(INLoggerManager logger, IManagerRepository repo, IMapper mapper)
        {
            this.logger = logger;
            this.repo = repo;
            this.mapper = mapper;
        }

        //--------------------project related endpoints------------------------
        // get all projects
        [HttpGet]
        [Route("v1/Projects")]
        public ActionResult<List<ProjectRes>> GetAllProjects()
        {
            logger.LogInfo("get all projects");
            return Ok(mapper.Map<List<ProjectRes>>(repo.GetAllProjects()));
        }

        //get all tasks under the project
        [HttpGet]
        [Route("v1/Projects/Tasks")]
        public ActionResult<List<TaskRes>> GetAllTasksInProject([FromQuery]int projectId)
        {
            logger.LogInfo("get all tasks in projects");
            return Ok(mapper.Map<List<TaskRes>>(repo.GetAllTasksInProject(projectId)));
        }

        //get all tasks under the module
        [HttpGet]
        [Route("v1/Projects/Modules/Tasks")]
        public ActionResult<List<TaskRes>> GetAllTasksInModule([FromQuery] int moduleId)
        {
            logger.LogInfo("get all tasks in module");
            return Ok(mapper.Map<List<TaskRes>>(repo.GetAllTasksInModule(moduleId)));
        }

        //get all modules under the project
        [HttpGet]
        [Route("v1/Projects/Modules")]
        public ActionResult<List<ModuleRes>> GetAllModulesInProject([FromQuery] int projectId)
        {
            logger.LogInfo("get all modules in projects");
            return Ok(mapper.Map<List<ModuleRes>>(repo.GetAllModulesInProject(projectId)));
        }

        //--------------------task related endpoints------------------------

        // get all tasks
        [HttpGet]
        [Route("v1/Tasks")]
        public ActionResult<List<TaskRes>> GetAllTasks()
        {
            logger.LogInfo("get all tasks");
            return Ok(mapper.Map<List<TaskRes>>(repo.GetAllTasks()));
        }

        //get all subtasks under a task
        [HttpGet]
        [Route("v1/Tasks/Subtasks")]
        public ActionResult<List<TaskRes>> GetSubTasks([FromQuery]int taskId)
        {
            logger.LogInfo("get all subtasks");
            return Ok(mapper.Map<List<TaskRes>>(repo.GetSubTasks(taskId)));
        }

        //Create new task
        [HttpPost]
        [Route("v1/Tasks")]
        public ActionResult<TaskModel> CreateTask([FromBody] TaskModel taskData)
        {
            if (repo.CreateTask(taskData))
            {
                return Ok(taskData);
            }
            else
            {
                return BadRequest("Could not create");
            }
        }

        // get all resources
        [HttpGet]
        [Route("v1/Resources")]
        public ActionResult<List<ResourceRes>> GetAllResources()
        {
            logger.LogInfo("get all resources");
            return Ok(repo.GetAllResources());
        }

        //--------------------sprint related endpoints------------------------

        //get all sprints
        [HttpGet]
        [Route("v1/Sprints")]
        public ActionResult<List<SprintRes>> GetAllSprints()
        {
            logger.LogInfo("get all sprints");
            return Ok(mapper.Map<List<SprintRes>>(repo.GetAllSprints()));
        }

        //create new sprint
        [HttpPost]
        [Route("v1/Sprints")]
        public ActionResult<SprintRes> CreateSprint([FromBody]SprintRes sprintData)
        {
            if (repo.CreateSprint(sprintData))
            {
                return Ok(sprintData);
            }
            else
            {
                return BadRequest("Could not create");
            }
        }

        //create task under sprint
        [HttpPost]
        [Route("v1/Sprints/Tasks")]
        public ActionResult<SprintTaskModel> CreateSprintTask([FromBody] SprintTaskModel sprintTaskData)
        {
            if (repo.CreateSprintTask(sprintTaskData))
            {
                return Ok(sprintTaskData);
            }
            else
            {
                return BadRequest("Could not create");
            }
        }

        //assign tasks to resources
        [HttpPatch]
        [Route("v1/Sprints/Tasks/Resources")]
        public ActionResult<SprintTaskModel> AssignUserToSprintTask([FromBody] SprintTaskModel sprintTaskData)
        {
            if (repo.AssignUserToSprintTask(sprintTaskData))
            {
                return Ok(sprintTaskData);
            }
            else
            {
                return BadRequest("Could not assign");
            }
        }
    }
}
