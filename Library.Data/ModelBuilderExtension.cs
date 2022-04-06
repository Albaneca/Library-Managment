using Library.Common;
using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder db)
        {
            var Users = new HashSet<User>
            {
                new User {
                    Id = 1,
                    ApplicationRoleId = 1,
                    CreatedOn = DateTime.Now,
                    EmailConfirmed = true,
                    Email = "anjelo98@gmail.com",
                    FirstName = "Anjelo",
                    LastName = "Jotov",
                    Username = "Albaneca",
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin123$"),
                    ProfilePictureURL = GlobalConstants.DefaultPicture, 
                    PhoneNumber = "0882712687"
                }
            };

            db.Entity<User>().HasData(Users);

            var roles = new List<ApplicationRole>()
            {
                new ApplicationRole
                {
                    Id = 1,
                    Name = "Admin"
                },
                new ApplicationRole
                {
                    Id = 2,
                    Name = "User"
                },
                new ApplicationRole
                {
                    Id = 3,
                    Name = "Banned"
                },
                new ApplicationRole
                {
                    Id = 4,
                    Name = "NotConfirmed"
                }
            };

            db.Entity<ApplicationRole>().HasData(roles);

            var Authors = new HashSet<Author>()
            {
                new Author
                {
                    Id = 1,
                    Name = "Христо Ботев"
                },
                new Author
                {
                    Id = 2,
                    Name = "Иван Вазов"
                }
            };

            db.Entity<Author>().HasData(Authors);

            var Books = new HashSet<Book>()
            {
                new Book
                {
                    Id = 1,
                    AuthorId = 1,
                    Title = "Хубава си, Татковино! Стихотворения",
                    PublishHouseId = 1,
                    Year = DateTime.ParseExact("2019", "yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    Description = "В изданието са включени 60 стихотворения от творчеството на Петко Славейков и 20 стихотворения на Христо Ботев. Книгата съдържа творби, задължително изучавани в училище, както и едни от най-популярните стихове на авторите, част от които завинаги остават в паметта на българската литература."
                },
                new Book
                {
                    Id = 2,
                    AuthorId = 2,
                    Title = "Под игото",
                    Year = DateTime.ParseExact("2017", "yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    PublishHouseId = 2,
                    Description = "Роман из живота на българите в предвечерието на Освобождението. В три части. С 25 илюстрации в текста. „Под игото“ е роман в три части от българския писател Иван Вазов, цялостно публикуван за първи път през 1894 г. и определян като първият пример за този жанр в българската литература."
                }
            };

            db.Entity<Book>().HasData(Books);

            var Houses = new HashSet<PublishHouse>
            {
                new PublishHouse
                {
                    Id = 1,
                    Name = "Art House"
                },
                new PublishHouse
                {
                    Id = 2,
                    Name = "Пан"
                }
            };

            db.Entity<PublishHouse>().HasData(Houses);

            var loans = new HashSet<Loan>
            {
                new Loan
                {
                    Id = 1,
                    Status = GlobalConstants.LOAN_NOT_CONFIRMED,
                    BookId = 1,
                    RequesterId = 1,
                    CreatedOn = DateTime.Now
                }
            };

            db.Entity<Loan>().HasData(loans);
        }

    }
}
