using CadastroProduto.DataVo.Inferfaces;
using CadastroProduto.DataVo.ValueObjects;
using CadastroProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroProduto.DataVo.Converters
{
    public class ProdutoConverters : IParser<ProdutoVo, Produto>, IParser<Produto, ProdutoVo>
    {
        private FornecedorConverters _fornecedorConverters = new FornecedorConverters();
        public Produto Parse(ProdutoVo origin)
        {
            if (origin == null) return new Produto();
            return new Produto
            {
                ID = origin.id,
                NOME = origin.nome,
                VALOR = origin.valor,
                QUANTIDADE = origin.quantidade,
                IDFORNECEDOR = origin.idFornecedor,
                Fornecedor = _fornecedorConverters.Parse(origin.Fornecedor)
            };
        }

        public ProdutoVo Parse(Produto origin)
        {
            if (origin == null) return new ProdutoVo();
            return new ProdutoVo
            {
                id = origin.ID,
                nome = origin.NOME,
                valor = origin.VALOR,
                quantidade = origin.QUANTIDADE,
                idFornecedor = origin.IDFORNECEDOR,
                Fornecedor = _fornecedorConverters.Parse(origin.Fornecedor)
            };
        }

        public List<Produto> ParseList(List<ProdutoVo> origin)
        {
            if (origin == null) return new List<Produto>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<ProdutoVo> ParseList(List<Produto> origin)
        {
            if (origin == null) return new List<ProdutoVo>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
