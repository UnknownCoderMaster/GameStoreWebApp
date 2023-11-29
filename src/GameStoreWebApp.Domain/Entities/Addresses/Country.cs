using GameStoreWebApp.Domain.Commons;
using System.Collections.Generic;

namespace GameStoreWebApp.Domain.Entities.Addresses;

public class Country : BaseEntity
{
    public string Name { get; set; }
    public List<Region> Regions { get; set; }
}
