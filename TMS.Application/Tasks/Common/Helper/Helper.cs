using AutoMapper;
using TMS.Application.Tasks.Commands.CreateTask;
using TMS.Application.Tasks.Dtos;
using TMS.Domain.Entities;

namespace TMS.Application.Tasks.Common.Helper
{
    public class Helper : Profile
    {
        public Helper()
        {
            CreateMap<CreateTaskCommand, TaskItem>();

            CreateMap<TaskItem, TaskDto>();
        }
    }
}
