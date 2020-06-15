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
    public class FornecedorService : ServiceBase<Fornecedor>, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly FornecedorConverters _fornecedorConverters;

        public FornecedorService(IFornecedorRepository fornecedorRepository, FornecedorConverters fornecedorConverters) : base(fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorConverters = fornecedorConverters;
        }

        /// <summary>
        /// Exclusão lógica de um produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FornecedorVo Excluir(int id)
        {
            var fornecedor = _fornecedorRepository.GetById(id);

            fornecedor.EXCLUIDO = 1;

            _fornecedorRepository.Update(fornecedor);

            return _fornecedorConverters.Parse(fornecedor);
        }

        /// <summary>
        /// Edita um fornecedor
        /// </summary>
        /// <param name="fornecedorVo"></param>
        /// <returns></returns>
        public FornecedorVo Update(FornecedorVo fornecedorVo)
        {
            Fornecedor fornecedorEntity = _fornecedorConverters.Parse(fornecedorVo);
            Fornecedor fornecedor = _fornecedorRepository.GetById(fornecedorVo.id);

            if (fornecedor != null)
            {
                fornecedor.NOME = fornecedorEntity.NOME;

                _fornecedorRepository.Update(fornecedor);
            }

            return fornecedorVo;
        }

        /// <summary>
        /// Retorna todos os fornecedores cadastrados paginados
        /// </summary>
        /// <returns></returns>
        public object GetAllFornecedores(int page, int lenght)
        {
            var Registros = _fornecedorConverters.ParseList(_fornecedorRepository.GetFornecedor(page, lenght));

            int totalRegistros = _fornecedorRepository.GetAllCount();

            return new { Registros, totalRegistros };
        }

    }
}
