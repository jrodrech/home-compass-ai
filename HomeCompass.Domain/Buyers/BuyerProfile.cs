namespace HomeCompass.Domain.Buyers;

public class BuyerProfile
{
    public Guid Id { get; private set; }
    public Guid UserAccountId { get; private set; }

    public int DesiredBedrooms { get; private set; }
    public decimal MaximumMonthlyHousingBudget { get; private set; }
    public bool RequiresVisualAccessibilityFeatures { get; private set; }

    private BuyerProfile() { }

    public BuyerProfile(
        Guid userAccountId,
        int desiredBedrooms,
        decimal maximumMonthlyHousingBudget,
        bool requiresVisualAccessibilityFeatures)
    {
        if (desiredBedrooms < 1)
            throw new ArgumentOutOfRangeException(nameof(desiredBedrooms));

        if (maximumMonthlyHousingBudget <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumMonthlyHousingBudget));

        Id = Guid.NewGuid();
        UserAccountId = userAccountId;
        DesiredBedrooms = desiredBedrooms;
        MaximumMonthlyHousingBudget = maximumMonthlyHousingBudget;
        RequiresVisualAccessibilityFeatures = requiresVisualAccessibilityFeatures;
    }
}
