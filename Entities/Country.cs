namespace Entities
{

    // here the real world obj(country) is entity which represnt one table in db ie it is model class



    /// <summary>
    /// Domain model for country
    /// </summary>
    /// 

    // we will not expose this domain model to the presentation layer(views and controllers directly  we will not expose this country type as a parameter or return type of any controller method
    // thats why we will create a separate dto class in the dtos folder
    // the controller sends country req as dto to the service layer and service layer converts that dto to country entity and then sends it to the repository layer here controller dont directly create
    // the obj of country entity so the service creates protection layer surrounding the domain so dto is common practise in real word projects
    public class Country
    {

        public Guid Id { get; set; }
        public string? CountryName { get; set; }



    }
}
