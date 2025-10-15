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

                 //1. Arrange
                  


        //3 . check if the countryName is duplicate or not if yes it should throw argument exception


        //4.if everything is correct then it should insert country in the list(as now we dont have db) return the CountryResponse object with correct details





        // note:: here we dont test whethere the country name is valid or not ,if it is greateer that 3 char or not etc as that can be tested using validator
        // in the class
    }
}
