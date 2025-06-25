
using CasosDeUso.InterfacesCasosUso;
using LogicaAccesoADatos.EF;
using LogicaAccesoADatos.Repos;
using LogicaAplicacion.CasosUsoConcretos.Envios;
using LogicaAplicacion.CasosUsoConcretos.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //Inyecciones

            //Repositorios
            builder.Services.AddScoped<IRepositorioEmpleados, RepositorioEmpleados>();
            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
            builder.Services.AddScoped<IRepositorioAcciones, RepositorioAcciones>();
            builder.Services.AddScoped<IRepositorioAgencias, RepositorioAgencias>();
            builder.Services.AddScoped<IRepositorioCiudades, RepositorioCiudades>();
            builder.Services.AddScoped<IRepositorioEnvios, RepositorioEnvios>();
            builder.Services.AddScoped<IRepositorioComentarios, RepositorioComentarios>();


            //Casos de uso que miedo.
            builder.Services.AddScoped<ILogin, Login>();
            builder.Services.AddScoped<ICambiarContraseña, CambiarContrasena>();

            //Envíos
            builder.Services.AddScoped<IObtenerEnvio, ObtenerEnvio>();
            builder.Services.AddScoped<IObtenerEnvioByTracking, ObtenerEnvioByTracking>();
            builder.Services.AddScoped<IObtenerEnviosByEmail, ObtenerEnviosByEmail>();
            builder.Services.AddScoped<IObtenerEnvioByComentario, ObtenerEnvioByComentario>();
            builder.Services.AddScoped<IObtenerEnviosByFecha, ObtenerEnviosByFecha>();





            //DB
            builder.Services.AddDbContext<EmpresaContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Empresa"),
                    sql => sql.EnableRetryOnFailure()
                )
            );


            //CAMBIAMOS LO ANTERIOR POR ESTO PARA PODER AUTENTICAR CON JWT EN SWAGGER:

            builder.Services.AddSwaggerGen(options =>
            {

                var archivo = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var ruta = Path.Combine(AppContext.BaseDirectory, archivo);
                options.IncludeXmlComments(ruta);


                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
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

            //////// FIN CÓDIGO PARA JWT EN SWAGGER



            //////////////// INICIO CÓDIGO PARA CONFIGURAR USO DE JWT

            var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

            builder.Services.AddAuthentication(aut =>
            {
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(aut =>
            {
                aut.RequireHttpsMetadata = false;
                aut.SaveToken = true;
                aut.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //////////////// FIN CÓDIGO PARA CONFIGURAR USO DE JWT
            ///




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (true)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
