using ContactsManager.Utils.WrapperModel;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.dto;
using ServiceContracts.enums;

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

        public IActionResult Index(string searchByForm,string? searchStringForm,string sortBy=nameof(PersonResponse.PersonName), SortOrderOption sortOrderOption=SortOrderOption.ascending)
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

            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrderOption = sortOrderOption.ToString();

           List<PersonResponse> sortedPersons= _personService.GetSortedPersons(allPersons, sortBy, sortOrderOption);

            return View(sortedPersons);
        }

        [Route("/persons/create")]
        [HttpGet]  // inorder to only recieve the get request

        public IActionResult Create()
        {
            List<CountryResponse> allCountries=_countriesService.GetAllCountries();
            ViewBag.countries = allCountries;
            ViewBag.genderEnums = Enum.GetValues<GenderType>();
            return View();
        }

        [Route("/persons/create")]
        [HttpPost]

        public IActionResult Create(PersonAddRequest addPersonRequest)
        {
            if (!ModelState.IsValid)
            {
                // if there is error message then we should return the same view page like above with
                // error messages

                List<CountryResponse> allCountries = _countriesService.GetAllCountries();
                ViewBag.countries = allCountries;
                ViewBag.genderEnums = Enum.GetValues<GenderType>();
                ViewBag.errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                // this will return us the list of Ienumerable<string> containing the error messages
               

                

                return View();


            }
            _personService.AddPerson(addPersonRequest);

            return RedirectToAction("Index","Person");
        }


        [HttpGet]
        [Route("[controller]/[action]/{personId}")]
        // /Person/Update/3456-7890-1234-56789abcdef0

        public IActionResult Update(Guid personId)
        {
           PersonResponse? personToBeUpdated=   _personService.GetPersonById(personId);
            if(personToBeUpdated == null)
            {
                return RedirectToAction("Index", "person");
            }

            PersonUpdateRequest? personUpdateRequest = personToBeUpdated?.ConvertToPersonUpdateRequest();

            ViewBag.genderEnums = Enum.GetValues<GenderType>();
            ViewBag.countries = _countriesService.GetAllCountries();
            

            return View(personUpdateRequest);
        }



        // now to handle the updated post req to 

        [HttpPost]
        [Route("[controller]/[action]/{personId}")]

        public IActionResult Update(PersonUpdateRequest personUpdateReq_From_Form,Guid personId)
        {
            // since here we are not recieving PersonId to display the update page of which person so we must pass it from 
            // razor page as a hidden feild here

            PersonResponse? personResponse = _personService.GetPersonById(personId);

            if (personResponse == null)
            {

                return RedirectToAction("Index", "person");

            }
            if (ModelState.IsValid)
            {
                _personService.UpdatePersonDetails(personUpdateReq_From_Form);
                return RedirectToAction("Index", "person");
            }
            else
            {
                ViewBag.genderEnums= Enum.GetValues<GenderType>();
                ViewBag.countries = _countriesService.GetAllCountries();
                ViewBag.errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();


                return View();

            }
                
        }

        [HttpGet]
        [Route("[controller]/[action]/{personId}")]


        public IActionResult Delete(Guid PersonId)
        {
            PersonResponse? personResponse = _personService.GetPersonById(PersonId);

            if (personResponse == null)
            {
                return RedirectToAction("Index", "person");
            }

            ViewBag.persondetails = new
            {
                name = personResponse.PersonName,
                id = personResponse.PersonId,
            };
            return View();
        }

        [HttpPost]
        [Route("[controller]/[action]/{personId}")]

        // here we are adding confirmation just to make this delete action different thatn above delete with httpget 
        public IActionResult Delete(Guid personId,string? confirmation)
        {
            PersonResponse deletedPerson = _personService.GetPersonById(personId);
            if (deletedPerson == null)
            {
                return RedirectToAction("Index", "person");
            }
            // if user clicks on confirm button
            _personService.DeletePersonById(personId);
            return RedirectToAction("Index", "person");
        }

    }
}
