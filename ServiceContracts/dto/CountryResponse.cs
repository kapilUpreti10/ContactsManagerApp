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

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(CountryResponse)) return false;

            CountryResponse country = (CountryResponse)obj;
            return this.Id == country.Id && this.CountryName == country.CountryName;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CountryName);
        }

        public override string ToString()
        {
            return $"CountryResponse {{ Id = {Id}, CountryName = {CountryName} }}";
        }
    }


    //overriding the Equals method to compare two CountryResponse objects based on their properties




    // since in Country class we have to write this function but in models/entities we should not write any function so we will use extension method which acts as funciton of Country class 
    // and it is made static because extension method should be in static class and it should be static method
    public static class CountryResponseExtension
    { 
    

        // converting domain model to dto as domain model is never exposed to controller or outside 
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
