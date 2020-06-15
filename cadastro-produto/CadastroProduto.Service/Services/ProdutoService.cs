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
    public class ProdutoService : ServiceBase<Produto>, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ProdutoConverters _produtoConverters;

        public ProdutoService(IProdutoRepository produtoRepository, ProdutoConverters produtoConverters) : base(produtoRepository)
        {
            _produtoRepository = produtoRepository;
            _produtoConverters = produtoConverters;
        }

        /// <summary>
        /// Exclusão lógica de um produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProdutoVo Excluir(int id)
        {
            var produto = _produtoRepository.GetById(id);

            produto.EXCLUIDO = 1;

            _produtoRepository.Update(produto);

            return _produtoConverters.Parse(produto);
        }

        /// <summary>
        /// Edita um produto
        /// </summary>
        /// <param name="produtoVo"></param>
        /// <returns></returns>
        public ProdutoVo Update(ProdutoVo produtoVo)
        {
            Produto produtoEntity = _produtoConverters.Parse(produtoVo);
            Produto produto = _produtoRepository.GetById(produtoVo.id);

            if (produto != null)
            {
                produto.NOME = produtoEntity.NOME;
                produto.VALOR = produtoEntity.VALOR;
                produto.QUANTIDADE = produtoEntity.QUANTIDADE;
                produto.IDFORNECEDOR = produtoEntity.IDFORNECEDOR;

                _produtoRepository.Update(produto);
            }

            return produtoVo;
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados paginados
        /// </summary>
        /// <returns></returns>
        public object GetAllProdutos(int page, int lenght)
        {
            var Registros = _produtoConverters.ParseList(_produtoRepository.GetProdutos(page, lenght));

            int totalRegistros = _produtoRepository.GetAllCount();

            return new { Registros, totalRegistros };
        }

    }
}
