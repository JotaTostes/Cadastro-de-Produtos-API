using CadastroProduto.DataVo.Inferfaces;
using CadastroProduto.DataVo.ValueObjects;
using CadastroProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroProduto.DataVo.Converters
{
    public class UsuarioConverters : IParser<UsuarioVo, Usuario>, IParser<Usuario, UsuarioVo>
    {
        public Usuario Parse(UsuarioVo origin)
        {
            if (origin == null) return new Usuario();
            return new Usuario
            {
                NOME = origin.nome,
                SENHA = origin.senha,
                EMAIL = origin.email
            };
        }

        public UsuarioVo Parse(Usuario origin)
        {
            if (origin == null) return new UsuarioVo();
            return new UsuarioVo
            {
                nome = origin.NOME,
                senha = origin.SENHA,
                email = origin.EMAIL

            };
        }

        public List<Usuario> ParseList(List<UsuarioVo> origin)
        {
            if (origin == null) return new List<Usuario>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<UsuarioVo> ParseList(List<Usuario> origin)
        {
            if (origin == null) return new List<UsuarioVo>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
