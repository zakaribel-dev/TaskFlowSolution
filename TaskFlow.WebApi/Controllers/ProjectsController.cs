using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlow.WebApi.DTOs;
using TaskFlow.Domain.Entities;

namespace TaskFlow.WebApi.Controllers;

[ApiController]
[Route("api/projects")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var projects = await _projectService.GetAllProjectsAsync(userId);
        return Ok(projects);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectCreateDto dto)
    {
        var userId = GetUserId();

        var project = new Project
        {
            Name = dto.Name ?? string.Empty,
            Description = dto.Description,
            UserId = userId
        };

        var createdProject = await _projectService.CreateProjectAsync(project, userId);

        // createdAtAction = renvoie c'qui a été created + code 201 , le premier param attend le nom en string d'où mle nameof
        return CreatedAtAction(nameof(GetById), new { id = createdProject.Id }, createdProject); 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = GetUserId();
        var project = await _projectService.GetProjectByIdAsync(id, userId);
        return Ok(project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProjectUpdateDto dto)
    {
        var userId = GetUserId();
        var project = await _projectService.GetProjectByIdAsync(id, userId);

        project.Name = dto.Name ?? string.Empty;
        project.Description = dto.Description;

        await _projectService.UpdateProjectAsync(project, userId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();
        await _projectService.DeleteProjectAsync(id, userId);
        return NoContent();
    }
}
