using Microsoft.EntityFrameworkCore;
using NerdySoftTestTask.Data;
using NerdySoftTestTask.Entities;
using NerdySoftTestTask.Services.Interfaces;
using NerdySoftTestTask.Shared.DTOs;

namespace NerdySoftTestTask.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly AppDbContext _context;
        public AnnouncementService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Announcement> AddAnnouncement(AnnouncementDto newAnnouncement)
        {
            Announcement announcementToAdd = new Announcement()
            {
                Title = newAnnouncement.Title,
                Description = newAnnouncement.Description,
                DateAdded = newAnnouncement.DateAdded
            };

            var addedAnnouncement = await _context.Announcements.AddAsync(announcementToAdd);

            await _context.SaveChangesAsync();

            return addedAnnouncement.Entity;
        }

        public async Task<Announcement> DeleteAnnouncement(int id)
        {
            var announcementToDelete = await _context.Announcements.FirstOrDefaultAsync(e => e.Id == id);

            if (announcementToDelete != null)
            {
                _context.Announcements.Remove(announcementToDelete);
                await _context.SaveChangesAsync();
            }

            return announcementToDelete;
        }

        public async Task<Announcement> EditAnnouncement(AnnouncementDto editedAnnouncement)
        {
            var announcementToEdit = await _context.Announcements.FirstOrDefaultAsync(e => e.Id == editedAnnouncement.Id);

            if (announcementToEdit != null)
            {
                announcementToEdit.Title = editedAnnouncement.Title;
                announcementToEdit.Description = editedAnnouncement.Description;

                await _context.SaveChangesAsync();
            }

            return announcementToEdit;
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncements()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task<Announcement> GetAnnouncement(int id)
        {
            return await _context.Announcements.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Announcement>> GetSimalarAnnouncements(int announcementId)
        {
            var originAnnouncement = await _context.Announcements.FirstOrDefaultAsync(e => e.Id == announcementId);

            if (originAnnouncement == null)
            {
                return default;
            }

            return await _context.Announcements.Where(e => IsSimilar(originAnnouncement, e)).Take(3).ToListAsync();

        }
        private bool IsSimilar(Announcement originAnnouncement, Announcement announcementToCompare)
        {
            if (originAnnouncement.Id == announcementToCompare.Id)
            {
                return false;
            }

            string[] originAnnouncementTitleWords = originAnnouncement.Title.ToLower().Split(' ');
            string[] originAnnouncementDescriptionWords = originAnnouncement.Description.ToLower().Split(' ');

            string[] announcementToComparetitleWords = announcementToCompare.Title.ToLower().Split(' ');
            string[] announcementToComparedescriptionWords = announcementToCompare.Description.ToLower().Split(' ');

            bool isTitleSimilar = originAnnouncementTitleWords.Any(word => announcementToComparetitleWords.Contains(word));
            bool isDescriptionSimilar = originAnnouncementDescriptionWords.Any(word => announcementToComparedescriptionWords.Contains(word));

            return isTitleSimilar && isDescriptionSimilar;
        }
    }
}
