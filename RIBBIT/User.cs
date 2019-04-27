using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RIBBIT
{
    public class User
    {
        public string username { get; set; }
        public string displayName { get; set; }
        public DateTime accountBirthday { get; set; }
        public string password { get; set; }
        public List<Tadpole> follows { get; set; }  //subreddits who follows
        public string email { get; set; }
        public int postKarma { get; set; }
        public int commentKarma { get; set; }
        public string about { get; set; }   //user info
        public List<Message> messages { get; set; }
        public List<int> posts { get; set; }
        public List<int> comments { get; set; }
        public string profilePicture { get; set; } //img source
        public bool isGold { get; set; }
        public List<string> trophies { get; set; }

        public User(string username, string password, string email)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            accountBirthday = DateTime.Now.Date;
            follows = new List<Tadpole>();
            messages = new List<Message>();
            posts = new List<int>();
            comments = new List<int>();
            trophies = new List<string>();
            trophies.Add("New User");
            profilePicture = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Users\pics\defaultPic.png");
            
        }
        public User() { }
    }
}
