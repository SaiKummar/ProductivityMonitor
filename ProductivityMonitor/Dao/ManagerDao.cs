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

        //create new sprint task
        public bool CreateSprintTask(SprintTaskModel sprintTaskData)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            var newData = mapper.Map<SprintTaskEnt>(sprintTaskData);
            string qry = "insert into sprinttasks values(@Sprn_Id,@Task_Id,@User_Id);";
            int numOfRecordsAffected = connection.Execute(qry, newData);
            return numOfRecordsAffected > 0;
        }

        public List<TaskEnt> GetAllTasksInProject(int projectId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(cs);
            string qry = "select t.* from tasks t inner join projecttasks pt on t.task_id = pt.task_id " +
                "inner join projectmodules pm on pt.task_modl_id = pm.modl_id where pm.modl_proj_id = @pid;";
            var tasks = connection.Query<TaskEnt>(qry, new {pid = projectId});
            //convert Ienumerable to list
            return tasks.ToList();
        }
    }
}
