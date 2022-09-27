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

            //mapping for customuser and resourceent models
            CreateMap<CustomUser, ResourceRes>()
            .ForMember(dest =>
            dest.UserEmployeeId,
            opt => opt.MapFrom(src => src.User_Empl_Id))
            .ForMember(dest =>
            dest.UserName,
            opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest =>
            dest.Email,
            opt => opt.MapFrom(src => src.Email))
            .ForMember(dest =>
            dest.PhoneNumber,
            opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest =>
            dest.UserStatus,
            opt => opt.MapFrom(src => src.User_Status));

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
        }
    }
}
