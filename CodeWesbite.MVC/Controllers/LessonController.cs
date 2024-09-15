// Lesson Controller: This controller handles lesson management, viewing, and progress updates

using CodeWesbite.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodeWebsiteProject.MVC.Controllers;

public class LessonController: Controller
{
    private readonly IProgressRepo _progressRepo;
    private List<Lesson> lessons;
    private const int totalLessons = 4;
    private int userId = 1;

    // Constructor: Initializes lessons and injects the progress repository
    public LessonController(IProgressRepo progressRepo)
    {
        _progressRepo = progressRepo;
        lessons = new List<Lesson>
        {
            new Lesson
            {
                Title = "Lesson 1: What is C#",
                Content = "C# is a general-purpose, modern and <em>object-oriented programming language</em> pronounced as “C sharp”.\n            It was developed by Microsoft led by <em>Anders Hejlsberg</em> and his team within the .Net initiative and\n            was approved by the European Computer Manufacturers Association (ECMA) and International Standards\n            Organization (ISO). C# is among the languages for Common Language Infrastructure and the current\n            version of C# is version 7.2. C# is <em>a lot similar to Java syntactically</em> and is easy for the users\n            who have knowledge of C, C++ or Java. A bit about .Net Framework .Net applications are multi-platform\n            applications and framework can be used from languages like C++, C#, Visual Basic, COBOL etc. It is\n            designed in a manner so that other languages can use it. (GeeksForGeeks)"
            },
            new Lesson
            {
                Title = "Lesson 2: Value types vs Reference types", 
                Content = "Value types differ from reference types in that <em> variables of the value types directly contain their data</em>, \n whereas <em>variables of the reference types store references to their data</em>, the latter being known as objects. \n            With reference types, it is possible for <em>two variables to reference the same object</em>, and thus possible for \n            operations on one variable to affect the object referenced by the other variable. With value types, the \n            <em>variables each have their own copy of the data</em>, and it is not possible for operations on one to affect the other.\n            (Microsoft)"
            },
            new Lesson
            {
                Title = "Lesson 3: Variables",
                Content = "Variables are <em>containers for storing data values.</em> In C#, there are different types of variables (defined with different\n keywords), for example: int - stores integers (whole numbers), without decimals, such as 123 or -123. double - stores\n floating point numbers, with decimals, such as 19.99 or -19.99. (W3Schools)"
            },
            new Lesson{
                Title = "Lesson 4: Methods",
                Content = "A method is <em>a code block that contains a series of statements.</em> A program causes the statements to be executed by calling\n the method and specifying any required method arguments. In C#, every executed instruction is performed in the context of a method. (Microsoft) \n <img src = \"https://media.geeksforgeeks.org/wp-content/uploads/methods-in-java.png\" /> "
        }

        };
    }

    // Index action: Displays the list of all lessons
    public IActionResult Index() 
    { 
        return View(lessons); 
    } 
 
    // ViewLesson action: Displays the specific lesson based on the lesson index
    public IActionResult ViewLesson(int lessonIndex) 
    {
        if (lessonIndex < 0 || lessonIndex >= lessons.Count)
        {
            return RedirectToAction("Index");
        } 
         
        ViewBag.LessonsIndex = lessonIndex; // Passes the lesson index to the view
        return View(lessons[lessonIndex]); 
    } 
    // CompleteLesson action: Marks a lesson as completed and updates progress
    public IActionResult CompleteLesson(int lessonIndex) 
    { 
        if (lessonIndex < 0 || lessonIndex >= totalLessons) 
        { 
            // If the lesson index is out of bounds, redirect to the lesson list
            return RedirectToAction("Index"); 
        } 
 
        // Gets the user's progress or creates a new progress entry if none exists
        var progress = _progressRepo.GetProgress(userId) ?? new Progress { UserId = userId };
        
        progress.CompletedLesson(); // marks lesson completed
        _progressRepo.UpdateProgress(userId, progress); // Updates the progress in the database

        // If all lessons are completed, redirect to the AllLessonsCompleted page
        if (progress.LessonsCompleted >= totalLessons)
        {
            TempData["Message"] = "Great job! You've completed all the lessons.";
            return RedirectToAction("AllLessonsCompleted");
        }

        TempData["SuccessMessage"] = "Lesson completed successfully!";
        return RedirectToAction("Index", "Lesson");
    }
    // Displays a view when all lessons are completed
    public IActionResult AllLessonsCompleted() 
    { 
        return View(); 
    }
}


