namespace HomeCompass.Domain.Buyers;

public sealed record FinancialProfile
{
    public decimal MaximumBudget { get; }

    public decimal AvailableDownPayment { get; }

    public FinancialProfile(
        decimal maximumBudget,
        decimal availableDownPayment)
    {
        if (maximumBudget <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(maximumBudget));

        if (availableDownPayment < 0)
            throw new ArgumentOutOfRangeException(
                nameof(availableDownPayment));

        if (availableDownPayment > maximumBudget)
            throw new ArgumentException(
                "Down payment cannot exceed maximum budget.");

        MaximumBudget = maximumBudget;
        AvailableDownPayment = availableDownPayment;
    }
}
