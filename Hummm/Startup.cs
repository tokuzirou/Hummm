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
            //�ڑ�����f�[�^�x�[�X�v���o�C�_�[�T�[�r�X
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SqlServer")));

            //�J�����̗�O�y�[�W�T�[�r�X
            //services.AddDatabaseDeveloperPageExceptionFilter();

            //���O�C���T�[�r�X�̐ݒ�
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            //Razor�y�[�W�ɂ��ẴI�v�V�����T�[�r�X
            services.AddRazorPages().AddRazorPagesOptions(options => {
                //���O�C�����Ȃ��ꍇ�A�}�C�y�[�W�ɂ̓A�N�Z�X�ł��Ȃ�
                options.Conventions.AuthorizeFolder("/MyPage");
                options.Conventions.AuthorizePage("/MyPage/Index");
                options.Conventions.AuthorizePage("/MyPage/Create");
                options.Conventions.AuthorizePage("/MyPage/Delete");
                options.Conventions.AuthorizePage("/MyPage/Edit");
            });

            //services.AddDirectoryBrowser();
        }

        //�~�h���E�F�A�Ăяo��
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //�J����
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //���ғ���
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