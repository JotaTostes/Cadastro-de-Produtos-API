using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CadastroProduto.DataVo.ValueObjects
{
    [DataContract]
    public class ProdutoVo
    {
        [DataMember(Order = 1, Name = "id", IsRequired = true)]
        public int id { get; set; }
        [DataMember(Order = 2, Name = "nome", IsRequired = true)]
        public string nome { get; set; }
        [DataMember(Order = 3, Name = "valor", IsRequired = true)]
        public int valor { get; set; }
        [DataMember(Order = 4, Name = "idFornecedor", IsRequired = true)]
        public int idFornecedor { get; set; }
        [DataMember(Order = 5, Name = "quantidade", IsRequired = true)]
        public int quantidade { get; set; }
        [DataMember(Order = 6, Name = "fornecedor", IsRequired = false)]
        public FornecedorVo Fornecedor { get; set; }


    }
}
