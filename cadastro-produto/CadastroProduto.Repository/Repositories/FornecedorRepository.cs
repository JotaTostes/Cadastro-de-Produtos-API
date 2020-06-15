using CadastroProduto.Domain.Entities;
using CadastroProduto.Repository.Context;
using CadastroProduto.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Repository.Repositories
{
    public class FornecedorRepository : BaseRepository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(CadastroProdutoContext context) : base(context)
        {}

        /// <summary>
        /// Retorna todos os fornecedores cadastrados com paginação
        /// </summary>
        /// <param name="page"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public List<Fornecedor> GetFornecedor(int page, int length)
        {
            using (var db = new CadastroProdutoContext())
            {
                var lQuery = (from Fornecedor in db.Fornecedor.AsNoTracking()
                              where Fornecedor.EXCLUIDO == 0
                              select Fornecedor)
                              .OrderByDescending(x => x.NOME)
                              .Skip((page - 1) * length)
                              .Take(length)
                              .ToList();

                return lQuery;
            }
        }

        /// <summary>
        /// Retorna quantidade de registros 
        /// </summary>
        /// <returns></returns>
        public int GetAllCount()
        {
            using (var db = new CadastroProdutoContext())
            {
                var lQuery = (from Fornecedor in db.Fornecedor.AsNoTracking()
                              where Fornecedor.EXCLUIDO == 0
                              select new Fornecedor
                              {
                                  ID = Fornecedor.ID
                              }).ToList();
                return lQuery.Count();
            }
        }
    }
}
