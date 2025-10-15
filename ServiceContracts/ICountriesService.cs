using ServiceContracts.dto;

namespace ServiceContracts
{

    /// <summary>
    /// represnts buisness logic for represnting country entity
    /// </summary>
    public interface ICountriesService
    {

        /// <summary>
        /// adds a country obj to the list of countries in db
        /// </summary>
        /// <param name="countryRequest"> country obj to add</param>
        /// <returns> returns the country obj after adding it </returns>

        CountryResponse AddCountry(AddCountryRequest? countryRequest);

    }
}
