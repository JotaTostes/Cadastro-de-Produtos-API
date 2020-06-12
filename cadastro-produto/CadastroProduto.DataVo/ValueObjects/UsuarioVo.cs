using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CadastroProduto.DataVo.ValueObjects
{
    [DataContract]
    public class UsuarioVo
    {
        [DataMember(Order = 1, Name = "id", IsRequired = true)]
        public int id { get; set; }
        [DataMember(Order = 2, Name = "nome", IsRequired = true)]
        public string nome { get; set; }

        [DataMember(Order = 3, Name = "senha", IsRequired = true)]
        public string senha { get; set; }

        [DataMember(Order = 4, Name = "email", IsRequired = true)]
        public string email { get; set; }
    }
}
