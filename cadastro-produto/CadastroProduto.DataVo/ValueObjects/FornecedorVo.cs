using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CadastroProduto.DataVo.ValueObjects
{
    [DataContract]
    public class FornecedorVo
    {
        [DataMember(Order = 1, Name = "id", IsRequired = true)]
        public int id { get; set; }
        [DataMember(Order = 2, Name = "nome", IsRequired = true)]
        public string nome { get; set; }
    }
}
