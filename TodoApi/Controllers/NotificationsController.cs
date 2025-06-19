using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.DTOs;
using Microsoft.VisualBasic;
using System.Collections;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _mapper;

        public NotificationsController(TodoContext todoContext, IMapper mapper)
            => (_todoContext,  _mapper) = (todoContext, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetAll() 
            => Ok(_mapper.Map<IEnumerable<NotificationDto>>(await _todoContext.Notifications.ToListAsync()));

        [HttpPost]
        public async Task<ActionResult<NotificationDto>> Create(CreateNotificationDto dto)
        {
            var notif = _mapper.Map<NotificationDto>(dto);
            _todoContext.Add(notif);
            await _todoContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), null, _mapper.Map<NotificationDto>(notif));
        }
    }
}
