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
    }
}
