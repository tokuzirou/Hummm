using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hummm.Data;
using Hummm.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hummm.Models.ViewModel;

namespace Hummm.Pages.MyPage
{
    public class CreateModel : PageModel
    {
        private readonly Hummm.Data.ApplicationDbContext _context;

        public CreateModel(Hummm.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGetAsync()
        {
            //投稿ページを表示
            return Page();
        }

        [BindProperty]
        public PostVM PostVM {  get; set; } 

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //投稿時検証がクリアしていない場合、
            //ページを再表示
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(PostVM.Image == null)
            {
                return Page();
            }

            if(PostVM.MP3 == null)
            {
                return Page();
            }

            byte[] image = default;
            byte[] mp3 = default;

            //IFormFileオブジェクトをmemoryStreamを介して、Byte配列にする
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //memoryStreamに画像データをコピー
                await PostVM.Image.CopyToAsync(memoryStream);

                image = memoryStream.ToArray();
            }
            //mp3オブジェクトにも同様の処理をかける
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await PostVM.MP3.CopyToAsync(memoryStream);

                mp3 = memoryStream.ToArray();
            }

            //Postオブジェクト
            Post post = new Post
            {
                Title = PostVM.Title,
                Description = PostVM.Description,
                Image = image,
                MP3 = mp3,
                PostDate = DateTime.Now
            };

            //投稿内容を更新
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            //リダイレクト
            return RedirectToPage("./Index");
        }
    }
}
