using Entities;
using Services.Helpers;
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
        public PersonService(bool initialize = true)
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();

            if (initialize)
            {
              
                
                _persons.AddRange(new List<Person>()

                {
                    new Person()
                    {
                        PersonId = Guid.Parse("C321D7E8-F400-4CC2-815B-280946A07903"),
                        PersonName = "John Doe",
                        Email = "johndoe@email.com",
                        DateOfBirth = new DateTime(1990, 5, 15),
                        Gender = GenderType.Male.ToString(),
                        Address = "Kathmandu",
                        CountryId = Guid.Parse("B7078C84-62DD-4551-BA74-DAD88E597492"),
                        
                    },

                    new Person()
                    {
                        PersonId = Guid.Parse("072413C7-2185-4E42-8259-823C03697463"),
                        PersonName = "Anita Sharma",
                        Email = "anita.sharma@email.com",
                        DateOfBirth = new DateTime(1992, 3, 10),
                        Gender = GenderType.Female.ToString(),
                        Address = "Delhi",
                        CountryId = Guid.Parse("A055FA40-94C0-43AE-83B0-86138B267297") // India
                    },

                    new Person()
                    {
                        PersonId = Guid.Parse("8107934E-75D4-4F75-B630-6B88F6777447"),
                        PersonName = "Tenzin Dorji",
                        Email = "tenzin.dorji@email.com",
                        DateOfBirth = new DateTime(1988, 11, 22),
                        Gender = GenderType.Male.ToString(),
                        Address = "Thimphu",
                        CountryId = Guid.Parse("C85C09FF-9DA2-44F8-AA86-59502FC3737F") // Bhutan
                    },

                    new Person()
                    {
                        PersonId = Guid.Parse("D0A8EE7C-9DF4-4277-862E-BF418C953E81"),
                        PersonName = "Ahmed Khan",
                        Email = "ahmed.khan@email.com",
                        DateOfBirth = new DateTime(1985, 8, 5),
                        Gender = GenderType.Male.ToString(),
                        Address = "Lahore",
                        CountryId = Guid.Parse("07481067-C679-4A48-9823-4B04F7E353E0") // Pakistan
                    },

                    new Person()
                    {
                        PersonId = Guid.Parse("CBF1B428-A099-489D-A66A-6023514FA346"),
                        PersonName = "Rahima Begum",
                        Email = "rahima.begum@email.com",
                        DateOfBirth = new DateTime(1995, 1, 18),
                        Gender = GenderType.Female.ToString(),
                        Address = "Dhaka",
                        CountryId = Guid.Parse("FEFC9D80-FB10-4114-AC9C-3F0F9E999974") // Bangladesh
                    },

                    new Person()
                    {
                        PersonId = Guid.Parse("8E9482B0-4440-45CC-A4BF-641327C40440"),
                        PersonName = "Nimal Perera",
                        Email = "nimal.perera@email.com",
                        DateOfBirth = new DateTime(1991, 9, 30),
                        Gender = GenderType.Male.ToString(),
                        Address = "Colombo",
                        CountryId = Guid.Parse("7DA058F2-DFBE-446B-BD9F-C68338399AED") // Sri Lanka
                    },

                    new Person()
                    {
                        PersonId = Guid.Parse("A6A41716-C23B-4772-BB40-0DA9AD0606E8"),
                        PersonName = "Aisha Ali",
                        Email = "aisha.ali@email.com",
                        DateOfBirth = new DateTime(1993, 7, 12),
                        Gender = GenderType.Female.ToString(),
                        Address = "Male",
                        CountryId = Guid.Parse("57643CCC-1371-4445-B92C-C74A6E428ED1") // Maldives
                    },


                });
            }
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
            return _persons.Select(person => ConvertPersonToPersonResponse_WithCountryName(person)).ToList();
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

            if(personUpdateReqObj.PersonId ==Guid.Empty || !_persons.Any(person => person.PersonId == personUpdateReqObj.PersonId))
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
            Person personToBeUpdated = _persons.First(person => person.PersonId == personUpdateReqObj.PersonId);

            personToBeUpdated.PersonName = personUpdateReqObj.PersonName != null ? personUpdateReqObj.PersonName : personToBeUpdated.PersonName;
            personToBeUpdated.Address = personUpdateReqObj.Address != null ? personUpdateReqObj.Address : personToBeUpdated.Address;
            personToBeUpdated.CountryId = personUpdateReqObj.CountryId != null ? personUpdateReqObj.CountryId : personToBeUpdated.CountryId;
            personToBeUpdated.DateOfBirth = personUpdateReqObj.DateOfBirth != null ? personUpdateReqObj.DateOfBirth : personToBeUpdated.DateOfBirth;
            personToBeUpdated.Email = personUpdateReqObj.Email != null ? personUpdateReqObj.Email : personToBeUpdated.Email;


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

            if(personId==Guid.Empty || !_persons.Any(person => person.PersonId == personId))
            {
                throw new ArgumentException("invalid person id");
            }

            //3. if everything is valid then delete the person from the list 

            Person matchedPerson = _persons.First(person => person.PersonId == personId);

            //_persons.RemoveAll(person=>person.PersonId==matchedPerson.PersonId);

            _persons.Remove(matchedPerson);

            return matchedPerson.ConvertPersonToPersonResponse();

        }

        #endregion
    }
}


