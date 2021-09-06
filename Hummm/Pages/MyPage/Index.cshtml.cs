using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hummm.Data;
using Hummm.Models;

namespace Hummm.Pages.MyPage
{
    public class IndexModel : PageModel
    {
        private readonly Hummm.Data.ApplicationDbContext _context;

        public IndexModel(Hummm.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Poster Poster { get;set; }

        public async Task OnGetAsync()
        {
            //Posterのidを取得する方法を考えなければならない


            //Posterの投稿履歴を取得
            Poster = await _context.Posters.Include(p => p.Posts).FirstOrDefaultAsync(p => p.PosterID == 0);
        }
    }
}
