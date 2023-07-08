using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.RepositoryManagement.UnityOfWork;
//using API.DataAccess.ORM.MsSQLDataModels;
using API.ServiceRegister;
using API.Settings;
using Microsoft.EntityFrameworkCore;
using API.signalr_hub;
//using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//Access AppSettings.JSON
var configBuilder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
IConfiguration _configuration = configBuilder.Build();
StaticInfos.MsSqlConnectionString = _configuration.GetValue<string>("MSSQLconCF");
StaticInfos.MySqlConnectionString = _configuration.GetValue<string>("MySqlConnectionString");
StaticInfos.PostgreSqlConnectionString = _configuration.GetValue<string>("PostGreSqlConnectionString");
StaticInfos.IsMsSQL = _configuration.GetValue<bool>("IsMsSQL");
StaticInfos.IsMySQL = _configuration.GetValue<bool>("IsMySQL");
StaticInfos.IsPostgreSQL = _configuration.GetValue<bool>("IsPostgreSQL");
StaticInfos.JwtKey = _configuration.GetValue<string>("Jwt:Key");
StaticInfos.JwtIssuer = _configuration.GetValue<string>("Jwt:Issuer");
StaticInfos.JwtAudience = _configuration.GetValue<string>("Jwt:Audience");
StaticInfos.JwtKeyExpireIn = _configuration.GetValue<int>("Jwt:ExpireIn");

//Custom file upload path

StaticInfos.FileUploadPath = _configuration.GetValue<string>("FileUploadPath");

//Microsoft.AspNetCore.Hosting.IHostingEnvironment env =
//With a transient service, a new instance is provided every time an instance is requested
//whether it is in the scope of same http request or across different http requests.
//builder.Services.AddTransient(_ => new MySqlDbConnection(StaticInfos.MySqlConnectionString));
//builder.Services.AddTransient(_ => new MsSqlDbConnection(StaticInfos.MsSqlConnectionString));
//builder.Services.AddTransient(_ => new PostGreSqlDbConnection(StaticInfos.PostgreSqlConnectionString));
//builder.Services.AddDbContext<API.DataAccess.ORM.MsSQLDataModels.NexKraftDbContext1>(options => options.UseSqlServer(StaticInfos.MsSqlConnectionString));
//builder.Services.AddDbContext<API.DataAccess.ORM.CodeFirst.NexKraftDbContextCF>(options => options.UseSqlServer(StaticInfos.MsSqlConnectionString));
builder.Services.AddDbContext<API.DataAccess.ORM.CodeFirst.NexKraftDbContextCF>();
// For Identity
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<NexKraftDbContext>()
//    .AddDefaultTokenProviders();
//Unity Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure In Memory Cache
builder.Services.AddMemoryCache();

//Configure Response Caching
builder.Services.AddResponseCaching(options =>
{
    //Cache responses with a body size smaller than or equal to 1,024 bytes.
    options.MaximumBodySize = 1024;
    //Store the responses by case-sensitive paths.
    //For example, /page1 and /Page1 are stored separately.
    options.UseCaseSensitivePaths = true;
});
//SignalR
builder.Services.AddSignalR();

//Register All services
RegisteredServices.Register(builder);


var app = builder.Build();
//set web r0ot path
StaticInfos.WebRootPath=app.Environment.WebRootPath;
StaticInfos.ContentRootPath=app.Environment.ContentRootPath;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

//UseCors must be called before UseResponseCaching
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


app.UseResponseCaching();
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(10)
        };
    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
        new string[] { "Accept-Encoding" };

    await next();
});
app.UseEndpoints(routes =>
{
    routes.MapHub<MessageHub>("/api/notify");
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsProduction())
{
    //Uncomment below for index.html
    DefaultFilesOptions options = new DefaultFilesOptions();
    options.DefaultFileNames.Clear();
    options.DefaultFileNames.Add("/index.html");
    app.UseDefaultFiles(options);

    //Uncomment below code for-- static files, such as HTML, CSS, images, and JavaScript, are assets an ASP.NET Core app serves directly to clients by default.
    app.UseStaticFiles();

    //Uncomment below code for--Enable all static file middleware(except directory browsing) for the current request path in the current directory.
    app.UseFileServer(enableDirectoryBrowsing: false);
}


app.Run();
