﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolySondage.Data;

namespace PolySondage.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220105143519_changeLenghtPassword")]
    partial class changeLenghtPassword
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PolySondage.Data.Models.Choice", b =>
                {
                    b.Property<int>("IdChoice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PollIdPoll")
                        .HasColumnType("int");

                    b.Property<int>("TotalVotes")
                        .HasColumnType("int");

                    b.Property<int?>("VoteIdVote")
                        .HasColumnType("int");

                    b.HasKey("IdChoice");

                    b.HasIndex("PollIdPoll");

                    b.HasIndex("VoteIdVote");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("PolySondage.Data.Models.Poll", b =>
                {
                    b.Property<int>("IdPoll")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activate")
                        .HasColumnType("bit");

                    b.Property<int>("CreatorIdUser")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Unique")
                        .HasColumnType("bit");

                    b.HasKey("IdPoll");

                    b.HasIndex("CreatorIdUser");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("PolySondage.Data.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("IdUser");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PolySondage.Data.Models.Vote", b =>
                {
                    b.Property<int>("IdVote")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PollIdPoll")
                        .HasColumnType("int");

                    b.Property<int?>("UserIdUser")
                        .HasColumnType("int");

                    b.HasKey("IdVote");

                    b.HasIndex("PollIdPoll");

                    b.HasIndex("UserIdUser");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("PolySondage.Data.Models.Choice", b =>
                {
                    b.HasOne("PolySondage.Data.Models.Poll", "Poll")
                        .WithMany("Choices")
                        .HasForeignKey("PollIdPoll")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolySondage.Data.Models.Vote", null)
                        .WithMany("Choices")
                        .HasForeignKey("VoteIdVote");

                    b.Navigation("Poll");
                });

            modelBuilder.Entity("PolySondage.Data.Models.Poll", b =>
                {
                    b.HasOne("PolySondage.Data.Models.User", "Creator")
                        .WithMany("PollsCreated")
                        .HasForeignKey("CreatorIdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("PolySondage.Data.Models.Vote", b =>
                {
                    b.HasOne("PolySondage.Data.Models.Poll", "Poll")
                        .WithMany()
                        .HasForeignKey("PollIdPoll");

                    b.HasOne("PolySondage.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserIdUser");

                    b.Navigation("Poll");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PolySondage.Data.Models.Poll", b =>
                {
                    b.Navigation("Choices");
                });

            modelBuilder.Entity("PolySondage.Data.Models.User", b =>
                {
                    b.Navigation("PollsCreated");
                });

            modelBuilder.Entity("PolySondage.Data.Models.Vote", b =>
                {
                    b.Navigation("Choices");
                });
#pragma warning restore 612, 618
        }
    }
}