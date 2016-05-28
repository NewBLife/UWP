using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using SqliteEFCoreDemo.Models;

namespace SqliteEFCoreDemo.Migrations
{
    [DbContext(typeof(EfDbContext))]
    partial class EfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("SqliteEFCoreDemo.Models.Course", b =>
                {
                    b.Property<string>("ID");

                    b.Property<string>("Name");

                    b.Property<string>("StudentID");

                    b.HasKey("ID");

                    b.HasAnnotation("Relational:TableName", "Course");
                });

            modelBuilder.Entity("SqliteEFCoreDemo.Models.Student", b =>
                {
                    b.Property<string>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasAnnotation("Relational:TableName", "Student");
                });

            modelBuilder.Entity("SqliteEFCoreDemo.Models.Course", b =>
                {
                    b.HasOne("SqliteEFCoreDemo.Models.Student")
                        .WithMany()
                        .HasForeignKey("StudentID");
                });
        }
    }
}
