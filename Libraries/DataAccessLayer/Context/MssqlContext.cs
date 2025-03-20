using System.Data.SqlClient;

namespace DataAccessLayer.Context {
    public class MssqlContext {
        private static string conn = $"Server=88.218.130.122; Database=jet_stok_web_site; User ID=jet_stok_web_user; Password=J!2et1St2asd213!2;";
        private static string marketDbConnectionString = $"server=141.98.207.218,1453; database=jetstok_marketplace;uid=jet_stok_commission_calculate_user; pwd=JM1et!.Bgs125.28!!.!;";

        public static SqlConnection OpenMarketDBConnection()
        {
            SqlConnection con = new SqlConnection(marketDbConnectionString);
            return con;
        }
        public static SqlConnection OpenConnection() {
            SqlConnection con = new SqlConnection(conn);
            return con;
        }
    }
}
