using ProductivityMonitor.Models.Entity;
using ProductivityMonitor.Models.Input;
using ProductivityMonitor.Models.Resource;

namespace ProductivityMonitor.Contracts
{
    public interface IManagerRepository
    {
        bool AssignUserToSprintTask(SprintTaskModel sprintTaskData);
        bool CreateSprint(SprintRes sprintData);
        bool CreateSprintTask(SprintTaskModel sprintTaskData);
        bool CreateTask(TaskModel taskData);
        List<ModuleEnt> GetAllModulesInProject(int projectId);
        List<ProjectEnt> GetAllProjects();
        List<ResourceRes> GetAllResources();
        List<SprintUsersRes> GetAllResourcesInSprint(int sprintId);
        List<SprintEnt> GetAllSprints();
        List<SprintEnt> GetAllSprintsInModule(int modId);
        List<SprintEnt> GetAllSprintsInProject(int projId);
        List<TaskEnt> GetAllTasks();
        List<TaskEnt> GetAllTasksInModule(int moduleId);
        List<TaskEnt> GetAllTasksInProject(int projectId);
        List<SprintTaskGetEnt> GetAllTasksInSprint(int sprintId);
        List<TaskEnt> GetSubTasks(int taskId);
    }
}