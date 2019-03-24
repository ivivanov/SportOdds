﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SO.Server.Data;

namespace SO.Server.Data.Migrations
{
    [DbContext(typeof(SODbContext))]
    [Migration("20190323205336_CreateDb")]
    partial class CreateDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("SO.Server.Data.Entities.Bet", b =>
                {
                    b.Property<int>("Id");

                    b.Property<bool>("IsLive");

                    b.Property<int>("MatchId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Event", b =>
                {
                    b.Property<int>("Id");

                    b.Property<bool>("IsLive");

                    b.Property<string>("Name");

                    b.Property<int>("SportId");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Match", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("EventId");

                    b.Property<string>("MatchType");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Odd", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("BetId");

                    b.Property<string>("Name");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.HasIndex("BetId");

                    b.ToTable("Odds");
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Sport", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Bet", b =>
                {
                    b.HasOne("SO.Server.Data.Entities.Match", "Match")
                        .WithMany("Bets")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Event", b =>
                {
                    b.HasOne("SO.Server.Data.Entities.Sport", "Sport")
                        .WithMany("Events")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Match", b =>
                {
                    b.HasOne("SO.Server.Data.Entities.Event", "Event")
                        .WithMany("Matches")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SO.Server.Data.Entities.Odd", b =>
                {
                    b.HasOne("SO.Server.Data.Entities.Bet", "Bet")
                        .WithMany("Odds")
                        .HasForeignKey("BetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}