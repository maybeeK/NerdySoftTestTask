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

namespace NerdySoftTestTask.Tests
{
    public class Tests
    {
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
                    Title= "Test qweer",
                    Description= "TestDesrc roman"
                },
                new Announcement{
                    Id = 2,
                    Title = "Test",
                    Description = "Test"
                }, 
                new Announcement{ 
                    Id = 3,
                    Title = "Test",
                    Description= "Test qwert"
                },  
                new Announcement{
                    Id = 4,
                    Title = "NOT NOT",
                    Description= "Test"
                } 
            }.ToList();

            return result;
        }
    }
}
