using GameStoreWebApp.Service.DTOs.Users;

namespace GameStoreWebApp.Service.DTOs.Feedbacks;

public record FeedbackGetDto(int Id, string FirstName, string LastName, string Email, string Message);