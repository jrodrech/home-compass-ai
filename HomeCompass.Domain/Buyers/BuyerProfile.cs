namespace HomeCompass.Domain.Buyers;

public class BuyerProfile
{
    public Guid Id { get; private set; }

    public Guid UserAccountId { get; private set; }

    public FinancialProfile FinancialProfile { get; private set; }

    public int MinimumBedrooms { get; private set; }

    public bool NeedsAccessibilityFeatures { get; private set; }

    private BuyerProfile()
    {
        FinancialProfile = null!;
    }

    public BuyerProfile(
        Guid userAccountId,
        FinancialProfile financialProfile,
        int minimumBedrooms,
        bool needsAccessibilityFeatures)
    {
        if (userAccountId == Guid.Empty)
            throw new ArgumentException(
                "User account is required.",
                nameof(userAccountId));

        if (financialProfile is null)
            throw new ArgumentNullException(
                nameof(financialProfile));

        if (minimumBedrooms < 1)
            throw new ArgumentOutOfRangeException(
                nameof(minimumBedrooms));

        Id = Guid.NewGuid();

        UserAccountId = userAccountId;
        FinancialProfile = financialProfile;
        MinimumBedrooms = minimumBedrooms;
        NeedsAccessibilityFeatures = needsAccessibilityFeatures;
    }

    public void UpdateFinancialProfile(FinancialProfile financialProfile)
    {
        ArgumentNullException.ThrowIfNull(financialProfile);

        FinancialProfile = financialProfile;
    }

    public void ChangeMinimumBedrooms(int minimumBedrooms)
    {
        if (minimumBedrooms < 1)
            throw new ArgumentOutOfRangeException(
                nameof(minimumBedrooms));

        MinimumBedrooms = minimumBedrooms;
    }

    public void RequireAccessibilityFeatures()
    {
        NeedsAccessibilityFeatures = true;
    }

    public void RemoveAccessibilityRequirement()
    {
        NeedsAccessibilityFeatures = false;
    }


}