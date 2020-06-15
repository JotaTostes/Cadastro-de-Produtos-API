using CadastroProduto.DataVo.ValueObjects;
using CadastroProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Service.Interfaces
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        /// <summary>
        /// Eclusão lógica de um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UsuarioVo Excluir(int id);

        UsuarioVo Update(UsuarioVo usuarioVo);

        /// <summary>
        /// Retorna todos os usuarios cadastrados paginados
        /// </summary>
        /// <returns></returns>
         object GetAllUsuarios(int page, int lenght);
    }
}
