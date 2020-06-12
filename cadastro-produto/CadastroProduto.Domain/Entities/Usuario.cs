using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CadastroProduto.Domain.Entities
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        public string NOME { get; set; }
        public string SENHA { get; set; }
        public string EMAIL { get; set; }
        public int EXCLUIDO { get; set; }
    }
}
