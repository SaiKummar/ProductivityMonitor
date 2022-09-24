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

        public ManagerRepository(IManagerDao dao)
        {
            this.dao = dao;
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

        //get all spll sprints
        public List<SprintEnt> GetAllSprints()
        {
            return dao.GetAllSprints();
        }

        //get all tasks
        public List<TaskEnt> GetAllTasks()
        {
            return dao.GetAllTasks();
        }

        public List<TaskEnt> GetAllTasksInProject(int projectId)
        {
            return dao.GetAllTasksInProject(projectId);
        }
    }
}
