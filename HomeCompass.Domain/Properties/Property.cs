using HomeCompass.Domain.Common;

namespace HomeCompass.Domain.Properties;

public class Property
{
    public Guid Id { get; private set; }

    public Address Address { get; private set; }

    public int Bedrooms { get; private set; }

    public decimal AskingPrice { get; private set; }

    public bool HasAccessibilityFeatures { get; private set; }


    private Property()
    {
        Address = null!;
    }


    public Property(
        Address address,
        int bedrooms,
        decimal askingPrice,
        bool hasAccessibilityFeatures)
    {
        if (address is null)
            throw new ArgumentNullException(nameof(address));

        if (bedrooms < 1)
            throw new ArgumentOutOfRangeException(nameof(bedrooms));

        if (askingPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(askingPrice));


        Id = Guid.NewGuid();
        Address = address;
        Bedrooms = bedrooms;
        AskingPrice = askingPrice;
        HasAccessibilityFeatures = hasAccessibilityFeatures;
    }
}