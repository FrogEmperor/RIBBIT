using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIBBIT
{
    public class Comment:Upload
    {
        public Comment(string owner, int ID, string content): 
            base(owner, ID)
        {
          this.content = content;
        }
        public override string ToString()
        {
            string votesString;
            int votes = upvote - downvote;
            if (votes>=0)
            {
                votesString = "+" + votes.ToString();
            }
            else
            {
                votesString = votes.ToString();
            }
            string comment = votesString+"/"+owner + "⚫\n" + content;
            return comment;
        }
    }
}
