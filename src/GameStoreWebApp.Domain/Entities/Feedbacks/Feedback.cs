using GameStoreWebApp.Domain.Commons;

namespace GameStoreWebApp.Domain.Entities.Feedbacks;

public class Feedback : BaseEntity
{
    public int UserId { get; set; }
    public string Message { get; set; }
}
