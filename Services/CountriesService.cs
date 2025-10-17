
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

        #region AddCountry
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
        #endregion



        #region GetCountryById


        public CountryResponse? GetCountryById(Guid? countryId)
        {
            if (countryId == null)
            {
                return null;
            }

            if (countryId == Guid.Empty)
            {
                throw new ArgumentException("countryId cannot be empty");
            }

            // if countryId is neither null or empty then check if it is valid countryId or not

            Country IsValid_Country_Response=_countries.FirstOrDefault(country => country.Id == countryId);

            if (IsValid_Country_Response == null)
            {
                return null;
            }
            else
            {

                return IsValid_Country_Response.ConvertCountryToCountryResponse();

            }
        }

        #endregion


        #region GetAllCountries


        public List<CountryResponse> GetAllCountries()
        {
            // here since select return    IEnumerable so we have to convert it to list using ToList() method as our return type is List<CountryResponse>
            // here select is used to project each country object .Think of Select as “for each item, make a new version of it.”

            return _countries.Select(country => country.ConvertCountryToCountryResponse()).ToList();
        }

      


        #endregion

    }
}
