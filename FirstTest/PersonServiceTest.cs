using ServiceContracts.enums;
using Entities;
using ServiceContracts;
using ServiceContracts.dto;
using Services;
using System.Security.Cryptography.X509Certificates;

namespace FirstTest
{
    public  class PersonServiceTest
    {

        private readonly IPersonService _personService;


        public PersonServiceTest()
        {
            _personService = new PersonService();
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


        }
    }