namespace EntityLayer.DTOs.JetStokApp
{
    public class PackageBaseDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal yearly_price { get; set; }
        public decimal package_product_price { get; set; }
        public decimal basket_total_price { get; set; }
        public int market_place_count { get; set; }
        public int xml_product_count { get; set; }
        public int xml_integration_count { get; set; }
        public bool advenced_reporting { get; set; }
        public bool critical_stock_alert { get; set; }
        public int user_count { get; set; }
        public int xml_update_time_interval { get; set; }
        public bool excel_import { get; set; }
        public bool excel_export { get; set; }
        public int online_training_count { get; set; }
        public int support_request_count { get; set; }
        public bool intelligent_stock_distribution { get; set; }
        public bool exchange_rate { get; set; }
        public bool gold_dry { get; set; }
        public bool remove_jet_stock_ads { get; set; }
        public bool ready_feature_copy { get; set; }
        public bool bundle_product { get; set; }
        public bool competition_analysis { get; set; }
        public bool parasut { get; set; }
        public bool store_sales { get; set; }
        public bool bulk_price_update { get; set; }
        public bool jet_stock_api { get; set; }
        public bool cargo_integration { get; set; }
        public bool xml_export { get; set; }
        public int product_count { get; set; }
        public int currency_exchange_count { get; set; }
        public bool global { get; set; }
        public bool accounting_integration { get; set; }
        public int disc_size { get; set; }
        public int jetstok_product_pool { get; set; }
        public int language_count { get; set; }
        public bool product_link_download { get; set; }
        public bool bulk_product_transfer { get; set; }
        public bool automatic_bill_send { get; set; }
        public bool customer_question { get; set; }
        public bool sms { get; set; }
        public int discount { get; set; }
        public decimal discount_price { get; set; }

    }
}
