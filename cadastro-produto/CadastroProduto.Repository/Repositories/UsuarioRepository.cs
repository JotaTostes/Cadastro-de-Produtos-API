using CadastroProduto.Domain.Entities;
using CadastroProduto.Repository.Context;
using CadastroProduto.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(CadastroProdutoContext context) : base(context)
        { }

    }
}
