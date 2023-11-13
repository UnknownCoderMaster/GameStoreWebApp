using GameStoreWebApp.Domain.Commons;

namespace GameStoreWebApp.Domain.Entities.Regions;

public class Region : BaseEntity
{
    public string RegionName { get; set; }
    public int CountryId { get; set; }
}
