using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Hummm.Models.ViewModel
{
    public class PostVM
    {
        [Required(ErrorMessage = "タイトルは必須です")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "タイトルは1文字以上50文字以内である必要があります")]
        [Display(Name = "タイトル")]
        public string Title { get; set; }

        [Required(ErrorMessage = "説明は必須です")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "説明は1文字以上50文字以内である必要があります")]
        [Display(Name = "説明欄")]
        public string Description { get; set; }

        [Required(ErrorMessage = "イメージ画像は必須です")]
        [Display(Name = "イメージ画像(.jpeg)")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "mp3ファイルは必須です")]
        [Display(Name = "音楽データ(.mp3)")]
        public IFormFile MP3 { get; set; }
    }
}
