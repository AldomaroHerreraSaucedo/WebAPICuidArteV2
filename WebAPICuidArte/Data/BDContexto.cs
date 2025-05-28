﻿using Microsoft.EntityFrameworkCore;
using WebAPICuidArte.Models;

namespace WebAPICuidArte.Data
{
    public class BDContexto: DbContext
    {
        public BDContexto(DbContextOptions<BDContexto> options) : base(options)
        {
        }

        public DbSet<AdultoMayor> AdultosMayores { get; set; }
        public DbSet<Cuidador> Cuidadores { get; set; }
        public DbSet<AdultoMayorCuidador> AdultoMayorCuidadores { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<AdultoMayorMedicamento> AdultoMayorMedicamentos { get; set; }
        public DbSet<Enfermedad> Enfermedades { get; set; }
        public DbSet<AdultoMayorEnfermedad> AdultoMayorEnfermedades { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<CitaMedica> CitasMedicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdultoMayor>().ToTable("AdultoMayor");
            modelBuilder.Entity<Cuidador>().ToTable("Cuidador");
            modelBuilder.Entity<AdultoMayorCuidador>()
                .ToTable("AdultoMayorCuidador").HasKey(amc => new { amc.AdultoMayorId, amc.CuidadorId });
            modelBuilder.Entity<Medicamento>().ToTable("Medicamento");
            modelBuilder.Entity<AdultoMayorMedicamento>()
                .ToTable("AdultoMayorMedicamento").HasKey(amm => new { amm.AdultoMayorId, amm.MedicamentoId });
            modelBuilder.Entity<Enfermedad>().ToTable("Enfermedad");
            modelBuilder.Entity<AdultoMayorEnfermedad>()
                .ToTable("AdultoMayorEnfermedad").HasKey(ame => new { ame.AdultoMayorId, ame.EnfermedadId });
            modelBuilder.Entity<Contacto>().ToTable("Contacto");
            modelBuilder.Entity<CitaMedica>().ToTable("CitaMedica");

            modelBuilder.Entity<Contacto>()
                .HasOne<AdultoMayor>().WithMany().HasForeignKey(c => c.AdultoMayorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CitaMedica>()
                .HasOne<AdultoMayor>().WithMany().HasForeignKey(cm => cm.AdultoMayorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdultoMayorCuidador>()
                .HasOne<AdultoMayor>().WithMany().HasForeignKey(amc => amc.AdultoMayorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdultoMayorCuidador>()
                .HasOne<Cuidador>().WithMany().HasForeignKey(amc => amc.CuidadorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdultoMayorEnfermedad>()
                .HasOne<AdultoMayor>().WithMany().HasForeignKey(ame => ame.AdultoMayorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdultoMayorEnfermedad>()
                .HasOne<Enfermedad>().WithMany().HasForeignKey(ame => ame.EnfermedadId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdultoMayorMedicamento>()
                .HasOne<AdultoMayor>().WithMany().HasForeignKey(amm => amm.AdultoMayorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdultoMayorMedicamento>()
                .HasOne<Medicamento>().WithMany().HasForeignKey(amm => amm.MedicamentoId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
