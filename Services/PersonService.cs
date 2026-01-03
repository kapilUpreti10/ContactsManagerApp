using Entities;
using Services.Helpers;
using ServiceContracts;
using ServiceContracts.dto;
using ServiceContracts.enums;
using Services.Helpers;
using System.ComponentModel.DataAnnotations;
using Entities.Migrations;


namespace Services
{
    public class PersonService:IPersonService
    {

        //private static readonly List<Person> _persons=new();
        private readonly ContactsManagerDbContext _db;
        private readonly ICountriesService _countriesService;
        public PersonService(ContactsManagerDbContext contactsManagerDbContext,ICountriesService countriesService)
        {
            //_persons = new List<Person>();
            //_countriesService = new CountriesService();

            //    if (initialize && _persons.Count==0)
            //    {


            //        _persons.AddRange(new List<Person>()

            //        {
            //            new Person()
            //            {
            //                PersonId = Guid.Parse("C321D7E8-F400-4CC2-815B-280946A07903"),
            //                PersonName = "John Doe",
            //                Email = "johndoe@email.com",
            //                DateOfBirth = new DateTime(1990, 5, 15),
            //                Gender = GenderType.Male.ToString(),
            //                Address = "Kathmandu",
            //                CountryId = Guid.Parse("B7078C84-62DD-4551-BA74-DAD88E597492"),

            //            },

            //           


            //        });
            //    }


            _db = contactsManagerDbContext;
            _countriesService = countriesService;
        }

        #region PersonResponse
        // here private because this method will be used only inside this PersonService class and it is made reusalbe because in future we might need it for different methods as well

        internal PersonResponse ConvertPersonToPersonResponse_WithCountryName(Person person)
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
            person.CountryId = Guid.NewGuid();  // since user selects a country from dropdown which already have countryId 
            // so we dont have to generate new country id here

            //3. adding person to the list
            //_db.Persons.Add(person);
            //_db.SaveChanges();

            // now instead of above add method and saveChanges we use the stored procedures
            _db.sp_CreatePerson(person);

            //4. convert the domain model person to personresponse dto

            return ConvertPersonToPersonResponse_WithCountryName(person);
            

        }
        #endregion


        #region GetAllPersons

        public List<PersonResponse> GetAllPersons()
        {
            //return _db.Persons.ToList().Select(person => ConvertPersonToPersonResponse_WithCountryName(person)).ToList();


            // here we will be using stored procedures to get all the persons 

            return _db.sp_GetAllThePersons().Select(person => ConvertPersonToPersonResponse_WithCountryName(person)).ToList();

            
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

            Person? foundPerson = _db.Persons.FirstOrDefault(person => person.PersonId == personId);

            if (foundPerson == null) return null;
            return ConvertPersonToPersonResponse_WithCountryName(foundPerson);
        }

        #endregion



        #region GetFilteredPersons

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> filteredPersons = allPersons;

            // If no search string, return all
            if (string.IsNullOrWhiteSpace(searchString))
                return allPersons;


            // method1:: using reflection to get property info dynamically
            /*
            // Get property info dynamically
            var propertyInfo = typeof(PersonResponse).GetProperty(searchBy,System.Reflection.BindingFlags.IgnoreCase); 
            // here we are ignoring the case sensitivity using bindingflags enum 

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

            */


            // method2: using switch expression 

            switch (searchBy)
            {
                case nameof(PersonResponse.PersonName):
                    filteredPersons = allPersons.Where(person => person.PersonName != null && person.PersonName!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonResponse.Email):
                    filteredPersons = allPersons.Where(person => person.Email != null && person.Email!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(PersonResponse.Gender):
                    filteredPersons = allPersons.Where(person => person.Gender != null && person.Gender!.Equals(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonResponse.Address):
                    filteredPersons = allPersons.Where(person => person.Address != null && person.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonResponse.CountryName):
                    filteredPersons=allPersons.Where(person=>person.CountryName!=null && person.CountryName!.Contains(searchString,StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
            }


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

                (nameof(PersonResponse.CountryName), SortOrderOption.ascending) =>
                filteredPersons.OrderBy(person => person.CountryName).ToList(),

                (nameof(PersonResponse.CountryName), SortOrderOption.descending) =>
               filteredPersons.OrderByDescending(person => person.CountryName).ToList(),

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

            // vallidate dto using a helper because we are outside the mvc pipeline as we dont have controller view so here we have to manually validate
            // but instead of writting that long code we have made validation helper class that helps to validate accroding the validation attributes
            // applied to the dto properties 

            ValidationHelper.ValidateModelProperties(personUpdateReqObj);

            //2. check if the personId is valid or not

            if(personUpdateReqObj.PersonId ==Guid.Empty || !_db.Persons.Any(person => person.PersonId == personUpdateReqObj.PersonId))
            {
                throw new ArgumentException("Invalid PersonId");
            }



            //3. if everything is valid then update the person details 

            //foreach (Person person in _persons)
            //{
            //    if (person.PersonId == personUpdateReqObj.PersonId)
            //    {
            //        person.PersonName = personUpdateReqObj.PersonName;
            //        person.Address = personUpdateReqObj.Address;
            //        person.CountryId = personUpdateReqObj.CountryId;
            //        person.DateOfBirth = personUpdateReqObj.DateOfBirth;
            //        person.Email = personUpdateReqObj.Email;
            //        person.Gender = personUpdateReqObj.Gender.ToString();
            //    }
            //}
            //// return the update person as personresponse dto 
            // Person updatedPerson=_persons.First(person=>person.PersonId== personUpdateReqObj.PersonId)
            //;


            // another method 

            // since Person is class which is of reference type so personToBeUpdated will have the actual reference fo the object in the _person list 
            Person personToBeUpdated = _db.Persons.First(person => person.PersonId == personUpdateReqObj.PersonId);

            personToBeUpdated.PersonName = personUpdateReqObj.PersonName != null ? personUpdateReqObj.PersonName : personToBeUpdated.PersonName;
            personToBeUpdated.Address = personUpdateReqObj.Address != null ? personUpdateReqObj.Address : personToBeUpdated.Address;
            personToBeUpdated.CountryId = personUpdateReqObj.CountryId != null ? personUpdateReqObj.CountryId : personToBeUpdated.CountryId;
            personToBeUpdated.DateOfBirth = personUpdateReqObj.DateOfBirth != null ? personUpdateReqObj.DateOfBirth : personToBeUpdated.DateOfBirth;
            personToBeUpdated.Email = personUpdateReqObj.Email != null ? personUpdateReqObj.Email : personToBeUpdated.Email;

            _db.SaveChanges();


            return ConvertPersonToPersonResponse_WithCountryName(personToBeUpdated);

            }
        #endregion


        #region DeletePersonById

        public PersonResponse DeletePersonById(Guid? personId)
        {

            //1. check if the person id null or not 

            if (personId == null)
            {
                throw new ArgumentNullException("id is null.. give the personid");
            }

            //2. check if the personid is valid or not

            if(personId==Guid.Empty || !_db.Persons.Any(person => person.PersonId == personId))
            {
                throw new ArgumentException("invalid person id");
            }

            //3. if everything is valid then delete the person from the list 

            Person matchedPerson = _db.Persons.First(person => person.PersonId == personId);

            //_persons.RemoveAll(person=>person.PersonId==matchedPerson.PersonId);

            _db.Persons.Remove(matchedPerson);
            _db.SaveChanges();

            return matchedPerson.ConvertPersonToPersonResponse();

        }

        #endregion
    }
}


