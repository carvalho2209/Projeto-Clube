using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidade
{
    [Serializable]
    public class EItemMovimentacao
    {
        public EItemMovimentacao()
        {
            Produto = new EProduto();
            MovimentacaoConta = new EMovimentacaoConta();
        }

        public int Identificador { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public int IDMovimentacao { get; set; }
        public int IDProduto { get; set; }

        public EProduto Produto { get; set; }
        public EMovimentacaoConta MovimentacaoConta { get; set; }

    }
}
