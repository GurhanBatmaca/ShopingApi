using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using shopapp.business.Abstract;
using shopapp.business.Concrete;
using shopapp.data.Abstract;
using shopapp.data.Concrete.EfCore;
using shopapp.webapi.Identity;
using shopapp.webui.EmailServices;
using shopapp.webui.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"));
});

builder.Services.AddDbContext<ApplicationContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"));
});

builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    // password
    options.Password.RequireDigit = true; // şifrede numara olmak zorunda
    options.Password.RequireLowercase = true; // küçük harf 
    options.Password.RequireUppercase = true; // büyük harf 
    options.Password.RequiredLength = 6; // 6 karakter minimum
    options.Password.RequireNonAlphanumeric = true; // alphanumeric 

    // lockout
    options.Lockout.MaxFailedAccessAttempts = 5; // 5 hatadan sonra hesap kilitlenir
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // kilitli hesap kaç dk sonra açılsın
    options.Lockout.AllowedForNewUsers = true; // lockout aktif olması için

    // options.User.AllowedUserNameCharacters = ""; // isimde olmasını istediğin ekler
    options.User.RequireUniqueEmail = true; // 1 mail adresi ile tek kullanıcı
    options.SignIn.RequireConfirmedEmail = true; // mail adresi onaylamadan giriş yapamaz
    options.SignIn.RequireConfirmedPhoneNumber = false; // telefonla onaylamadan giriş yapamaz
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/account/login"; // cookie tanınmaz ise vs gideceği sayfa
    options.LogoutPath = "/account/logout"; // çıkış yapınca gidilecek yer
    options.AccessDeniedPath = "/account/accessdenied"; // yetkisiz yere girmek istenince gidlecek sayfa
    options.SlidingExpiration = true; // giriş yaptıktan sonra istek yaptığında var sayılan süreyi sıfırlar(alttaki süre tekrar başlar)
    options.ExpireTimeSpan = TimeSpan.FromDays(7); // giriş yaptıktan sonra ne kadar süre site kullanıcıyı tanısın(istek yapmadan, eğer istek yaparsa üstteki ayar sayesinde sıfırlanır)

    // FromMinutes(60)

    options.Cookie = new CookieBuilder
    {
        HttpOnly = true, // cookieler sadece http isteği ile alınır
        Name = ".MyShopapp.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();   

builder.Services.AddScoped<IProductService,ProductManager>();   
builder.Services.AddScoped<ICategoryService,CategoryManager>();  

builder.Services.AddCors(options => 
{
    options.AddPolicy(
        name: "_myAllowOrigins",
        builder => {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});


builder.Services.AddAuthentication(auth => {
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "shopingapi",
        ValidIssuer = "shopingapi",
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the key we will use")),
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddScoped<IEmailSender,SmtpEmailSender>( i => 
    new SmtpEmailSender(
        builder.Configuration["EmailSender:Host"]!,
        builder.Configuration.GetValue<int>("EmailSender:Port"),
        builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
        builder.Configuration["EmailSender:UserName"]!,
        builder.Configuration["EmailSender:Password"]!
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("_myAllowOrigins");

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
