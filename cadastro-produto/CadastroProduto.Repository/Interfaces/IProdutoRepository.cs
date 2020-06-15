using CadastroProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Repository.Interfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        /// <summary>
        /// Retornar todas os produtos cadastrados 
        /// </summary>
        /// <returns></returns>
        List<Produto> GetProdutos(int page, int lenght);

        /// <summary>
        /// Retorna quantidade de registros 
        /// </summary>
        /// <returns></returns>
        int GetAllCount();
    }
}
