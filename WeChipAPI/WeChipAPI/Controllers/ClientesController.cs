using WeChipApi.Model;
using WeChipApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeChipApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase {
        private readonly IClientesRepository _clienteRepository;
        public ClientesController(IClientesRepository clienteRepository) {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Clientes>> GetClientes() {
            return await _clienteRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetClientes(int id) {
            return await _clienteRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes([FromBody] Clientes cliente) {
            var newCliente = await _clienteRepository.Create(cliente);
            return CreatedAtAction(nameof(GetClientes), new { id = newCliente.Id }, newCliente);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id) {
            var clienteToDelete = await _clienteRepository.Get(id);

            if (clienteToDelete == null)
                return NotFound();

            await _clienteRepository.Delete(clienteToDelete.Id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutClientes(int id, [FromBody] Clientes cliente) {
            if (id != cliente.Id)
                return BadRequest();

            await _clienteRepository.Update(cliente);

            return NoContent();
        }
    }
}