using Book4H2Ten.Core.Helpers;
using Book4H2Ten.EntityFrameWorkCore;
using Book4H2Ten.EntityFrameWorkCore.Repositories;
using Book4H2Ten.Services.Books;
using Book4H2Ten.Services.Carts;
using Book4H2Ten.Services.Emails;
using Book4H2Ten.Services.OrderDetails;
using Book4H2Ten.Services.Orders;
using Book4H2Ten.Services.Tokens;
using Book4H2Ten.Services.TypeBooks;
using Book4H2Ten.Services.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;

namespace Book4H2Ten.Host.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("Book4H2TenDbContext");
            services.AddDbContext<Book4H2TenDbContext>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            services.AddScoped<IUnitOfWork<Book4H2TenDbContext>, UnitOfWork<Book4H2TenDbContext>>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IUserNotificationSettingService, UserNotificationSettingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITypeBookService, TypeBookService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
