namespace API.Models
{
    public class User
    {
        public long id { get; set; }
        public string user { get; set; } = "";
        public string password { get; set; } = "";
        public int? roleID { get; set; } 
    }
    public class UserLoginResponse
    {
        public string name { get; set; } = "";
        public string token { get; set; } = "";
        public int? roleID { get; set; }
    }
    public class UserLogin
    {
        public string email { get; set; } = "";
        public string password { get; set; } = "";
    }
    public class UserInsert
    {
        public string email { get; set; } = "";
        public string name { get; set; } = "";
        public string password { get; set; } = "";
        public int? roleID { get; set; }
    }
    public class UserUpdate
    {
        public string email { get; set; } = "";
        public string name { get; set; } = "";
        public string password { get; set; } = "";
        public int? roleID { get; set; }
    }

}
