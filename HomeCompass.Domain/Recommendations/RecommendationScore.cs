namespace HomeCompass.Domain.Recommendations;

public sealed record RecommendationScore
{
    public int Value { get; }

    public RecommendationScore(int value)
    {
        if (value < 0 || value > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        Value = value;
    }

    public bool IsStrongMatch => Value >= 80;

    public bool IsGoodMatch => Value >= 60;

    public bool IsWeakMatch => Value < 60;
}