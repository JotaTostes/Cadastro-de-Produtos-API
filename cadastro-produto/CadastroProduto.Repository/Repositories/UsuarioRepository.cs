﻿using CadastroProduto.Domain.Entities;
using CadastroProduto.Repository.Context;
using CadastroProduto.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(CadastroProdutoContext context) : base(context)
        { }

        /// <summary>
        /// Retorna todos os usuario cadastrados com paginação
        /// </summary>
        /// <param name="page"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public List<Usuario> GetUsuarios(int page, int lenght)
        {
            using (var db = new CadastroProdutoContext())
            {
                var lQuery = (from Usuario in db.Usuario.AsNoTracking()
                              where Usuario.EXCLUIDO == 0
                              select Usuario)
                              .OrderByDescending(x => x.NOME)
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
                var lQuery = (from Usuario in db.Usuario.AsNoTracking()
                              where Usuario.EXCLUIDO == 0
                              select new Usuario
                              {
                                  ID = Usuario.ID
                              }).ToList();
                return lQuery.Count();
            }
        }
    }
}
