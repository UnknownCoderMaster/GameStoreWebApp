namespace GameStoreWebApp.Service.DTOs.Feedbacks;

public record FeedbackCreateDto(
    ///<example>example value</example>
    string FirstName, string LastName, string Email, string Message);