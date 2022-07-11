using Library.Common;
using Library.Data;
using Library.Services.DTOs;
using Library.Services.DTOs.BookDTOs;
using Library.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Tests
{
    [TestClass]
    public class BookServiceTests
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
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();
            var count = await context.Books.CountAsync();
            var service = new BookService(context);
            var result = await service.DeleteAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Хубава си, Татковино! Стихотворения", result.Title);
            Assert.AreEqual(count - 1, await context.Books.CountAsync());
        }

        [TestMethod]
        public async Task DeleteAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);
            var result = await service.DeleteAsync(5);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.BOOK_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetAsync()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);
            var result = await service.GetAsync(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetAsync_NotFound_Page()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);
            var result = await service.GetAsync(100);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetBookByNameOrIdAsync()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);
            var result = await service.GetBookByNameOrIdAsync("Под игото");

            Assert.IsNotNull(result);
            Assert.AreEqual("Под игото", result.Title);
        }

        [TestMethod]
        public async Task GetBookByNameOrIdAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);
            var result = await service.GetBookByNameOrIdAsync("");

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.BOOK_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);

            var obj = new CreateBookDTO
            {
                Title = "test",
                AuthorId = 1,
                Description = "test",
                PublishHouseId = 1,
                Year = DateTime.ParseExact("2019", "yyyy", System.Globalization.CultureInfo.InvariantCulture)
            };


            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Title);
        }

        [TestMethod]
        public async Task PostAsync_MissingData()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);

            var obj = new CreateBookDTO { Title = null };

            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync_ExistingData()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);

            var existingTitle = new CreateBookDTO
            {
                Title = "Под игото",
                AuthorId = 1,
                Description = "test",
                PublishHouseId = 1,
                Year = DateTime.ParseExact("2019", "yyyy", System.Globalization.CultureInfo.InvariantCulture)
            };


            var result1 = await service.PostAsync(existingTitle);

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result1.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_BookFound()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);

            var obj = new CreateBookDTO
            {
                Title = "test",
                AuthorId = 1,
                Description = "test",
                PublishHouseId = 1,
                Year = DateTime.ParseExact("2019", "yyyy", System.Globalization.CultureInfo.InvariantCulture)
            };


            var result = await service.UpdateAsync(1, obj);

            Assert.IsNotNull(result);
            Assert.AreEqual(obj.Title, result.Title);
        }
        [TestMethod]
        public async Task UpdatePicture_Correct()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);

            var result = await service.UpdatePicture(1, "someurl");

            Assert.IsNotNull(result);
            Assert.AreEqual("someurl", result.URL);
        }

        [TestMethod]
        public async Task UpdatePicture_Error()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);

            var result = await service.UpdatePicture(10, "someurl");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_BookNotFound()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);

            var obj = new CreateBookDTO
            {
                Title = "test",
                AuthorId = 1,
                Description = "test",
                PublishHouseId = 1,
                Year = DateTime.ParseExact("2019", "yyyy", System.Globalization.CultureInfo.InvariantCulture)
            };


            var result = await service.UpdateAsync(100, obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_BookTitleExistFound()
        {
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Loans);

            await context.SaveChangesAsync();

            var service = new BookService(context);

            var obj = new CreateBookDTO
            {
                Title = "Под игото",
                AuthorId = 1,
                Description = "test",
                PublishHouseId = 1,
                Year = DateTime.ParseExact("2019", "yyyy", System.Globalization.CultureInfo.InvariantCulture)
            };


            var result = await service.UpdateAsync(1, obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }
    }
}
