using ServiceContracts.enums;
using Entities;
using System.ComponentModel.DataAnnotations;
namespace ServiceContracts.dto
{
    public class PersonAddRequest
    {

        // note:: it is not necessary that all properties of domain model should be present in the AddRequest dto and properties name can be different also 


        [Required(ErrorMessage ="you must provide the personname")]
        public string PersonName { get; set; }


        [Required(ErrorMessage ="you must provide the email")]
        [EmailAddress(ErrorMessage ="Invalid email format")]
        // if value is compulsory then we can use non nullable types otherwise nullable types
        public string Email { get; set; }

        [Required(ErrorMessage ="you must provide the date of birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string? Address { get; set; }
        public GenderType? Gender { get; set; }




        // now to convert this dto into model class
        public Person ConvertToPerson()
        {
            return new Person()
            {
                PersonName = this.PersonName,
                Email= this.Email,
                DateOfBirth= this.DateOfBirth,
                Address= this.Address,
                Gender=this.Gender.ToString()      // as the datatype of gender in Person model class is string but here is enum so converting to string

            };
        }
    }

    
}
