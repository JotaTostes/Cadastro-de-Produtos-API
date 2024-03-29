﻿using CadastroProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Repository.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        /// <summary>
        /// Retorna todos os usuario cadastrados com paginação
        /// </summary>
        /// <param name="page"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        List<Usuario> GetUsuarios(int page, int lenght);

        /// <summary>
        /// Retorna quantidade de registros 
        /// </summary>
        /// <returns></returns>
         int GetAllCount();
    }
}
