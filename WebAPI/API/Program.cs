
using CasosDeUso.InterfacesCasosUso;
using LogicaAccesoADatos.EF;
using LogicaAccesoADatos.Repos;
using LogicaAplicacion.CasosUsoConcretos.Envios;
using LogicaAplicacion.CasosUsoConcretos.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddScoped<IRegistroEmpleado, RegistroEmpleado>();
            builder.Services.AddScoped<IListarEmpleados, ListarEmpleados>();
            builder.Services.AddScoped<IObtenerEmpleado, ObtenerEmpleado>();
            builder.Services.AddScoped<IEditarEmpleado, EditarEmpleado>();
            builder.Services.AddScoped<IBajaEmpleado, BajaEmpleado>();
            builder.Services.AddScoped<IObtenerAgencias, ObtenerAgencias>();
            builder.Services.AddScoped<IObtenerCiudades, ObtenerCiudades>();
            builder.Services.AddScoped<IAltaEnvio, AltaEnvio>();
            builder.Services.AddScoped<IObtenerEnvio, ObtenerEnvio>();
            builder.Services.AddScoped<IFinalizarEnvio, FinalizarEnvio>();
            builder.Services.AddScoped<IComentarioEnvio, ComentariosEnvios>();


            //DB
            builder.Services.AddDbContext<EmpresaContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Empresa"),
                    sql => sql.EnableRetryOnFailure()
                )
            );



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
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
