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
            //TaskList maps
            CreateMap<TaskList, TaskListDto>();
            CreateMap<TaskListDto, TaskList>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.UserId, o => o.Ignore());
            CreateMap<CreateTaskListDto, TaskList>();
            CreateMap<UpdateTaskListDto, TaskList>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.UserId, o => o.Ignore())
                .ForMember(d => d.Items, o => o.Ignore());

            //Tag maps
            CreateMap<Tag,  TagDto>();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>()
                .ForMember(d => d.Id, o => o.Ignore());

            //Items maps
            CreateMap<TaskItem, TaskItemDto>()
                .ForCtorParam("TagNames",
                opt => opt.MapFrom(src => src.TaskItemTags
                          .Select(t => t.Tag!.Name)));
            CreateMap<CreateTaskItemDto, TaskItem>();
            CreateMap<UpdateTaskItemDto, TaskItem>()
                .ForMember(d => d.TaskItemTags, opt => opt.Ignore());


            CreateMap<Notification, NotificationDto>();
            CreateMap<CreateNotificationDto, Notification>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Sent, o => o.Ignore());

            

        }
    }
}
