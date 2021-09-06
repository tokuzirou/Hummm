using Hummm.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Hummm
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //接続するデータベースプロバイダーサービス
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SqlServer")));

            //開発中の例外ページサービス
            //services.AddDatabaseDeveloperPageExceptionFilter();

            //ログインサービスの設定
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            //Razorページについてのオプションサービス
            services.AddRazorPages().AddRazorPagesOptions(options => {
                //ログインしない場合、マイページにはアクセスできない
                options.Conventions.AuthorizeFolder("/MyPage");
                options.Conventions.AuthorizePage("/MyPage/Index");
                options.Conventions.AuthorizePage("/MyPage/Create");
                options.Conventions.AuthorizePage("/MyPage/Delete");
                options.Conventions.AuthorizePage("/MyPage/Edit");
            });

            //services.AddDirectoryBrowser();
        }

        //ミドルウェア呼び出し
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //開発時
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //実稼働時
            if (env.IsProduction()){
                app.UseExceptionHandler("/Error");
            }

            app.UseMigrationsEndPoint();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
