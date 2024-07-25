using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Service.Interfaces.Addresses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStoreWebApp.API.Controllers.Addresses
{
	[Route("api/[controller]")]
	[ApiController]

	#pragma warning disable CS1591
	public class AddressController : ControllerBase
	{
		private readonly IAddressService addressService;

		public AddressController(IAddressService addressService)
        {
			this.addressService = addressService;
		}

		/// <summary>
		/// Get one region by region Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("region/{id}")]
		public async ValueTask<IActionResult> GetRegionByIdAsync([FromRoute] int id)
			=> Ok(await addressService.GetRegionAsync(r => r.Id == id));

		/// <summary>
		/// Get all regions by Country Id
		/// </summary>
		/// <param name="params"></param>
		/// <param name="countryId"></param>
		/// <returns></returns>
		[HttpGet("region")]
		public async ValueTask<IActionResult> GetRegionsAsync([FromQuery] PaginationParams @params, int countryId)
			=> Ok(await addressService.GetAllRegionsAsync(@params, r => r.CountryId == countryId));

		/// <summary>
		/// Get country by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("country/{id}")]
		public async ValueTask<IActionResult> GetCountryByIdAsync([FromRoute] int id)
			=> Ok(await addressService.GetCountryAsync(r => r.Id == id));

		/// <summary>
		/// Get all countries
		/// </summary>
		/// <param name="params"></param>
		/// <returns></returns>
		[HttpGet("country")]
		public async ValueTask<IActionResult> GetCountryAsync([FromQuery] PaginationParams @params)
			=> Ok(await addressService.GetAllCountriesAsync(@params));
	}

	#pragma warning restore CS1591
}
