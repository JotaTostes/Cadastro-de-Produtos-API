using CadastroProduto.DataVo.ValueObjects;
using CadastroProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Service.Interfaces
{
    public interface IFornecedorService : IServiceBase<Fornecedor>
    {
        /// <summary>
        /// Exclusão lógica de um produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FornecedorVo Excluir(int id);

        /// <summary>
        /// Edita um fornecedor
        /// </summary>
        /// <param name="fornecedorVo"></param>
        /// <returns></returns>
        FornecedorVo Update(FornecedorVo fornecedorVo);

        /// <summary>
        /// Retorna todos os fornecedores cadastrados paginados
        /// </summary>
        /// <returns></returns>
        object GetAllFornecedores(int page, int length);
    }
}
