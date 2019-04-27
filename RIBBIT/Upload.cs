using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIBBIT
{
    public abstract class Upload
    {
        public int ID { get; }
        public string owner { get; }
        public DateTime creationDate { get; }
        public int upvote { get; set; }
        public int downvote { get; set; }
        public List<int> comments { get; set; }
        public int award { get; set; }
        public List<string> tags { get; set; }
        public string content { get; set; }
        public Upload(string owner,int ID)
        {
            this.ID = ID;
            this.owner = owner;
            this.creationDate = DateTime.Now;
            this.comments = new List<int>();
            this.tags = new List<string>();
            this.upvote = 0;
            this.downvote = 0;
        }
    }
}
