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
            CreateMap<TaskList, TaskListDTO>();
            CreateMap<CreateTaskListDTO, TaskList>();
            CreateMap<UpdateTaskListDTO, TaskList>();

            CreateMap<Tag, TagDTO>();
            CreateMap<CreateTagDTO,  TagDTO>();
            CreateMap<UpdateTaskListDTO,  TagDTO>();

            CreateMap<Notification, NotificationDTO>();
            CreateMap<CreateNotificationDTO, Notification>();

            CreateMap<TaskItem, TaskItemDTO>()
                .ForMember(d => d.Tags, opt =>
                    opt.MapFrom(s => s.TaskItemTags.Select(t => t.Tag!.Name)));

            CreateMap<CreateTaskItemDTO, TaskItem>();
            CreateMap<UpdateTaskItemDTO, TaskItem>()
                .ForMember(d => d.TaskItemTags, opt => opt.Ignore());

        }
    }
}
