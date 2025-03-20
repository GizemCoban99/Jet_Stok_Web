using Dapper;
using DataAccessLayer.Context;
using EntityLayer.Models;

namespace DataAccessLayer.Repos.Marketplaces
{
    public class MarketplaceOperationRateRepo
    {

        public MarketplaceOperationRate Get(int marketplace_id)
        {
            using (var conn = MssqlContext.OpenMarketDBConnection())
            {
                var query = $"SELECT * FROM jetstok_marketplace.dbo.marketplace_operation_rate where marketplace_id=@marketplace_id";
                return conn.Query<MarketplaceOperationRate>(query, new { marketplace_id }).FirstOrDefault();
            }
        }
    }
}
