using Library.Common;
using Library.Common.Contracts;
using Library.Data;
using Library.Services;
using Library.Services.Contracts;
using Library.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Library.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //DB
            var connectionStrings = config.GetConnectionString("Anjelo");  /* Write connection string here */
            services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connectionStrings));

            //Services
            // services.AddScoped<I, >();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBanService, BanService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IMailSettings, MailSettings>();
            services.AddTransient<IMailService, MailService>();
            services.AddScoped<IInboxService, InboxService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILoanService,LoanService >();
            services.AddScoped<IPublishHouseService, PublishHouseService>();
            services.AddScoped<GetSelectListService>();

            services.AddHostedService<BanHostedService>();
            return services;
        }
    }
}
