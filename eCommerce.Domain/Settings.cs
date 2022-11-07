using eCommerce.Domain.Countries;

namespace eCommerce.Domain;

public class Settings
{
    public Country BusinessCountry { get; protected set; }

    public Settings(Country businessCountry)
    {
        BusinessCountry = businessCountry;
    }
}
