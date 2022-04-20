using WeChipApi.Model;
using WeChipApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeChipApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase {
        private readonly IProdutosRepository _produtoRepository;
        public ProdutosController(IProdutosRepository produtoRepository) {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Produtos>> GetProdutos() {
            return await _produtoRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produtos>> GetProdutos(int id) {
            return await _produtoRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Produtos>> PostProdutos([FromBody] Produtos produto) {
            var newproduto = await _produtoRepository.Create(produto);
            return CreatedAtAction(nameof(GetProdutos), new { id = newproduto.Id }, newproduto);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id) {
            var produtoToDelete = await _produtoRepository.Get(id);

            if (produtoToDelete == null)
                return NotFound();

            await _produtoRepository.Delete(produtoToDelete.Id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutProdutos(int id, [FromBody] Produtos produto) {
            if (id != produto.Id)
                return BadRequest();

            await _produtoRepository.Update(produto);

            return NoContent();
        }
    }
}
