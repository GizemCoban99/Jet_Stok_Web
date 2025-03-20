using Dapper;
using DataAccessLayer.Context;
using EntityLayer.Models; 

namespace DataAccessLayer.Repos
{
    public class GenericTypeRepo
    {
        
        public bool Add(GenericType data)
        { 
            using (var conn = MssqlContext.OpenConnection())
            {
                return conn.Execute("INSERT INTO generic_types ([type_name],is_name,is_summary,is_image,is_description_image,is_seo_url,is_seo_title,is_seo_description,is_content_1,is_content_2,is_content_3,is_content_4,is_content_5,is_parent_1,parent_1_id,parent_1_title,is_title,is_subtitle,is_order_number,is_sss) VALUES (@type_name,@is_name,@is_summary,@is_image,@is_description_image,@is_seo_url,@is_seo_title,@is_seo_description,@is_content_1,@is_content_2,@is_content_3,@is_content_4,@is_content_5,@is_parent_1,@parent_1_id,@parent_1_title,@is_title,@is_subtitle,@is_order_number,@is_sss);", data) >0; 
            }
        }
        public List<GenericType> GetList()
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * from generic_types ORDER BY id desc";
                return conn.Query<GenericType>(query).ToList();
            }
        }
        public GenericType Get(int id)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * from generic_types where id=@id";
                return conn.Query<GenericType>(query, new {id=id }).FirstOrDefault();
            }
        }


        public bool Update(GenericType data)
        {
            using (var conn = MssqlContext.OpenConnection())
            { 
                var query = $"UPDATE generic_types SET [type_name]=@type_name, is_name=@is_name, is_summary=@is_summary, is_image=@is_image, is_description_image=@is_description_image, is_seo_url=@is_seo_url, is_seo_title=@is_seo_title, is_seo_description=@is_seo_description, is_content_1=@is_content_1, is_content_2=@is_content_2, is_content_3=@is_content_3, is_content_4=@is_content_4, is_content_5=@is_content_5, is_parent_1=@is_parent_1, parent_1_id=@parent_1_id, parent_1_title=@parent_1_title, is_title=@is_title, is_subtitle=@is_subtitle, is_order_number=@is_order_number, is_sss=@is_sss WHERE id=@id";
               
               return conn.Execute(query, data) > 0; 
            }
        } 
    }
}
