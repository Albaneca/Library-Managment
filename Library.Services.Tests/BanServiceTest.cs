using Library.Common;
using Library.Data;
using Library.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Library.Services.Tests
{
    [TestClass]
    public class BanServiceTests
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
        public async Task PermaBanUserAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();
            var service = new BanService(context);
            var result = await service.BanUserAsync("user1@gmail.com", "knows too much", null);

            Assert.IsNotNull(result);
            Assert.AreEqual("knows too much", result.Reason);
        }

        [TestMethod]
        public async Task BanUserAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();
            var service = new BanService(context);
            var result = await service.BanUserAsync("user1@gmail.com", "knows too much", DateTime.Now.AddDays(5));

            Assert.IsNotNull(result);
            Assert.AreEqual("knows too much", result.Reason);
        }

        [TestMethod]
        public async Task BanUserWithNoReasonAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();
            var service = new BanService(context);
            var result = await service.BanUserAsync("user1@gmail.com", null, DateTime.Now.AddDays(5));

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.NO_COMMENT, result.Reason);
        }

        [TestMethod]
        public async Task BanUserAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();
            var service = new BanService(context);
            var result = await service.BanUserAsync("", "knows too much", null);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.USER_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetAllBannedUsersAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();
            var service = new BanService(context);

            await service.BanUserAsync("user3@gmail.com", "knows too much", null);

            var result = await service.GetAllBannedUsersAsync(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetAllBannedUsersAsync_WrongPage()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();
            var service = new BanService(context);

            var result = await service.GetAllBannedUsersAsync(10);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task UnbanUserAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();
            var service = new BanService(context);

            var result = await service.UnbanUserAsync("user1@gmail.com");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.UserId);
        }

        [TestMethod]
        public async Task UnbanUserAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();
            var service = new BanService(context);

            var result = await service.UnbanUserAsync("");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetMaxPageAsync()
        {
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();
            var service = new BanService(context);

            var result = await service.GetMaxPageAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result);
        }

    }
}
