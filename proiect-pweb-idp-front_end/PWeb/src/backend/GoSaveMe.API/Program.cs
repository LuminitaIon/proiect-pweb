using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GoSaveMe API", Version = "v1.0.0" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "Using the Authorization header with the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
              { securitySchema, new[] { "Bearer" } }
          });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://apdev25.eu.auth0.com/";
    options.Audience = "https://api.gosaveme.com";
});

builder.Services.AddAuthorization(configure =>
{
    configure.AddPolicy("AdminAccess", policy => policy.RequireClaim("permissions", "gosaveme:admin"));
    configure.AddPolicy("NormalUserAccess", policy => policy.RequireClaim("permissions", "gosaveme:normaluser"));
    configure.AddPolicy("PrivilegedUserAccess", policy => policy.RequireClaim("permissions", "gosaveme:privilegeduser"));
    configure.AddPolicy("NewsModificationAccess", policy => policy.RequireClaim("permissions", new string[] { "gosaveme:admin", "gosaveme:privilegeduser" }));
    configure.AddPolicy("AuthenticatedAccess", policy => policy.RequireClaim("permissions", new string[] { "gosaveme:admin", "gosaveme:normaluser", "gosaveme:privilegeduser" }));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
