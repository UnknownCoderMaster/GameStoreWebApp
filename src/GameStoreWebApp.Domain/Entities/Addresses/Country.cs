using GameStoreWebApp.Domain.Commons;

namespace GameStoreWebApp.Domain.Entities.Addresses;

public class Country : BaseEntity
{
    public string Name { get; set; }
    public List<Region> Regions { get; set; }
}
