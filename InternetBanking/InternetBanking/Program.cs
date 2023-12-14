using InternetBanking.Models;
using InternetBanking.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
// using InternetBanking.Data;
using InternetBanking.Areas.Identity.Data;
using InternetBanking.Service.MailService;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("IdentityConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<InternetBankingContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddIdentity<InternetBankingUser, IdentityRole>()
            .AddEntityFrameworkStores<InternetBankingContext>()
            .AddDefaultTokenProviders();
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        //builder.Services.AddDefaultIdentity<InternetBankingUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<InternetBankingContext>();


        builder.Services.Configure<IdentityOptions>(options =>
        {
            //thiet lap password
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 1;
            //cau hinh logout - khoa user
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.AllowedForNewUsers = true;
            //cau hinh nguoi dung
            options.User.AllowedUserNameCharacters = "zxcvbnmasdfghjklqwertyuiopZXCVBNMASDFGHJKLQWERTYUIOP1234567890~!@#$%^&+-.";
            options.User.RequireUniqueEmail = true;
            //cau hinh dang nhap
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedAccount = true;

        });
        builder.Services.AddOptions();
        var mailSettings = builder.Configuration.GetSection("MailSettings");
        builder.Services.Configure<MailSetting>(mailSettings);

        builder.Services.AddTransient<TransactionService>();
        builder.Services.AddTransient<IEmailSender,SendConfirmationMail>();
        builder.Services.AddTransient<SendMailService>();
        builder.Services.AddAuthentication().AddGoogle(options =>
        {
            IConfigurationSection section = builder.Configuration.GetSection("Authentication:Google");
            options.ClientId = section["ClientId"];
            options.ClientSecret = section["ClientSecret"];
            options.CallbackPath = "/dang-nhap-google";
        });

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";
            options.LogoutPath = "/Identity/Account/Logout";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );
        app.MapRazorPages();
        app.Run();

    }
    
}