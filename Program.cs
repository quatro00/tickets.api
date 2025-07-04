using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using tickets.api.Data;
using tickets.api.Mappings;
using tickets.api.Models;
using tickets.api.Models.Domain;
using tickets.api.Repositories.Implementation;
using tickets.api.Repositories.Interface;
using tickets.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Activar licencia gratuita
QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });

    // Autenticaci�n con JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Token JWT. Ejemplo: 'Bearer {tu_token_aqu�}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<DbAb1c8aTicketsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IOrganizacionRepository, OrganizacionRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<ICatCategoriaRepository, CatCategoriaRepository>();
builder.Services.AddScoped<ICatPrioridadRepository, CatPrioridadRepository>();
builder.Services.AddScoped<IAspNetUsersRepository, AspNetUsersRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ICatEstatusTicketRepository, CatEstatusTicketRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEquipoTrabajoIntegrantesRepository, EquipoTrabajoIntegrantesRepository>();
builder.Services.AddScoped<IEquipoTrabajoRepository, EquipoTrabajoRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    // Aqu� puedes configurar reglas de contrase�a, bloqueo, etc.
})
.AddRoles<IdentityRole>()
.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("BDS")
.AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        /*
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"[JWT] Error de autenticaci�n: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"[JWT] Token v�lido: {context.Principal.Identity?.Name}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine("[JWT] Desaf�o de autenticaci�n.");
                return Task.CompletedTask;
            }
        };
        */
        options.TokenValidationParameters = new TokenValidationParameters
        {
            AuthenticationType = "Jwt",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(
            "https://localhost",
            "https://localhost:8100",
            "ionic://localhost",
            "capacitor://localhost",
            "https://quatro0-001-site2.ktempurl.com",
            "https://quatro0-001-site3.ktempurl.com",
            "https://quatro0-001-site4.ktempurl.com",
            "https://quatro0-001-site5.ktempurl.com", // <-- Agrega este
            "https://quatro0-001-site6.ktempurl.com",
            "http://quatro0-001-site6.ktempurl.com",
            "http://localhost:51293",
            "http://localhost:8100",
            "http://localhost:4200") // Origen permitido
              .AllowAnyHeader() // Permitir cualquier encabezado
              .AllowAnyMethod(); // Permitir cualquier m�todo HTTP
    });
});


var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
//if (
//    app.Environment.IsDevelopment()
//    || app.Environment.IsProduction()
//    )
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}


app.UseRouting();

app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();