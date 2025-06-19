using AutoMapper;
using TodoApi.DTOs;
using TodoApi.Models;
using static TodoApi.DTOs.TagDto;
using static TodoApi.DTOs.TaskListDto;
using static TodoApi.DTOs.NotificationDto;
using static TodoApi.DTOs.TaskItemDto;

namespace TodoApi.Services
{
    public class MappingService : Profile
    {
        public MappingService()
        {
            //Task List
            CreateMap<TaskList, TaskListDto>();

            CreateMap<TaskListDto, TaskList>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.UserId, o => o.Ignore());

            CreateMap<CreateTaskListDto, TaskList>();
            CreateMap<UpdateTaskListDto, TaskList>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.UserId, o => o.Ignore())
                .ForMember(d => d.Items, o => o.Ignore());

            CreateMap<Tag, TagDto>();
            CreateMap<CreateTagDto,  TagDto>();
            CreateMap<UpdateTaskListDto,  TagDto>();

            CreateMap<Notification, NotificationDto>();
            CreateMap<CreateNotificationDto, Notification>();

            CreateMap<TaskItem, TaskItemDto>()
                .ForMember(d => d.Tags, opt =>
                    opt.MapFrom(s => s.TaskItemTags.Select(t => t.Tag!.Name)));

            CreateMap<CreateTaskItemDto, TaskItem>();
            CreateMap<UpdateTaskItemDto, TaskItem>()
                .ForMember(d => d.TaskItemTags, opt => opt.Ignore());

        }
    }
}
