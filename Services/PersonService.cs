using Entities;
using ServiceContracts;
using ServiceContracts.dto;
using ServiceContracts.enums;
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


        #region PersonResponse
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
        #endregion


        #region AddPerson
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
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
        #endregion


        #region GetAllPersons

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(person => person.ConvertPersonToPersonResponse()).ToList();
        }
        #endregion


        #region GetPersonById

        public PersonResponse GetPersonById(Guid? personId)
        {
            if (personId == null)
            {
                throw new ArgumentNullException("person id cannot be null");
            }

            //Person foundPerson=_persons.FirstOrDefault(person => person.PersonId == personId);  

            // since if the id doesnt match it returns null so we should make it nullable type

            Person? foundPerson = _persons.FirstOrDefault(person => person.PersonId == personId);

            if (foundPerson == null) return null;
            return foundPerson.ConvertPersonToPersonResponse();
        }

        #endregion



        #region GetFilteredPersons

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();

            // If no search string, return all
            if (string.IsNullOrWhiteSpace(searchString))
                return allPersons;

            // Get property info dynamically
            var propertyInfo = typeof(PersonResponse).GetProperty(searchBy);

            // If property doesn't exist, return all (or throw exception if you want)
            if (propertyInfo == null)
                return allPersons;

            // Filter dynamically
            List<PersonResponse> filteredPersons = allPersons
                .Where(person =>
                {
                    var value = propertyInfo.GetValue(person);
                    return value != null &&
                           value.ToString()!.Contains(searchString, StringComparison.OrdinalIgnoreCase);
                })
                .ToList();

            return filteredPersons;
        }



        #endregion


        #region GetSortedPersons

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> filteredPersons, string? sortBy, SortOrderOption sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy)) return filteredPersons;



            List<PersonResponse> sortedPersons = (sortBy, sortOrder)
                switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOption.ascending) =>
                    filteredPersons.OrderBy(person => person.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.PersonName), SortOrderOption.descending) =>
                    filteredPersons.OrderByDescending(person => person.PersonName, StringComparer.OrdinalIgnoreCase).ToList(), // here we are converting the Ienumerable<T> to list  as
                                                                                                                               // ordeby returns ienumerable<T>

                (nameof(PersonResponse.Email), SortOrderOption.ascending) =>
                   filteredPersons.OrderBy(person => person.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOption.descending) =>
                    filteredPersons.OrderByDescending(person => person.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOption.ascending) =>
                    filteredPersons.OrderBy(person => person.DateOfBirth).ToList(),


                (nameof(PersonResponse.DateOfBirth), SortOrderOption.descending) =>
                    filteredPersons.OrderByDescending(person => person.DateOfBirth).ToList(),


                (nameof(PersonResponse.Age), SortOrderOption.ascending) =>
                    filteredPersons.OrderBy(person => person.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOption.descending) =>
                    filteredPersons.OrderByDescending(person => person.Age).ToList(),


                (nameof(PersonResponse.RecieveNewsLetters), SortOrderOption.ascending) =>
                    filteredPersons.OrderBy(person => person.RecieveNewsLetters).ToList(),

                (nameof(PersonResponse.RecieveNewsLetters), SortOrderOption.descending) =>
                    filteredPersons.OrderByDescending(person => person.RecieveNewsLetters).ToList(),

                _ => filteredPersons       // default case if no match found which is represented by _(underscore symbol)




            };

            return sortedPersons;
        }


        #endregion


        #region UpdatePersonDetails

        public PersonResponse UpdatePersonDetails(PersonUpdateRequest? personUpdateReqObj)
        {

            //1. check if the personUpdateReqObj is null or not

            if (personUpdateReqObj == null)
            {
              throw new ArgumentNullException(nameof(personUpdateReqObj));
            }

            //2. check if the personId is valid or not

            if(personUpdateReqObj.PersonId ==Guid.Empty || !_persons.Any(person => person.PersonId == personUpdateReqObj.PersonId))
            {
                throw new ArgumentException("Invalid PersonId");
            }


            //3. if everything is valid then update the person details 

            foreach (Person person in _persons)
            {
                if (person.PersonId == personUpdateReqObj.PersonId)
                {
                    person.PersonName = personUpdateReqObj.PersonName;
                    person.Address = personUpdateReqObj.Address;
                    person.CountryId = personUpdateReqObj.CountryId;
                    person.DateOfBirth = personUpdateReqObj.DateOfBirth;
                    person.Email = personUpdateReqObj.Email;
                    person.Gender = personUpdateReqObj.Gender.ToString();
                }
            }
            // return the update person as personresponse dto 
             Person updatedPerson=_persons.First(person=>person.PersonId== personUpdateReqObj.PersonId);

                return updatedPerson.ConvertPersonToPersonResponse();

            }
        #endregion
    }
}


