using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidade;
using System.Data.SqlServerCe;

namespace Persistencia
{
    public class PItemMovimentacao
    {
        public void Incluir(EItemMovimentacao itemMovimentacao, SqlCeConnection cnn)
        {
            //SqlCeConnection cnn = new SqlCeConnection();
            //cnn.ConnectionString = Conexao.Caminho;

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = cnn;

            #region inserção do itemMovimentacao
            cmd.CommandText = @"INSERT INTO Item_Movimentacao
                               (Quantidade, ValorUnitario, ID_Movimentacao_Conta, ID_Produto)
                                VALUES ( @Quantidade, @ValorUnitario, @ID_Movimentacao_Conta,@ID_Produto)";

            PreencherParametros(itemMovimentacao, cmd);

            //Executa o comando setado - INSERT
            //cnn.Open();
            cmd.ExecuteNonQuery();
            #endregion inserção do itemMovimentacao

            //Gera o comando sql para recuperar o último id 
            //gerado pelo insert acima
            cmd.CommandText = "SELECT @@Identity as id";
            
            //Executa o command retornando um DataReader
            SqlCeDataReader rdr =  cmd.ExecuteReader();

            //Lê o datareader gerado
            rdr.Read();
            //Seta para a entidade, o valor retornado pelo dataReader
            itemMovimentacao.Identificador = int.Parse(rdr["id"].ToString());
            
            //Fecha a conexão
            //cnn.Close();

            //return itemMovimentacao;
        }

        public bool Excluir(int identificador, SqlCeConnection cnn)
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = cnn;

            #region exclusao do itemMovimentacao
            cmd.CommandText = @"DELETE FROM Movimentacao_Conta
                               WHERE ID_Movimentacao_Conta = @Identificador ";

            cmd.Parameters.Add("@Identificador", identificador);

            //Executa o comando setado - DELETE
            cnn.Open();
            cmd.ExecuteNonQuery();
            #endregion exclusao do itemMovimentacao

            //Fecha a conexão
            cnn.Close();

            return true;
        }

        public EItemMovimentacao Consultar(int identificador)
        {
            #region declaração de variáveis
            SqlCeConnection cnn = new SqlCeConnection();
            SqlCeCommand cmd = new SqlCeCommand();

            cnn.ConnectionString = Conexao.Caminho;
            cmd.Connection = cnn;
            #endregion declaração de variáveis

            cmd.CommandText = "SELECT * FROM Item_Movimentacao WHERE identificador = @Identificador";


            cmd.Parameters.Add("@Identificador", identificador);

            cnn.Open();
            SqlCeDataReader rdr = cmd.ExecuteReader();
            EItemMovimentacao _itemMovimentacao = new EItemMovimentacao();

            if (rdr.Read())
            {
                PreencherObjeto(rdr, _itemMovimentacao);
            }
            cnn.Close();
            return _itemMovimentacao;
        }

        public List<EItemMovimentacao> Listar(int identificador)
        {
            #region declaração de variáveis
            SqlCeConnection cnn = new SqlCeConnection();
            SqlCeCommand cmd = new SqlCeCommand();

            cnn.ConnectionString = Conexao.Caminho;
            cmd.Connection = cnn;
            #endregion declaração de variáveis

            cmd.CommandText = "SELECT * FROM Item_Movimentacao Where ID_Movimentacao_Conta = @identificador";
            cmd.Parameters.Add("@Identificador", identificador);


            cnn.Open();
            SqlCeDataReader rdr = cmd.ExecuteReader();

            List<EItemMovimentacao> lstRetorno = new List<EItemMovimentacao>();

            while (rdr.Read())
            {
                EItemMovimentacao _itemMovimentacao = new EItemMovimentacao();
                PreencherObjeto(rdr, _itemMovimentacao);
                lstRetorno.Add(_itemMovimentacao);            
            }
            cnn.Close();
            return lstRetorno;
        }

        
        private static void PreencherParametros(EItemMovimentacao itemMovimentacao, SqlCeCommand cmd)
        {
            cmd.Parameters.Add("@Quantidade", itemMovimentacao.Quantidade);
            cmd.Parameters.Add("@ID_Movimentacao_Conta", itemMovimentacao.IDMovimentacao);
            cmd.Parameters.Add("@ValorUnitario", itemMovimentacao.ValorUnitario);
            cmd.Parameters.Add("@ID_Produto", itemMovimentacao.IDProduto);
            cmd.Parameters.Add("@Identificador", itemMovimentacao.Identificador);
        }

        private static void PreencherObjeto(SqlCeDataReader rdr, EItemMovimentacao _itemMovimentacao)
        {
            _itemMovimentacao.Identificador = int.Parse(rdr["identificador"].ToString());
            _itemMovimentacao.Quantidade = decimal.Parse(rdr["Quantidade"].ToString());
            _itemMovimentacao.ValorUnitario = decimal.Parse(rdr["ValorUnitario"].ToString());
            _itemMovimentacao.IDMovimentacao = int.Parse(rdr["ID_Movimentacao_Conta"].ToString());
            _itemMovimentacao.IDProduto = int.Parse(rdr["ID_Produto"].ToString());
            _itemMovimentacao.Produto = new PProduto().Consultar(_itemMovimentacao.IDProduto);
            _itemMovimentacao.MovimentacaoConta = new PMovimentacaoConta().Consultar(_itemMovimentacao.IDMovimentacao);

            _itemMovimentacao.ValorTotal = _itemMovimentacao.ValorUnitario * _itemMovimentacao.Quantidade;
        }
    
    }
}
