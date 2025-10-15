using Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace ServiceContracts.dto
{

    /// <summary>
    /// DTO class which is used as return type for most of the CountriesService methods like during get,update, delete operations
    /// </summary>
    public class CountryResponse
    {

        public Guid Id { get; set; }
        public string? CountryName { get; set; }
    }

    // since in Country class we have to write this function but in models/entities we should not write any function so we will use extension method which acts as funciton of Country class 
    // and it is made static because extension method should be in static class and it should be static method
    public static class CountryResponseExtension
    { 
    
        public static CountryResponse ConvertCountryToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                Id = country.Id,
                CountryName = country.CountryName

            };
        }
    }
}
