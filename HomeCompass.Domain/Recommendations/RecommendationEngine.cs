namespace HomeCompass.Domain.Recommendations;

using HomeCompass.Domain.Buyers;
using HomeCompass.Domain.Properties;

public sealed class RecommendationEngine
{
    private const int BudgetWeight = 40;
    private const int BedroomWeight = 30;
    private const int AccessibilityWeight = 30;

    public RecommendationScore Evaluate(
        BuyerProfile buyer,
        Property property)
    {
        ArgumentNullException.ThrowIfNull(buyer);
        ArgumentNullException.ThrowIfNull(property);

        var score = 0;

        if (property.AskingPrice <= buyer.FinancialProfile.MaximumBudget)
        {
            score += BudgetWeight;
        }

        if (property.Bedrooms >= buyer.MinimumBedrooms)
        {
            score += BedroomWeight;
        }

        if (!buyer.NeedsAccessibilityFeatures ||
            property.HasAccessibilityFeatures)
        {
            score += AccessibilityWeight;
        }

        return new RecommendationScore(score);
    }
}