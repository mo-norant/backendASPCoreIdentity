using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIdentityServer.data.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public string ID { get; set; }
        public DateTime dateTime { get; set; }
        public string content { get; set; }

    }
}
