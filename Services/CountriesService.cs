
using ServiceContracts;
using ServiceContracts.dto;
using Entities;
namespace Services
{
    public class CountriesService:ICountriesService
    {

        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();

        }
        public CountryResponse AddCountry(AddCountryRequest? countryRequest)
        {



            //validatons::

            //1. check if the countryRequest is null or not if null throw ArgumentNullException

            if (countryRequest == null)
            {
                throw new ArgumentNullException(nameof(countryRequest));
            }

            //2. check if the CountryName property of countryRequest is null or empty or whitespace if yes throw ArgumentException

            if (countryRequest.CountryName == null)
            {
                throw new ArgumentException("CountryName cannot be null");
            }

            //3. check if the countryname is duplicate or not 

            if(_countries.Where(country => country.CountryName == countryRequest.CountryName).Any()){

                throw new ArgumentException("country name already exists");
            }








            //1. converting the AddCountryRequest dto to Country model

            Country country = countryRequest.ConvertToCountry();

            //2. generating the guid Id
            country.Id = Guid.NewGuid();


            //3. adding to the list/db

            _countries.Add(country);


            //4 returning the response as dto 

            return country.ConvertCountryToCountryResponse();

        
        }

    }
}
