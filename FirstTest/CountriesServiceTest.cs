using System;
using System.Collections.Generic;
using ServiceContracts;
using ServiceContracts.dto;
using Services;
namespace FirstTest
{
 public class CountriesServiceTest
    {

            private readonly ICountriesService _countriesService;

        // we can also do like this but since we are doing dependecy injection we must add service in IServiceCollection in Program.cs but since 
        // here Tests is the library project and it does not have Program .cs file so we manually create the object of CountriesService class

        //public CountriesServiceTest(ICountriesService countriesService)
        //{
        //    _countriesService = countriesService;
        //}



        public CountriesServiceTest()
        {
            _countriesService = new CountriesService(); // here we are manually creating the object of CountriesService class which is hold by 
                                                        // reference variable of type ICountriesService interface


        }


        #region AddCountry
        // the test cases we need to perform are::

        //1. check if the argument is null or not ie AddCountryRequest object is null or not if yes it should throw argument null exception

        [Fact]

        public void AddCountry_NullAdCountryRequest()
        {
            //1. Arrange
                 AddCountryRequest ? addCountryRequest = null;

            //2. Act
            //_countriesService.AddCountry(addCountryRequest); 

            //3.Assert

            Assert.Throws<ArgumentNullException>(() => _countriesService.AddCountry(addCountryRequest));
        }


        //2. check if the CountryName property of AddCountryRequest object is null or empty or whitespace if yes it should throw argument exception

        
        [Fact]

          public void AddCountry_EmptyCountryName()
        {
            //1. Arrange

            AddCountryRequest? request = new AddCountryRequest()
            {
                CountryName = null
            };

            //2.Act
            //_countriesService.AddCountry(request);

            //3.Assert
           

            Assert.Throws<ArgumentException>(() => _countriesService.AddCountry(request));


        }





        //3 . check if the countryName is duplicate or not if yes it should throw argument exception

        [Fact]


         public void AddCountry_DuplicateCountryName()
        {
            //1.Arrange
            AddCountryRequest? request1=new AddCountryRequest()
            {
                CountryName = "Nepal"
            };

            AddCountryRequest? request2 = new AddCountryRequest()
            {
                CountryName = "Nepal"
            };

            //2.Act

            //_countriesService.AddCountry(request1);
            //_countriesService.AddCountry(request2);


            //3.Assert

            // here Assert.Throws will check if the code inside the lambda expression throws the specified exception or not ie ArgumentException in this case 

            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }
        //4.if everything is correct then it should insert country in the list(as now we dont have db) return the CountryResponse object with correct details

        [Fact]

        public void AddCountry_ValidCountry()
        {
            //1.Arrange
            AddCountryRequest? request = new AddCountryRequest()
            {
                CountryName = "Nepal"
            };
            //2.Act
            CountryResponse? countryResponse = _countriesService.AddCountry(request);


            //3.Assert
            Assert.NotNull(countryResponse); // to check if the returned obj is not null
            Assert.Equal(request.CountryName, countryResponse.CountryName); // to check if the country name in request and response are same
            Assert.True(countryResponse.Id !=Guid.Empty); // to check if the country id is greater than 0 or not as it should be auto generated and greater than 0


            // so here AddCountry_ValidCountry unit test pass huna 3 wota assert conidtion pass hunu parxa euta matra fail vayo vane yo purai nai fail hunxa
        }



        // note:: here we dont test whethere the country name is valid or not ,if it is greateer that 3 char or not etc as that can be tested using validator
        // in the class

        #endregion



        // note each test case runs independently so the list of countries will be empty for each test case as the constructor of CountriesService class will be called for each test case

        #region GetCountryById

        //1. check if the couuntryId is empty or not 

        [Fact]

        public void GetCountryById_EmptyCountryId()
        {
            //1.Arrange
            Guid test1 = Guid.Empty;

            //2.Assert

            Assert.Throws<ArgumentException>(() => _countriesService.GetCountryById(test1));
        }


        [Fact]

        //2. check if the the countryId is null or not

        public void GetCountryById_NullCountryId()
        {
            //1. Arrange
            Guid? test2 = null;


            //2. Act
           CountryResponse response_Country_from_id= _countriesService.GetCountryById(test2);

            //3.Assert

            Assert.Null(response_Country_from_id);  // as the countryId is null so it should return null
        }

        //3 check if the countryId is not found in the list/db it should return null 

        [Fact]

        public void GetCountryById_CountryIdNotFound()
        {
            //1.Arrange
            Guid test3 = Guid.NewGuid(); // as the list is empty so any guid we generate will not be found in the list
            //2.Act
            CountryResponse? response_country_from_id = _countriesService.GetCountryById(test3);
            //3.Assert
            Assert.Null(response_country_from_id); // as the countryId is not found in the list so it should return null
        }

        //4. if the countryId is found in the list/db it should return the countryResponse object

        [Fact]

        public void GetCountryById_CountryIdValidAndFound()
        {
            //1.Arrange
            // but at first we should make sure that the list is not empty so we will add a country first and then we will get the countryId from the returned CountryResponse object and then we will use that countryId to get the
            // countryResponse object using GetCountryById method

            AddCountryRequest? request = new AddCountryRequest()
            {
                CountryName = "China"
            };

            CountryResponse response_from_add=_countriesService.AddCountry(request);

            //2.act
            CountryResponse country_response_from_get=_countriesService.GetCountryById(response_from_add.Id);


            //3.Assert

            Assert.Equal(response_from_add.Id, country_response_from_get.Id); // to check if the countryId in both the response are same


        }

        #endregion
    }
}
