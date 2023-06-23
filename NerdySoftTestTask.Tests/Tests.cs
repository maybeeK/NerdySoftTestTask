using Microsoft.EntityFrameworkCore;
using Moq;
using NerdySoftTestTask.Controllers;
using NerdySoftTestTask.Data;
using NerdySoftTestTask.Entities;
using NerdySoftTestTask.Services;
using NerdySoftTestTask.Services.Interfaces;
using NerdySoftTestTask.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;

namespace NerdySoftTestTask.Tests
{
    public class Tests
    {

        [Fact]
        public async void GetAllAnnsServiceTest()
        {
            // Arrange
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DataBase").Options;
            using (var context = new AppDbContext(opts))
            {
                context.Announcements.AddRange(GetTestAnnouncements());
                context.SaveChanges();
            }
            // Act
            using (var context = new AppDbContext(opts))
            {
                var AS = new AnnouncementService(context);
                var anns = await AS.GetAllAnnouncements();

                // Assert
                Assert.Equal(4, anns.Count());
            }

        }


        [Fact]
        public async void GetSomeAnnServiceTest()
        {
            // Arrange
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DataBase").Options;
            using (var context = new AppDbContext(opts))
            {
                context.Announcements.AddRange(GetTestAnnouncements());
                context.SaveChanges();
            }
            // Act
            using (var context = new AppDbContext(opts))
            {
                var AS = new AnnouncementService(context);
                var ann = await AS.GetAnnouncement(1);

                // Assert
                Assert.Equal(1, ann.Id);
            }
        }

        [Fact]
        public async void GetSomeNotExistingAnnServiceTest()
        {
            // Arrange
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DataBase").Options;
            using (var context = new AppDbContext(opts))
            {
                context.Announcements.AddRange(GetTestAnnouncements());
                context.SaveChanges();
            }
            // Act
            using (var context = new AppDbContext(opts))
            {
                var AS = new AnnouncementService(context);
                var ann = await AS.GetAnnouncement(100);

                // Assert
                Assert.Null(ann);
            }
        }

        [Fact]
        public async void DeleteSomeAnnServiceTest()
        {
            // Arrange
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DataBase").Options;
            using (var context = new AppDbContext(opts))
            {
                context.Announcements.AddRange(GetTestAnnouncements());
                context.SaveChanges();
            }
            // Act
            using (var context = new AppDbContext(opts))
            {
                var AS = new AnnouncementService(context);
                var deletedAnn = await AS.DeleteAnnouncement(1);
                var allAnns = await AS.GetAllAnnouncements();
                // Assert
                Assert.Equal(1, deletedAnn.Id);
                Assert.Equal(3, allAnns.Count());
            }
        }

        [Fact]
        public async void DeleteSomeNotExistingAnnServiceTest()
        {
            // Arrange
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DataBase").Options;
            using (var context = new AppDbContext(opts))
            {
                context.Announcements.AddRange(GetTestAnnouncements());
                context.SaveChanges();
            }
            // Act
            using (var context = new AppDbContext(opts))
            {
                var AS = new AnnouncementService(context);
                var deletedAnn = await AS.DeleteAnnouncement(100);
                // Assert
                Assert.Null(deletedAnn);
            }
        }

        [Fact]
        public async void AddAnnServiceTest()
        {
            // Arrange
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DataBase").Options;
            using (var context = new AppDbContext(opts))
            {
                context.Announcements.AddRange(GetTestAnnouncements());
                context.SaveChanges();
            }
            // Act
            using (var context = new AppDbContext(opts))
            {
                var AS = new AnnouncementService(context);
                var toAddAnn = new AnnouncementDto()
                {
                    Title = "Tittle",
                    Description = "Descr"
                };
                var addedAnn = await AS.AddAnnouncement(toAddAnn);
                var allAnns = await AS.GetAllAnnouncements();
                // Assert
                Assert.Equal(6, addedAnn.Id);
                Assert.Equal("Tittle", addedAnn.Title);
                Assert.Equal(6, allAnns.Count());
            }
        }

        [Fact]
        public async void EditAnnServiceTest()
        {
            // Arrange
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DataBase").Options;
            using (var context = new AppDbContext(opts))
            {
                context.Announcements.AddRange(GetTestAnnouncements());
                context.SaveChanges();
            }
            // Act
            using (var context = new AppDbContext(opts))
            {
                var AS = new AnnouncementService(context);
                var toEditAnn = new AnnouncementDto()
                {
                    Id = 1,
                    Title = "NewTittle",
                    Description = "NewDescr"
                };
                var editedAnn = await AS.EditAnnouncement(toEditAnn);
                // Assert
                Assert.Equal(1, editedAnn.Id);
                Assert.Equal("NewTittle", editedAnn.Title);
                Assert.Equal("NewDescr", editedAnn.Description);
            }
        }

        [Fact]
        public async void GetSimilarAnnsServiceTest()
        {
            // Arrange
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DataBase").Options;
            using (var context = new AppDbContext(opts))
            {
                context.Announcements.AddRange(GetTestAnnouncements());
                context.SaveChanges();
            }
            // Act
            using (var context = new AppDbContext(opts))
            {
                var AS = new AnnouncementService(context);
                var similar = await AS.GetSimalarAnnouncements(1);
                // Assert
                Assert.Equal(3, similar.Count());
            }
        }

        [Fact]
        public async void GetNoExistingSimilarAnnsServiceTest()
        {
            // Arrange
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DataBase").Options;
            using (var context = new AppDbContext(opts))
            {
                context.Announcements.AddRange(GetTestAnnouncements());
                context.SaveChanges();
            }
            // Act
            using (var context = new AppDbContext(opts))
            {
                var AS = new AnnouncementService(context);
                var similar = await AS.GetSimalarAnnouncements(100);
                // Assert
                Assert.Null(similar);
            }
        }

        [Fact]
        public async void GetNoEnoughExistingSimilarAnnsServiceTest()
        {
            // Arrange
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DataBase").Options;
            using (var context = new AppDbContext(opts))
            {
                context.Announcements.AddRange(GetTestAnnouncements());
                context.SaveChanges();
            }
            // Act
            using (var context = new AppDbContext(opts))
            {
                var AS = new AnnouncementService(context);
                var similar = await AS.GetSimalarAnnouncements(4);
                // Assert
                Assert.Empty(similar);
            }
        }

        [Fact]
        public async void GetAnnouncementTest()
        {
            // Arrange
            var mock = new Mock<IAnnouncementService>();
            mock.Setup(repo => repo.GetAnnouncement(It.IsAny<int>())).ReturnsAsync(GetTestAnnouncements()[0]);
            AnnouncementController controller = new(mock.Object);
            // Act
            var anns = await controller.GetAnnouncement(1);
            // Assert
            Assert.NotNull(anns);
        }
        
        [Fact]
        public async void GetAnnouncementsTest()
        {
            // Arrange
            var mock = new Mock<IAnnouncementService>();
            mock.Setup(repo => repo.GetAllAnnouncements()).ReturnsAsync(GetTestAnnouncements());
            AnnouncementController controller = new(mock.Object);
            // Act
            var anns = await controller.GetAnnouncements();
            // Assert
            Assert.NotNull(anns);
        }        
        [Fact]
        public async void GetSimilarAnnouncementsTest()
        {
            // Arrange
            var mock = new Mock<IAnnouncementService>();
            mock.Setup(repo => repo.GetSimalarAnnouncements(It.IsAny<int>())).ReturnsAsync(GetTestAnnouncements().Take(3));
            AnnouncementController controller = new(mock.Object);
            // Act
            var anns = await controller.GetSimilarAnnouncements(1);
            // Assert
            Assert.NotNull(anns);
        }
        
        [Fact]
        public async void DeleteAnnouncementTest()
        {
            // Arrange
            var mock = new Mock<IAnnouncementService>();
            mock.Setup(repo => repo.DeleteAnnouncement(It.IsAny<int>())).ReturnsAsync(GetTestAnnouncements()[0]);
            AnnouncementController controller = new(mock.Object);
            // Act
            var anns = await controller.DeleteAnnouncement(1);
            // Assert
            Assert.NotNull(anns);
        }        
        [Fact]
        public async void EditAnnouncementTest()
        {
            // Arrange
            var mock = new Mock<IAnnouncementService>();
            mock.Setup(repo => repo.EditAnnouncement(It.IsAny<AnnouncementDto>())).ReturnsAsync(GetTestAnnouncements()[0]);
            AnnouncementController controller = new(mock.Object);
            // Act
            var anns = await controller.EditAnnouncement(new AnnouncementDto());
            // Assert
            Assert.NotNull(anns);
        }        
        [Fact]
        public async void AddAnnouncementTest()
        {
            // Arrange
            var mock = new Mock<IAnnouncementService>();
            mock.Setup(repo => repo.AddAnnouncement(It.IsAny<AnnouncementDto>())).ReturnsAsync(GetTestAnnouncements()[0]);
            AnnouncementController controller = new(mock.Object);
            // Act
            var anns = await controller.AddAnnouncement(new AnnouncementDto());
            // Assert
            Assert.NotNull(anns);
        }


        private List<Announcement> GetTestAnnouncements()
        {
            var result = new Announcement[] { 
                new Announcement {
                    Id= 1,
                    Title= "Test",
                    Description= "TestD qweqwe"
                },
                new Announcement{
                    Id = 2,
                    Title= "Test",
                    Description= "TestD 12312"
                }, 
                new Announcement{ 
                    Id = 3,
                    Title= "Test",
                    Description= "TestD 123123"
                },  
                new Announcement{
                    Id = 4,
                    Title = "NOT NOT",
                    Description= "TestD"
                },
                new Announcement{
                    Id = 5,
                    Title= "Test",
                    Description= "TestD 123123"
                }
            }.ToList();

            return result;
        }
    }
}
