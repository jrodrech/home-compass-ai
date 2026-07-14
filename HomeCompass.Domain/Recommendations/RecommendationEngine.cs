namespace HomeCompass.Domain.Recommendations;

using HomeCompass.Domain.Buyers;
using HomeCompass.Domain.Properties;

public sealed class RecommendationEngine
{
    public RecommendationScore Evaluate(
        BuyerProfile buyer,
        Property property)
    {
        ArgumentNullException.ThrowIfNull(buyer);
        ArgumentNullException.ThrowIfNull(property);

        var score = 0;

        if (property.Price <= buyer.FinancialProfile.MaximumBudget)
        {
            score += 40;
        }

        if (property.Bedrooms >= buyer.MinimumBedrooms)
        {
            score += 30;
        }

        if (!buyer.NeedsAccessibilityFeatures ||
            property.HasAccessibilityFeatures)
        {
            score += 30;
        }

        return new RecommendationScore(score);
    }
}