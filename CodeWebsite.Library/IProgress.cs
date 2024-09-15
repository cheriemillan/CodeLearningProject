namespace CodeWebsiteProject;

// Properties to store progress details
public interface IProgress 
{
    public int UserId { get; set; }
    public int LessonsCompleted { get; set; }
    public int QuizzesPassed { get; set; }
    public int TestsPassed { get; set; }

    //Methods to update progress
    void CompletedQuiz();
    void CompletedTest();
    void CompletedLesson();
}