using Microsoft.AspNetCore.Mvc;
using Note.Api.Filters;
using Note.Core.DTO.Note;
using Note.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    [Route("api/v1/dashboards")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        protected readonly IDashboardService _dashboardService;

        public DashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET api/notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DashboardDTO>>> GetAsync()
        {
            var dashboards = await _dashboardService.GetAllDashboardsAsync();
            return Ok(dashboards.ToList());
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DashboardDTO>> GetAsync(string id)
        {
            var dashboard = await _dashboardService.GetDashboardAsync(id);
            return Ok(dashboard);
        }

        // POST api/notes
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<DashboardDTO>> PostAsync([FromBody] CreateDashboardDTO dto)
        {
            var dashboard = await _dashboardService.CreateDashboardAsync(dto);
            return Ok(dashboard);
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<ActionResult<DashboardDTO>> PutAsync(string id, [FromBody] UpdateDashboardDTO dto)
        {
            var dashboard = await _dashboardService.UpdateDashboardAsync(id, dto);
            return Ok(dashboard);
        }

        // DELETE api/notes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result = await _dashboardService.DeleteDashboardAsync(id);
            return Ok();
        }
    }
}