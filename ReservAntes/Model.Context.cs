﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReservAntes
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbReservantesEntities : DbContext
    {
        public dbReservantesEntities()
            : base("name=dbReservantesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<DatosBancarios> DatosBancarios { get; set; }
        public DbSet<Domicilio> Domicilio { get; set; }
        public DbSet<EstadoReserva> EstadoReserva { get; set; }
        public DbSet<EstiloMenu> EstiloMenu { get; set; }
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<MedioPago> MedioPago { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<NivelRestaurante> NivelRestaurante { get; set; }
        public DbSet<Partido> Partido { get; set; }
        public DbSet<Plato> Plato { get; set; }
        public DbSet<PlatosElegidos> PlatosElegidos { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Restaurante> Restaurante { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
