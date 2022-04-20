using WeChipApi.Model;
using WeChipApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeChipApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUsersRepository _userRepository;
        public UsersController(IUsersRepository userRepository) {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Users>> GetUsers() {
            return await _userRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id) {
            return await _userRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers([FromBody] Users user) {
            var newuser = await _userRepository.Create(user);
            return CreatedAtAction(nameof(GetUsers), new { id = newuser.Id }, newuser);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id) {
            var userToDelete = await _userRepository.Get(id);

            if (userToDelete == null)
                return NotFound();

            await _userRepository.Delete(userToDelete.Id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutUsers(int id, [FromBody] Users user) {
            if (id != user.Id)
                return BadRequest();

            await _userRepository.Update(user);

            return NoContent();
        }
    }
}