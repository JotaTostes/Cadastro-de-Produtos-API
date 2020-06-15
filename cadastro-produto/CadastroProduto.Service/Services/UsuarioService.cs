using CadastroProduto.DataVo.Converters;
using CadastroProduto.DataVo.ValueObjects;
using CadastroProduto.Domain.Entities;
using CadastroProduto.Repository.Interfaces;
using CadastroProduto.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Service.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioConverters _usuarioConverter;



        public UsuarioService(IUsuarioRepository usuarioRepository, UsuarioConverters usuarioConverter) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioConverter = usuarioConverter;
        }

        /// <summary>
        /// Eclusão lógica de um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UsuarioVo Excluir(int id)
        {
            var usuario = _usuarioRepository.GetById(id);

            usuario.EXCLUIDO = 1;

            _usuarioRepository.Update(usuario);

            return _usuarioConverter.Parse(usuario);
        }

        public UsuarioVo Update(UsuarioVo usuarioVo)
        {
            Usuario usuarioENtity = _usuarioConverter.Parse(usuarioVo);
            Usuario usuario = _usuarioRepository.GetById(usuarioVo.id);

            if (usuario != null)
            {
                usuario.NOME = usuarioENtity.NOME;
                usuario.EMAIL = usuarioENtity.EMAIL;
                usuario.SENHA = usuarioENtity.SENHA;

                _usuarioRepository.Update(usuario);
            }

            return usuarioVo;
        }

        /// <summary>
        /// Retorna todos os usuarios cadastrados paginados
        /// </summary>
        /// <returns></returns>
        public object GetAllUsuarios(int page, int lenght)
        {
            var Registros = _usuarioConverter.ParseList(_usuarioRepository.GetUsuarios(page, lenght));

            int totalRegistros = _usuarioRepository.GetAllCount();

            return new { Registros, totalRegistros };
        }
    }
}
