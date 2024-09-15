namespace CodeWebsiteProject;

public interface IProgressRepo
{
    //Get progress for specified user
    Progress GetProgress(int userId);
    //Create
    public void CreateProgress(int userId);
    //Update
    public void UpdateProgress(int userId, Progress progress);
    
}