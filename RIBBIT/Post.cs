using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIBBIT
{
    class Post:Upload
    {
        public string title { get; set; }
        public string[] images { get; }
        public string video { get; }
        public Post(string title, string[] images, string video, string content)
        {
            this.images = images;
            this.video = video;
            this.content = content;
        }
    }
}
