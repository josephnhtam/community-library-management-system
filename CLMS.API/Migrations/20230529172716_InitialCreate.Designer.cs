﻿// <auto-generated />
using System;
using CLMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CLMS.API.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    [Migration("20230529172716_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CLMS.Domain.Aggregates.AuthorAggregate.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.BookAggregate.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AvailableNumberOfCopies")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("PublicationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalNumberOfCopies")
                        .HasColumnType("integer");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AvailableNumberOfCopies");

                    b.HasIndex("TotalNumberOfCopies");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.BookAggregate.BookAuthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthor");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.BookAggregate.BookCopy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PatronId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("IsAvailable");

                    b.HasIndex("PatronId")
                        .IsUnique();

                    b.ToTable("BookCopies");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.PatronAggregate.BookDonation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookCopyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PatronId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookCopyId")
                        .IsUnique();

                    b.HasIndex("PatronId");

                    b.ToTable("BookDonations");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.PatronAggregate.BookLoan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookCopyId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("BorrowDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PatronId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("ReturnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookCopyId")
                        .IsUnique();

                    b.HasIndex("BorrowDate");

                    b.HasIndex("DueDate");

                    b.HasIndex("PatronId");

                    b.HasIndex("ReturnDate");

                    b.ToTable("BookLoans");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.PatronAggregate.Patron", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ConcurrentBookLoansCount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalBookDonationsCount")
                        .HasColumnType("integer");

                    b.Property<int>("TotalBookLoansCount")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Type");

                    b.ToTable("Patrons");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.BookAggregate.BookAuthor", b =>
                {
                    b.HasOne("CLMS.Domain.Aggregates.AuthorAggregate.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CLMS.Domain.Aggregates.BookAggregate.Book", "Book")
                        .WithMany("Authors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.BookAggregate.BookCopy", b =>
                {
                    b.HasOne("CLMS.Domain.Aggregates.BookAggregate.Book", "Book")
                        .WithMany("Copies")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CLMS.Domain.Aggregates.PatronAggregate.Patron", null)
                        .WithOne()
                        .HasForeignKey("CLMS.Domain.Aggregates.BookAggregate.BookCopy", "PatronId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.PatronAggregate.BookDonation", b =>
                {
                    b.HasOne("CLMS.Domain.Aggregates.BookAggregate.BookCopy", null)
                        .WithOne()
                        .HasForeignKey("CLMS.Domain.Aggregates.PatronAggregate.BookDonation", "BookCopyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CLMS.Domain.Aggregates.PatronAggregate.Patron", "Patron")
                        .WithMany("BookDonations")
                        .HasForeignKey("PatronId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patron");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.PatronAggregate.BookLoan", b =>
                {
                    b.HasOne("CLMS.Domain.Aggregates.BookAggregate.BookCopy", null)
                        .WithOne()
                        .HasForeignKey("CLMS.Domain.Aggregates.PatronAggregate.BookLoan", "BookCopyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CLMS.Domain.Aggregates.PatronAggregate.Patron", "Patron")
                        .WithMany("BookLoans")
                        .HasForeignKey("PatronId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patron");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.PatronAggregate.Patron", b =>
                {
                    b.OwnsOne("CLMS.Domain.Aggregates.PatronAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("PatronId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("PatronId");

                            b1.ToTable("Patrons");

                            b1.WithOwner()
                                .HasForeignKey("PatronId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.BookAggregate.Book", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Copies");
                });

            modelBuilder.Entity("CLMS.Domain.Aggregates.PatronAggregate.Patron", b =>
                {
                    b.Navigation("BookDonations");

                    b.Navigation("BookLoans");
                });
#pragma warning restore 612, 618
        }
    }
}
