using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hummm.Models;
using Hummm.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using Hummm.Models.ViewModel;

namespace Hummm.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _hosting;

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger, IWebHostEnvironment hosting)
        {
            _context = context;
            _logger = logger;
            _hosting = hosting;
        }

        //検索結果の入れ物
        public Consumer Consumer { get; set; }
        public List<Post> Posts { get; set; }
        public List<PostVM> PostsVM { get; set; }
        public string[] JpegFilenameList { get; set; }
        public string[] Mp3FilenameList { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            //最新の投稿を表示
            //最新の投稿を取得
            //後でページネーションを用意
            Posts = await _context.Posts.ToListAsync();

            //検索された場合
            if (searchString != null)
            {
                //検索文字列に応じて検索結果を変更
                Posts = await _context.Posts
                                .Where(post => post.Title.Contains(searchString) || post.Description.Contains(searchString))
                                .AsNoTracking()
                                .ToListAsync();

                //検索履歴
                Consumer consumer = new Consumer
                {
                    SearchString = searchString,
                    SearchDate = DateTime.Now.Date
                };

                //コンテキストに保存
                _context.Consumers.Add(consumer);

                //データベースに保存
                await _context.SaveChangesAsync();
            }

            //取得したPostデータをwwwrootに保存
            var root = this._hosting.WebRootPath;
            string path = Path.Combine(root, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (var post in Posts)
            {
                //ファイル名を動的に処理
                string jpegFileName = Path.GetFileName(post.PostID + post.Title +
                    post.PostDate.ToString("yyyy-dd-M--HH-mm-ss") + ".jpeg");
                string mp3FileName = Path.GetFileName(post.PostID + post.Title +
                    post.PostDate.ToString("yyyy-dd-M--HH-mm-ss") + ".mp3");

                //ファイル情報オブジェクトを作成
                FileInfo fileinfo = new FileInfo(path + @"\" + jpegFileName);
                //ファイル作成から書き込みまで実行
                using (FileStream stream = fileinfo.Create())
                {
                    for (int i = 0; i <= post.Image.Length - 1; i++)
                    {
                        stream.WriteByte(post.Image[i]);
                    }
                }

                //上と同様
                FileInfo mp3FileInfo = new FileInfo(path + @"\" + mp3FileName);
                using (FileStream stream = mp3FileInfo.Create())
                {
                    for (int i = 0; i <= post.MP3.Length - 1; i++)
                    {
                        stream.WriteByte(post.MP3[i]);
                    }
                }
            }

            //wwwrootに保存してあるファイル名リストを動的に処理
            JpegFilenameList = Directory.GetFiles(path, "*.jpeg");
            Mp3FilenameList = Directory.GetFiles(path, "*.mp3");

            for (int i = 0; i <= JpegFilenameList.Length - 1; i++)
            {
                JpegFilenameList[i] = Path.GetFileName(JpegFilenameList[i]);
                Mp3FilenameList[i] = Path.GetFileName(Mp3FilenameList[i]);

                JpegFilenameList[i] = "/Uploads/" + JpegFilenameList[i];
                Mp3FilenameList[i] = "/Uploads/" + Mp3FilenameList[i];
            }
        }
    }
}
