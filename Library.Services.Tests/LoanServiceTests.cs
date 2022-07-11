using Library.Common;
using Library.Data;
using Library.Services.DTOs.LoanDTOs;
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
    public class LoanServiceTests
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

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var count = await context.Loans.CountAsync();
            var result = await service.DeleteAsync(2);

            Assert.IsNotNull(result);
            Assert.AreEqual("Под игото", result.BookName);
            Assert.AreEqual(count - 1, await context.Loans.CountAsync());
        }

        [TestMethod]
        public async Task DeleteAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var count = await context.Loans.CountAsync();
            var result = await service.DeleteAsync(5);

            Assert.IsNotNull(result);
            Assert.AreEqual(count, await context.Loans.CountAsync());
            Assert.AreEqual(GlobalConstants.LOAN_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetAsync()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var result = await service.GetAsync(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetAsync_NotFound_Page()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var result = await service.GetAsync(100);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetHouseByNameOrIdAsync()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var count = await context.Loans.CountAsync();
            var result = await service.GetLoansByRequesterNameOrIdAsync("1", 0);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task PostAsync()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var count = await context.Loans.CountAsync();

            var obj = new CreateLoanDTO
            {
                BookId = 1
            };

            var result = await service.PostAsync("user1@gmail.com", obj);

            Assert.IsNotNull(result);
            Assert.AreEqual("Ivan Ivanov", result.RequesterName);
            Assert.AreEqual(count + 1, await context.Loans.CountAsync());
        }


        [TestMethod]
        public async Task PostAsync_UserNotFound()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var count = await context.Loans.CountAsync();

            var obj = new CreateLoanDTO
            {
                BookId = 1
            };

            var result = await service.PostAsync("user10@gmail.com", obj);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.USER_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetNotConfirmedAsync()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var count = await context.Loans.CountAsync();

            var result = await service.GetNotConfirmedAsync(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task DecideLoan_Approved()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var count = await context.Loans.CountAsync();

            var result = await service.DecideLoan("anjelo98@gmail.com", 1, true);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, await context.Loans.Where(x => x.Status == GlobalConstants.LOAN_CONFIRMED).CountAsync());
        }

        [TestMethod]
        public async Task DecideLoan_Denied()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var count = await context.Loans.CountAsync();

            var result = await service.DecideLoan("anjelo98@gmail.com", 1, false);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, await context.Loans.Where(x => x.Status == GlobalConstants.LOAN_DENIED).CountAsync());
        }

        [TestMethod]
        public async Task DecideLoan_LoanNotFound()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var count = await context.Loans.CountAsync();

            var result = await service.DecideLoan("anjelo98@gmail.com", 5, false);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.LOAN_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task DecideLoan_ApproverNotFound()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);
            await context.AddRangeAsync(DataInitializer.Loans);
            await context.AddRangeAsync(DataInitializer.Authors);
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();
            var service = new LoanService(context);
            var count = await context.Loans.CountAsync();

            var result = await service.DecideLoan("anjelo@gmail.com", 1, false);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.USER_NOT_FOUND, result.ErrorMessage);
        }
    }
}
