using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidade
{
    [Serializable]
    public class EMovimentacaoConta
    {
        public EMovimentacaoConta()
        {
            Associado = new EAssociado();
            ListaItensMovimentacao = new List<EItemMovimentacao>();
        }

        public int Identificador { get; set; }
        public int ID_Associado { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataHoraMovimentacao { get; set; }
        public EAssociado Associado { get; set; }
        public List<EItemMovimentacao> ListaItensMovimentacao { get; set; }

    }
}
