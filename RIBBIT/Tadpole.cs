using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIBBIT
{
    public class Tadpole
    {
        public string tadpoleName { get; set; }
        public List<int> posts { get; set; }
        public List<string> followers { get; set; }
        public string about { get; set; }
        public string slogan { get; set; }

        public Tadpole(string tadpoleName, string slogan, string about)
        {
            this.tadpoleName = tadpoleName;
            this.slogan = slogan;
            this.about = about;
        }
    }
}
