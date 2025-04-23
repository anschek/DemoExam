using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffQualificationAssessment.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly PmContext _db;
        public StaffController(PmContext db)
        {
            _db = db;
        }

        [HttpGet("{rawCode}")]
        public async Task<ActionResult> Get([FromRoute] string rawCode)
        {
            try
            {
                return Ok(await DetermineReturnCode(rawCode));
            }
            catch
            {
                return BadRequest();
            }
        }

        private async Task<int> DetermineReturnCode(string rawCode)
        {
            if (!Int32.TryParse(rawCode, out int code)) return -1;

            if (code < 1000) return -1;

            if (!await UserExists(code)) return 0;

            if (!await UserHasActivities(code)) return 1;

            return 2;
        }

        private async Task<bool> UserExists(int code) =>
            await _db.Staff.AnyAsync(s => s.CodStaff == code);

        private async Task<bool> UserHasActivities(int code) =>
            await _db.EmployeeMetrics
            .Include(em => em.Staff)
            .AnyAsync(em => em.Staff.CodStaff == code);

    }
}
