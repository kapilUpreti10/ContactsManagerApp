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

        public IActionResult Index()
        {
            List<PersonResponse>allPersons=_personService.GetAllPersons();
            List<CountryResponse> allCountries = _countriesService.GetAllCountries();


            //var WrapperObj = new WrapperModel()
            //{
            //    PersonDetails= allPersons,
            //    CountryDetails = allCountries

            //};



            return View(allPersons);
        }
    }
}
