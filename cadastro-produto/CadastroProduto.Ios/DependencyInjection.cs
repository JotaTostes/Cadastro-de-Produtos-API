using CadastroProduto.DataVo.Converters;
using CadastroProduto.Repository.Interfaces;
using CadastroProduto.Repository.Repositories;
using CadastroProduto.Service.Interfaces;
using CadastroProduto.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Ioc
{
    public class DependencyInjection
    {
        /// <summary>
        /// Adiciona a injeção de dependência entre os serviços e suas interfaces.
        /// </summary>
        /// <param name="services"></param>
        public static void InjecaoDependenciaServicos(ref IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
        }

        /// <summary>
        /// Adiciona a injeção de dependência entre os repositorios e suas interfaces.
        /// </summary>
        /// <param name="services"></param>
        public static void InjecaoDependenciaRepositorios(ref IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

        public static void InjecaoDependenciaConverters(ref IServiceCollection services)
        {
            services.AddScoped<UsuarioConverters, UsuarioConverters>();
        }
    }
}
