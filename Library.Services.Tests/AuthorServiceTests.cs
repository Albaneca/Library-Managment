using Library.Common;
using Library.Data;
using Library.Services.DTOs.AuthorDTOs;
using Library.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Tests
{
    [TestClass]
    public class AuthorServiceTests
    {
        private LibraryDbContext context;

        [TestInitialize]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                               .Options;

            LibraryDbContext libraryDbContext = new LibraryDbContext(options);
            context = libraryDbContext;
        }

        [TestMethod]
        public async Task DeleteAsync()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var bookCount = await context.Books.CountAsync();
            var authorCount = await context.Authors.CountAsync();
            var service = new AuthorService(context);
            var result = await service.DeleteAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Христо Ботев", result.Name);
            Assert.AreEqual(authorCount - 1, await context.Authors.CountAsync());
            Assert.AreEqual(bookCount - result.BooksCount, await context.Books.CountAsync());
        }

        [TestMethod]
        public async Task DeleteAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);
            var result = await service.DeleteAsync(10);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.AUTHOR_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task FilterAuthorsAsync()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);
            var result = await service.FilterAuthorsAsync(0, "Ботев");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task FilterAuthorsAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);
            var result = await service.FilterAuthorsAsync(0, "@@@");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task FilterAuthorsAsync_NotFound_Page()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);
            var result = await service.FilterAuthorsAsync(5, "@");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetAsync()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);
            var result = await service.GetAsync(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetAsync_NotFound_Page()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);
            var result = await service.GetAsync(100);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetAuthorByIdAsync()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);
            var result = await service.GetAuthorById("1");

            Assert.IsNotNull(result);
            Assert.AreEqual("Христо Ботев", result.Name);
        }

        [TestMethod]
        public async Task GetAuthorByIdAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);
            var result = await service.GetAuthorById("5");

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.AUTHOR_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);

            var obj = new CreateAuthorDTO
            {
                Name = "Петър Берон"
            };


            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.AreEqual("Петър Берон", result.Name);
        }

        [TestMethod]
        public async Task PostAsync_ExistingAuthor()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);

            var obj = new CreateAuthorDTO
            {
                Name = "Иван Вазов"
            };


            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.AUTHOR_EXISTS, result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync_NullData()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);

            var obj = new CreateAuthorDTO { Name = null};

            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result.ErrorMessage);
            Assert.AreEqual(GlobalConstants.INCORRECT_DATA, result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_AuthorFound()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);

            var obj = new CreateAuthorDTO
            {
                Name = "Алеко Константинов"
            };


            var result = await service.UpdateAsync(1, obj);

            Assert.IsNotNull(result);
            Assert.AreEqual(obj.Name, result.Name);
        }
        [TestMethod]
        public async Task UpdatePicture_Correct()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);

            var result = await service.UpdatePicture(1, "someurl");

            Assert.IsNotNull(result);
            Assert.AreEqual("someurl", result.URL);
        }

        [TestMethod]
        public async Task UpdatePicture_Error()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);

            var result = await service.UpdatePicture(5, "someurl");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_AuthorNotFound()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);

            var obj = new CreateAuthorDTO
            {
                Name = "test"
            };


            var result = await service.UpdateAsync(10, obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_AuthorExist()
        {
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();

            var service = new AuthorService(context);

            var obj = new CreateAuthorDTO
            {
                Name = "Иван Вазов"
            };


            var result = await service.UpdateAsync(1, obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

    }
}
