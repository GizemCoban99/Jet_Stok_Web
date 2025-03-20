using Dapper;
using DataAccessLayer.Context;
using EntityLayer.Models; 

namespace DataAccessLayer.Repos
{
    public class RoleRepo
    {
        public List<Role> GetRoles()
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * FROM roles WHERE is_deleted=0 ORDER BY id DESC;";
                return conn.Query<Role>(query).ToList();
            }
        }
 
        public Role GetByIdRole(int id)
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * FROM roles WHERE is_deleted=0 AND id=@id;";
                return conn.Query<Role>(query, new { id }).FirstOrDefault();
            }
        }
        public Role Add(Role data)
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                int result = conn.Query<int>("INSERT INTO roles (name,description,permissions) VALUES (@name,@description,@permissions); SELECT SCOPE_IDENTITY();", data).FirstOrDefault();

                return GetByIdRole(result);
            }
        }

        public Role Update(Role data)
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                bool result = conn.Execute("UPDATE roles SET name=@name, description=@description, permissions=@permissions WHERE id=@id", data) > 0;

                if (result)
                {
                    return GetByIdRole(data.id);
                }
                else
                {
                    return new Role();
                }
            }

        }

        public bool Delete(int id)
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                bool result = conn.Execute("UPDATE roles SET is_deleted=1 WHERE id=@id;", new { id }) > 0;
                return result;
            }
        }

        public Role GetRoleCheck(Role role)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"select * from roles where is_deleted=0 and id<>@id and name=@name";
                return conn.Query<Role>(query, role).FirstOrDefault();
            }
        }
    }
}
