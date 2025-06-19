using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.DTOs;
using System.Security.Claims;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListsController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _mapper;

        private static string CurrentUserId(ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public TaskListsController(TodoContext todoContext, IMapper mapper)
            => (_todoContext,  _mapper) = (todoContext, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListDto>>> GetAll()
        {
            var userId = CurrentUserId(User);
            var lists = await _todoContext.TaskLists.Where(t => t.UserId == userId).ToListAsync();

            return Ok(_mapper.Map<IEnumerable<TaskListDto>>(lists));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskListDto>> Get(int id)
        {
            var entity = await _todoContext.TaskLists.SingleOrDefaultAsync(t => t.Id == id && t.UserId == CurrentUserId(User));
            if (entity is null) return NotFound();

            return Ok(_mapper.Map<TaskListDto>(entity));
        }
            

        [HttpPost]
        public async Task<ActionResult<TaskListDto>> Create(CreateTaskListDto dto)
        {
            var entity = _mapper.Map<TaskList>(dto);
            entity.UserId = CurrentUserId(User);
            _todoContext.TaskLists.Add(entity);
            await _todoContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, _mapper.Map<TaskListDto>(entity));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TaskListDto>> Update(int id, UpdateTaskListDto dto)
        {
            var entity = await _todoContext.TaskLists.SingleOrDefaultAsync(t => t.Id == id && t.UserId == CurrentUserId(User));
            if (entity is null) return NotFound();
            _mapper.Map(dto, entity);
            await _todoContext.SaveChangesAsync();

            return Ok(_mapper.Map<TaskListDto>(entity));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _todoContext.TaskLists.SingleOrDefaultAsync(t => t.Id == id && t.UserId == CurrentUserId(User));
            if (entity is null) return NotFound();
            _todoContext.TaskLists.Remove(entity);
            await _todoContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
