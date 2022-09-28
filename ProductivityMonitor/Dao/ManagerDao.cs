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

        //get all proejcts from projects table
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
            //get all users from identity tables using usermanager
            List<CustomUser> users = userManager.Users.ToList();

            //map the identity user table to resource model and return the result
            return mapper.Map<List<ResourceRes>>(users);
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

        //get all tas from tasks table
        public List<SprintEnt> GetAllSprints()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select * from sprints;";
            var sprints = connection.Query<SprintEnt>(qry);
            //convert Ienumerable to list
            return sprints.ToList();
        }

        //create new sprint
        public bool CreateSprint(SprintRes sprintData)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            var newData = mapper.Map<SprintEnt>(sprintData);
            string qry = "insert into sprints values(@Sprn_id,@Sprn_modl_id,@Sprn_master,@Sprn_stdate,@Sprn_stdate);";
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
    }
}
