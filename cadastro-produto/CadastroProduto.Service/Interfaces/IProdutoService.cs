using CadastroProduto.DataVo.ValueObjects;
using CadastroProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Service.Interfaces
{
    public interface IProdutoService : IServiceBase<Produto>
    {
        /// <summary>
        /// Exclusão lógica de um produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProdutoVo Excluir(int id);

        /// <summary>
        /// Edita um produto
        /// </summary>
        /// <param name="produtoVo"></param>
        /// <returns></returns>
        ProdutoVo Update(ProdutoVo produtoVo);

        /// <summary>
        /// Retorna todos os produtos cadastrados paginados
        /// </summary>
        /// <returns></returns>
        object GetAllProdutos(int page, int lenght);
    }
}
