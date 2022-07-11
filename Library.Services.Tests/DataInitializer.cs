using Library.Common;
using Library.Data;
using Library.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Tests
{
    public static class DataInitializer
    {
        public static List<User> Users
        {
            get
            {
                return new List<User>()
                {
                    new User
                    {
                        Id = 1,
                        Username = "User1",
                        FirstName = "Ivan",
                        LastName = "Ivanov",
                        ApplicationRoleId = 3,
                        Email = "user1@gmail.com",
                        EmailConfirmed = true,
                        Password = BCrypt.Net.BCrypt.HashPassword("User123$"),
                        ProfilePictureURL = GlobalConstants.DefaultPicture,
                        PhoneNumber = "0881122334",
                        CreatedOn = DateTime.Now
                    },
                    new User
                    {
                        Id = 2,
                        Username = "User2",
                        FirstName = "Sasho",
                        LastName = "Ivanov",
                        ApplicationRoleId = 3,
                        Email = "user2@gmail.com",
                        EmailConfirmed = true,
                        Password = BCrypt.Net.BCrypt.HashPassword("User123$"),
                        ProfilePictureURL = GlobalConstants.DefaultPicture,
                        PhoneNumber = "0881122331",
                        CreatedOn = DateTime.Now
                    },
                    new User
                    {
                        Id = 3,
                        Username = "User3",
                        FirstName = "Petur",
                        LastName = "Ivanov",
                        ApplicationRoleId = 4,
                        Email = "user3@gmail.com",
                        EmailConfirmed = false,
                        Password = BCrypt.Net.BCrypt.HashPassword("User123$"),
                        ProfilePictureURL = GlobalConstants.DefaultPicture,
                        PhoneNumber = "0881122332",
                        CreatedOn = DateTime.Now
                    },
                    new User
                    {
                        Id = 4,
                        Username = "Albaneca",
                        FirstName = "Anjelo",
                        LastName = "Jotov",
                        ApplicationRoleId = 1,
                        Email = "anjelo98@gmail.com",
                        EmailConfirmed = true,
                        Password = BCrypt.Net.BCrypt.HashPassword("User123$"),
                        ProfilePictureURL = GlobalConstants.DefaultPicture,
                        PhoneNumber = "0889997771",
                        CreatedOn = DateTime.Now
                    }
                };
            }
        }
        public static List<Author> Authors
        {
            get
            {
                return new List<Author>()
                {
                    new Author
                    {
                        Id = 1,
                        Name = "Христо Ботев",
                        CreatedOn = DateTime.Now,
                        URL = GlobalConstants.DefaultAuthorURL
                    },
                    new Author
                    {
                        Id = 2,
                        Name = "Иван Вазов",
                        CreatedOn = DateTime.Now,
                        URL = GlobalConstants.DefaultAuthorURL
                    },
                    new Author
                    {
                        Id = 3,
                        Name = "Христо Смирненски",
                        CreatedOn = DateTime.Now,
                        URL = GlobalConstants.DefaultAuthorURL
                    }
                };
            }
        }
        public static List<Book> Books
        {
            get
            {
                return new List<Book>()
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
                    },
                    new Book
                    {
                        Id = 3,
                        AuthorId = 1,
                        Title = "До моето първо либе",
                        PublishHouseId = 3,
                        Year = DateTime.ParseExact("2019", "yyyy", System.Globalization.CultureInfo.InvariantCulture)
                    }
                };

            }
        }
        public static List<PublishHouse> PublishHouses
        {
            get
            {
                return new List<PublishHouse>()
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
                    },
                    new PublishHouse
                    {
                        Id = 3,
                        Name = "Персей"
                    }
                };
            }
        }
        public static List<Loan> Loans
        {
            get
            {
                return new List<Loan>
                {
                    new Loan
                    {
                        Id = 1,
                        Status = GlobalConstants.LOAN_NOT_CONFIRMED,
                        BookId = 1,
                        RequesterId = 1,
                        CreatedOn = DateTime.Now
                    },
                    new Loan
                    {
                        Id = 2,
                        Status = GlobalConstants.LOAN_CONFIRMED,
                        BookId = 2,
                        RequesterId = 2,
                        ApproverId = 4,
                        CreatedOn = DateTime.Now
                    },
                    new Loan
                    {
                        Id = 3,
                        Status = GlobalConstants.LOAN_DENIED,
                        BookId = 2,
                        RequesterId = 3,
                        ApproverId = 4,
                        CreatedOn= DateTime.Now
                    }
                };
            }
        }
        public static List<ApplicationRole> ApplicationRoles
        {
            get
            {
                return new List<ApplicationRole>()
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
            }
        }

        public static List<Ban> Bans
        {
            get
            {
                return new List<Ban>()
                {
                    new Ban
                    {
                        Id = 1,
                        UserId = 1,
                        BlockedOn = System.DateTime.Today,
                        BlockedDue = System.DateTime.Today.AddDays(5)
                    },
                    new Ban
                    {
                        Id = 2,
                        UserId = 2,
                        BlockedOn = System.DateTime.Today
                    }
                };
            }
        }

        public static Mock<LibraryDbContext> MockDbContext
        {
            get
            {
                var mockDbContext = new Mock<LibraryDbContext>();
                return mockDbContext;
            }
        }
    }
}
