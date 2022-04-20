using WeChipApi.Model;
using WeChipApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeChipApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase {
        private readonly IEnderecosRepository _enderecoRepository;
        public EnderecosController(IEnderecosRepository enderecoRepository) {
            _enderecoRepository = enderecoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Enderecos>> GetEnderecos() {
            return await _enderecoRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enderecos>> GetEnderecos(int id) {
            return await _enderecoRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Enderecos>> PostEnderecos([FromBody] Enderecos endereco) {
            var newEndereco = await _enderecoRepository.Create(endereco);
            return CreatedAtAction(nameof(GetEnderecos), new { id = newEndereco.Id }, newEndereco);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id) {
            var enderecoToDelete = await _enderecoRepository.Get(id);

            if (enderecoToDelete == null)
                return NotFound();

            await _enderecoRepository.Delete(enderecoToDelete.Id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutEnderecos(int id, [FromBody] Enderecos endereco) {
            if (id != endereco.Id)
                return BadRequest();

            await _enderecoRepository.Update(endereco);

            return NoContent();
        }
    }
}