using AutoMapper;
using DataAccessLayer.Repos;
using DataAccessLayer.Repos.Marketplaces;
using EntityLayer.DTOs;

namespace BusinessLayer.Services.Marketplaces
{
    public class MarketplacesService
    {
        private readonly IMapper _mapper;
        private readonly MarketplacesRepo _repo = new MarketplacesRepo();
        public MarketplacesService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ServiceResultDTO<List<MarketplacesCategoryDTO>> SearchCategories(string search, int marketplace_id)
        {
            try
            {
                var data = _repo.SearchCategories(search, marketplace_id);
                return new ServiceResultDTO<List<MarketplacesCategoryDTO>>(data: _mapper.Map<List<MarketplacesCategoryDTO>>(data));

            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<MarketplacesCategoryDTO>>(exception: ex);
            }
        }
    }
}
