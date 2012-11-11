using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidade;
using System.Data.SqlServerCe;
using System.Transactions;

namespace Persistencia
{
    public class PMovimentacaoConta
    {
        public EMovimentacaoConta Incluir(EMovimentacaoConta movimentacaoConta)
        {
            SqlCeConnection cnn = new SqlCeConnection();
            cnn.ConnectionString = Conexao.Caminho;

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = cnn;

            try
            {
                using (TransactionScope transacao = new TransactionScope())
                {
                    #region inclusao
                    #region inserção do movimentacaoConta
                    cmd.CommandText = @"INSERT INTO Movimentacao_Conta 
                               (DataHoraMovimentacao, IDAssociado, ValorTotal)
                                VALUES ( @DataHoraMovimentacao, @IDAssociado, @ValorTotal)";

                    PreencherParametros(movimentacaoConta, cmd);

                    //Executa o comando setado - INSERT
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    #endregion inserção do movimentacaoConta

                    //Gera o comando sql para recuperar o último id 
                    //gerado pelo insert acima
                    cmd.CommandText = "SELECT @@Identity as id";

                    //Executa o command retornando um DataReader
                    SqlCeDataReader rdr = cmd.ExecuteReader();

                    //Lê o datareader gerado
                    rdr.Read();
                    //Seta para a entidade, o valor retornado pelo dataReader
                    movimentacaoConta.Identificador = int.Parse(rdr["id"].ToString());

                    //Incluir os itens de movimentacao
                    foreach (EItemMovimentacao varLocal in movimentacaoConta.ListaItensMovimentacao)
                    {
                        varLocal.IDMovimentacao = movimentacaoConta.Identificador;
                        new PItemMovimentacao().Incluir(varLocal, cnn);
                    }
                    //Fim da inclusao dos itens

                    #endregion inclusao
                    transacao.Complete();
                }
            }
            catch (Exception)
            {
                Transaction.Current.Rollback();
                throw new Exception();
            }
            finally
            {
                cnn.Close();
            }

            return movimentacaoConta;
        }

        public bool Alterar(EMovimentacaoConta movimentacaoConta)
        {
            SqlCeConnection cnn = new SqlCeConnection();
            cnn.ConnectionString = Conexao.Caminho;

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = cnn;

            try
            {
                using (TransactionScope transacao = new TransactionScope())
                {
                    #region alteracao do movimentacaoConta
                    cmd.CommandText = @"UPDATE Movimentacao_Conta SET
                                DataHoraMovimentacao = @DataHoraMovimentacao,
                                IDAssociado = @IDAssociado, 
                                ValorTotal = @ValorTotal, 
                                WHERE Identificador = @Identificador)";

                    PreencherParametros(movimentacaoConta, cmd);

                    //Executa o comando setado - UPDATE
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    #endregion alteracao do movimentacaoConta

                    //Apagar todos os itens da movimentacao a partir 
                    //do identificador da movimentacao
                    bool retorno = new PItemMovimentacao().Excluir(movimentacaoConta.Identificador, cnn);

                    //Percorre a lista incluindo todos os itens
                    foreach (var item in movimentacaoConta.ListaItensMovimentacao)
                    {
                        item.IDMovimentacao = movimentacaoConta.Identificador;
                        new PItemMovimentacao().Incluir(item, cnn);
                    }

                    //"Commita" a transação
                    transacao.Complete();
                }
            }
            catch (Exception)
            {
                //Caso ocorra algum erro, executar o rollback da transação
                Transaction.Current.Rollback();
                throw new Exception();
            }
            finally
            {
                //Fecha a conexão
                cnn.Close();
            }

            return true;
        }

        public bool Excluir(int identificador)
        {
            SqlCeConnection cnn = new SqlCeConnection();
            cnn.ConnectionString = Conexao.Caminho;

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = cnn;

            try
            {
                using (TransactionScope transacao = new TransactionScope())
                {
                    //Exclui primeiramente os itens
                    bool retorno = new PItemMovimentacao().Excluir(identificador, cnn);

                    #region exclusao do movimentacaoConta
                    cmd.CommandText = @"DELETE FROM Movimentacao_Conta
                               WHERE Identificador = @Identificador ";

                    cmd.Parameters.Add("@Identificador", identificador);

                    //Executa o comando setado - DELETE
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    #endregion exclusao do movimentacaoConta

                    transacao.Complete();
                }
            }
            catch (Exception)
            {
                Transaction.Current.Rollback();
                throw new Exception();
            }
            finally
            {
                //Fecha a conexão
                cnn.Close();
            }

            return true;
        }

        public EMovimentacaoConta Consultar(int identificador)
        {
            #region declaração de variáveis
            SqlCeConnection cnn = new SqlCeConnection();
            SqlCeCommand cmd = new SqlCeCommand();

            cnn.ConnectionString = Conexao.Caminho;
            cmd.Connection = cnn;
            #endregion declaração de variáveis

            cmd.CommandText = "SELECT * FROM Movimentacao_Conta WHERE identificador = @Identificador";


            cmd.Parameters.Add("@Identificador", identificador);

            cnn.Open();
            SqlCeDataReader rdr = cmd.ExecuteReader();
            EMovimentacaoConta _movimentacaoConta = new EMovimentacaoConta();

            if (rdr.Read())
            {
                PreencherObjeto(rdr, _movimentacaoConta);
            }
            cnn.Close();
            return _movimentacaoConta;
        }

        public List<EMovimentacaoConta> Listar(EMovimentacaoConta movimentacaoConta)
        {
            #region declaração de variáveis
            SqlCeConnection cnn = new SqlCeConnection();
            SqlCeCommand cmd = new SqlCeCommand();

            cnn.ConnectionString = Conexao.Caminho;
            cmd.Connection = cnn;
            #endregion declaração de variáveis

            cmd.CommandText = "SELECT * FROM MovimentacaoConta";
            cmd.CommandText += " ORDER BY Descricao";

            cnn.Open();
            SqlCeDataReader rdr = cmd.ExecuteReader();

            List<EMovimentacaoConta> lstRetorno = new List<EMovimentacaoConta>();

            while (rdr.Read())
            {
                EMovimentacaoConta _movimentacaoConta = new EMovimentacaoConta();
                PreencherObjeto(rdr, _movimentacaoConta);
                lstRetorno.Add(_movimentacaoConta);
            }
            cnn.Close();
            return lstRetorno;
        }

        private static void PreencherParametros(EMovimentacaoConta movimentacaoConta, SqlCeCommand cmd)
        {
            cmd.Parameters.Add("@DataHoraMovimentacao", movimentacaoConta.DataHoraMovimentacao);
            cmd.Parameters.Add("@IDAssociado", movimentacaoConta.ID_Associado);
            cmd.Parameters.Add("@ValorTotal", movimentacaoConta.ValorTotal);
            cmd.Parameters.Add("@Identificador", movimentacaoConta.Identificador);
        }

        private static void PreencherObjeto(SqlCeDataReader rdr, EMovimentacaoConta _movimentacaoConta)
        {
            _movimentacaoConta.Identificador = int.Parse(rdr["identificador"].ToString());
            _movimentacaoConta.DataHoraMovimentacao = DateTime.Parse(rdr["DataHoraMovimentacao"].ToString());
            _movimentacaoConta.ID_Associado = int.Parse(rdr["IDAssociado"].ToString());
            _movimentacaoConta.ValorTotal = decimal.Parse(rdr["ValorTotal"].ToString());
            _movimentacaoConta.Associado = new PAssociado().Consultar(_movimentacaoConta.ID_Associado);
            _movimentacaoConta.ListaItensMovimentacao = new PItemMovimentacao().Listar(_movimentacaoConta.Identificador);

        }

    }
}
