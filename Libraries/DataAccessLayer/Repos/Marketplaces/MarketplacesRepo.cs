using CoreLayer;
using CoreLayer.Enum;
using Dapper;
using DataAccessLayer.Context;
using EntityLayer.DTOs;

namespace DataAccessLayer.Repos.Marketplaces
{
    public class MarketplacesRepo
    {
        public List<MarketplacesCategoryDTO> SearchCategories(string search, int marketplace_id)
        {
            using (var db = MssqlContext.OpenMarketDBConnection())
            {
                var query = "";
                switch (marketplace_id)
                {
                    case (int)MarketPlaceEnum.Hepsiburada:
                        query = "SELECT TOP 20 c.category_id, c.name as category_name, c.path FROM jetstok_marketplace.dbo.hepsiburada_categories c left join  jetstok_marketplace.dbo.marketplace_commision_rate mcr on c.category_id = mcr.category_id where mcr.marketplace_id = @marketplace_id AND c.name like @search";
                        break;
                    case (int)MarketPlaceEnum.Trendyol:
                        query = "SELECT TOP 20 c.id as category_id, c.name as category_name, c.path FROM jetstok_marketplace.dbo.trendyol_categories c left join  jetstok_marketplace.dbo.marketplace_commision_rate mcr on c.id = mcr.category_id where mcr.marketplace_id = @marketplace_id AND c.name like @search";
                        break;
                    case (int)MarketPlaceEnum.CicekSepeti:
                        query = "SELECT TOP 20 c.id as category_id, c.name as category_name, c.path FROM jetstok_marketplace.dbo.ciceksepeti_categories c left join  jetstok_marketplace.dbo.marketplace_commision_rate mcr on c.id = mcr.category_id where mcr.marketplace_id = @marketplace_id AND c.name like @search";
                        break;
                    case (int)MarketPlaceEnum.N11:
                        query = "SELECT TOP 20 c.id as category_id, c.name as category_name, c.path FROM jetstok_marketplace.dbo.n11_categories c left join  jetstok_marketplace.dbo.marketplace_commision_rate mcr on c.id = mcr.category_id where mcr.marketplace_id = @marketplace_id AND c.name like @search";
                        break;
                    case (int)MarketPlaceEnum.PttAvm:
                        query = "SELECT TOP 20 c.id as category_id, c.name as category_name, c.path FROM jetstok_marketplace.dbo.pttavm_categories c left join  jetstok_marketplace.dbo.marketplace_commision_rate mcr on c.id = mcr.category_id where mcr.marketplace_id = @marketplace_id AND c.name like @search";
                        break;
                    default:
                        break;
                }
                search = search.ClearToSqlFullLikeQuery();
                return db.Query<MarketplacesCategoryDTO>(query, new { search, marketplace_id }).ToList();
            }
        }

    }
}
