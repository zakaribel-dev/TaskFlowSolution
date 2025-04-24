using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlow.Domain.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Storage.Repositories;
using TaskFlow.Domain.Enums;

namespace TaskFlow.WebApi.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TasksController(ITaskItemRepository taskRepo, IProjectRepository projectRepo) : ControllerBase
{
    private readonly ITaskItemRepository _taskRepo = taskRepo;
    private readonly IProjectRepository _projectRepo = projectRepo;

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public IActionResult GetAll()
    {
        var userId = GetUserId();
        var userProjects = _projectRepo.GetByUserId(userId).Select(p => p.Id).ToList();
        var tasks = _taskRepo.GetAll().Where(t => userProjects.Contains(t.ProjectId));
        return Ok(tasks);
    }

    [HttpPost]
    public IActionResult Create(TaskCreateDto dto)
    {
        var userId = GetUserId();
        var project = _projectRepo.GetById(dto.ProjectId);
        if (project == null || project.UserId != userId)
            return Forbid();

        var task = new TaskItem
        {
            Title = dto.Title ?? string.Empty,
            Status = dto.Status,
            DueDate = dto.DueDate,
            ProjectId = dto.ProjectId,
            Commentaires = dto.Commentaires ?? string.Empty 
        };

        _taskRepo.Add(task);
        _taskRepo.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = _taskRepo.GetById(id);
        if (task == null) return NotFound();
        if (task.Project.UserId != GetUserId()) return Forbid();

        return Ok(task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, TaskUpdateDto dto)
    {
        var task = _taskRepo.GetById(id);
        if (task == null) return NotFound();
        if (task.Project.UserId != GetUserId()) return Forbid();

        task.Title = dto.Title ?? string.Empty;
        task.Status = dto.Status;
        task.DueDate = dto.DueDate;

        _taskRepo.Update(task);
        _taskRepo.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = _taskRepo.GetById(id);
        if (task == null) return NotFound();
        if (task.Project.UserId != GetUserId()) return Forbid();

        _taskRepo.Delete(id);
        _taskRepo.SaveChanges();

        return NoContent();
    }
}
