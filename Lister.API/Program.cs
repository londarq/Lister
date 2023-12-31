using Lister.API.OptionsSetup;
using Lister.Database;
using Lister.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Lister.Services.Services.UserService;
using Lister.Services.Authentification;
using Microsoft.OpenApi.Models;
using Lister.Services.Services.TestService;
using Lister.Services.Services.QuestionService;
using Lister.Services.Services.HistoryService;
using Lister.Services.Services.AnswerService;
using Lister.Services.Services.UserAnswerService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IAuthRequestHandler, AuthRequestHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IUserAnswerService, UserAnswerService>();

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>((serviceProvider, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ListerDbConnection"), b =>
    {
        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

var app = builder.Build();

app.UseCors(options => options.WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader() 
    .AllowAnyOrigin());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
