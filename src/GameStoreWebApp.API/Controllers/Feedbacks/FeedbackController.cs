using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Service.DTOs.Feedbacks;
using GameStoreWebApp.Service.Interfaces.Feedbacks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStoreWebApp.API.Controllers.Feedbacks;

[Route("api/[controller]")]
[ApiController]
public class FeedbackController : ControllerBase
{
	private readonly IFeedbackService feedbackService;

	public FeedbackController(IFeedbackService feedbackService)
    {
		this.feedbackService = feedbackService;
	}

	/// <summary>
	/// Create new feedback
	/// </summary>
	/// <param name="feedbackCreateDto"></param>
	/// <returns></returns>
	[HttpPost]
	public async ValueTask<IActionResult> CreateFeedbackAsync(FeedbackCreateDto feedbackCreateDto)
		=> Ok(await feedbackService.CreateAsync(feedbackCreateDto));

	/// <summary>
	/// Get feedback by Id
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("{id}")]
	public async ValueTask<IActionResult> GetFeedbackAsync([FromRoute] int id)
		=> Ok(await feedbackService.GetAsync(f => f.Id == id));

	/// <summary>
	/// Get all feedbacks
	/// </summary>
	/// <param name="params"></param>
	/// <returns></returns>
	[HttpGet]
	public async ValueTask<IActionResult> GetAllFeedbacksAsync([FromQuery] PaginationParams @params)
		=> Ok(await feedbackService.GetAllAsync(@params));
}
