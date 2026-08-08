using HomeCompass.Domain.Recommendations;
using HomeCompass.Domain.Tests.Builders;

namespace HomeCompass.Domain.Tests.Recommendations;

public class RecommendationEngineTests
{
    [Fact]
    public void Evaluate_ShouldScore100_WhenAllRequirementsAreSatisfied()
    {
        // Arrange
        var buyer = new BuyerProfileBuilder().Build();

        var property = new PropertyBuilder().Build();

        var engine = new RecommendationEngine();

        // Act
        var score = engine.Evaluate(buyer, property);

        // Assert
        Assert.Equal(100, score.Value);
        Assert.True(score.IsStrongMatch);
    }

    [Fact]
    public void Evaluate_ShouldScore60_WhenBudgetRequirementIsNotSatisfied()
    {
        // Arrange
        var buyer = new BuyerProfileBuilder().Build();

        var property = new PropertyBuilder()
            .WithPrice(180_000m)
            .Build();

        var engine = new RecommendationEngine();

        // Act
        var score = engine.Evaluate(buyer, property);

        // Assert
        Assert.Equal(60, score.Value);
        Assert.True(score.IsGoodMatch);
    }

    [Fact]
    public void Evaluate_ShouldScore70_WhenBedroomRequirementIsNotSatisfied()
    {
        // Arrange
        var buyer = new BuyerProfileBuilder().Build();

        var property = new PropertyBuilder()
            .WithBedrooms(2)
            .Build();

        var engine = new RecommendationEngine();

        // Act
        var score = engine.Evaluate(buyer, property);

        // Assert
        Assert.Equal(70, score.Value);
        Assert.True(score.IsGoodMatch);
    }

    [Fact]
    public void Evaluate_ShouldScore70_WhenAccessibilityRequirementIsNotSatisfied()
    {
        // Arrange
        var buyer = new BuyerProfileBuilder().Build();

        var property = new PropertyBuilder()
            .WithoutAccessibility()
            .Build();

        var engine = new RecommendationEngine();

        // Act
        var score = engine.Evaluate(buyer, property);

        // Assert
        Assert.Equal(70, score.Value);
        Assert.True(score.IsGoodMatch);
    }

    [Fact]
    public void Evaluate_ShouldScore30_WhenOnlyAccessibilityRequirementIsSatisfied()
    {
        // Arrange
        var buyer = new BuyerProfileBuilder().Build();

        var property = new PropertyBuilder()
            .WithBedrooms(1)
            .WithPrice(500_000m)
            .Build();

        var engine = new RecommendationEngine();

        // Act
        var score = engine.Evaluate(buyer, property);

        // Assert
        Assert.Equal(30, score.Value);
        Assert.True(score.IsWeakMatch);
    }

    [Fact]
    public void Evaluate_ShouldThrowArgumentNullException_WhenBuyerIsNull()
    {
        // Arrange
        var property = new PropertyBuilder().Build();

        var engine = new RecommendationEngine();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            engine.Evaluate(null!, property));
    }

    [Fact]
    public void Evaluate_ShouldThrowArgumentNullException_WhenPropertyIsNull()
    {
        // Arrange
        var buyer = new BuyerProfileBuilder().Build();

        var engine = new RecommendationEngine();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            engine.Evaluate(buyer, null!));
    }
}