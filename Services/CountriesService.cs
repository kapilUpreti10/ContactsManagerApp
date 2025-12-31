
using ServiceContracts;
using ServiceContracts.dto;
using Entities;
namespace Services
{
    public class CountriesService:ICountriesService
    {

        //private readonly List<Country> _countries;
        // now instead of using _countries we will use dbcontext to perform crude operation ie _db.dbset name 

        private readonly ContactsManagerDbContext _db;


        public CountriesService(ContactsManagerDbContext contactsManagerDbContext)
        {
            //_countries = new List<Country>();

            _db = contactsManagerDbContext;

            // we already provided the seed data in dbcontext so no need to add here again
            //if (initialize)
            //{
            //    _countries.AddRange(new List<Country>()
            //    {

            //        new Country(){Id=Guid.Parse("B7078C84-62DD-4551-BA74-DAD88E597492"),CountryName="Nepal"},
            //        new Country(){Id=Guid.Parse("A055FA40-94C0-43AE-83B0-86138B267297"),CountryName="India"},
            //        new Country(){Id=Guid.Parse("C85C09FF-9DA2-44F8-AA86-59502FC3737F"),CountryName="Bhutan"},
            //        new Country(){Id=Guid.Parse("07481067-C679-4A48-9823-4B04F7E353E0"),CountryName="Pakistan"},
            //        new Country(){Id=Guid.Parse("FEFC9D80-FB10-4114-AC9C-3F0F9E999974"),CountryName="Bangladesh"},
            //        new Country(){Id=Guid.Parse("7DA058F2-DFBE-446B-BD9F-C68338399AED"),CountryName="Srilanka"},
            //        new Country(){Id=Guid.Parse("57643CCC-1371-4445-B92C-C74A6E428ED1"),CountryName="Maldives"}

            //    });
            //}

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

            if(_db.Countries.Where(country => country.CountryName == countryRequest.CountryName).Any()){

                throw new ArgumentException("country name already exists");
            }








            //1. converting the AddCountryRequest dto to Country model

            Country country = countryRequest.ConvertToCountry();

            //2. generating the guid Id
            country.Id = Guid.NewGuid();


            //3. adding to the list/db

            _db.Countries.Add(country);
            _db.SaveChanges();


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

            Country IsValid_Country_Response=_db.Countries.FirstOrDefault(country => country.Id == countryId);

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

            return _db.Countries.Select(country => country.ConvertCountryToCountryResponse()).ToList();
        }

      


        #endregion

    }
}
