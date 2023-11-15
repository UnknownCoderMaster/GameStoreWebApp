using GameStoreWebApp.Domain.Commons;

namespace GameStoreWebApp.Domain.Entities.Addresses;

public class Region : BaseEntity
{
    public string RegionName { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
}
