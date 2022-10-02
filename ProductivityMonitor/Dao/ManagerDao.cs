using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using ProductivityMonitor.Contracts;
using ProductivityMonitor.Models.Entity;
using ProductivityMonitor.Models;
using ProductivityMonitor.Models.Resource;
using ProductivityMonitor.Models.Input;

namespace ProductivityMonitor.Dao
{
    //perform all manager database related tasks 
    public class ManagerDao : IManagerDao
    {
        public IConfiguration configuration;
        private UserManager<CustomUser> userManager;
        //for auto mapping
        private IMapper mapper;
        public string cs;

        //constructor
        public ManagerDao(IConfiguration configuration, UserManager<CustomUser> userManager, IMapper mapper)
        {
            this.configuration = configuration;
            //get the connection string from appsettings.json
            cs = configuration.GetConnectionString("csnpgsql");
            this.userManager = userManager;
            this.mapper = mapper;
        }

        //get all projects from projects table
        public List<ProjectEnt> GetAllProjects()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select * from projects;";
            var projects = connection.Query<ProjectEnt>(qry);
            //convert Ienumerable to list
            return projects.ToList();
        }

        //get all resources from cutsomusers identity table
        public List<ResourceRes> GetAllResources()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "SELECT u.\"Id\", u.\"User_Empl_Id\" as UserEmployeeId, u.\"UserName\", u.\"Email\", u.\"PhoneNumber\"," +
                " u.\"User_Status\" as UserStatus, r.\"Name\" as RoleName FROM public.\"AspNetUserRoles\" ur inner join" +
                " public.\"AspNetUsers\" u on ur.\"UserId\" = u.\"Id\" inner join public.\"AspNetRoles\" r on ur.\"RoleId\" = r.\"Id\"";
            var resources = connection.Query<ResourceRes>(qry);
            //convert Ienumerable to list
            return resources.ToList();
        }

        //get all tasks from tasks table
        public List<TaskEnt> GetAllTasks()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select * from tasks;";
            var tasks = connection.Query<TaskEnt>(qry);
            //convert Ienumerable to list
            return tasks.ToList();
        }

        //get all sprints from sprints table
        public List<SprintEnt> GetAllSprints()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select * from sprints;";
            var sprints = connection.Query<SprintEnt>(qry);
            //convert Ienumerable to list
            return sprints.ToList();
        }

        //get all sprints in project
        public List<SprintEnt> GetAllSprintsInProject(int projId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select s.* from ProjectModules pm inner join sprints s on pm.modl_id = s.sprn_modl_id where pm.modl_proj_id = @pid";
            var sprints = connection.Query<SprintEnt>(qry, new {pid = projId});
            //convert Ienumerable to list
            return sprints.ToList();
        }

        //get all sprints in module
        public List<SprintEnt> GetAllSprintsInModule(int modId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select s.*,u.\"UserName\" from sprints s inner join public.\"AspNetUsers\" u on s.sprn_master = u.\"Id\" where s.sprn_modl_id = @mid";
            var sprints = connection.Query<SprintEnt>(qry, new { mid = modId });
            //convert Ienumerable to list
            return sprints.ToList();
        }

        //create new sprint
        public bool CreateSprint(SprintRes sprintData)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            var newData = mapper.Map<SprintEnt>(sprintData);
            string qry = "insert into sprints(sprn_modl_id, sprn_master, sprn_stdate, sprn_enddate) values(@Sprn_modl_id,@Sprn_master,@Sprn_stdate,@Sprn_stdate);";
            int numOfRecordsAffected = connection.Execute(qry, newData);
            return numOfRecordsAffected > 0;
        }

        //get all tasks in a project
        public List<TaskEnt> GetAllTasksInProject(int projectId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select t.* from tasks t inner join projecttasks pt on t.task_id = pt.task_id " +
                "inner join projectmodules pm on pt.task_modl_id = pm.modl_id where pm.modl_proj_id = @pid;";
            var tasks = connection.Query<TaskEnt>(qry, new {pid = projectId});
            //convert Ienumerable to list
            return tasks.ToList();
        }

        //get all tasks in a module
        public List<TaskEnt> GetAllTasksInModule(int moduleId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select t.* from projecttasks pt inner join tasks t" +
                " on pt.task_id = t.task_id where pt.task_modl_id = @mid";
            var tasks = connection.Query<TaskEnt>(qry, new { mid = moduleId });
            //convert Ienumerable to list
            return tasks.ToList();
        }

        //get all modules in a project
        public List<ModuleEnt> GetAllModulesInProject(int projectId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select * from projectmodules where modl_proj_id = @pid";
            var modules = connection.Query<ModuleEnt>(qry, new { pid = projectId });
            //convert Ienumerable to list
            return modules.ToList();
        }

        //get all subtasks under a task
        public List<TaskEnt> GetSubTasks(int taskId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select t.* from subtasks st inner join tasks t on st.sbts_id = t.task_id where st.task_id = @tid";
            var tasks = connection.Query<TaskEnt>(qry, new { tid = taskId });
            //convert Ienumerable to list
            return tasks.ToList();
        }

        //create new sprint task
        public bool CreateSprintTask(SprintTaskModel sprintTaskData)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            var newData = mapper.Map<SprintTaskEnt>(sprintTaskData);
            string qry = "insert into sprinttasks values(@Sprn_Id,@Task_Id,@User_Id);";
            int numOfRecordsAffected = connection.Execute(qry, newData);
            return numOfRecordsAffected > 0;
        }

        //assign user to a sprint task
        public bool AssignUserToSprintTask(SprintTaskModel sprintTaskData)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            var newData = mapper.Map<SprintTaskEnt>(sprintTaskData);
            string qry = "update sprinttasks set user_id = @User_Id where task_id = @Task_Id;";
            int numOfRecordsAffected = connection.Execute(qry, newData);
            return numOfRecordsAffected > 0;
        }

        //create new task
        public bool CreateTask(TaskModel taskData)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            var newData = mapper.Map<TaskEnt>(taskData);
            if(newData.Task_Ref_Task_Id == 0)
            {
                newData.Task_Ref_Task_Id = null;
            }
            string qry = "insert into tasks(task_name,task_cdatetime,task_type,task_ref_task_id,task_category,task_desc," +
                "task_creator,task_noh_reqd,task_exp_datetime,task_supervisor,task_status)" +
                "values(@Task_Name, now(), @Task_Type, @Task_Ref_Task_Id, @Task_Category, @Task_Desc," +
                "@Task_Creator,@Task_Noh_Reqd,@Task_exp_datetime,@Task_supervisor,'todo');";
            int numOfRecordsAffected = connection.Execute(qry, newData);
            return numOfRecordsAffected > 0;
        }

        //get sprintwise resources
        public List<SprintUsersRes> GetAllResourcesInSprint(int sprintId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select u.\"UserName\",r.\"Name\" as RoleName from sprintresources sr inner join public.\"AspNetUsers\" u on sr.user_id = u.\"Id\" inner join public.\"AspNetRoles\" r on sr.sprs_role_id = r.\"Id\" where sprn_id = @spid";
            var resources = connection.Query<SprintUsersRes>(qry, new {spid = sprintId});
            //convert Ienumerable to list
            return resources.ToList();
        }

        //get tasks in sprint
        public List<SprintTaskGetEnt> GetAllTasksInSprint(int sprintId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select t.*,u.\"UserName\" from sprinttasks st inner join tasks t on st.task_id = t.task_id inner join public.\"AspNetUsers\" u on st.user_id = u.\"Id\" where st.sprn_id = @spid";
            var tasks = connection.Query<SprintTaskGetEnt>(qry, new { spid = sprintId });
            //convert Ienumerable to list
            return tasks.ToList();
        }
    }
}
