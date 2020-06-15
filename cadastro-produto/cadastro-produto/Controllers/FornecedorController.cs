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
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly FornecedorConverters _fornecedorConverters;

        public FornecedorController(IFornecedorService fornecedorService, FornecedorConverters fornecedorConverters)
        {
            _fornecedorService = fornecedorService;
            _fornecedorConverters = fornecedorConverters;
        }

        /// <summary>
        /// Adiciona um novo fornecedor
        /// </summary>
        /// <param name="produtoVo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] FornecedorVo fornecedorVo)
        {
            try
            {
                var ret = _fornecedorService.Add(_fornecedorConverters.Parse(fornecedorVo));
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
        /// Exclui um fornecedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Id/{id}")]
        public ActionResult<int> Excluir(int id)
        {
            try
            {
                var ret = _fornecedorService.Excluir(id);
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
        /// Edita um fornecedor
        /// </summary>
        /// <param name="produtoVo"></param>
        /// <returns></returns>
        [HttpPut("Forn/{fornecedorVo}")]
        public ActionResult Update([FromBody] FornecedorVo fornecedorVo)
        {
            try
            {
                var ret = _fornecedorService.Update(fornecedorVo);
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
        /// Retorna todos os fornecedores cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page/{page}/Length/{length}")]
        public ActionResult Get(int page, int length)
        {
            try
            {
                var ret = _fornecedorService.GetAllFornecedores(page, length);
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
        /// Retorna todos os fornecedores cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var ret = _fornecedorService.GetAll().Where(x => x.EXCLUIDO == 0);
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