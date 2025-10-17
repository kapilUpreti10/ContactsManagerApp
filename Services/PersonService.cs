using Entities;
using ServiceContracts;
using ServiceContracts.dto;
namespace Services
{
    public class PersonService:IPersonService
    {

        private readonly List<Person> _persons;

        public PersonService()
        {
            _persons = new List<Person>();

        }
     

      public  PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            return AddPerson(personAddRequest);
        }

        public List<PersonResponse> GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
