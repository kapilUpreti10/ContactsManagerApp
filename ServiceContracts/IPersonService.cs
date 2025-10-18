using ServiceContracts.dto;
namespace ServiceContracts
{
public  interface IPersonService
    {
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        List<PersonResponse> GetAllPersons();


        PersonResponse? GetPersonById(Guid? PersonId);
    }

    
}
