using HomeCompass.Domain.Common;
using HomeCompass.Domain.Properties;
using HomeCompass.Domain.Tests.TestData;

namespace HomeCompass.Domain.Tests.Builders;

public sealed class PropertyBuilder
{
    private Address _address =
        new(
            TestDefaults.Street,
            TestDefaults.City,
            TestDefaults.State,
            TestDefaults.PostalCode);

    private int _bedrooms = TestDefaults.MinimumBedrooms;

    private decimal _askingPrice = TestDefaults.AskingPrice;

    private bool _hasAccessibilityFeatures =
        TestDefaults.HasAccessibilityFeatures;

    public static PropertyBuilder Default()
    {
        return new PropertyBuilder();
    }

    public PropertyBuilder WithAddress(Address address)
    {
        _address = address;
        return this;
    }

    public PropertyBuilder WithPrice(decimal askingPrice)
    {
        _askingPrice = askingPrice;
        return this;
    }

    public PropertyBuilder WithBedrooms(int bedrooms)
    {
        _bedrooms = bedrooms;
        return this;
    }

    public PropertyBuilder WithAccessibility(bool hasAccessibilityFeatures)
    {
        _hasAccessibilityFeatures = hasAccessibilityFeatures;
        return this;
    }

    public PropertyBuilder WithoutAccessibility()
    {
        _hasAccessibilityFeatures = false;
        return this;
    }

    public Property Build()
    {
        return new Property(
            _address,
            _bedrooms,
            _askingPrice,
            _hasAccessibilityFeatures);
    }
}