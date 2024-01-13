using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Addresses;
using GameStoreWebApp.Service.DTOs.Addresses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Interfaces.Addresses;

public interface IAddressService
{
	ValueTask<CountryGetDto> GetCountryAsync(Expression<Func<Country, bool>> expression);
	ValueTask<IEnumerable<CountryGetDto>> GetAllCountriesAsync(PaginationParams @params, Expression<Func<Country, bool>> expression = null);
	ValueTask<RegionGetDto> GetRegionAsync(Expression<Func<Region, bool>> expression);
	ValueTask<IEnumerable<RegionGetDto>> GetAllRegionsAsync(PaginationParams @params, Expression<Func<Region, bool>> expression = null);
}
