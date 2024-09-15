namespace CodeWebsiteProject;


public class MainMenu
{
    // List to store lessons, quizzes, and tests
    private List<Lesson> lessons = new List<Lesson>();
    private List<Quiz> quizzes = new List<Quiz>();
    private List<Test> tests = new List<Test>();
    // Stores progress for the user
    private Progress progress = new Progress();
    // Interface for accessing the progress repository
    private IProgressRepo _progressRepo;
    // User ID of the currently logged-in user (example: hardcoded to 1)
    private int userId = 1;

   
}