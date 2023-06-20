using Microsoft.AspNetCore.Mvc;
using NerdySoftTestTask.Extentions;
using NerdySoftTestTask.Services.Interfaces;
using NerdySoftTestTask.Shared.DTOs;

namespace NerdySoftTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementService _announcementService;
        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AnnouncementDto>> AddAnnouncement([FromBody] AnnouncementDto announcement)
        {
            try
            {
                var addedAnnouncement = await _announcementService.AddAnnouncement(announcement);

                if (addedAnnouncement == null)
                {
                    return BadRequest();
                }

                var addedAnnouncementDto = addedAnnouncement.ConvertToDto();

                return Ok(addedAnnouncementDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("Edit")]
        public async Task<ActionResult<AnnouncementDto>> EditAnnouncement(AnnouncementDto toEditedAnnouncement)
        {
            try
            {
                var editedAnnouncement = await _announcementService.EditAnnouncement(toEditedAnnouncement);

                if (editedAnnouncement == null)
                {
                    return BadRequest();
                }

                var editedAnnouncementDto = editedAnnouncement.ConvertToDto();

                return Ok(editedAnnouncementDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("Delete/id:int")]
        public async Task<ActionResult<AnnouncementDto>> DeleteAnnouncement(int id)
        {
            try
            {
                var deletedAnnouncement = await _announcementService.DeleteAnnouncement(id);

                if (deletedAnnouncement == null)
                {
                    return NotFound();
                }

                var deletedAnnouncementDto = deletedAnnouncement.ConvertToDto();
                
                return Ok(deletedAnnouncementDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetAnnouncements()
        {
            try
            {
                var announcements = await _announcementService.GetAllAnnouncements();

                if (announcements == null)
                {
                    return NotFound();
                }

                var announcementsDto = announcements.ConvertToDto();

                return Ok(announcementsDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("id:int")]
        public async Task<ActionResult<AnnouncementDto>> GetAnnouncement(int id)
        {
            try
            {
                var announcement = await _announcementService.GetAnnouncement(id);

                if (announcement == null)
                {
                    return NotFound();
                }

                var announcementDto = announcement.ConvertToDto();

                return Ok(announcementDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("id:int/Similar")]
        public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetSimilarAnnouncements(int id)
        {
            try
            {
                var similarAnnouncements = await _announcementService.GetSimalarAnnouncements(id);

                if (similarAnnouncements == null)
                {
                    return NotFound();
                }

                var similarAnnouncementsDto = similarAnnouncements.ConvertToDto();

                return Ok(similarAnnouncementsDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
