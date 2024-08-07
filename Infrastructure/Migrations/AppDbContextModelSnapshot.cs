﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Timezone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Domain.Entities.Externals", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Imdb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Thetvdb")
                        .HasColumnType("int");

                    b.Property<int>("Tvrage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Externals");
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Medium")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Original")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domain.Entities.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PreviousepisodeId")
                        .HasColumnType("int");

                    b.Property<int>("SelfId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PreviousepisodeId");

                    b.HasIndex("SelfId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("Domain.Entities.Network", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficialSite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Networks");
                });

            modelBuilder.Entity("Domain.Entities.Previousepisode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Href")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Previousepisodes");
                });

            modelBuilder.Entity("Domain.Entities.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Average")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Domain.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Days")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Domain.Entities.Self", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("href")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Self");
                });

            modelBuilder.Entity("Domain.Entities.Show", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("AverageRuntime")
                        .HasColumnType("int");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Ended")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExternalsId")
                        .HasColumnType("int");

                    b.Property<string>("Genres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LinkId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NetworkId")
                        .HasColumnType("int");

                    b.Property<string>("OfficialSite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Premiered")
                        .HasColumnType("datetime2");

                    b.Property<int>("RatingId")
                        .HasColumnType("int");

                    b.Property<int?>("Runtime")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Updated")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WebChannelId")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("ExternalsId");

                    b.HasIndex("ImageId");

                    b.HasIndex("LinkId")
                        .IsUnique();

                    b.HasIndex("NetworkId");

                    b.HasIndex("RatingId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("WebChannelId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("Domain.Entities.WebChannel", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficialSite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("WebChannels");
                });

            modelBuilder.Entity("Domain.Entities.Link", b =>
                {
                    b.HasOne("Domain.Entities.Previousepisode", "Previousepisode")
                        .WithMany("Links")
                        .HasForeignKey("PreviousepisodeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Self", "Self")
                        .WithMany("Links")
                        .HasForeignKey("SelfId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Previousepisode");

                    b.Navigation("Self");
                });

            modelBuilder.Entity("Domain.Entities.Network", b =>
                {
                    b.HasOne("Domain.Entities.Country", "Country")
                        .WithMany("Networks")
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Domain.Entities.Show", b =>
                {
                    b.HasOne("Domain.Entities.Country", "DvdCountry")
                        .WithMany("Shows")
                        .HasForeignKey("CountryId");

                    b.HasOne("Domain.Entities.Externals", "Externals")
                        .WithMany()
                        .HasForeignKey("ExternalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Image", "Image")
                        .WithMany("Shows")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Link", "Links")
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Show", "LinkId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Network", "Network")
                        .WithMany()
                        .HasForeignKey("NetworkId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Rating", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.WebChannel", "WebChannel")
                        .WithMany()
                        .HasForeignKey("WebChannelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("DvdCountry");

                    b.Navigation("Externals");

                    b.Navigation("Image");

                    b.Navigation("Links");

                    b.Navigation("Network");

                    b.Navigation("Rating");

                    b.Navigation("Schedule");

                    b.Navigation("WebChannel");
                });

            modelBuilder.Entity("Domain.Entities.WebChannel", b =>
                {
                    b.HasOne("Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Domain.Entities.Country", b =>
                {
                    b.Navigation("Networks");

                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Domain.Entities.Previousepisode", b =>
                {
                    b.Navigation("Links");
                });

            modelBuilder.Entity("Domain.Entities.Self", b =>
                {
                    b.Navigation("Links");
                });
#pragma warning restore 612, 618
        }
    }
}
