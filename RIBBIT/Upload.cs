using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIBBIT
{
    abstract class Upload
    {
        public string userName { get; }
        public DateTime creationDate { get; }
        public int upvote { get; set; }
        public int downvote { get; set; }
        public List<Comment> comments { get; set; }
        public int award { get; set; }
        public List<string> tags { get; set; }
        public string content { get; set; }
    }
}
