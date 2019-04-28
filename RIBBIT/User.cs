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
        public string email { get; set; }
        public int postKarma { get; set; }
        public int commentKarma { get; set; }
        public string about { get; set; }   //user info
        public List<int> posts { get; set; }
        public List<int> comments { get; set; }
        public string profilePicture { get; set; } //img source
        public bool isGold { get; set; }
        public List<int> trophies { get; set; }
        public List<int[]> vote { get; set; }
        public List<int> saves { get; set; }

        public User(string username, string password, string email)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.accountBirthday = DateTime.Now.Date;
            this.posts = new List<int>();
            this.comments = new List<int>();
            this.vote = new List<int[]>();
            this.trophies = new List<int>();
            this.saves = new List<int>();
            this.profilePicture = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Users\pics\defaultPic.png");

        }
        public User() { }
    }
}
