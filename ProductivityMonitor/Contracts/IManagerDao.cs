using ProductivityMonitor.Models.Entity;
using ProductivityMonitor.Models.Input;
using ProductivityMonitor.Models.Resource;

namespace ProductivityMonitor.Contracts
{
    public interface IManagerDao
    {
        bool CreateSprint(SprintRes sprintData);
        bool CreateSprintTask(SprintTaskModel sprintTaskData);
        List<ProjectEnt> GetAllProjects();
        List<ResourceRes> GetAllResources();
        List<SprintEnt> GetAllSprints();
        List<TaskEnt> GetAllTasks();
        List<TaskEnt> GetAllTasksInProject(int projectId);
    }
}