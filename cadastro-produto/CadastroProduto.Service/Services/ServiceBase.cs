using CadastroProduto.Repository.Interfaces;
using CadastroProduto.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Service.Services
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        protected IBaseRepository<TEntity> BaseRepository;

        protected ServiceBase(IBaseRepository<TEntity> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        /// <summary>
        /// Adiciona um novo registro.
        /// </summary>
        /// <param name="obj"></param>
        public TEntity Add(TEntity obj)
        {
            //var add = Atribuicoes.AtribuirLojaIdEntidade(obj, GetIdUsuarioLogado());
            //BaseRepository.Add(Atribuicoes.AtribuirUsuarioInclusaoId(add, GetIdUsuarioLogado()));

            return BaseRepository.Add(obj);
        }

        /// <summary>
        /// Busca todos os registros.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return BaseRepository.GetAll();
        }

        /// <summary>
        /// Retorna um registro específico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(int id)
        {
            return BaseRepository.GetById(id);
        }

        /// <summary>
        /// Remove um registro.
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(TEntity obj)
        {
            BaseRepository.Remove(obj);
        }

        /// <summary>
        /// Remove varios registros
        /// </summary>
        /// <param name="predicate"></param>
        public void Remove(Func<TEntity, bool> predicate)
        {
            BaseRepository.Remove(predicate);
        }

        /// <summary>
        /// Atualiza um registro.
        /// </summary>
        /// <param name="obj"></param>
        public void Update(TEntity obj)
        {
            // atualizar = Atribuicoes.AtribuirUsuarioInclusaoId(obj, GetIdUsuarioLogado());
            BaseRepository.Update(obj);
        }
    }
}
