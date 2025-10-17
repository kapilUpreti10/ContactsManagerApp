
using Entities;
using System.Runtime.CompilerServices;
namespace ServiceContracts.dto
{
    public class PersonResponse
    {

        public Guid PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }

        // theese are teh additional properties not present in the domain model Person class but we want to send as resoponse to the contrller so 
        // its functionality have to implement in the service class method which is returning this dto

        public double? Age { get; set; }

        public string? CountryName { get; set; }

        public bool RecieveNewsLetters { get; set; }

        // overridng the equals method 

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if(obj.GetType() != this.GetType()) return false;

            PersonResponse myObj= (PersonResponse)obj;

            return this.PersonId == myObj.PersonId &&
                   this.PersonName == myObj.PersonName &&
                   this.Email == myObj.Email &&
                   this.DateOfBirth == myObj.DateOfBirth &&
                   this.Address == myObj.Address &&
                   this.Gender == myObj.Gender &&
                   this.CountryId == myObj.CountryId &&
                   this.RecieveNewsLetters == myObj.RecieveNewsLetters;
        }

        // this is useful when in the future we want to use this dto as key in dictionary or in hashset

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

       
    }

    public static class PersonResponseExtension
    {
        public static PersonResponse ConvertPersonToPersonResponse(this Person person)
        {
            // actually jun chij availabe xa teslai chai convert garne ho ,since here Person is availabe so we are converting it to PersonResponse dto but since Person is availabe
            // in model class Name but in model class we only write properties so we use extension method to convert it 
            return new PersonResponse()
            {

                // here in left side we have properties name of Dto and in right side we have properties of model class
                PersonId = person.PersonId,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Address = person.Address,
                Gender = person.Gender,
                CountryId = person.CountryId,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365) : null

                // note math.Round returns double type not and int
            };
        }
    }
}
