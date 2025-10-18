using ServiceContracts.enums;
using Entities;
using ServiceContracts;
using ServiceContracts.dto;
using Services;
using System.Security.Cryptography.X509Certificates;
using Xunit.Sdk;

namespace FirstTest
{
    public  class PersonServiceTest
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

            PersonResponse personResult=_personService.AddPerson(validPersonDetails);

            List<PersonResponse> expectedPersons = _personService.GetAllPersons();
            //3.Assert
           
            Assert.True(personResult.PersonId != Guid.Empty);
            Assert.NotNull(personResult.PersonName);
            Assert.NotNull(personResult.Email);
            Assert.Contains(personResult,expectedPersons);
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

            CountryResponse validCountryWithId=_countriesService.AddCountry(addCountryName);  // since after calling addCountry it creates a valid country id which is returned in countryresponse


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
            PersonResponse? validPersonWithId=_personService.GetPersonById(validResponsePerson?.PersonId);

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

           List<PersonResponse> personResponse=_personService.GetAllPersons();


            //2. assert

            Assert.Empty(personResponse);



        }

        //2. it should return all the list of person if the personlist is not empty

        [Fact]

        public void GetAllPersons_RetrunAllThePersons()
        {

            //1. arrange

            AddCountryRequest addCountry = new AddCountryRequest() { CountryName="Nepal"};

            CountryResponse countryResponse=_countriesService.AddCountry(addCountry);


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

            foreach(PersonAddRequest personAddReq in PersonAddRequests)
            {
                personResponses.Add(_personService.AddPerson(personAddReq));
            }


            // now finally assert

            List<PersonResponse> get_all_persons=_personService.GetAllPersons();

            foreach(PersonResponse personResponse in personResponses)
            {
                Assert.Contains(personResponse, get_all_persons);
            }
        }


        #endregion


    }
}