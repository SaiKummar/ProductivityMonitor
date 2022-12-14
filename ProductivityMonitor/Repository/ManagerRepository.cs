using ProductivityMonitor.Contracts;
using ProductivityMonitor.Models.Entity;
using ProductivityMonitor.Models.Input;
using ProductivityMonitor.Models.Resource;

namespace ProductivityMonitor.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        //reference to store a dao object
        private IManagerDao dao;

        //constructor
        public ManagerRepository(IManagerDao dao)
        {
            this.dao = dao;
        }

        public bool AssignUserToSprintTask(SprintTaskModel sprintTaskData)
        {
            return dao.AssignUserToSprintTask(sprintTaskData);
        }

        //create a new sprint
        public bool CreateSprint(SprintRes sprintData)
        {
            return dao.CreateSprint(sprintData);
        }

        //create a new task under sprint
        public bool CreateSprintTask(SprintTaskModel sprintTaskData)
        {
            return dao.CreateSprintTask(sprintTaskData);
        }

        //create new task
        public bool CreateTask(TaskModel taskData)
        {
            return dao.CreateTask(taskData);
        }

        //get all modules in a project
        public List<ModuleEnt> GetAllModulesInProject(int projectId)
        {
            return dao.GetAllModulesInProject(projectId);
        }

        //get all projects
        public List<ProjectEnt> GetAllProjects()
        {
            return dao.GetAllProjects();
        }

        //get all resources
        public List<ResourceRes> GetAllResources()
        {
            return dao.GetAllResources();
        }

        //get all resources in sprint
        public List<SprintUsersRes> GetAllResourcesInSprint(int sprintId)
        {
            return dao.GetAllResourcesInSprint(sprintId);
        }

        //get all sprints
        public List<SprintEnt> GetAllSprints()
        {
            return dao.GetAllSprints();
        }

        //get all sprints in module
        public List<SprintEnt> GetAllSprintsInModule(int modId)
        {
            return dao.GetAllSprintsInModule(modId);
        }

        //get all sprints in project
        public List<SprintEnt> GetAllSprintsInProject(int projId)
        {
            return dao.GetAllSprintsInProject(projId);
        }

        //get all tasks
        public List<TaskEnt> GetAllTasks()
        {
            return dao.GetAllTasks();
        }

        //get all tasks in a module
        public List<TaskEnt> GetAllTasksInModule(int moduleId)
        {
            return dao.GetAllTasksInModule(moduleId);
        }

        //get all tasks in a project
        public List<TaskEnt> GetAllTasksInProject(int projectId)
        {
            return dao.GetAllTasksInProject(projectId);
        }

        //get all tasks in sprint
        public List<SprintTaskGetEnt> GetAllTasksInSprint(int sprintId)
        {
            return dao.GetAllTasksInSprint(sprintId);
        }

        //get all subtasks in a task
        public List<TaskEnt> GetSubTasks(int taskId)
        {
            return dao.GetSubTasks(taskId);
        }
    }
}
