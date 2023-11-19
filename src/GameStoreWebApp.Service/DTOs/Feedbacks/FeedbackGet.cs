using GameStoreWebApp.Service.DTOs.Users;

namespace GameStoreWebApp.Service.DTOs.Feedbacks;

public record FeedbackGet(UserGetDto User, string Message);