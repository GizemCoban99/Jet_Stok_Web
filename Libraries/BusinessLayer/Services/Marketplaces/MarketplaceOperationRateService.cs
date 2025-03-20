using AutoMapper;
using DataAccessLayer.Repos.Marketplaces;
using EntityLayer.DTOs;

namespace BusinessLayer.Services.Marketplaces
{
    public class MarketplaceOperationRateService
    {
        private readonly MarketplaceOperationRateRepo _repo = new MarketplaceOperationRateRepo();
        private readonly IMapper _mapper;
        public MarketplaceOperationRateService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ServiceResultDTO<MarketplaceOperationRateDTO> Get(int marketplace_id)
        {
            try
            {
                return new ServiceResultDTO<MarketplaceOperationRateDTO>(data: _mapper.Map<MarketplaceOperationRateDTO>(_repo.Get(marketplace_id)));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<MarketplaceOperationRateDTO>(exception: ex);
            }
        }
    }
}
