// Main Menu Controller: This controller handles the main navigation options (Lessons, Quizzes, Tests)
using CodeWebsiteProject;
using Microsoft.AspNetCore.Mvc;

namespace CodeWesbite.MVC.Controllers;

public class MainMenuController : Controller
{
    // Displays the main menu view with options for Lessons, Quizzes, and Tests
    public IActionResult Index()
    {
        return View(); 
    }

    // Redirects the user to the Lessons page
    public IActionResult GoToLessons()
    {
        return RedirectToAction("Index", "Lesson");
    }

    // Redirects the user to the Quizzes page
    public IActionResult GoToQuizzes()
    {
        return RedirectToAction("Index", "Quiz");
    }

    // Redirects the user to the Tests page
    public IActionResult GoToTests()
    {
        return RedirectToAction("Index", "Tests");
    }
  
}



