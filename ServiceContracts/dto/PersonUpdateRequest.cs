using ServiceContracts.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;


namespace ServiceContracts.dto
{
    public class PersonUpdateRequest
    {


        [Required(ErrorMessage ="you must provide the {0}")]
        public Guid PersonId { get; set; }


            
        public string PersonName { get; set; }


        public int Age { get; set; }


        public GenderType Gender { get; set; }

        [EmailAddress(ErrorMessage ="Invalid email format")]

        public string Email { get; set; }


        [DataType(DataType.Date,ErrorMessage ="invalid date format")]

        public DateTime DateOfBirth { get; set; }


        public string Address { get; set; }


        public Guid? CountryId { get; set; }



        public Person personUpdateRequestDTO_ToPerson()
        {
            return new Person()
            {
                PersonName = this.PersonName,
                CountryId = this.CountryId,
                DateOfBirth = this.DateOfBirth,
                Email = this.Email,
                Address = this.Address,
                Gender = this.Gender.ToString(),
                PersonId = this.PersonId


            };
        }



    }
}
