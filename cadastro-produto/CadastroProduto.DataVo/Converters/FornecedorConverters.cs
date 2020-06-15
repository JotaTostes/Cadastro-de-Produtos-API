using CadastroProduto.DataVo.Inferfaces;
using CadastroProduto.DataVo.ValueObjects;
using CadastroProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroProduto.DataVo.Converters
{
    public class FornecedorConverters : IParser<FornecedorVo, Fornecedor>, IParser<Fornecedor, FornecedorVo>
    {
        public Fornecedor Parse(FornecedorVo origin)
        {
            if (origin == null) return new Fornecedor();
            return new Fornecedor
            {
                ID = origin.id,
                NOME = origin.nome,
            };
        }

        public FornecedorVo Parse(Fornecedor origin)
        {
            if (origin == null) return new FornecedorVo();
            return new FornecedorVo
            {
                id = origin.ID,
                nome = origin.NOME,
            };
        }

        public List<Fornecedor> ParseList(List<FornecedorVo> origin)
        {
            if (origin == null) return new List<Fornecedor>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<FornecedorVo> ParseList(List<Fornecedor> origin)
        {
            if (origin == null) return new List<FornecedorVo>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
