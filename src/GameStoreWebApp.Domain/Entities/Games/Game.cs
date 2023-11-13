using GameStoreWebApp.Domain.Commons;
using GameStoreWebApp.Domain.Enums;

namespace GameStoreWebApp.Domain.Entities.Games;

public class Game : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string FilePath { get; set; }
    public Category Category { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Developer { get; set; }
    public int Downloads { get; set; }
}
