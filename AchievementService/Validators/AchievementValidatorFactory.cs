namespace AchievementService.Validators;

public static class AchievementValidatorFactory
{
    public static IAchievementValidator? CreateAchievementValidator(int validatorType, string dsn)
    {
        // Based on the validator type, return a specific validator.
        switch (validatorType)
        {
            case 1000:
                return new ThresholdValidator(dsn);
            case 1001:
                return new TimeOfDayRangeValidator(dsn);
            case 1002:
                return new DayOfWeekValidator(dsn);
            case 1003:
                return new DayOfYearValidator(dsn);
            default:
                return null;
        }
    }
}