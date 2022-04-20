using WeChipApi.Model;
using WeChipApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeChipApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase {
        private readonly IStatusRepository _statusRepository;
        public StatusController(IStatusRepository statusRepository) {
            _statusRepository = statusRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Status>> GetStatus() {
            return await _statusRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id) {
            return await _statusRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus([FromBody] Status status) {
            var newstatus = await _statusRepository.Create(status);
            return CreatedAtAction(nameof(GetStatus), new { id = newstatus.Id }, newstatus);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id) {
            var statusToDelete = await _statusRepository.Get(id);

            if (statusToDelete == null)
                return NotFound();

            await _statusRepository.Delete(statusToDelete.Id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutStatus(int id, [FromBody] Status status) {
            if (id != status.Id)
                return BadRequest();

            await _statusRepository.Update(status);

            return NoContent();
        }
    }
}
