using System.Data;
using Microsoft.Data.SqlClient;
using AchievementService.Models;
using AchievementService.Repositories;

namespace AchievementService.Validators;

public class DayOfYearValidator : IAchievementValidator
{
    private string _dsn;
    
    public DayOfYearValidator(string dsn)
    {
        _dsn = dsn;
    }

    public bool Validate(Achievement achievement, UserAchievement? userAchievement, UserAction userAction)
    {
        // Temp flag to indicate if the user met achievement.
        bool achievementMet = false;
        
        // Check to see if the user has this achievement already.
        if (userAchievement == null)
        {
            // Check to see if the julian dates match.
            // TODO: Need to account for leap years with ActionDate.IsLeapYear
            if (achievement.SingleValue == userAction.ActionDate.DayOfYear)
            {
                achievementMet = true;
                
                // Create new achievement for this user.
                userAchievement = new UserAchievement
                {
                    // Save values.
                    CurrentValue = 1,
                    AchievementDate = DateTime.Now,
                    UserId = userAction.UserId,
                    AchievementId = achievement.Id
                };

                // Insert new achievement for user.
                UserAchievementRepository repository = new UserAchievementRepository();
                
                // Use connection context.
                using (IDbConnection con = new SqlConnection(_dsn))
                {
                    con.Open();

                    repository.Insert(con, userAchievement);
                }
            }
        }
        
        return achievementMet;
    }
}