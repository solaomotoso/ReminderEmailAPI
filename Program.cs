using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
// using Sieve.Models;
// using Sieve.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddAuthentication(opt =>
{
    // opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7146",
            ValidAudience = "https://localhost:7146",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        };
    });
builder.Services.AddControllers();
builder.Services.AddScoped<IEmailSender, EmailSender>();
// builder.Services.AddScoped<ISieveProcessor, SieveProcessor>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration
 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
 .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Services.Configure<Setting>(builder.Configuration.GetSection("Setting"));
var setting = builder.Configuration.GetSection("Setting").Get<Setting>();
builder.Services.AddSingleton<Setting>(setting);
Setting.initializeRepoDb();
builder.Services.AddSingleton<DoctorDetailsRepository>();
builder.Services.AddSingleton<ReminderDetailsRepository>();
builder.Services.AddSingleton<ReminderTypeRepository>();
builder.Services.AddSingleton<SpecialtyRepository>();
builder.Services.AddSingleton<TitleMasterRepository>();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder => builder.WithOrigins("http://localhost",
            "http://localhost:90")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowedToAllowWildcardSubdomains());
});
var app = builder.Build();
// var app = builder.Build();
app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowedToAllowWildcardSubdomains());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseCors("MyAllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
