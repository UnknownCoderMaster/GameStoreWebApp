using GameStoreWebApp.Domain.Commons;
using GameStoreWebApp.Domain.Entities.Users;

namespace GameStoreWebApp.Domain.Entities.Feedbacks;

public class Feedback : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}
