namespace AchievementService.Models;

public class UserAction
{
    public int UserId { get; set; }
    public UserActionType UserActionType { get; set; }
    public DateTime ActionDate { get; set; }

    public UserAction(int userId, UserActionType userActionType, DateTime actionDate)
    {
        UserId = userId;
        UserActionType = userActionType;
        ActionDate = actionDate;
    }
}