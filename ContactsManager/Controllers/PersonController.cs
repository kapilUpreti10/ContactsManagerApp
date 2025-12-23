using ContactsManager.Utils.WrapperModel;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.dto;

namespace ContactsManager.Controllers
{
    public class PersonController:Controller
    {

        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;

        public PersonController(IPersonService personService,ICountriesService countriesService)
        {
            _personService = personService;
            _countriesService = countriesService;

        }

        [Route("/persons/index")]

        public IActionResult Index(string searchByForm,string? searchStringForm)
        {
            List<PersonResponse>allPersons=_personService.GetFilteredPersons(searchByForm,searchStringForm);
            List<CountryResponse> allCountries = _countriesService.GetAllCountries();


            //var WrapperObj = new WrapperModel()
            //{
            //    PersonDetails= allPersons,
            //    CountryDetails = allCountries

            //};

            ViewBag.SearchByFeildsName = new Dictionary<string, string>()
            {
                {  nameof(PersonResponse.PersonName),"Name" },
                {nameof(PersonResponse.Email) ,"Email"},
                {nameof(PersonResponse.Gender) ,"Gender"},
                {nameof(PersonResponse.Address) ,"Address"},
                {nameof(CountryResponse.CountryName) ,"Country"}

            };
            
            // this if for value to persist in searchbox and dropwdown after new view page reloads

            ViewBag.CurrentSearchString = searchStringForm;
            ViewBag.CurrentSearchBy = searchByForm;


            return View(allPersons);
        }
    }
}
