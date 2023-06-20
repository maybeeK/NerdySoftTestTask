using NerdySoftTestTask.Entities;
using NerdySoftTestTask.Shared.DTOs;

namespace NerdySoftTestTask.Extentions
{
    public static class DtoConversions
    {
        public static AnnouncementDto ConvertToDto(this Announcement announcement)
        {
            return new AnnouncementDto
            {
                Id = announcement.Id,
                Title = announcement.Title,
                DateAdded = announcement.DateAdded,
                Description = announcement.Description,
            };
        }
        public static IEnumerable<AnnouncementDto> ConvertToDto(this IEnumerable<Announcement> announcements)
        {
            return announcements.Select(e=>e.ConvertToDto());
        }
    }
}
