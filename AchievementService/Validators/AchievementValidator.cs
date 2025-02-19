using System.Data;
using Microsoft.Data.SqlClient;
using AchievementService.Models;
using AchievementService.Repositories;

namespace AchievementService.Validators
{
    public abstract class AchievementValidator
    {
        private readonly string _dsn;

        protected AchievementValidator(string dsn)
        {
            _dsn = dsn;
        }
        
        public abstract bool Validate(Achievement achievement, UserAchievement? userAchievement, UserAction userAction);

        protected void CreateUserAchievement(int value, DateTime achievementDate, int userId, int achievementId)
        {
            // Create new achievement for this user.
            UserAchievement userAchievement = new UserAchievement
            {
                // Save values.
                CurrentValue = value,
                AchievementDate = achievementDate,
                UserId = userId,
                AchievementId = achievementId
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

        protected void UpdateUserAchievement(UserAchievement userAchievement)
        {
            // Create Repository.
            UserAchievementRepository repository = new UserAchievementRepository();
                
            // Use connection context.
            using (IDbConnection con = new SqlConnection(_dsn))
            {
                con.Open();

                repository.Update(con, userAchievement);
            }
        }
    }
}