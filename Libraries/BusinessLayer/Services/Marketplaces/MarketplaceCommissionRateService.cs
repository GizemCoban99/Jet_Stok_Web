using AutoMapper;
using DataAccessLayer.Repos;
using DataAccessLayer.Repos.Marketplaces;
using EntityLayer.DTOs;

namespace BusinessLayer.Services.Marketplaces
{
    public class MarketplaceCommissionRateService
    {
        private readonly MarketplaceCommissionRateRepo _repo = new MarketplaceCommissionRateRepo();
        private readonly IMapper _mapper;
        public MarketplaceCommissionRateService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ServiceResultDTO<MarketplaceCommissionRateDTO> Get(int category_id)
        {
            try
            {
                return new ServiceResultDTO<MarketplaceCommissionRateDTO>(data: _mapper.Map<MarketplaceCommissionRateDTO>(_repo.Get(category_id)));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<MarketplaceCommissionRateDTO>(exception: ex);
            }
        }
    }
}
