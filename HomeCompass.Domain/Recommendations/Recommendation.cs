namespace HomeCompass.Domain.Recommendations;

public class Recommendation
{
    public Guid Id { get; private set; }

    public Guid BuyerProfileId { get; private set; }

    public Guid PropertyId { get; private set; }

    public RecommendationScore MatchScore { get; private set; }

    public string Explanation { get; private set; }

    public RecommendationStatus Status { get; private set; }


    private Recommendation()
    {
        Explanation = string.Empty;
    }


    public Recommendation(
        Guid buyerProfileId,
        Guid propertyId,
        RecommendationScore matchScore,
        string explanation)
    {
        if (buyerProfileId == Guid.Empty)
            throw new ArgumentException(
                "Buyer profile is required.",
                nameof(buyerProfileId));

        if (propertyId == Guid.Empty)
            throw new ArgumentException(
                "Property is required.",
                nameof(propertyId));

        if (matchScore == null)
            throw new ArgumentNullException(
                nameof(matchScore));


        if (string.IsNullOrWhiteSpace(explanation))
            throw new ArgumentException(
                "Explanation is required.",
                nameof(explanation));


        Id = Guid.NewGuid();

        BuyerProfileId = buyerProfileId;
        PropertyId = propertyId;
        MatchScore = matchScore;
        Explanation = explanation.Trim();

        Status = RecommendationStatus.Generated;
    }
}
