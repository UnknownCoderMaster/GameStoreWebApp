namespace GameStoreWebApp.Service.DTOs.Feedbacks;

public record FeedbackCreateDto(string FirstName, string LastName, string Email, string Message);