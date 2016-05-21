using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using EfSqliteUwpDemo.Models;

namespace EfSqliteUwpDemo.Migrations
{
    [DbContext(typeof(SensorDbContext))]
    [Migration("20160519014400_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("EfSqliteUwpDemo.Models.Ambience", b =>
                {
                    b.Property<int>("AmbienceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Humidity");

                    b.Property<Guid?>("SensorSensorId");

                    b.Property<int>("Temp");

                    b.Property<int>("TimeStamp");

                    b.HasKey("AmbienceId");
                });

            modelBuilder.Entity("EfSqliteUwpDemo.Models.Sensor", b =>
                {
                    b.Property<Guid>("SensorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Location");

                    b.HasKey("SensorId");
                });

            modelBuilder.Entity("EfSqliteUwpDemo.Models.Ambience", b =>
                {
                    b.HasOne("EfSqliteUwpDemo.Models.Sensor")
                        .WithMany()
                        .HasForeignKey("SensorSensorId");
                });
        }
    }
}
