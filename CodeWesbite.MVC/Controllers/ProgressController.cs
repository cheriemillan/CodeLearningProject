// Progress Controller: Handles retrieving, creating, and updating user progress
using CodeWebsiteProject;
using CodeWesbite.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeWesbite.MVC.Controllers;

public class ProgressController : Controller
{
    private readonly IProgressRepo _repo;

    //Constructor for progress repo
    public ProgressController(IProgressRepo repo)
    {
        _repo = repo;
    }

    // Retrieves and displays user progress based on user ID
    public IActionResult GetProgress(int userId)
    {
        var result = _repo.GetProgress(userId);

        if (result == null)
        {
            // Returns 404 if no progress is found
            return NotFound();
        }
        // Displays the progress data
        return View(result);
    }
    // Creates a new progress entry for the user if it doesn't already exist
    public IActionResult CreateProgress(int userId)
    {
        var existingProgress = _repo.GetProgress(userId);

        if (existingProgress != null)
        {
            return Conflict("Progress entry for you already exists. Please resume.");
            
        }
        _repo.CreateProgress(userId);  // Inserts progress into the database
        return CreatedAtAction(nameof(GetProgress), new { userId }, new { userId });
    }

    // Displays the view for updating progress
    public IActionResult UpdateProgress(int userId)
    {
        var progress = _repo.GetProgress(userId);

        if (progress == null)
        {
            // Returns an error view if no progress is found
            return View("Error", new ErrorViewModel());
        }

        return View(progress);
    }

    // Updates the progress data in the database
    public IActionResult UpdateProgressToDatabase(Progress progress)
    {
        if (progress == null)
        {
            return BadRequest("Progress data is required");
            
        }
        
        _repo.UpdateProgress(progress.UserId, progress); // Updates progress in the database

        return RedirectToAction("UpdateProgress", new { userId = progress.UserId });
    }
}