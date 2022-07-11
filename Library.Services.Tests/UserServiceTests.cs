using Library.Common;
using Library.Data;
using Library.Services.DTOs;
using Library.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Tests
{
    [TestClass]
    public class UserServiceTests
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
        public async Task FilterUsersAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();

            var service = new UserService(context);
            var result = await service.FilterUsersAsync(0, "@");

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public async Task FilterUsersAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();

            var service = new UserService(context);
            var result = await service.FilterUsersAsync(0, "@@@");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task FilterUsersAsync_NotFound_Page()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();

            var service = new UserService(context);
            var result = await service.FilterUsersAsync(5, "@");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task DeleteAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();
            var count = await context.Users.CountAsync();
            var service = new UserService(context);
            var result = await service.DeleteAsync("user1@gmail.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("user1@gmail.com", result.Email);
            Assert.AreEqual(count - 1, await context.Users.CountAsync());
        }

        [TestMethod]
        public async Task DeleteAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();

            var service = new UserService(context);
            var result = await service.DeleteAsync("");

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.USER_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task GetAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();

            var service = new UserService(context);
            var result = await service.GetAsync(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public async Task GetAsync_NotFound_Page()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();

            var service = new UserService(context);
            var result = await service.GetAsync(100);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetUserByEmailOrIdAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();

            var service = new UserService(context);
            var result = await service.GetUserByEmailOrIdAsync("anjelo98@gmail.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("Anjelo", result.FirstName);
        }

        [TestMethod]
        public async Task GetUserByEmailOrIdAsync_NotFound()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);
            await context.AddRangeAsync(DataInitializer.Bans);

            await context.SaveChangesAsync();

            var service = new UserService(context);
            var result = await service.GetUserByEmailOrIdAsync("");

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.USER_NOT_FOUND, result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);

            await context.SaveChangesAsync();

            var service = new UserService(context);

            var obj = new RegisterUserDTO
            {
                Username = "test",
                FirstName = "pesho",
                LastName = "peshev",
                Email = "jwr@mm.nh",
                PhoneNumber = "+3590759089",
                Password = "User123$"
            };


            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Username);
        }

        [TestMethod]
        public async Task PostAsync_MissingData()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);

            await context.SaveChangesAsync();

            var service = new UserService(context);

            var obj = new RegisterUserDTO
            {
                FirstName = "pesho",
                LastName = "peshev",
                Email = "jwr@mm.nh",
                PhoneNumber = "+3590759089",
                Password = "User123$"
            };

            var result = await service.PostAsync(obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task PostAsync_ExistingData()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);

            await context.SaveChangesAsync();

            var service = new UserService(context);

            var existingUsername = new RegisterUserDTO
            {
                Username = "Albaneca",
                FirstName = "Anjelo",
                LastName = "Jotov",
                Email = "anjelo98@gmail.com",
                PhoneNumber = "+3590759089",
                Password = "User123$"
            };
            var existingPhone = new RegisterUserDTO
            {
                Username = "Somename",
                FirstName = "fName",
                LastName = "lName",
                Email = "someemail@gmail.com",
                PhoneNumber = "0881122331",
                Password = "User123$"
            };
            var existingEmail = new RegisterUserDTO
            {
                Username = "Somename",
                FirstName = "fName",
                LastName = "lName",
                Email = "anjelo98@gmail.com",
                PhoneNumber = "0881122331",
                Password = "User123$"
            };
            var invalidData = new RegisterUserDTO
            {
                Username = "S",
                FirstName = "fName",
                LastName = "lName",
                Email = "anjelo98.com",
                PhoneNumber = "088112",
                Password = "user1"
            };


            var result1 = await service.PostAsync(existingUsername);
            var result2 = await service.PostAsync(existingPhone);
            var result3 = await service.PostAsync(existingEmail);
            var result4 = await service.PostAsync(invalidData);

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result1.ErrorMessage);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result2.ErrorMessage);
            Assert.IsNotNull(result3);
            Assert.IsNotNull(result3.ErrorMessage);
            Assert.IsNotNull(result4);
            Assert.IsNotNull(result4.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_UserFound()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);

            await context.SaveChangesAsync();

            var service = new UserService(context);

            var obj = new UserDTO
            {
                Username = "Test",
                FirstName = "Qni",
                LastName = "Yotov",
                Email = "qni98@gmail.com",
                Password = "Test123$",
                PhoneNumber = "0881231234"
            };


            var result = await service.UpdateAsync("anjelo98@gmail.com", obj);

            Assert.IsNotNull(result);
            Assert.AreEqual(obj.Username, result.Username);
        }
        [TestMethod]
        public async Task UpdateProfilePicture_Correct()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);

            await context.SaveChangesAsync();

            var service = new UserService(context);

            var result = await service.UpdateProfilePicture("anjelo98@gmail.com", "someurl");

            Assert.IsNotNull(result);
            Assert.AreEqual("someurl", result.ImageLink);
        }

        [TestMethod]
        public async Task UpdateProfilePicture_Error()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);

            await context.SaveChangesAsync();

            var service = new UserService(context);

            var result = await  service.UpdateProfilePicture("", "someurl");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_UserNotFound()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);

            await context.SaveChangesAsync();

            var service = new UserService(context);

            var obj = new UserDTO
            {
                Username = "test"
            };


            var result = await service.UpdateAsync("", obj);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateAsync_UserEmailAlreadyExists()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);

            await context.SaveChangesAsync();

            var service = new UserService(context);

            var obj = new UserDTO
            {
                Username = "Endless",
                Email = "user1@gmail.com"
            };


            var result = await service.UpdateAsync("anjelo98@gmail.com", obj);

            Assert.IsNotNull(result);
            Assert.AreEqual(GlobalConstants.USER_EXISTS, result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdatePasswordAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);
            await context.AddRangeAsync(DataInitializer.ApplicationRoles);

            await context.SaveChangesAsync();
            var pass = context.Users
                .Where(x => x.Email == "anjelo98@gmail.com")
                .Select(x => x.Password)
                .FirstOrDefaultAsync();

            var service = new UserService(context);

            var result = await service.UpdatePasswordAsync("anjelo98@gmail.com", "Test123$");

            Assert.IsNotNull(result);
            Assert.AreNotEqual(pass, result.Password);
        }

        [TestMethod]
        public async Task UsersCountAsync()
        {
            await context.AddRangeAsync(DataInitializer.Users);

            await context.SaveChangesAsync();

            var service = new UserService(context);

            var result = await service.UsersCountAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(await context.Users.CountAsync(), result);
        }
    }

}
