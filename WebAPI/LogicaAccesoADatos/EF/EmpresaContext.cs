using LogicaNegocio.EntidadesDominio.Acciones;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.ValueObjects.Usuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoADatos.EF
{
    public class EmpresaContext : DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Accion> Acciones { get; set; }
        public DbSet<AccionAdministracion> AccionesAdministracion { get; set; }
        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<EnvioComun> EnviosComunes { get; set; }
        public DbSet<EnvioUrgente> EnviosUrgentes { get; set; }
        public DbSet<ComentarioEnvio> Comentarios { get; set; }



        public EmpresaContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // opcional, pero recomendado

            modelBuilder.Entity<Usuario>()
            .Property(u => u.Email)
            .HasConversion(
            email => email.Email,
            Email => new UsuarioEmail(Email)
            );

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();


            modelBuilder.Entity<AccionAdministracion>()
                .HasOne(a => a.Afectado)
                .WithMany()
                .HasForeignKey(a => a.AfectadoId)
                .OnDelete(DeleteBehavior.SetNull); // puede quedar

            modelBuilder.Entity<AccionAdministracion>()
                .HasOne(a => a.Realizador)
                .WithMany()
                .HasForeignKey(a => a.RealizadorId)
                .OnDelete(DeleteBehavior.NoAction); // o .Restrict


            modelBuilder.Entity<EnvioUrgente>()
                .HasOne(e => e.Ciudad)
                .WithMany()
                .HasForeignKey(e => e.CiudadId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Agencia>()
                .HasOne(e => e.Ciudad)
                .WithMany()
                .HasForeignKey(e => e.CiudadId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<EnvioComun>()
                .HasOne(e => e.Agencia)
                .WithMany()
                .HasForeignKey(e => e.AgenciaId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Envio>()
                .HasOne(e => e.EmpleadoResponable)
                .WithMany()
                .HasForeignKey(e => e.EmpleadoResponableId)
                .OnDelete(DeleteBehavior.Restrict);




        }


    }
}
