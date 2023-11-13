using GameStoreWebApp.Service.DTOs.Users;

namespace GameStoreWebApp.Service.DTOs.Feedbacks;

public record FeedbackGet(UserGet User, string Message);