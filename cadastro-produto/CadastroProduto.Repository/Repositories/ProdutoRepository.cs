using CadastroProduto.Domain.Entities;
using CadastroProduto.Repository.Context;
using CadastroProduto.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Repository.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(CadastroProdutoContext context) : base(context)
        { }

        /// <summary>
        /// Retornar todas os produtos cadastrados 
        /// </summary>
        /// <returns></returns>
        public List<Produto> GetProdutos(int page, int lenght)
        {
            using (var db = new CadastroProdutoContext())
            {
                var lQuery = (from Produto in db.Produto.AsNoTracking()
                              join Fornecedor in db.Fornecedor.AsNoTracking()
                                on Produto.IDFORNECEDOR equals Fornecedor.ID
                              where Produto.EXCLUIDO == 0

                              select new Produto
                              {
                                  ID = Produto.ID,
                                  NOME = Produto.NOME,
                                  QUANTIDADE = Produto.QUANTIDADE,
                                  VALOR = Produto.VALOR,
                                  IDFORNECEDOR = Produto.IDFORNECEDOR,
                                  Fornecedor = new Fornecedor
                                  {
                                      NOME = Fornecedor.NOME
                                  }

                              }).OrderByDescending(x => x.NOME)
                              .Skip((page - 1) * lenght)
                              .Take(lenght)
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
                var lQuery = (from Produto in db.Produto.AsNoTracking()
                              join Fornecedor in db.Fornecedor.AsNoTracking()
                                on Produto.IDFORNECEDOR equals Fornecedor.ID
                              where Produto.EXCLUIDO == 0
                              select new Produto
                              {
                                  ID = Produto.ID
                              }).ToList();

                return lQuery.Count();
            }
        }
    }
}
