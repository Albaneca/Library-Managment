﻿// <auto-generated />
using System;
using Library.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.Data.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Library.Data.Models.ApplicationRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationRoles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "User"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Banned"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "NotConfirmed"
                        });
                });

            modelBuilder.Entity("Library.Data.Models.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Христо Ботев",
                            URL = ""
                        },
                        new
                        {
                            Id = 2L,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Иван Вазов",
                            URL = ""
                        });
                });

            modelBuilder.Entity("Library.Data.Models.Ban", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BlockedDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("BlockedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Bans");
                });

            modelBuilder.Entity("Library.Data.Models.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLoaned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("PublishHouseId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PublishHouseId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AuthorId = 1L,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "В изданието са включени 60 стихотворения от творчеството на Петко Славейков и 20 стихотворения на Христо Ботев. Книгата съдържа творби, задължително изучавани в училище, както и едни от най-популярните стихове на авторите, част от които завинаги остават в паметта на българската литература.",
                            IsDeleted = false,
                            IsLoaned = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PublishHouseId = 1L,
                            Title = "Хубава си, Татковино! Стихотворения",
                            URL = "",
                            Year = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            AuthorId = 2L,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Роман из живота на българите в предвечерието на Освобождението. В три части. С 25 илюстрации в текста. „Под игото“ е роман в три части от българския писател Иван Вазов, цялостно публикуван за първи път през 1894 г. и определян като първият пример за този жанр в българската литература.",
                            IsDeleted = false,
                            IsLoaned = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PublishHouseId = 2L,
                            Title = "Под игото",
                            URL = "",
                            Year = new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Library.Data.Models.Inbox", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("FromUserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Seen")
                        .HasColumnType("bit");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Inboxes");
                });

            modelBuilder.Entity("Library.Data.Models.Loan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ApproverId")
                        .HasColumnType("bigint");

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("RequesterId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("BookId");

                    b.HasIndex("RequesterId");

                    b.ToTable("Loans");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            BookId = 1L,
                            CreatedOn = new DateTime(2022, 1, 21, 15, 12, 12, 160, DateTimeKind.Local).AddTicks(9124),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RequesterId = 1L,
                            Status = "Waiting for approval"
                        });
                });

            modelBuilder.Entity("Library.Data.Models.PublishHouse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PublishHouses");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Art House",
                            URL = ""
                        },
                        new
                        {
                            Id = 2L,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Пан",
                            URL = ""
                        });
                });

            modelBuilder.Entity("Library.Data.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ApplicationRoleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProfilePictureURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationRoleId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasCheckConstraint("Password_contains_space", "Password NOT LIKE '% %'");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ApplicationRoleId = 2L,
                            CreatedOn = new DateTime(2022, 1, 21, 15, 12, 11, 944, DateTimeKind.Local).AddTicks(7289),
                            Email = "anjelo98@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Anjelo",
                            IsDeleted = false,
                            LastName = "Jotov",
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "$2a$11$BMh.2wQgIKT.2IjIFGlQiu7XZRs1FfUiesEbHOT.Xj53QKJQyA/Py",
                            PhoneNumber = "0882712687",
                            ProfilePictureURL = "https://res.cloudinary.com/denfz6l1q/image/upload/v1639012542/profilePictures/oymjlgd6v2yi9dm3pynn.jpg",
                            Username = "Albaneca"
                        });
                });

            modelBuilder.Entity("Library.Data.Models.Ban", b =>
                {
                    b.HasOne("Library.Data.Models.User", "User")
                        .WithOne("Ban")
                        .HasForeignKey("Library.Data.Models.Ban", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Library.Data.Models.Book", b =>
                {
                    b.HasOne("Library.Data.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Data.Models.PublishHouse", "PublishHouse")
                        .WithMany("Books")
                        .HasForeignKey("PublishHouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("PublishHouse");
                });

            modelBuilder.Entity("Library.Data.Models.Inbox", b =>
                {
                    b.HasOne("Library.Data.Models.User", "User")
                        .WithMany("InboxMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Library.Data.Models.Loan", b =>
                {
                    b.HasOne("Library.Data.Models.User", "Approver")
                        .WithMany()
                        .HasForeignKey("ApproverId");

                    b.HasOne("Library.Data.Models.Book", "Book")
                        .WithMany("Loans")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Data.Models.User", "Requester")
                        .WithMany("Loans")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Approver");

                    b.Navigation("Book");

                    b.Navigation("Requester");
                });

            modelBuilder.Entity("Library.Data.Models.User", b =>
                {
                    b.HasOne("Library.Data.Models.ApplicationRole", "ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("ApplicationRoleId");

                    b.Navigation("ApplicationRole");
                });

            modelBuilder.Entity("Library.Data.Models.ApplicationRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Library.Data.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Library.Data.Models.Book", b =>
                {
                    b.Navigation("Loans");
                });

            modelBuilder.Entity("Library.Data.Models.PublishHouse", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Library.Data.Models.User", b =>
                {
                    b.Navigation("Ban");

                    b.Navigation("InboxMessages");

                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
