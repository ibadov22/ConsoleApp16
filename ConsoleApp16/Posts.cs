using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postnamepsace
{
    public class Post
    {
        public Guid ID { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public decimal LikeCount { get; set; }
        public decimal ViewCount { get; set; }

        public Post()
        {
            ID = Guid.NewGuid();
            CreationDateTime = DateTime.Now;
            LikeCount = 0;
            ViewCount = 0;
        }

        public void Contents()
        {

        }

        public void AddLike() => LikeCount++;
        public void AddView() => ViewCount++;
    }
}
