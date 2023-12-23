using GameStoreWebApp.Data.IRepositories;
using GameStoreWebApp.Domain.Entities.Users;
using GameStoreWebApp.Service.DTOs.Users;
using GameStoreWebApp.Service.Interfaces.Users;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using GameStoreWebApp.Service.Exceptions;
using GameStoreWebApp.Service.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using GameStoreWebApp.Service.Helpers;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using GameStoreWebApp.Service.Extensions;

namespace GameStoreWebApp.Service.Services.Users;

public class AuthService : IAuthService
{
	private readonly IGenericRepository<User> userRepository;
	private readonly IGenericRepository<Verification> verficationRepository;
	private readonly IConfiguration configuration;
	private readonly IConfigurationSection config;

	public AuthService(IGenericRepository<User> userRepository, IConfiguration configuration,
		IGenericRepository<Verification> verificationRepository)
    {
		this.userRepository = userRepository;
		this.configuration = configuration;
		this.config = configuration.GetSection("Email");
		this.verficationRepository = verificationRepository;
	}
    public async ValueTask<bool> ConfirmEmail(VerificationForCreationDto dto)
	{
		var existVerification =
			await verficationRepository.GetAsync(
				v => v.Code == dto.Code &&
				v.Email == dto.Email);

		if (existVerification == null)
			throw new GameAppException(400, "verification is invalid");

		var user = await userRepository.GetAsync(u => u.Email == dto.Email);

		user.IsEmailConfirmed = true;

		userRepository.Update(user);
		await userRepository.SaveChangesAsync();

		return true;
	}

	public async ValueTask<string> GenerateToken(string email, string password)
	{
		User user = await userRepository.GetAsync(u =>
			u.Email == email && u.Password.Equals(password.Encrypt()));

		if (user is null)
			throw new GameAppException(400, "Login or Password is incorrect");

		if (!user.IsEmailConfirmed)
			throw new GameAppException(400, "Please verify your email address");

		var authSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

		var token = new JwtSecurityToken(
			issuer: configuration["JWT:ValidIssuer"],
			expires: DateTime.Now.AddHours(int.Parse(configuration["JWT:Expire"])),
			claims: new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			},
			signingCredentials: new SigningCredentials(
				key: authSigningKey,
				algorithm: SecurityAlgorithms.HmacSha256)
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public async ValueTask<bool> SendEmailVerification(int userId)
	{
		var user = await userRepository.GetAsync(u => u.Id == userId);
		if (user is null)
			throw new GameAppException(404, "User not found");

		var verification = new Verification()
		{
			Code = Guid.NewGuid(),
			Email = user.Email
		};

		await verficationRepository.CreateAsync(verification);
		await verficationRepository.SaveChangesAsync();

		var emailMessage = new EmailMessage()
		{
			To = user.Email,
			Subject = "Verification code",
			Body = @$"
    <p>Hi {user.Email} this message was sent you from MyCareer please click button bellow to verify your account
            {DateTime.UtcNow}</p>
    <button style=""background-color: #2196F3; 
        color: white;
        border: none;
        padding: 30px 60px; 
        text-align: center; 
        text-decoration: none; 
        display: inline-block;
        font-size: 20px; 
        border-radius: 5px;"">
            <a href=""http://{HttpContextHelper.HttpContext.Request.Host.Value}/api/auth/verify-email?Code={verification.Code}&Email={user.Email}"" style=""color: white; 
                text-decoration: none;"">Verify</a>
    </button>"
		};

		var email = new MimeMessage();
		email.From.Add(MailboxAddress.Parse(config["EmailAddress"]));
		email.To.Add(MailboxAddress.Parse(emailMessage.To));
		email.Subject = emailMessage.Subject;
		email.Body = new TextPart(TextFormat.Html) { Text = emailMessage.Body };

		var smtp = new SmtpClient();
		await smtp.ConnectAsync(config["Host"], 587, SecureSocketOptions.StartTls);
		await smtp.AuthenticateAsync(config["EmailAddress"], config["Password"]);
		await smtp.SendAsync(email);

		return true;
	}
}
