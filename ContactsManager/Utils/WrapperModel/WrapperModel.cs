using ServiceContracts.dto;

namespace ContactsManager.Utils.WrapperModel
{
    public class WrapperModel
    {
        public List<PersonResponse>? PersonDetails { get; set; }

        public List<CountryResponse>? CountryDetails { get; set; }

    }
}
