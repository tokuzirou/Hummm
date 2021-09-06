using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hummm.Models
{
    public class Consumer
    { 
        public int ConsumerID { get; set; }
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string SearchString { get; set; }
        [DataType(DataType.Date)]
        public DateTime SearchDate { get; set; }

        //外部キー
        public int PostID { get; set; }

        //ナビゲーションプロパティ
        public IList<Post> Posts { get; set; }
    }
}
