using AutoMapper;
using TMS.Application.Comments.Commands.CreateComment;
using TMS.Application.Comments.Dtos;
using TMS.Application.Tasks.Commands.CreateTask;
using TMS.Application.Tasks.Dtos;
using TMS.Application.Teams.Commands.CreateTeam;
using TMS.Application.Teams.Dtos;
using TMS.Application.Users.Dtos;
using TMS.Domain.Entities;

namespace TMS.Application.Common.Helper
{
    public class Helper : Profile
    {
        public Helper()
        {
            CreateMap<CreateTaskCommand, TaskItem>();
            CreateMap<TaskItem, TaskDto>();
            CreateMap<UpdateTaskDto, TaskItem>();


            CreateMap<CreateTeamCommand, Team>();
            CreateMap<Team, TeamDto>();


            CreateMap<Team, TeamViewDto>()
                                        .ForCtorParam("TeamId", opt => opt.MapFrom(src => src.TeamId))
                                        .ForCtorParam("TeamName", opt => opt.MapFrom(src => src.TeamName))
                                        .ForCtorParam("Members", opt => opt.MapFrom(src => src.UserTeams.Select(ut => ut.User).ToList()))
                                        .ForCtorParam("Tasks", opt => opt.MapFrom(src => src.AssignedTasks.ToList()));
            CreateMap<User, UserViewDto>();
            CreateMap<TaskItem, TaskViewDto>();



            CreateMap<CreateCommentCommand, TaskComment>();



            CreateMap<TaskItem , TaskDetailsDto>()
                                                .ForCtorParam("AssignedTeam", opt => opt.MapFrom(src => src.AssigendToTeam))
                                                .ForCtorParam("AssignedUser", opt => opt.MapFrom(src => src.AssignedToUser))
                                                .ForCtorParam("Comments", opt => opt.MapFrom(src => src.TaskComments));
            CreateMap<Team, TeamTaskDto>();
            CreateMap<User, UserTaskDto>();



            CreateMap<TaskComment , CommentDto>()
                        .ForCtorParam("email", opt => opt.MapFrom(src => src.User.Email));
        }
    }
}
