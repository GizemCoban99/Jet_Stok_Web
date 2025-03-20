using Dapper;
using DataAccessLayer.Context;
using EntityLayer.Models;

namespace DataAccessLayer.Repos
{
    public class PermissionRepo
    {
        public List<Permission> GetPermissions()
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * FROM permissions ORDER BY group_id ASC;";
                return conn.Query<Permission>(query).ToList();
            }
        }
        public List<Permission> GetPermissions(string permissions)
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * FROM permissions where id in ({permissions}) ORDER BY group_id ASC;";
                return conn.Query<Permission>(query).ToList();
            }
        }
        public List<PermissionGroup> GetPermissionGroup()
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT DISTINCT group_id, group_name FROM permissions ORDER BY group_id ASC;";
                return conn.Query<PermissionGroup>(query).ToList();
            }
        }
        public Permission GetByIdPermission(int id)
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT * FROM permissions WHERE id=@id;";
                return conn.Query<Permission>(query, new { id }).FirstOrDefault();
            }
        }
    }
}
