using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.Tasks.Common.Interfaces;
using TMS.Application.Tasks.Dtos;
using TMS.Domain.Entities;

namespace TMS.Application.Tasks.Queries
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, ICollection<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetAllTasksQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var result = await _taskRepository.GetAllAsync();

            var tasks = _mapper.Map<ICollection<TaskDto>>(result);

            return tasks;
        }
    }
}
