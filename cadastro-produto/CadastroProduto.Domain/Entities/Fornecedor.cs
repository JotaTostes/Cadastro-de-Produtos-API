using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CadastroProduto.Domain.Entities
{
    [Table("FORNECEDOR")]
   public class Fornecedor
    {
        [Key]
        public int ID { get; set; }
        public string NOME { get; set; }
        public int EXCLUIDO { get; set; }
    }
}
