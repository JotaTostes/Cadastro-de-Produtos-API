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
    }
}
