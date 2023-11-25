using GameStoreWebApp.Domain.Commons;
using GameStoreWebApp.Domain.Entities.Games;
using GameStoreWebApp.Domain.Entities.Users;

namespace GameStoreWebApp.Domain.Entities.Feedbacks;

public class Rate : BaseEntity
{
    public int Star { get; set; }
    public string Message { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}