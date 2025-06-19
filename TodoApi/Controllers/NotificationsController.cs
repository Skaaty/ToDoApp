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
using System.Security.Claims;

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
        public async Task<ActionResult<IEnumerable<NotificationDto>>> Get([FromQuery] int? taskItemId)
        {
            var userId = CurrentUserId(User);
            var q = _todoContext.Notifications
                        .Include(n => n.TaskItem)
                        .Where(n => n.TaskItem.TaskList!.UserId == userId);
            if (taskItemId is not null)
                q = q.Where(n => n.TaskItemId == taskItemId);
            var dtoList = _mapper.Map<IEnumerable<NotificationDto>>(await q.ToListAsync());

            return Ok(dtoList);
        }

        [HttpPost]
        public async Task<ActionResult<NotificationDto>> Create(CreateNotificationDto dto)
        {
            var userId = CurrentUserId(User);
            var task = await _todoContext.TaskItems
                                         .Include(t => t.TaskList)
                                         .SingleOrDefaultAsync(t => t.Id == dto.TaskItemId &&
                                                                t.TaskList!.UserId == userId);
            if (task is null) return NotFound("TaskItem not found or not yours.");

            var entity = _mapper.Map<Notification>(dto);
            _todoContext.Notifications.Add(entity);
            await _todoContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, _mapper.Map<NotificationDto>(entity));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = CurrentUserId(User);
            var entity = await _todoContext.Notifications
                                           .Include(n => n.TaskItem.TaskList)
                                           .SingleOrDefaultAsync(n => n.Id == id &&
                                                n.TaskItem.TaskList!.UserId == userId);

            if (entity is null) return NotFound();
            _todoContext.Notifications.Remove(entity);
            await _todoContext.SaveChangesAsync();
            return NoContent();
        }

        private static string CurrentUserId(ClaimsPrincipal u) =>
            u.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

}
