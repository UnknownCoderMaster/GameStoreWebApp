using GameStoreWebApp.Domain.Commons;
using GameStoreWebApp.Domain.Entities.Users;

namespace GameStoreWebApp.Domain.Entities.Feedbacks;

public class Feedback : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public string Message { get; set; }
}
