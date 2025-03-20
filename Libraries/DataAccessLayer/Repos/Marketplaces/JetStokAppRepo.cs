using Dapper;
using DataAccessLayer.Context;
using EntityLayer.Models.JetStokApp;

namespace DataAccessLayer.Repos.Marketplaces
{
    public class JetStokAppRepo
    {
        public JetStokCompany AddJetstokCompany(JetStokCompany company)
        {
            try
            {
                using (var conn = MssqlContext.OpenMarketDBConnection())
                {
                    var result = conn.Query<int>("INSERT INTO jetstok_marketplace.dbo.companies (title,email,phone,start_date,end_date,is_active,approved) VALUES (@title,@email,@phone,@start_date,@end_date,@is_active,@approved); SELECT SCOPE_IDENTITY();", company).FirstOrDefault();

                    return GetCompanyById(result);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JetStokCompany GetCompanyById(long id)
        {
            using (var conn = MssqlContext.OpenMarketDBConnection())
            {
                var query = $"select * from jetstok_marketplace.dbo.companies WITH(NOLOCK) where is_deleted=0 and id=@id";
                return conn.Query<JetStokCompany>(query, new { id }).FirstOrDefault();
            }
        }

        public bool AddJetstokPackage(CompanyPackage package)
        {
            using (var conn = MssqlContext.OpenMarketDBConnection())
            {
                var query = $"INSERT INTO jetstok_marketplace.dbo.company_package (name, description, yearly_price,sale_price, market_place_count, xml_product_count, xml_integration_count, advenced_reporting, critical_stock_alert, user_count, xml_update_time_interval, excel_import, online_training_count, support_request_count, intelligent_stock_distribution, exchange_rate, gold_dry, remove_jet_stock_ads, ready_feature_copy, bundle_product, competition_analysis, parasut, store_sales, bulk_price_update, jet_stock_api, cargo_integration, xml_export, product_count, company_id, package_id,is_sale,end_date,currency_exchange_count,excel_export,customer_question,automatic_bill_send,bulk_product_transfer,product_link_download) VALUES(@name, @description, @yearly_price,@sale_price, @market_place_count, @xml_product_count, @xml_integration_count, @advenced_reporting, @critical_stock_alert, @user_count, @xml_update_time_interval, @excel_import, @online_training_count, @support_request_count, @intelligent_stock_distribution, @exchange_rate, @gold_dry, @remove_jet_stock_ads, @ready_feature_copy, @bundle_product, @competition_analysis, @parasut, @store_sales, @bulk_price_update, @jet_stock_api, @cargo_integration, @xml_export, @product_count, @company_id, @package_id,@is_sale,@end_date,@currency_exchange_count,@excel_export,@customer_question,@automatic_bill_send,@bulk_product_transfer,@product_link_download)";
                return conn.Execute(query, package) > 0;
            }
        }

        public Package GetJetstokPackage(long id)
        {

            using (var conn = MssqlContext.OpenMarketDBConnection())
            {
                var query = $"SELECT * FROM jetstok_marketplace.dbo.package WITH(NOLOCK) WHERE is_active = 1 and id=@id";
                return conn.Query<Package>(query, new { id }).FirstOrDefault();
            }
        }
        public JetStokUser AddJetstokUser(JetStokUser user)
        {
            try
            {
                using (var conn = MssqlContext.OpenMarketDBConnection())
                {
                    int result = conn.Query<int>("INSERT INTO jetstok_marketplace.dbo.users (email,name,surname,create_date,password,company_id,phone,role_id,admin_login_key) VALUES (@email,@name,@surname,@create_date,@password,@company_id,@phone, @role_id,@admin_login_key); SELECT SCOPE_IDENTITY();", user).FirstOrDefault();

                    return GetJetstokUserById(result, user.company_id);
                }
            }
            catch (Exception)
            {
                return new JetStokUser();
            }
        }

        public JetStokUser GetJetstokUserById(long id, long company_id)
        {

            using (var conn = MssqlContext.OpenMarketDBConnection())
            {
                var query = $"select * from jetstok_marketplace.dbo.users WITH(NOLOCK) where is_deleted=0 and id=@id and company_id=@company_id";
                return conn.Query<JetStokUser>(query, new { id, company_id }).FirstOrDefault();
            }
        }

        public JetStokUser GetJetstokUserByEmail(string email)
        {

            using (var conn = MssqlContext.OpenMarketDBConnection())
            {
                var query = $"select * from jetstok_marketplace.dbo.users WITH(NOLOCK) where is_deleted=0 and email=@email";
                return conn.Query<JetStokUser>(query, new { email }).FirstOrDefault();
            }
        }

        public JetStokUser GetJetstokUserByGuid(string guid)
        {

            using (var conn = MssqlContext.OpenMarketDBConnection())
            {
                var query = $"select * from jetstok_marketplace.dbo.users WITH(NOLOCK) where is_deleted=0 and admin_login_key=@guid";
                return conn.Query<JetStokUser>(query, new { guid }).FirstOrDefault();
            }
        }

        public CompanyPackage GetActivePackage(long company_id)
        {
            using (var conn = MssqlContext.OpenMarketDBConnection())
            {
                var query = $"SELECT * FROM jetstok_marketplace.dbo.company_package WITH(NOLOCK) WHERE end_date>GETDATE() and is_sale=1 and company_id=@company_id;";
                return conn.Query<CompanyPackage>(query, new { company_id }).FirstOrDefault();
            }
        }
        public bool InsertPaymentRecord(PaymentLog paymentModel)
        {
            using (var db = MssqlContext.OpenMarketDBConnection())
            {
                return db.Execute(@"INSERT INTO jetstok_marketplace.dbo.payments
(companyid, total_price, installment, shipmentAddress, billAddress, identity_number, tax_no, city, district, is_yearly, amount, status, piece, conversation_id)
VALUES(@companyid, @total_price, @installment, @shipmentAddress, @billAddress, @identity_number, @tax_no, @city, @district, @is_yearly, @amount, @status, @piece, @conversation_id);", paymentModel) > 0;
            }
        }
        public bool UpdateCompanyApprovedActive(long id)
        {
            using (var conn = MssqlContext.OpenMarketDBConnection())
            {
                return conn.Execute("UPDATE jetstok_marketplace.dbo.companies SET is_active=1,approved=1 WHERE id=@id", new { id }) > 0;
            }
        }
    }
}
