using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hummm.Models
{
    public class Poster
    {
        public int PosterID { get; set; }
        public string PosterName { get; set; }

        //外部キー
        public int PostID { get; set; }

        //ナビゲーションプロパティ
        public IList<Post> Posts { get; set; }
    }
}
