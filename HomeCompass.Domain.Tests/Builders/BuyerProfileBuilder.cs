using HomeCompass.Domain.Buyers;
using HomeCompass.Domain.Tests.TestData;

namespace HomeCompass.Domain.Tests.Builders;

public sealed class BuyerProfileBuilder
{
    private Guid _userId = Guid.NewGuid();

    private decimal _maximumBudget = TestDefaults.MaximumBudget;

    private decimal _downPayment = TestDefaults.DownPayment;

    private int _minimumBedrooms = TestDefaults.MinimumBedrooms;

    private bool _needsAccessibility = TestDefaults.NeedsAccessibility;

    public static BuyerProfileBuilder Default()
    {
        return new BuyerProfileBuilder();
    }

    public BuyerProfileBuilder WithUser(Guid userId)
    {
        _userId = userId;
        return this;
    }

    public BuyerProfileBuilder WithBudget(decimal budget)
    {
        _maximumBudget = budget;
        return this;
    }

    public BuyerProfileBuilder WithDownPayment(decimal downPayment)
    {
        _downPayment = downPayment;
        return this;
    }

    public BuyerProfileBuilder WithMinimumBedrooms(int bedrooms)
    {
        _minimumBedrooms = bedrooms;
        return this;
    }

    public BuyerProfileBuilder RequireAccessibility()
    {
        _needsAccessibility = true;
        return this;
    }

    public BuyerProfileBuilder WithoutAccessibilityRequirement()
    {
        _needsAccessibility = false;
        return this;
    }

    public BuyerProfile Build()
    {
        return new BuyerProfile(
            _userId,
            new FinancialProfile(
                _maximumBudget,
                _downPayment),
            _minimumBedrooms,
            _needsAccessibility);
    }
}