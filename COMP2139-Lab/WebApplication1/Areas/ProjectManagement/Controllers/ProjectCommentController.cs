using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.ProjectManagement.Models;
using WebApplication1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Areas.ProjectManagement.Controllers;

public class ProjectCommentController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProjectCommentController> _logger;

    public ProjectCommentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("ProjectManagement/ProjectComment/GetComments")]
    public async Task<IActionResult> GetComments(int projectId)
    {
        var comments = await _context.ProjectComments
            .Where(c => c.ProjectId == projectId)
            .OrderByDescending(c => c.DatePosted)
            .ToListAsync();
        
        //_logger.LogTrace("Getting Comments from DB at {time}", DateTime.Now);

        return Json(comments);
    }

    [HttpPost]
    [Route("ProjectManagement/ProjectComment/AddComment")]
    public async Task<IActionResult> AddComment([FromBody] ProjectComment comment)
    {
        if (ModelState.IsValid)
        {
            // Current date and time of the comment
            comment.DatePosted = DateTime.Now;

            // Save the comment
            _context.ProjectComments.Add(comment);
            
            //_logger.LogTrace("Added Comment {comment} at {time}", comment, DateTime.Now);

            //commit to database
            await _context.SaveChangesAsync();
            
            return Json( new { success = true, message = "Comment Added Successfully."});

        }
        
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        return Json(( new { success = true, message = "Invalid comment data.", errors = errors}));
    }
}

