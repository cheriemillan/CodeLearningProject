using System.Data;
using Dapper;

namespace CodeWebsiteProject;

// Class that implements IProgressRepo for managing progress data in the database
public class ProgressRepo: IProgressRepo
{
    private readonly IDbConnection _connection;

    // Constructor to initialize the connection object
    public ProgressRepo(IDbConnection connection)
    {
        _connection = connection;
    }
    
    // Method to retrieve progress data for a specific user
    public Progress GetProgress(int userId)
    {
        return _connection.QuerySingleOrDefault<Progress>("SELECT UserId, LessonsCompleted, QuizzesPassed, TestsPassed FROM Progress WHERE UserId = @UserId", new {userId = userId});
    }

    // Method to create a new progress entry for a user
    public void CreateProgress(int userId)
    {
         _connection.Execute(
            "INSERT INTO Progress (UserId, LessonsCompleted, QuizzesPassed, TestsPassed) VALUES (@UserId, 0, 0, 0)", new {UserId = userId});
    }

    // Method to update a user's progress
    public void UpdateProgress(int userId, Progress progress)
    {
        _connection.Execute("UPDATE Progress SET LessonsCompleted = @LessonsCompleted,QuizzesPassed = @QuizzesPassed, TestsPassed = @TestsPassed WHERE UserId = @UserId", new
        {
            LessonsCompleted = progress.LessonsCompleted, 
            QuizzesPassed = progress.QuizzesPassed,
            TestsPassed = progress.TestsPassed,
            UserId = userId
        });
    }
}