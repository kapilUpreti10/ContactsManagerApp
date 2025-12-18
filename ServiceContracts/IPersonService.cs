using ServiceContracts.enums;
using ServiceContracts.dto;
namespace ServiceContracts
{
public  interface IPersonService
    {
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        List<PersonResponse> GetAllPersons();


        PersonResponse? GetPersonById(Guid? PersonId);


        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);


        List<PersonResponse> GetSortedPersons(List<PersonResponse> filteredPersons,string? sortBy, SortOrderOption sortOrder);


        PersonResponse UpdatePersonDetails(PersonUpdateRequest? personUpdateReqObj);
    }

    
}
