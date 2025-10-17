using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class Person
    {

        // this is the domain model and it shouldnt be exposed to outside world directly ie controller/xuint test

        public Guid PersonId { get; set; }

        public string? PersonName { get; set; }

        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }
        
        public string? Address { get; set; }

        public Guid? CountryId { get; set; }
    }
}
