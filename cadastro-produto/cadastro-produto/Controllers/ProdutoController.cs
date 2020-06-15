using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroProduto.DataVo.Converters;
using CadastroProduto.DataVo.ValueObjects;
using CadastroProduto.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cadastro_produto.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ProdutoConverters _produtoConverters;

        public ProdutoController(IProdutoService produtoService, ProdutoConverters produtoConverters)
        {
            _produtoService = produtoService;
            _produtoConverters = produtoConverters;
        }

        /// <summary>
        /// Adiciona um novo produtos
        /// </summary>
        /// <param name="produtoVo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post ([FromBody] ProdutoVo produtoVo)
        {
            try
            {
                var ret = _produtoService.Add(_produtoConverters.Parse(produtoVo));
                
                return Ok(ret);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " | " + e.InnerException.Message);
            }
        }

        /// <summary>
        /// Exlui um produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<int> Excluir(int id)
        {
            try
            {
                var ret = _produtoService.Excluir(id);
                return Ok(ret);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " | " + e.InnerException.Message);
            }
        }

        /// <summary>
        /// Edita um produto
        /// </summary>
        /// <param name="produtoVo"></param>
        /// <returns></returns>
        [HttpPut("Prod/{produtoVo}")]
        public ActionResult Update([FromBody] ProdutoVo produtoVo)
        {
            try
            {
                var ret = _produtoService.Update(produtoVo);
                return Ok(ret);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " | " + e.InnerException.Message);
            }
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page/{page}/Lenght/{lenght}")]
        public ActionResult Get(int page, int lenght)
        {
            try
            {
                var ret = _produtoService.GetAllProdutos(page, lenght);
                return Ok(ret);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " | " + e.InnerException.Message);
            }
        }
    }
}   