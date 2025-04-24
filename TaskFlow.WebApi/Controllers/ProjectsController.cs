using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlow.Domain.DTOs;
using TaskFlow.Storage.Repositories;
using TaskFlow.Domain.Entities;

namespace TaskFlow.WebApi.Controllers;

[ApiController]
[Route("api/projects")]
[Authorize]
public class ProjectsController(IProjectRepository projectRepo) : ControllerBase
{
    private readonly IProjectRepository _projectRepo = projectRepo;

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public IActionResult GetAll()
    {
        var userId = GetUserId();
        var projects = _projectRepo.GetByUserId(userId);
        return Ok(projects);
    }

    [HttpPost]
    public IActionResult Create(ProjectCreateDto dto)
    {
        var userId = GetUserId();

        var project = new Project
        {
            Name = dto.Name ?? string.Empty,
            Description = dto.Description,
            UserId = userId
        };

        _projectRepo.Add(project);
        _projectRepo.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var project = _projectRepo.GetById(id);
        if (project == null) return NotFound();
        if (project.UserId != GetUserId()) return Forbid();

        return Ok(project);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, ProjectUpdateDto dto)
    {
        var project = _projectRepo.GetById(id);
        if (project == null) return NotFound();
        if (project.UserId != GetUserId()) return Forbid();

        project.Name = dto.Name ?? string.Empty;
        project.Description = dto.Description;

        _projectRepo.Update(project);
        _projectRepo.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var project = _projectRepo.GetById(id);
        if (project == null) return NotFound();
        if (project.UserId != GetUserId()) return Forbid();

        _projectRepo.Delete(id);
        _projectRepo.SaveChanges();

        return NoContent();
    }
}
