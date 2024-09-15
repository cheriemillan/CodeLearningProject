// TestsController handles the logic for displaying and submitting test questions
using CodeWebsiteProject;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeWesbite.MVC.Controllers;

public class TestsController : Controller
{
    private readonly IProgressRepo _progressRepo;
    private List<Test> tests;
    private int userId = 1;

    // Constructor that initializes the test list and the progress repository
    public TestsController(IProgressRepo progressRepo)
    {
        _progressRepo = progressRepo;
        tests = new List<Test>
        {
            new Test
            {
                TestQuestion = "Which of the following data types can store floating-point numbers?",
                TestOptions = new[] { "int", "double", "char", "bool" },
                TestCorrectOption = 1
            },
            
            new Test
            {
                TestQuestion = "Which organization is responsible for approving C# as a standard?",
                TestOptions = new[] { "ISO", "W3C", "ECMA", "IEEE" },
                TestCorrectOption = 2
            },
            new Test
            {
                TestQuestion = "What type of variable is used to store whole numbers in C#?",
                TestOptions = new[] { "float", "double", "int", "char" },
                TestCorrectOption = 2
            },
            new Test {
                TestQuestion = "Which version of C# is mentioned in the lesson?",
                TestOptions = new[] { "Version 6.0", "Version 7.0", "Version 7.2", "Version 8.0" },
                TestCorrectOption = 2
            },
            new Test { 
                TestQuestion = "What do reference types store?",
                TestOptions = new[] { "Their data", "References to their data", "Local variables", "Functions" },
                TestCorrectOption = 1
            },
            new Test
            {
                TestQuestion = "What kind of data can a double type store in C#?",
                TestOptions = new[] { "Whole numbers", "Boolean values", "Floating point numbers", "Characters" },
                TestCorrectOption = 2
            },
            new Test
            {
                TestQuestion = "How does a program execute a method in C#?",
                TestOptions = new[] { "By calling the method", "By assigning a variable", "By importing a library", "By closing the method" },
                TestCorrectOption = 0
            },
            new Test {
                TestQuestion = "How many parts is a method made up of?",
                TestOptions = new[] { "2", "5", "1", "A method doesn't have any parts to it" },
                TestCorrectOption = 1
            },
            new Test{
            TestQuestion = "Is this a method?\npublic int add(int a, int b)\n{\nbody of method\n}",
                           TestOptions = new[] {"Yes","No"},
                           TestCorrectOption = 0
            }
        };

    }

    // Index method to display a test question at the given index
    public IActionResult Index(int testIndex = 0)
    {
        // Redirect if the test index is out of bounds
        if (testIndex >= tests.Count || testIndex < 0)
        {
            return RedirectToAction("CompletedTest");
        }

        ViewData["TestIndex"] = testIndex; // Store the test index in the view
        return View(tests[testIndex]); // Return the test question for the given index
    }

    // Method to submit the answer for a test question
    public IActionResult SubmitAnswer(int testIndex, int selectedChoice)
    {
        // Validate if the selected choice or test index is out of bounds
        if (testIndex >= tests.Count || testIndex < 0 || selectedChoice < 0 || selectedChoice >= tests[testIndex].TestOptions.Length)
        {
            return RedirectToAction("CompletedTest");
        }
        
        var progress = _progressRepo.GetProgress(userId) ?? new Progress { UserId = userId };
        var test = tests[testIndex];  // Get the current test
        
        System.Diagnostics.Debug.WriteLine($"TestIndex: {testIndex}, SelectedChoice: {selectedChoice}, CorrectOption: {test.TestCorrectOption}");

        // Check if the answer is correct
        if (selectedChoice  == test.TestCorrectOption)
        {
            progress.CompletedTest();
            _progressRepo.UpdateProgress(userId, progress);
            
            // If all tests are completed, redirect to the "CompletedTest" view
            if (testIndex + 1 >= tests.Count)
            {
                return RedirectToAction("CompletedTest");
            }
            // Otherwise, show the next test question
            else
            {
                ViewBag.Feedback = "Correct!"; // Display feedback
                ViewBag.FeedbackClass = "correct";
                ViewData["TestIndex"] = testIndex + 1; // Increment the test index
                return View("Index", tests[testIndex + 1]); // Load the next test question
            }
        }
        else
        {
            // If the answer is incorrect, show feedback and reload the same question
            ViewBag.Feedback = $"Option {selectedChoice + 1} is incorrect. Please try again.";
            ViewBag.FeedbackClass = "incorrect";
            // Return the same question for retry
            ViewData["TestIndex"] = testIndex; // Keep the current test index
            return View("Index", test); // Return the same test question
        }
    }

    // Method to show the "Test Completed" view when all tests are finished
    public IActionResult CompletedTest()
    {
        return View();
    }
}