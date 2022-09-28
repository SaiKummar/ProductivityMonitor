using ProductivityMonitor.Models.Entity;
using ProductivityMonitor.Models.Input;
using ProductivityMonitor.Models.Resource;

namespace ProductivityMonitor.Contracts
{
    public interface IManagerDao
    {
        bool AssignUserToSprintTask(SprintTaskModel sprintTaskData);
        bool CreateSprint(SprintRes sprintData);
        bool CreateSprintTask(SprintTaskModel sprintTaskData);
        bool CreateTask(TaskModel taskData);
        List<ModuleEnt> GetAllModulesInProject(int projectId);
        List<ProjectEnt> GetAllProjects();
        List<ResourceRes> GetAllResources();
        List<SprintEnt> GetAllSprints();
        List<TaskEnt> GetAllTasks();
        List<TaskEnt> GetAllTasksInProject(int projectId);
        List<TaskEnt> GetSubTasks(int taskId);
    }
}