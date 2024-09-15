// Quiz Controller: Manages quiz questions, submission, and progress updates
using Microsoft.AspNetCore.Mvc;
using CodeWebsiteProject;

namespace CodeWesbite.MVC.Controllers;

public class QuizController : Controller
{
    private List<Quiz> quizzes;
    private IProgressRepo _progressRepo;
    private int userId = 1;


    // Constructor: Injects the progress repository and initializes quiz questions
    public QuizController(IProgressRepo progressRepo)
    {
        _progressRepo = progressRepo;
        quizzes = new List<Quiz>
        {
            new Quiz
            {
                Question = "C# is not an object-oriented programming language?", Options = new[] { "True", "False\n" },
                CorrectOption = 1
            },
            new Quiz
            {
                Question = "Who developed C#?", Options = new[] { "James Gosling", "Guido Rossum", "Andres Hejlisberg\n" }, CorrectOption = 2
            },
            new Quiz
            {
                Question = "What are value types?", Options = new []{"Value types store references to their data","Value types hold no data","Value types hold the entire program's code","Value types directly contain their data"}, CorrectOption = 3
            },
            new Quiz
            {
                Question = "For reference types can two variables reference the same reference?", Options = new []{"True","False"}, CorrectOption = 0
            },
            new Quiz
            {
                Question = "What is a method?", Options = new []{"A method provides the HTML statements","A method doesn't mean anything in coding","A method is a code block that contains a series of statements"}, CorrectOption = 2
            }

        };
    }
    // Displays the quiz question based on question index
    public IActionResult Index(int questionIndex = 0)
    {
        if (questionIndex >= quizzes.Count || questionIndex < 0)
        {
            return RedirectToAction("CompletedQuiz");
        }

        ViewData["QuestionIndex"] = questionIndex;
        return View(quizzes[questionIndex]);
    }
    // Submits the answer, checks if it is correct, and updates progress
    public IActionResult SubmitAnswer(int questionIndex, int selectedChoice)
    {
        var progress = _progressRepo.GetProgress(userId) ?? new Progress { UserId = userId };
        if (questionIndex >= quizzes.Count)
        {
            return RedirectToAction("CompletedQuiz");
        }

        var quiz = quizzes[questionIndex];
        
        // Check if the selected choice is correct
        if (selectedChoice == quiz.CorrectOption)
        {
            progress.CompletedQuiz(); // Marks the quiz as completed
            _progressRepo.UpdateProgress(userId, progress); // Updates progress in the database

            // If all quizzes are completed, redirect to the CompletedQuiz view
            if (questionIndex + 1 >= quizzes.Count)
            {
                return RedirectToAction("CompletedQuiz");
            }
            else 
            {
                // Show feedback for a correct answer and load the next question
                ViewBag.Feedback = "Correct!";
                ViewBag.FeedbackClass = "correct";
                ViewData["QuestionIndex"] = questionIndex + 1;
                return View("Index", quizzes[questionIndex + 1]);
                
            }
        }
        else
        {
            // Show feedback for an incorrect answer and stay on the same question
            ViewBag.Feedback = $"Option {selectedChoice + 1} is incorrect. Please try again.";
            ViewBag.FeedbackClass = "incorrect";
            ViewData["QuestionIndex"] = questionIndex; // Stay on the same question
            return View("Index", quiz);
        }
    }

    // Displays the quiz completion view
    public IActionResult CompletedQuiz()
    {
        return View();
    }
}