using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIBBIT
{
    class User
    {
        public string username { get; }
        public DateTime userBirthday { get; }
        public DateTime accountBirthday { get; }
        public string password { get; }
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

        public int age  //edad de la persona en años
        {
            get
            {
                return DateTime.Now.Year - userBirthday.Year;
            }
        }

        public User(string username, DateTime userBirthday, string password, string email, string about, string profilePicture)
        {
            this.username = username;
            this.userBirthday = userBirthday;
            this.password = password;
            this.email = email;
            this.about = about;
            this.profilePicture = profilePicture;
        }
    }
}
