using Entities;
using ServiceContracts;
using ServiceContracts.dto;
using Services.Helpers;
using System.ComponentModel.DataAnnotations;


namespace Services
{
    public class PersonService:IPersonService
    {

        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;
        public PersonService()
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();


        }

        // here private because this method will be used only inside this PersonService class and it is made reusalbe because in future we might need it for different methods as well

        private PersonResponse ConvertPersonToPersonResponse_WithCountryName(Person person)
        {
            // here whenever we want to convert person domain to personresponse then we have to call method as well as we also need to call getcountrybyid method of countrieservice
            //so we make one reusable method for that 

            // here person ma aba sabai domain model ma vako properties xa esbata k k filter garne vanne kura convertpersontoresponse method le handle garne ho
            PersonResponse personResponse = person.ConvertPersonToPersonResponse();

            personResponse.CountryName = _countriesService.GetCountryById(person.CountryId)?.CountryName;

            // or alternative method
            //CountryResponse? countryResponse = _countriesService.GetCountryById(person.CountryId);
            //personResponse.CountryName = countryResponse?.CountryName;

            return personResponse;
        }
     

      public  PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            //perfrom validations for test cases

            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));

            }

            // since here we are only validation less no of properties so for properties this one if statement is enough but if we validate other 
            // properteis as well then we have to write separate if statement for each property


            //if (string.IsNullOrEmpty(personAddRequest.PersonName) || string.IsNullOrEmpty(personAddRequest.Email))
            //{
            //    throw new ArgumentException("PersonName and Email cannot be null or empty");
            //}



            // using new validation technique

            // making this validation reusable so that in future it can also be used for other service as well 


            //ValidationContext validationContext = new ValidationContext(personAddRequest);

            //// here this list is to collect all the validation results either error or success
            //List<ValidationResult> validationREsults = new List<ValidationResult>();

            //bool isValid = Validator.TryValidateObject(personAddRequest, validationContext, validationREsults, true);
            //// here if we dont give true then it only validate the properties which have validation attribute required but since we want to validate all properties which dont have req also


            //if (!isValid)
            //{
            //        // collect all error messages from validationresults list
            //    string errorMessages = string.Join("; ", validationREsults.Select(result => result.ErrorMessage));
            //    throw new ArgumentException(errorMessages);
            //}


            // now calling the helper function

            ValidationHelper.ValidateModelProperties(personAddRequest);

            // since its return type is void so if everything is valid then it will simply come out of the method otherwise it will throw exception






            // if everything is valid the convert dto to domain model

            // here hamro converttoperson method ma jun jun properties xa tyo already aba person ma initialze vaisakyo but since person is of Person type it can also access new
            // other properties of Person model class
            Person person = personAddRequest.ConvertToPerson();

            //2. generate guid id
            person.PersonId = Guid.NewGuid();
            person.CountryId = Guid.NewGuid();

            //3. adding person to the list
            _persons.Add(person);

            //4. convert the domain model person to personresponse dto

            return ConvertPersonToPersonResponse_WithCountryName(person);
            

        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(person => person.ConvertPersonToPersonResponse()).ToList();
        }
    }

}
