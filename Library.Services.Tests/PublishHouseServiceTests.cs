using Library.Common;
using Library.Data;
using Library.Services.DTOs;
using Library.Services.DTOs.BookDTOs;
using Library.Services.DTOs.PublishHouseDTOs;
using Library.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Tests
{
    [TestClass]
    public class PublishHouseServiceTests
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

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);
            var count = await context.PublishHouses.CountAsync();
            var result = await service.DeleteAsync(2);

            Assert.IsNotNull(result);
            Assert.AreEqual("Пан", result.Name);
            Assert.AreEqual(count - 1, await context.PublishHouses.CountAsync());
        }

        [TestMethod]
        public async Task DeleteAsync_NotFound()
        { 
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);
            var result = await service.DeleteAsync(5);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.HOUSE_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task FilterHousesAsync()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);
            var result = await service.FilterPublishHousesAsync(0, "Art");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task FilterHousesAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);
            var result = await service.FilterPublishHousesAsync(0, "@@@");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task FilterHousesAsync_NotFound_Page()
        {
            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);
            var result = await service.FilterPublishHousesAsync(5, "@");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetAsync()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);
            var result = await service.GetAsync(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetAsync_NotFound_Page()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);
            var result = await service.GetAsync(100);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetHouseByNameOrIdAsync()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);
            var result = await service.GetHouseByNameOrIdAsync("House");

            Assert.IsNotNull(result);
            Assert.AreEqual("Art House", result.Name);
        }

        [TestMethod]
        public async Task GetHouseByNameOrIdAsync_NotFound()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);
            var result = await service.GetHouseByNameOrIdAsync("asd");

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.HOUSE_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);
            var count = await context.PublishHouses.CountAsync();

            var obj = new CreatePublishHouseDTO
            {
                Name = "test",
                Description = "test"
            };

            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Name);
            Assert.AreEqual(count + 1, await context.PublishHouses.CountAsync());
        }

        [TestMethod]
        public async Task PostAsync_MissingData()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);

            var obj = new CreatePublishHouseDTO { Name = null };

            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync_ExistingData()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);

            var obj = new CreatePublishHouseDTO
            {
                Name = "Art House",
                Description = "test"
            };


            var result1 = await service.PostAsync(obj);

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result1.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_HouseFound()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);

            var obj = new CreatePublishHouseDTO
            {
                Name = "test",
                Description = "test"
            };


            var result = await service.UpdateAsync(1, obj);

            Assert.IsNotNull(result);
            Assert.AreEqual(obj.Name, result.Name);
        }
        [TestMethod]
        public async Task UpdatePicture_Correct()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);

            var result = await service.UpdatePicture(1, "someurl");

            Assert.IsNotNull(result);
            Assert.AreEqual("someurl", result.URL);
        }

        [TestMethod]
        public async Task UpdatePicture_Error()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);

            var result = await service.UpdatePicture(10, "someurl");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_HouseNotFound()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);

            var obj = new CreatePublishHouseDTO
            {
                Name = "test",
                Description = "test"
            };


            var result = await service.UpdateAsync(100, obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_HouseNameExist()
        {

            await context.AddRangeAsync(DataInitializer.PublishHouses);
            await context.AddRangeAsync(DataInitializer.Books);

            await context.SaveChangesAsync();
            var service = new PublishHouseService(context);

            var obj = new CreatePublishHouseDTO
            {
                Name = "Пан",
                Description = "test"
            };


            var result = await service.UpdateAsync(1, obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }
    }
}
