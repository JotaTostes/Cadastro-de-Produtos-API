using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroProduto.DataVo.Converters;
using CadastroProduto.DataVo.ValueObjects;
using CadastroProduto.Domain.Entities;
using CadastroProduto.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cadastro_produto.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly UsuarioConverters _usuarioConverter;


        public UsuarioController(IUsuarioService usuarioService, UsuarioConverters usuarioConverter)
        {
            _usuarioService = usuarioService;
            _usuarioConverter = usuarioConverter;
        }

        /// <summary>
        /// Inserir um novo usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Usuario> Post (UsuarioVo usuarioVo)
        {
            try
            {
                var ret = _usuarioService.Add(_usuarioConverter.Parse(usuarioVo));

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
        /// Retorna um usuario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Id/{id}")]
        public ActionResult<int> GetById(int id)
        {
            try
            {
                var ret = _usuarioService.GetById(id);
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
        /// Exlui um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<int> Excluir(int id)
        {
            try
            {
                var ret = _usuarioService.Excluir(id);
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

        [HttpPut("Usuario/{usuario}")]
        public ActionResult Update([FromBody] UsuarioVo usuario)
        {
            try
            {
                var ret = _usuarioService.Update(usuario);
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