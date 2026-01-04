using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    public class Person
    {

        // this is the domain model and it shouldnt be exposed to outside world directly ie controller/xuint test
        // in this domain model we dont add validation here because this is only for data storage purpose we add validation to the dto classes 


        [Key]
        public Guid PersonId { get; set; }


        [StringLength(20)]
        public string? PersonName { get; set; }


        [StringLength(30)]
        public string? Email { get; set; }


        public DateTime? DateOfBirth { get; set; }


        [StringLength(10)]
        public string? Gender { get; set; }


        [StringLength(100)]
        public string? Address { get; set; }

        
        public Guid? CountryId { get; set; }

       
    }
}
