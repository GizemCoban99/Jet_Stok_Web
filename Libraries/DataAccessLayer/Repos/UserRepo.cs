using Dapper;
using DataAccessLayer.Context;
using EntityLayer.Models; 

namespace DataAccessLayer.Repos
{
    public class UserRepo
    {
        public User Login(User user)
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"select * from users where email=@email and password=@password and is_deleted=0;";
               
                return conn.Query<User>(query, user).FirstOrDefault();
            }
        }
        public User Add(User user)
        {
            user.create_date = DateTime.Now;
            using (var conn = MssqlContext.OpenConnection())
            {
                int result = conn.Query<int>("INSERT INTO users (email,name,surname,create_date,password,phone,  tc_number,role_id) VALUES (@email,@name,@surname,@create_date,@password,@phone,@tc_number, @role_id); SELECT SCOPE_IDENTITY();", user).FirstOrDefault();

                return GetUserById(result);
            }
        }
        public List<User> GetAllUsers()
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"SELECT u.*, r.name as role_name FROM users u INNER JOIN roles r on r.id = u.role_id WHERE u.is_deleted=0 ORDER BY u.id ASC";
                return conn.Query<User>(query).ToList();
            }
        }

        public User GetUserById(int id)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"select * from users where is_deleted=0 and id=@id";
                return conn.Query<User>(query, new { id }).FirstOrDefault();
            }
        }

        public User Update(User user)
        {
            using (var conn = MssqlContext.OpenConnection())
            { 
                var query = $"UPDATE users SET name=@name, surname=@surname, role_id=@role_id, phone=@phone,tc_number=@tc_number, {(string.IsNullOrEmpty(user.password)?"": "password=@password,")}  last_login_date=@last_login_date WHERE is_deleted=0 and id=@id";
               
                bool result = conn.Execute(query, user) > 0;

                if (result)
                {
                    return GetUserById(user.id);
                }
                else
                {
                    return new User();
                }
            }
        }

        public bool Delete(User user)
        {
            using (var conn = MssqlContext.OpenConnection())
            {
                bool result = conn.Execute("UPDATE users SET is_deleted = 1 WHERE id=@id;",user) > 0;

                return result;
            }
        }
        public User HasUserByEmail(string email)
        {

            using (var conn = MssqlContext.OpenConnection())
            {
                var query = $"select * from users where is_deleted=0 and email=@email";
                return conn.Query<User>(query, new { email }).FirstOrDefault();
            }
        }
    }
}
