using ServiceContracts.enums;
using Entities;
using ServiceContracts;
using ServiceContracts.dto;
using Services;
using Services.Helpers;
using System.Security.Cryptography.X509Certificates;
using Xunit.Sdk;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace FirstTest
{
    public class PersonServiceTest
    {

        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;

        public PersonServiceTest()
        {
            _personService = new PersonService();
            _countriesService = new CountriesService();
        }


        #region AddPerson

        //1. test if the PersonAddRequest is null then it should throw ArgumentNullException

        [Fact]
        public void AddPerson_NullPersonAddRequest()
        {
            //1. Arrange
            PersonAddRequest? personAddRequest = null;

            //2. Act & Assert

            Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(personAddRequest));
        }


        //2. test if the PersonName and email properties of PersonAddRequest are null or empty then it should throw ArgumentException


        [Fact]

        public void AddPerson_NullPersonDetails()
        {
            //1.Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = null,
                Email = null,
            };


            //2. Act & Assert

            Assert.Throws<ArgumentException>(() => _personService.AddPerson(personAddRequest));

        }


        //3. check if the PersonDetails is empty string
        [Fact]

        public void AddPerson_EmptyPersonDetails()
        {
            //1.Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = string.Empty,
                Email = string.Empty,
            };

            //2. Act & Assert
            Assert.Throws<ArgumentException>(() => _personService.AddPerson(personAddRequest));
        }


        //4. when valid PersonAddRequest is passed then it should return PersonResponse with same details

        [Fact]
        public void AddPerson_ValidPersonDetails()
        {
            PersonAddRequest? validPersonDetails = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "johnedoe@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "123 Main St, Anytown, USA",


                Gender = GenderType.Male,

            };

            //2.Act

            PersonResponse personResult = _personService.AddPerson(validPersonDetails);

            List<PersonResponse> expectedPersons = _personService.GetAllPersons();
            //3.Assert

            Assert.True(personResult.PersonId != Guid.Empty);
            Assert.NotNull(personResult.PersonName);
            Assert.NotNull(personResult.Email);
            Assert.Contains(personResult, expectedPersons);
        }



        #endregion


        #region GetPersonById


        //1. check if the id is null or not

        [Fact]

        public void GetPersonById_NullId()
        {
            //1. Arrange
            Guid? personId = null;

            //2. Act and Assert

            Assert.Throws<ArgumentNullException>(() => _personService.GetPersonById(personId));

        }

        //2. if we insert valid person id then we should get proper personresponse but to get valid personid we should add new person to the list 

        [Fact]

        public void GetPersonById_ValidId()
        {
            // since our addperson also contains teh country name for that we should supply a country for that we can use Addcountry request

            AddCountryRequest? addCountryName = new AddCountryRequest()
            {
                CountryName = "Japan"
            };

            CountryResponse validCountryWithId = _countriesService.AddCountry(addCountryName);  // since after calling addCountry it creates a valid country id which is returned in countryresponse


            PersonAddRequest? personRequest = new PersonAddRequest()
            {
                PersonName = "hello",
                Email = "hello@example.com",
                DateOfBirth = new DateTime(2003, 12, 20),
                Address = "new thimi",
                Gender = GenderType.Male,
                CountryId = validCountryWithId.Id



            };
            //2. ACt

            PersonResponse validResponsePerson = _personService.AddPerson(personRequest);
            PersonResponse? validPersonWithId = _personService.GetPersonById(validResponsePerson?.PersonId);

            //3. Assert
            Assert.NotNull(validPersonWithId);
            Assert.Equal(validResponsePerson, validPersonWithId); // since hamile jun person add gareko tei person we should we get when we search by id addPerson bata aayeko response validResponsePerson ho vane
                                                                  // validPersonWithId is the response obj we get 

        }


        #endregion



        #region GetAllPersons

        //1. it should return empty list when no person is added
        [Fact]

        public void GetAllPerson_EmptyPersonsList()
        {
            //1. act

            List<PersonResponse> personResponse = _personService.GetAllPersons();


            //2. assert

            Assert.Empty(personResponse);



        }

        //2. it should return all the list of person if the personlist is not empty

        [Fact]

        public void GetAllPersons_RetrunAllThePersons()
        {

            //1. arrange

            AddCountryRequest addCountry = new AddCountryRequest() { CountryName = "Nepal" };

            CountryResponse countryResponse = _countriesService.AddCountry(addCountry);


            PersonAddRequest personReqObj = new PersonAddRequest()
            {
                PersonName = "john doe",
                Email = "john@example.com",
                DateOfBirth = new DateTime(2003, 12, 20),
                Gender = GenderType.Male,
                Address = "thimi",
                CountryId = countryResponse.Id,

            };


            //PersonResponse personAddResponse=_personService.AddPerson(personReqObj);



            // adding another person in the list

            AddCountryRequest addCountry2 = new AddCountryRequest()
            {
                CountryName = "HongKong"
            };


            CountryResponse countryResponse2 = _countriesService.AddCountry(addCountry2);


            PersonAddRequest personReqObj2 = new PersonAddRequest()
            {
                PersonName = "Kiritka",
                Address = "Thailand",
                CountryId = countryResponse2.Id,
                Gender = GenderType.Female,
                Email = "Kritika@sample.com",
                DateOfBirth = new DateTime(2003, 12, 10),


            };

            //PersonResponse personAddResponse2 = _personService.AddPerson(personReqObj2);


            // up to now we have added two persons in the list 



            //2. Act

            //List<PersonResponse> get_all_persons=_personService.GetAllPersons();

            //Assert.Contains(personAddResponse2, get_all_persons);
            //Assert.Contains(personAddResponse, get_all_persons);




            // instead of performing repetitive task live above but for lage no of test it is not suitable so we can make list of <PersonAddRequest>

            List<PersonAddRequest> PersonAddRequests = new List<PersonAddRequest>()
            {

                personReqObj,personReqObj2
            };


            // to store the response we should make empty list


            List<PersonResponse> personResponses = new List<PersonResponse>();


            // now adding this person in the list using for each loop

            foreach (PersonAddRequest personAddReq in PersonAddRequests)
            {
                personResponses.Add(_personService.AddPerson(personAddReq));
            }


            // now finally assert

            List<PersonResponse> get_all_persons = _personService.GetAllPersons();

            foreach (PersonResponse personResponse in personResponses)
            {
                Assert.Contains(personResponse, get_all_persons);
            }
        }


        #endregion



        #region GetFilteredPersons




        //2. it should return all the list of person if the searchllist is empty

        [Fact]

        public void ReturnAllPerson_SearchListEmpty()
        {

            //1. arrange

            AddCountryRequest addCountry = new AddCountryRequest() { CountryName = "Nepal" };

            CountryResponse countryResponse = _countriesService.AddCountry(addCountry);


            PersonAddRequest personReqObj = new PersonAddRequest()
            {
                PersonName = "john doe",
                Email = "john@example.com",
                DateOfBirth = new DateTime(2003, 12, 20),
                Gender = GenderType.Male,
                Address = "thimi",
                CountryId = countryResponse.Id,

            };


            //PersonResponse personAddResponse=_personService.AddPerson(personReqObj);



            // adding another person in the list

            AddCountryRequest addCountry2 = new AddCountryRequest()
            {
                CountryName = "HongKong"
            };


            CountryResponse countryResponse2 = _countriesService.AddCountry(addCountry2);


            PersonAddRequest personReqObj2 = new PersonAddRequest()
            {
                PersonName = "Kiritka",
                Address = "Thailand",
                CountryId = countryResponse2.Id,
                Gender = GenderType.Female,
                Email = "Kritika@sample.com",
                DateOfBirth = new DateTime(2003, 12, 10),


            };

            //PersonResponse personAddResponse2 = _personService.AddPerson(personReqObj2);


            // up to now we have added two persons in the list 



            //2. Act

            //List<PersonResponse> get_all_persons=_personService.GetAllPersons();

            //Assert.Contains(personAddResponse2, get_all_persons);
            //Assert.Contains(personAddResponse, get_all_persons);




            // instead of performing repetitive task live above but for lage no of test it is not suitable so we can make list of <PersonAddRequest>

            List<PersonAddRequest> PersonAddRequests = new List<PersonAddRequest>()
            {

                personReqObj,personReqObj2
            };


            // to store the response we should make empty list


            List<PersonResponse> personResponses_from_addPerson = new List<PersonResponse>();


            // now adding this person in the list using for each loop

            foreach (PersonAddRequest personAddReq in PersonAddRequests)
            {
                personResponses_from_addPerson.Add(_personService.AddPerson(personAddReq));
            }


            // now finally assert

            List<PersonResponse> get_all_filtered_persons_from_search = _personService.GetFilteredPersons(nameof(Person.PersonName), "");

            foreach (PersonResponse personResponse in personResponses_from_addPerson)
            {
                Assert.Contains(personResponse, get_all_filtered_persons_from_search);
            }
        }








        //2. testcase :: here it should return filtered list of person based on searchby and searchstring


        //2. it should return all the list of person if the searchllist is empty

        [Fact]

        public void ReturnAllPerson_SearchIsNotEmpty()
        {

            //1. arrange

            AddCountryRequest addCountry = new AddCountryRequest() { CountryName = "Nepal" };

            CountryResponse countryResponse = _countriesService.AddCountry(addCountry);


            PersonAddRequest personReqObj = new PersonAddRequest()
            {
                PersonName = "john doe",
                Email = "john@example.com",
                DateOfBirth = new DateTime(2003, 12, 20),
                Gender = GenderType.Male,
                Address = "thimi",
                CountryId = countryResponse.Id,

            };


            //PersonResponse personAddResponse=_personService.AddPerson(personReqObj);



            // adding another person in the list

            AddCountryRequest addCountry2 = new AddCountryRequest()
            {
                CountryName = "HongKong"
            };


            CountryResponse countryResponse2 = _countriesService.AddCountry(addCountry2);


            PersonAddRequest personReqObj2 = new PersonAddRequest()
            {
                PersonName = "Kiritka",
                Address = "Thailand",
                CountryId = countryResponse2.Id,
                Gender = GenderType.Female,
                Email = "Kritika@sample.com",
                DateOfBirth = new DateTime(2003, 12, 10),


            };



            // instead of performing repetitive task live above but for lage no of test it is not suitable so we can make list of <PersonAddRequest>

            List<PersonAddRequest> PersonAddRequests = new List<PersonAddRequest>()
            {

                personReqObj,personReqObj2
            };


            // to store the response we should make empty list


            List<PersonResponse> personResponses_from_addPerson = new List<PersonResponse>();


            // now adding this person in the list using for each loop

            foreach (PersonAddRequest personAddReq in PersonAddRequests)
            {
                personResponses_from_addPerson.Add(_personService.AddPerson(personAddReq));
            }


            // now finally assert

            List<PersonResponse> get_all_filtered_persons_from_search = _personService.GetFilteredPersons(nameof(Person.PersonName), "kirtika");

            foreach (PersonResponse personResponse in personResponses_from_addPerson)
            {
                if (personResponse.PersonName != null)
                {


                    if (personResponse.PersonName!.ToLower().Contains("kirtika"))
                    {

                        Assert.Contains(personResponse, get_all_filtered_persons_from_search);
                    }
                }
            }
        }

        #endregion



        #region GetSortedPersons

        [Fact]

        //1. if we put sortbyorder as null it should throw argumentnull exception

        public void GetSortedPersons_NULLSortBy()
        {
            //1. Arrange

            List<PersonResponse> filteredPersons = _personService.GetAllPersons();

            string? sortByInput = null;

            //2. Act
            //
            List<PersonResponse> actualResponse = _personService.GetSortedPersons(filteredPersons, sortByInput, SortOrderOption.descending);
            // Assert

            Assert.Equal(filteredPersons, actualResponse);
        }


        //2. it should return sorted list in descending order if sortorder is descending


        [Fact]


        public void GetSortedPersons_DescendingOrder()
        {

            //1. arrange



            List<AddCountryRequest> addcountryRequests = new List<AddCountryRequest>()
            {

            new AddCountryRequest()
            {
                CountryName = "Kenya"
            },
            new AddCountryRequest()
            {
                CountryName="Tanzania"
            },
            new AddCountryRequest()
            {
                CountryName="Uganda"
            },
            new AddCountryRequest()
            {
                                CountryName="Rwanda"
            },
            new AddCountryRequest()
            {
                                CountryName="Burundi"
            }


            };


            List<CountryResponse> countryResponses = new List<CountryResponse>();


            foreach (AddCountryRequest addCountry in addcountryRequests)
            {
                countryResponses.Add(_countriesService.AddCountry(addCountry));
            }


            List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
            {

                new PersonAddRequest()
                {
                    PersonName="Kenya Person",
                    Email="kenya@gmail.com",
                    Address="Nairobi",
                    CountryId=countryResponses[0].Id,
                    Gender=GenderType.Male,
                    DateOfBirth=new DateTime(2022,3,3)
                },

                   new PersonAddRequest()
                {
                    PersonName="Tanzania Person",
                    Email="kenya@gmail.com",
                    Address="Nairobi",
                    CountryId=countryResponses[1].Id,
                    Gender=GenderType.Male,
                    DateOfBirth=new DateTime(2022,3,3)
                },
                new PersonAddRequest()
                {
                    PersonName="Uganda Person",
                    Email="uganda@gmail.com",
                    Address="Nairobi",
                    CountryId=countryResponses[2].Id,
                    Gender=GenderType.Male,
                    DateOfBirth=new DateTime(2022,3,3)
                },
                 new PersonAddRequest()
                {
                    PersonName="Rwanda Person",
                    Email="rwanda@gmail.com",
                    Address="Rwanda city",
                    CountryId=countryResponses[0].Id,
                    Gender=GenderType.Female,
                    DateOfBirth=new DateTime(2022,3,3)
                },
                new PersonAddRequest()
                {
                    PersonName="Burundi Person",
                    Email="burundi@gmail.com",
                    Address="Nairobi",
                    CountryId=countryResponses[0].Id,
                    Gender=GenderType.Others,
                    DateOfBirth=new DateTime(2022,3,3)


                },


            };



            List<PersonResponse> personResponses_from_addPerson = new List<PersonResponse>();

            foreach (var personAddReq in personAddRequests)
            {
                personResponses_from_addPerson.Add(_personService.AddPerson(personAddReq));
            }



            List<PersonResponse> allPersons = _personService.GetAllPersons();
            // now since we have responses after adding persons to the db 





            // now we must sort the person list from add in descending order to compare with the actual sorted list we get from the services
            // expected value 

            List<PersonResponse> personResponses_from_AddPerson_Desc = personResponses_from_addPerson.OrderByDescending(person => person.PersonName).ToList();

            //2. Act 

            //actual value 
            List<PersonResponse> sortedPersonList_in_DescendingOrder = _personService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOption.descending);





            //3. assert (Now we need to check whethere the first elem in list of sortedPersonList_in_DescendingOrder is same as the last elem of personResponses_from_addPerson
            //because in descending order last elem will be first and so on)

            for (int i = 0; i < personResponses_from_addPerson.Count; i++)
            {

                Assert.Equal(personResponses_from_AddPerson_Desc[i], sortedPersonList_in_DescendingOrder[i]);

            }
        }
        #endregion


        #region UpdatePersonDetails


        //1. it should throw argument nulll exception if the personUpdateRequest is null

        [Fact]

        public void PersonUpdateRequest_Null()
        {


        //1. Arrange 
        PersonUpdateRequest? personUpdateRequest = null;


            Assert.Throws<ArgumentNullException>(() =>
            _personService.UpdatePersonDetails(personUpdateRequest)); 
        }


        //2. it should throw argument exception if the personid is invalid 

        [Fact]


        public void PersonUpdateRequest_InvalidPersonId()
        {

            // at firsst we should also add some persons to the list so that we can get fill that we are comparing with invalid id 
            // huna tw person add nagare ni farak tw pardaina

            AddCountryRequest addCountry = new AddCountryRequest()
            {
                CountryName = "Duoling"
            };
            CountryResponse countryResponse = _countriesService.AddCountry(addCountry);
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "hello world",
                Address = "somewhere",
                CountryId = countryResponse.Id,
                DateOfBirth = new DateTime(2000, 1, 1),
                Email = "sample@example.com",
            };

            PersonResponse personResponse = _personService.AddPerson(personAddRequest);


            // now creating personupdaterequest with invalid personid
            //1. Arrange 
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest()
            {
                PersonId = Guid.NewGuid(),  // since this id is not present in the list it is invalid
                PersonName = "Updated Name",
            };

            //2. act & assert


            Assert.Throws<ArgumentException>(() =>
            _personService.UpdatePersonDetails(personUpdateRequest)

            );

        }



        // should throw error if the validation fail for the dto objects

        [Fact]

        public void UpdatePersonDetails_InvalidiProperties()
        {
            //1. Arrange

            //creating a dto with invalid email format

            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest()
            {
                PersonId = Guid.NewGuid(),  // since this id is not present in the list it is invalid
                PersonName = "Updated Name",
                Email = "invalidemail@email.com" // invalid email format
            };

            // context is req by validator to check what obj and properties to validate
            //var context = new ValidationContext(personUpdateRequest);

            // to store the validation results
            //var results = new List<ValidationResult>();


            // this validates not only to email but all the properties of the dto object where the validators are applied ie email,personId,dob,etc
            //bool isValid = Validator.TryValidateObject(personUpdateRequest, context, results, true);




            // instead of trying to validatate object manually we will use validationhelper class

            var excpetion =Assert.Throws<ArgumentException>(() =>
          ValidationHelper.ValidateModelProperties(personUpdateRequest)
            );

            



            // here assert.false means we are expecting the validation to fail since email format we gave is invalid so 
            // if the actual value is also false then expected and actual value are same so our test case will pass

            // in this case test case will pass 

            //Assert.False(isValid);     // this means there is validation error but we dont know in which property 


            // in this whole code this is the only line that checks for email property validation error specifically above all it is for general case 
            // ie validation applied to all the properties of the dto object 
            //Assert.Contains(results, error => error.MemberNames.Contains("Email")); // this checks for if email property is present in the 
            // list of validation errors 

        }

            //3. if the person id is valid then it should update the person details and return updated personresponse


        [Fact]

        public void PersonUpdateRequest_ValidPersonId()
        {

            //1. Arrange 

            AddCountryRequest addCountry = new AddCountryRequest()
            {
                CountryName = "Duoling"
            };

            CountryResponse countryResponse = _countriesService.AddCountry(addCountry);


            List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
            {
                new PersonAddRequest()
                {
                    PersonName = "hello world",
                    Address = "somewhere",
                    CountryId = countryResponse.Id,
                    DateOfBirth = new DateTime(2000, 1, 1),
                    Email = "hello@example.com",
                    Gender = GenderType.Others,


                },

                new PersonAddRequest()
                {
                    PersonName = "hello john",
                    Address = "somewhere",
                    CountryId = countryResponse.Id,
                    DateOfBirth = new DateTime(1999, 5, 5),
                    Email = "johndoe@sample.com",
                    Gender = GenderType.Male,
                }

            };

            List<PersonResponse> _personResponses = new List<PersonResponse>();

            foreach (var personAddReq in personAddRequests)
            {
                _personResponses.Add(_personService.AddPerson(personAddReq));
            }


            // let us take one sample personId from the above added persons

            Guid validPersonId = _personResponses[0].PersonId;

            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest()
            {
                PersonId = validPersonId,
                PersonName = "Updated Name",
                Address = "Updated Address",
                CountryId = countryResponse.Id,
                DateOfBirth = new DateTime(1995, 12, 12),
                Email = "update@email.com",
                Gender = GenderType.Female,

            };

          

            //Act

            PersonResponse updatedPersonResponse = _personService.UpdatePersonDetails(personUpdateRequest);

            // expected value 

            PersonResponse expectedPersonResponse_from_get = _personService.GetPersonById(validPersonId);


            //3. Assert

            Assert.Equal(expectedPersonResponse_from_get, updatedPersonResponse);


        }
                #endregion


    }
}
  