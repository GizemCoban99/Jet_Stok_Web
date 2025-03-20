using Dapper;
using DataAccessLayer.Context;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos.Marketplaces
{
    public class MarketplaceCommissionRateRepo
    {
        public MarketplaceCommissionRate Get(int category_id)
        {
            using (var conn = MssqlContext.OpenMarketDBConnection())
            {
                var query = $"SELECT * FROM jetstok_marketplace.dbo.marketplace_commision_rate where category_id=@category_id";
                return conn.Query<MarketplaceCommissionRate>(query, new { category_id }).FirstOrDefault();
            }
        }
    }
}
