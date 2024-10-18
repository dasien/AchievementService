namespace AchievementService.Validators;

public static class AchievementValidatorFactory
{
    public static IAchievementValidator CreateAchievementValidator(int validatorType, string dsn)
    {
        // Based on the validator type, return a specific validator.
        switch (validatorType)
        {
            case 1000:
            case 1001:
                return new ThresholdValidator(dsn);
                break;
            default:
                return null;
        }
    }
}