using NerdySoftTestTask.Entities;
using NerdySoftTestTask.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdySoftTestTask.Services.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<Announcement>> GetAllAnnouncements();
        Task<Announcement> GetAnnouncement(int id);
        Task<Announcement> AddAnnouncement(AnnouncementDto newAnnouncement);
        Task<Announcement> EditAnnouncement(AnnouncementDto editedAnnouncement);
        Task<Announcement> DeleteAnnouncement(int id);
        Task<IEnumerable<Announcement>> GetSimalarAnnouncements(int announcementId);
    }
}
