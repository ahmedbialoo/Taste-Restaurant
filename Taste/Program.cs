using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Taste.DataAccess;
using Taste.DataAccess.Data.Repository;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "This Field is Required.");
})
    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);



builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "1018666975395185";
    options.AppSecret = "f28cd4652131ff08132fd8be5f7e80a0";
});
builder.Services.AddAuthentication().AddMicrosoftAccount(options =>
{
    options.ClientId = "94ea46fe-6046-4060-a608-05b3ebcadc4d";
    options.ClientSecret = "F427Q~mnHYtDtAgr3ppo9cFYAxY2gUirGRPh_";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretKey"];
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.UseMvc();


app.Run();
