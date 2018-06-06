﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using wetest.Models;

namespace wetest.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180606145801_migratiosn_312")]
    partial class migratiosn_312
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("wetest.Models.Item", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("Information");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("wetest.Models.models.Appeal", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Date");

                    b.Property<string>("Information");

                    b.Property<string>("OrderId");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("Appeal");
                });

            modelBuilder.Entity("wetest.Models.models.Payment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppealDate");

                    b.Property<string>("ClosedDate");

                    b.Property<string>("Information");

                    b.Property<string>("OrderId");

                    b.Property<int>("Price");

                    b.Property<string>("ProviderId");

                    b.Property<string>("ServicerId");

                    b.HasKey("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("wetest.Models.models.UserInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("RegistDate");

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("wetest.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndDate");

                    b.Property<string>("Information");

                    b.Property<long>("Price");

                    b.Property<int>("Progress");

                    b.Property<string>("ProviderId");

                    b.Property<string>("ServicerId");

                    b.Property<string>("StartDate");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("wetest.Models.relatmodels.UserToItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ItemId");

                    b.Property<string>("SellerId");

                    b.HasKey("Id");

                    b.ToTable("UserToItem");
                });

            modelBuilder.Entity("wetest.Models.relatmodels.UserToOrder", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BuyerId");

                    b.Property<string>("OrderId");

                    b.Property<string>("SellerId");

                    b.HasKey("Id");

                    b.ToTable("UserToOrder");
                });

            modelBuilder.Entity("wetest.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
