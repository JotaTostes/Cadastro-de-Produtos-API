using CadastroProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Repository.Interfaces
{
    public interface IFornecedorRepository : IBaseRepository<Fornecedor>
    {
        /// <summary>
        /// Retorna todos os fornecedores cadastrados com paginação
        /// </summary>
        /// <param name="page"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        List<Fornecedor> GetFornecedor(int page, int lenght);


        /// <summary>
        /// Retorna quantidade de registros 
        /// </summary>
        /// <returns></returns>
        int GetAllCount();
    }
}
