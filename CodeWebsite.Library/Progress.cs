using System.Data;
using Dapper;

namespace CodeWebsiteProject;

// Class representing the Progress of a user, implements IProgress
public class Progress : IProgress
{
    // Properties for storing progress data
    public int UserId { get; set; }
    public int LessonsCompleted { get; set; }
    public int QuizzesPassed { get; set; }
    public int TestsPassed { get; set; }
    

    // Methods to update progress data
    public void CompletedQuiz()
    {
        QuizzesPassed++;
    }
    public void CompletedTest()
    {
        TestsPassed++;
    }

    public void CompletedLesson()
    {
        LessonsCompleted++;
    }
    
}
