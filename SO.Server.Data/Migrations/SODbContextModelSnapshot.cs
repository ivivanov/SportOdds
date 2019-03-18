﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SO.Server.Data;

namespace SO.Server.Data.Migrations
{
    [DbContext(typeof(SODbContext))]
    partial class SODbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("SO.Server.Data.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsLive");

                    b.Property<string>("Name");

                    b.Property<int?>("SportId");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EventId");

                    b.Property<int>("MatchTypeId");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("MatchTypeId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("SO.Server.Data.Entities.MatchType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("MatchType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "PreMatch"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Live"
                        });
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Event", b =>
                {
                    b.HasOne("SO.Server.Data.Entities.Sport")
                        .WithMany("Events")
                        .HasForeignKey("SportId");
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Match", b =>
                {
                    b.HasOne("SO.Server.Data.Entities.Event")
                        .WithMany("Matches")
                        .HasForeignKey("EventId");

                    b.HasOne("SO.Server.Data.Entities.MatchType", "MatchType")
                        .WithMany()
                        .HasForeignKey("MatchTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
