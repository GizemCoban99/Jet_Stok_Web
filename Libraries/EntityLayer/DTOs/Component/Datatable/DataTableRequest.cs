namespace EntityLayer.DTOs.Component.Datatable
{
    public class DataTableRequest
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public DataTableRequestSearch search { get; set; }

        public class DataTableRequestSearch
        {
            public string value { get; set; }
        }
    }
}
