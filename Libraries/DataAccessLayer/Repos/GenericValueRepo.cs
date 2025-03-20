using Dapper;
using DataAccessLayer.Context;
using EntityLayer.Models;
using CoreLayer;
using EntityLayer.DTOs;

namespace DataAccessLayer.Repos
{
    public class GenericValueRepo
    {

        public bool Add(GenericValue data)
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                return conn.Execute(
                    "INSERT INTO generic_values (name,summary,image,image_description,image_banner,seo_url,seo_title,seo_description,content_1,content_2,content_3,content_4,content_5,content_6,content_7,parent_1_id,title,subtitle,type_id,order_number,dynamic_title_1,dynamic_description_1,dynamic_title_2,dynamic_description_2,dynamic_title_3,dynamic_description_3,dynamic_title_4,dynamic_description_4,dynamic_title_5,dynamic_description_5) " +
                    "VALUES (@name,@summary,@image,@image_description,@image_banner,@seo_url,@seo_title,@seo_description,@content_1,@content_2,@content_3,@content_4,@content_5,@content_6,@content_7,@parent_1_id,@title,@subtitle,@type_id,@order_number,@dynamic_title_1,@dynamic_description_1,@dynamic_title_2,@dynamic_description_2,@dynamic_title_3,@dynamic_description_3,@dynamic_title_4,@dynamic_description_4,@dynamic_title_5,@dynamic_description_5);", data) > 0;
            }
        }
        public List<GenericValue> GetList(int start, int end, string search, int type_id, out int count)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                search = string.IsNullOrEmpty(search) ? "" : search.ClearToSqlFullLikeQuery();
                var searchQuery = string.IsNullOrEmpty(search) ? "" : $"AND (name like @search)";

                var query = $"SELECT * FROM generic_values where type_id=@type_id {searchQuery} order by 1 desc  OFFSET @start ROWS FETCH NEXT @end ROWS ONLY;";
                if ((type_id==15 && (end-start)<25) || end==10000)
                {
                    query = $"select [id],[name],[content_1],[content_2],[content_3],[content_4],[content_5],[content_6],content_7,[summary],[image],[image_description],[image_banner],[seo_url],[seo_title],[seo_description],[parent_1_id],[title],[subtitle],[type_id],[order_number],[dynamic_title_1],[dynamic_description_1],[dynamic_title_2],[dynamic_description_2],[dynamic_title_3],[dynamic_description_3],[dynamic_title_4],[dynamic_description_4],[dynamic_title_5],[dynamic_description_5],[create_date],[jetstok_package_id],is_pricing_summary FROM generic_values where type_id=@type_id {searchQuery} order by 1 desc  OFFSET @start ROWS FETCH NEXT @end ROWS ONLY;";
                }
                count = conn.Query<int>($"SELECT count(id) FROM generic_values where type_id=@type_id {searchQuery}", new { search, type_id }).FirstOrDefault();
                return conn.Query<GenericValue>(query, new { search, start, end, type_id }).ToList();
            }
        }

        public List<GenericValue> GetListParent(int start, int end, string search, int parent_id, out int count)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                search = string.IsNullOrEmpty(search) ? "" : search.ClearToSqlFullLikeQuery();
                var searchQuery = string.IsNullOrEmpty(search) ? "" : $"AND (name like @search)";

                var query = $"SELECT * FROM generic_values where parent_1_id=@parent_id {searchQuery} order by 1 desc  OFFSET @start ROWS FETCH NEXT @end ROWS ONLY;";
                count = conn.Query<int>($"SELECT count(id) FROM generic_values where parent_1_id=@parent_id {searchQuery}", new { search, parent_id }).FirstOrDefault();
                return conn.Query<GenericValue>(query, new { search, start, end, parent_id }).ToList();
            }
        }

        public List<GenericValue> GetAll(int start, int end, string search, out int count)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                search = string.IsNullOrEmpty(search) ? "" : search.ClearToSqlFullLikeQuery();
                var searchQuery = string.IsNullOrEmpty(search) ? "" : $"AND (name like @search)";

                var query = $"SELECT * FROM generic_values  {searchQuery} order by 1 desc  OFFSET @start ROWS FETCH NEXT @end ROWS ONLY;";
                count = conn.Query<int>($"SELECT count(id) FROM generic_values {searchQuery}", new { search }).FirstOrDefault();
                return conn.Query<GenericValue>(query, new { search, start, end }).ToList();
            }
        }

        public List<GenericValue> GetHome(List<int> types, List<int> ids)
        {

            using (var conn = MssqlContext.OpenConnection())
            {

                var query = $"SELECT * FROM generic_values  where type_id in ({String.Join(",",types)}) or id in ({String.Join(",", ids)})"; 
                return conn.Query<GenericValue>(query).ToList();
            }
        }

        public GenericValue Get(int id)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * from generic_values where id=@id";
                return conn.Query<GenericValue>(query, new { id }).FirstOrDefault();
            }
        }
        public GenericValue Get(string url)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * from generic_values where seo_url=@seo_url";
                return conn.Query<GenericValue>(query, new { seo_url = url }).FirstOrDefault();
            }
        }
        public bool Delete(GenericValueDTO data)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"delete from generic_values where id=@id";
                return conn.Execute(query, data) > 0;
            }
        }


        public bool Update(GenericValue data)
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"UPDATE generic_values SET name=@name, summary=@summary, image=@image,image_description=@image_description,image_banner=@image_banner, seo_url=@seo_url, seo_title=@seo_title, seo_description=@seo_description, content_1=@content_1, content_2=@content_2, content_3=@content_3, content_4=@content_4, content_5=@content_5,content_6=@content_6,content_7=@content_7, parent_1_id=@parent_1_id, title=@title, subtitle=@subtitle, order_number=@order_number,dynamic_title_1=@dynamic_title_1,dynamic_description_1=@dynamic_description_1,dynamic_title_2=@dynamic_title_2,dynamic_description_2=@dynamic_description_2,dynamic_title_3=@dynamic_title_3,dynamic_description_3=@dynamic_description_3,dynamic_title_4=@dynamic_title_4,dynamic_description_4=@dynamic_description_4,dynamic_title_5=@dynamic_title_5,dynamic_description_5=@dynamic_description_5 WHERE id=@id";

                return conn.Execute(query, data) > 0;
            }
        }

        public List<GenericValue> GetBlogCategoryList()
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * FROM generic_values where type_id=30 ;";
                return conn.Query<GenericValue>(query).ToList();
            }
        }

        public GenericValue GetPackagesByJetstokId(int id)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * from generic_values where jetstok_package_id=@id";
                return conn.Query<GenericValue>(query, new { id }).FirstOrDefault();
            }
        }
    }
}
