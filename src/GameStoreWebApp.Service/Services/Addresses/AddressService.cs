using AutoMapper;
using GameStoreWebApp.Data.IRepositories;
using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Addresses;
using GameStoreWebApp.Service.DTOs.Addresses;
using GameStoreWebApp.Service.Exceptions;
using GameStoreWebApp.Service.Extensions;
using GameStoreWebApp.Service.Interfaces.Addresses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Services.Addresses;

public class AddressService : IAddressService
{
	private readonly IGenericRepository<Country> countryRepository;
	private readonly IGenericRepository<Region> regionRepository;
	private readonly IMapper mapper;

	public AddressService(IGenericRepository<Country> countryRepository, IGenericRepository<Region> regionRepository,
		IMapper mapper)
    {
		this.countryRepository = countryRepository;
		this.regionRepository = regionRepository;
		this.mapper = mapper;
	}
    public async ValueTask<IEnumerable<CountryGetDto>> GetAllCountriesAsync(PaginationParams @params, Expression<Func<Country, bool>> expression = null)
	{
		var countries = countryRepository.GetAll(expression: expression, isTracking: false);
		return mapper.Map<List<CountryGetDto>>(await countries.ToPagedList(@params).ToListAsync());
	}

	public async ValueTask<IEnumerable<RegionGetDto>> GetAllRegionsAsync(PaginationParams @params, Expression<Func<Region, bool>> expression = null)
	{
		var regions = regionRepository.GetAll(expression: expression, isTracking: false);
		return mapper.Map<List<RegionGetDto>>(await regions.ToPagedList(@params).ToListAsync());
	}

	public async ValueTask<CountryGetDto> GetCountryAsync(Expression<Func<Country, bool>> expression)
	{
		var country = await countryRepository.GetAsync(expression);

		if (country is null)
			throw new GameAppException(404, "Country Not Found");

		return mapper.Map<CountryGetDto>(country);
	}

	public async ValueTask<RegionGetDto> GetRegionAsync(Expression<Func<Region, bool>> expression)
	{
		var region = await regionRepository.GetAsync(expression);

		if (region is null)
			throw new GameAppException(404, "Region Not Found");

		return mapper.Map<RegionGetDto>(region);
	}
}
