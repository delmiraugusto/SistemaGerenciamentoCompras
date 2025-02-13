using API.Core;
using API.Models;
using Microsoft.Data.Sqlite;

namespace API.Repositories
{
    public class UserRepository : IRepository<UserInsert, UserUpdate>
    {
        public string TABLE => "User";

        public dynamic SetAttributes(SqliteDataReader reader) => new User()
        {
            id = long.Parse(reader["id"].ToString()!),
            user = reader["email"].ToString()!,
            password = reader["password"].ToString()!,
            roleID = int.Parse(reader["roleID"].ToString()!)
        };

        public int Insert(UserInsert obj)
        {
            using DB db = new();
            db.NewCommand($"INSERT INTO {TABLE} (email, password, name, roleId) VALUES (@email, @password, @name, @roleID)");
            db.Parameter("@email", obj.email);
            db.Parameter("@password", obj.password);
            db.Parameter("@name", obj.name);
            db.Parameter("@roleID", obj.roleID);
            return db.Execute();
        }

        public List<dynamic> SelectAll()
        {
            using DB db = new();
            db.NewCommand($"SELECT id, email, password, roleID, name FROM {TABLE}");
            List<dynamic> list = [];
            using SqliteDataReader reader = db.Execute();
            while (reader.Read())
            {
                list.Add(SetAttributes(reader));
            }
            return list;
        }

        public dynamic SelectByEmail(string email)
        {
            using DB db = new();

            try
            {
                db.NewCommand($"SELECT id, email, password, roleID, name FROM {TABLE} WHERE email = @email");
                db.Parameter("@email", email);
                using SqliteDataReader reader = db.Execute();

                if (reader.Read())
                    return SetAttributes(reader);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Invalid email or password.{ex.Message}");
            }
        }


        public dynamic SelectById(long id)
        {
            using DB db = new();
            db.NewCommand($"SELECT id, email, password, roleID, name FROM {TABLE} WHERE id = @id");
            db.Parameter("@id", id);
            using SqliteDataReader reader = db.Execute();
            if (reader.Read())
            {
                return SetAttributes(reader);
            }

            return null;
        }

        public int UpdateById(UserUpdate obj, long id)
        {
            using DB db = new();
            db.NewCommand($"UPDATE {TABLE} SET email=@email, password=@password, name=@name WHERE id = @id");
            db.Parameter("@id", id);
            db.Parameter("@email", obj.email);
            db.Parameter("@password", obj.password);
            db.Parameter("@name", obj.name);
            return db.Execute();
        }

        public int DeleteById(long id)
        {
            using DB db = new();
            db.NewCommand($"DELETE FROM {TABLE} WHERE id = @id");
            db.Parameter("@id", id);
            return db.Execute();
        }
    }
}
