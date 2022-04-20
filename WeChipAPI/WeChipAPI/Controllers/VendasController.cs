using WeChipApi.Model;
using WeChipApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeChipApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase {
        private readonly IVendasRepository _vendaRepository;
        public VendasController(IVendasRepository vendaRepository) {
            _vendaRepository = vendaRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Vendas>> GetVendas() {
            return await _vendaRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendas>> GetVendas(int id) {
            return await _vendaRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Vendas>> PostVendas([FromBody] Vendas venda) {
            var newVenda = await _vendaRepository.Create(venda);
            return CreatedAtAction(nameof(GetVendas), new { id = newVenda.Id }, newVenda);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id) {
            var vendaToDelete = await _vendaRepository.Get(id);

            if (vendaToDelete == null)
                return NotFound();

            await _vendaRepository.Delete(vendaToDelete.Id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutVendas(int id, [FromBody] Vendas venda) {
            if (id != venda.Id)
                return BadRequest();

            await _vendaRepository.Update(venda);

            return NoContent();
        }
    }
}