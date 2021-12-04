using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{


    public interface InMemoryDatabase<T>
    {
        List<T> Data { get; set; }
    }

    public interface IUserDatabase
    {
        public void AddUser(string username);
        public List<string> GetAllUsers();
    }

    public class UserDatabase : InMemoryDatabase<string>, IUserDatabase
    {
        public List<string> Data { get; set; }
        public string GuidStr;
        public UserDatabase()
        {
            GuidStr = Guid.NewGuid().ToString();
            Console.WriteLine(GuidStr);
            Data = new List<string>();
        }

        public void AddUser(string username)
        {
            Data.Add(username);
        }


        public List<string> GetAllUsers()
        {
            return Data;
        }
    }
}
