using System;
using System.Collections.Generic;


using Entities;

namespace ServiceContracts.dto
{
    /// <summary>
    /// DTO class for adding a new country
    /// </summary>
    public class AddCountryRequest
    {
        public string? CountryName { get; set; }
    // after validating the CountryName property of this dto now the obj of type Contry model should be created as it is the actual model which will be stored in the db

        // this is converting the obj from dto to domain model
        public Country ConvertToCountry()

        // since Country is in different project(entities so we must add a reference of that project in this project
        {
            return new Country()
            {
                CountryName = this.CountryName
            };
        }
    }
}
