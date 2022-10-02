using AutoMapper;
using ProductivityMonitor.Models.Entity;
using ProductivityMonitor.Models;
using ProductivityMonitor.Models.Resource;
using ProductivityMonitor.Models.Input;

namespace ProductivityMonitor.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //mapping for project entity and resource models
            CreateMap<ProjectEnt, ProjectRes>()
                .ForMember(dest =>
            dest.ProjectId,
            opt => opt.MapFrom(src => src.Proj_Id))
        .ForMember(dest =>
            dest.ProjectName,
            opt => opt.MapFrom(src => src.Proj_Name))
        .ForMember(dest =>
            dest.ProjectDescription,
            opt => opt.MapFrom(src => src.Proj_Desc))
        .ForMember(dest =>
            dest.ProjectStartDate,
            opt => opt.MapFrom(src => src.Proj_Stdate))
        .ForMember(dest =>
            dest.ProjectManager,
            opt => opt.MapFrom(src => src.Proj_Manager))
        .ForMember(dest =>
            dest.ProjectStatus,
            opt => opt.MapFrom(src => src.Proj_Status))
        .ForMember(dest =>
            dest.ProjectLastUpdateDate,
            opt => opt.MapFrom(src => src.Proj_Ludate));

            //mapping for taskent and taskres
            CreateMap<TaskEnt, TaskRes>()
           .ForMember(dest =>
           dest.TaskId,
           opt => opt.MapFrom(src => src.Task_Id))
           .ForMember(dest =>
           dest.TaskName,
           opt => opt.MapFrom(src => src.Task_Name))
           .ForMember(dest =>
           dest.TaskCreationDate,
           opt => opt.MapFrom(src => src.Task_Cdatetime))
           .ForMember(dest =>
           dest.TaskType,
           opt => opt.MapFrom(src => src.Task_Type))
           .ForMember(dest =>
           dest.TaskReferenceTaskId,
           opt => opt.MapFrom(src => src.Task_Ref_Task_Id))
           .ForMember(dest =>
           dest.TaskCategory,
           opt => opt.MapFrom(src => src.Task_Category))
           .ForMember(dest =>
           dest.TaskDescription,
           opt => opt.MapFrom(src => src.Task_Desc))
           .ForMember(dest =>
           dest.TaskCreator,
           opt => opt.MapFrom(src => src.Task_Creator))
           .ForMember(dest =>
           dest.NumberOfHoursRequired,
           opt => opt.MapFrom(src => src.Task_Noh_Reqd))
           .ForMember(dest =>
           dest.ExpirationDate,
           opt => opt.MapFrom(src => src.Task_exp_datetime))
           .ForMember(dest =>
           dest.CompletionDate,
           opt => opt.MapFrom(src => src.Task_cmp_datetime))
           .ForMember(dest =>
           dest.TaskSupervisor,
           opt => opt.MapFrom(src => src.Task_supervisor))
           .ForMember(dest =>
           dest.TaskRemarks,
           opt => opt.MapFrom(src => src.Task_remarks))
           .ForMember(dest =>
           dest.TaskStatus,
           opt => opt.MapFrom(src => src.Task_status));

            //mapping for sprintent and sprintres
            CreateMap<SprintEnt, SprintRes>()
           .ForMember(dest =>
           dest.SprintId,
           opt => opt.MapFrom(src => src.Sprn_id))
           .ForMember(dest =>
           dest.SprintModuleId,
           opt => opt.MapFrom(src => src.Sprn_modl_id))
           .ForMember(dest =>
           dest.SprintMaster,
           opt => opt.MapFrom(src => src.Sprn_master))
           .ForMember(dest =>
           dest.SprintStartDate,
           opt => opt.MapFrom(src => src.Sprn_stdate))
           .ForMember(dest =>
           dest.SprintEndDate,
           opt => opt.MapFrom(src => src.Sprn_enddate))
           .ForMember(dest =>
           dest.UserName,
           opt => opt.MapFrom(src => src.UserName))
           .ReverseMap();

            //mapping for sprinttaskent and sprinttask input model
            CreateMap<SprintTaskModel, SprintTaskEnt>()
           .ForMember(dest =>
           dest.Sprn_Id,
           opt => opt.MapFrom(src => src.SprintId))
           .ForMember(dest =>
           dest.Task_Id,
           opt => opt.MapFrom(src => src.TaskId))
           .ForMember(dest =>
           dest.User_Id,
           opt => opt.MapFrom(src => src.UserId));

            //mapping for Moduleent and module res
            CreateMap<ModuleEnt, ModuleRes>()
           .ForMember(dest =>
           dest.ModuleId,
           opt => opt.MapFrom(src => src.Modl_Id))
           .ForMember(dest =>
           dest.ModuleName,
           opt => opt.MapFrom(src => src.Modl_Name))
           .ForMember(dest =>
           dest.ModuleDescription,
           opt => opt.MapFrom(src => src.Modl_Desc))
           .ForMember(dest =>
           dest.ModuleProjectId,
           opt => opt.MapFrom(src => src.Modl_Proj_Id));


            //mapping for taskmodel and taskent
            CreateMap<TaskModel, TaskEnt>()
           .ForMember(dest =>
           dest.Task_Name,
           opt => opt.MapFrom(src => src.TaskName))
           .ForMember(dest =>
           dest.Task_Type,
           opt => opt.MapFrom(src => src.TaskType))
           .ForMember(dest =>
           dest.Task_Ref_Task_Id,
           opt => opt.MapFrom(src => src.TaskReferenceTaskId))
           .ForMember(dest =>
           dest.Task_Category,
           opt => opt.MapFrom(src => src.TaskCategory))
           .ForMember(dest =>
           dest.Task_Desc,
           opt => opt.MapFrom(src => src.TaskDescription))
           .ForMember(dest =>
           dest.Task_Creator,
           opt => opt.MapFrom(src => src.TaskCreator))
           .ForMember(dest =>
           dest.Task_Noh_Reqd,
           opt => opt.MapFrom(src => src.NumberOfHoursRequired))
           .ForMember(dest =>
           dest.Task_exp_datetime,
           opt => opt.MapFrom(src => src.ExpirationDate))
           .ForMember(dest =>
           dest.Task_supervisor,
           opt => opt.MapFrom(src => src.TaskSupervisor));
        }
    }
}
