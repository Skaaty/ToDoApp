using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.DTOs;
using Microsoft.VisualBasic;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _mapper;

        public TaskItemsController(TodoContext todoContext, IMapper mapper)
            => (_todoContext, _mapper) = (todoContext, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAll([FromQuery] TaskItemQuery query)
        {
            var qry = _todoContext.TaskItems
                .Include(ti => ti.TaskItemTags).ThenInclude(nam => nam.Tag)
                .AsQueryable();

            if (query.IsCompleted is not null) qry = qry.Where(t => t.IsCompleted == query.IsCompleted);
            if (query.Priority is not null) qry = qry.Where(t => t.Priority == query.Priority);
            if (query.DueDate is not null) qry = qry.Where(t => t.DueDate <= query.DueDate);
            if (query.TagId is not null) qry = qry.Where(t => t.TaskItemTags.Any(tt => tt.TagId == query.TagId));

            var items = await qry
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<TaskItemDto>>(items));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskItemDto>> Get(int id)
        {
            var item = await _todoContext.TaskItems
                .Include(ti => ti.TaskItemTags).ThenInclude(tit => tit.Tag)
                .FirstOrDefaultAsync(t => t.Id == id);

            return item is null ? NotFound() : Ok(_mapper.Map<TaskItemDto>(item));
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> Create(CreateTaskItemDto dto)
        {
            var entity = _mapper.Map<TaskItem>(dto);
            _todoContext.TaskItems.Add(entity);

            foreach (var tagId in dto.TagIds ?? Enumerable.Empty<int>())
            {
                entity.TaskItemTags.Add(new TaskItemTag { TagId = tagId });
            }

            await _todoContext.SaveChangesAsync();

            await _todoContext.Entry(entity)
                                .Collection(t => t.TaskItemTags)
                                .Query()
                                .Include(tit => tit.Tag)
                                .LoadAsync();

            return CreatedAtAction(nameof(Get), new {id = entity.Id}, _mapper.Map<TaskItemDto>(entity));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TaskItemDto>> Update(int id, UpdateTaskItemDto dto)
        {
            var entity = await _todoContext.TaskItems
                .Include(ti => ti.TaskItemTags)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity is null) return NotFound();

            _mapper.Map(dto, entity);

            entity.TaskItemTags.Clear();
            foreach (var tagId in dto.TagIds ?? Enumerable.Empty<int>())
            {
                entity.TaskItemTags.Add(new TaskItemTag{ TagId = tagId });
            }

            await _todoContext.SaveChangesAsync();
            return Ok(_mapper.Map<TaskItemDto>(entity));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _todoContext.TaskItems.FindAsync(id);
            if (entity is null) return NotFound();
            _todoContext.Remove(entity);
            await _todoContext.SaveChangesAsync();
            return NoContent();

        }

    
    }
}
