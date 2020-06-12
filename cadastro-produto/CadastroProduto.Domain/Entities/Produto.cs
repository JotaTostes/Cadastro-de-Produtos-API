using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CadastroProduto.Domain.Entities
{
    [Table("PRODUTO")]
    public class Produto
    {
        [Key]
        public int ID { get; set; }
        public string NOME { get; set; }
        public int VALOR { get; set; }
        public int IDFORNECEDOR { get; set; }
    }
}
