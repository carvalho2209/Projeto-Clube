using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidade;
using System.Data.SqlServerCe;

namespace Persistencia
{
    public class PProduto
    {
        public EProduto Incluir(EProduto produto)
        {
            SqlCeConnection cnn = new SqlCeConnection();
            cnn.ConnectionString = Conexao.Caminho;

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = cnn;

            #region inserção do produto
            cmd.CommandText = @"INSERT INTO Produto 
                               (Descricao, Categoria, ValorUnitario, Quantidade)
                                VALUES ( @Descricao, @Categoria, @ValorUnitario, @Quantidade)";

            PreencherParametros(produto, cmd);

            //Executa o comando setado - INSERT
            cnn.Open();
            cmd.ExecuteNonQuery();
            #endregion inserção do produto

            //Gera o comando sql para recuperar o último id 
            //gerado pelo insert acima
            cmd.CommandText = "SELECT @@Identity as id";
            
            //Executa o command retornando um DataReader
            SqlCeDataReader rdr =  cmd.ExecuteReader();

            //Lê o datareader gerado
            rdr.Read();
            //Seta para a entidade, o valor retornado pelo dataReader
            produto.Identificador = int.Parse(rdr["id"].ToString());
            
            //Fecha a conexão
            cnn.Close();

            return produto;
        }
    
        public bool Alterar(EProduto produto)
        {
            SqlCeConnection cnn = new SqlCeConnection();
            cnn.ConnectionString = Conexao.Caminho;

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = cnn;

            #region alteracao do produto
            cmd.CommandText = @"UPDATE Produto SET
                                Descricao = @Descricao,
                                Categoria = @Categoria, 
                                ValorUnitario = @ValorUnitario, 
                                Quantidade = @Quantidade
                                WHERE Identificador = @Identificador)";

            PreencherParametros(produto, cmd);

            //Executa o comando setado - UPDATE
            cnn.Open();
            cmd.ExecuteNonQuery();
            #endregion alteracao do produto
            
            //Fecha a conexão
            cnn.Close();

            return true;
        }

        public bool Excluir(int identificador)
        {
            SqlCeConnection cnn = new SqlCeConnection();
            cnn.ConnectionString = Conexao.Caminho;

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = cnn;

            #region exclusao do produto
            cmd.CommandText = @"DELETE FROM Produto
                               WHERE Identificador = @Identificador ";

            cmd.Parameters.Add("@Identificador", identificador);

            //Executa o comando setado - DELETE
            cnn.Open();
            cmd.ExecuteNonQuery();
            #endregion exclusao do produto

            //Fecha a conexão
            cnn.Close();

            return true;
        }

        public EProduto Consultar(int identificador)
        {
            #region declaração de variáveis
            SqlCeConnection cnn = new SqlCeConnection();
            SqlCeCommand cmd = new SqlCeCommand();

            cnn.ConnectionString = Conexao.Caminho;
            cmd.Connection = cnn;
            #endregion declaração de variáveis

            cmd.CommandText = "SELECT * FROM Produto WHERE identificador = @Identificador";


            cmd.Parameters.Add("@Identificador", identificador);

            cnn.Open();
            SqlCeDataReader rdr = cmd.ExecuteReader();
            EProduto _produto = new EProduto();

            if (rdr.Read())
            {
                PreencherObjeto(rdr, _produto);
            }
            cnn.Close();
            return _produto;
        }

        public List<EProduto> Listar(EProduto produto)
        {
            #region declaração de variáveis
            SqlCeConnection cnn = new SqlCeConnection();
            SqlCeCommand cmd = new SqlCeCommand();

            cnn.ConnectionString = Conexao.Caminho;
            cmd.Connection = cnn;
            #endregion declaração de variáveis

            cmd.CommandText = "SELECT * FROM Produto";
            cmd.CommandText += " ORDER BY Descricao";

            cnn.Open();
            SqlCeDataReader rdr = cmd.ExecuteReader();

            List<EProduto> lstRetorno = new List<EProduto>();

            while (rdr.Read())
            {
                EProduto _produto = new EProduto();
                PreencherObjeto(rdr, _produto);
                lstRetorno.Add(_produto);            
            }
            cnn.Close();
            return lstRetorno;
        }

        
        private static void PreencherParametros(EProduto produto, SqlCeCommand cmd)
        {
            cmd.Parameters.Add("@Descricao", produto.Descricao);
            cmd.Parameters.Add("@Categoria", produto.Categoria);
            cmd.Parameters.Add("@ValorUnitario", produto.ValorUnitario);
            cmd.Parameters.Add("@Quantidade", produto.Quantidade);
            cmd.Parameters.Add("@Identificador", produto.Identificador);
        }

        private static void PreencherObjeto(SqlCeDataReader rdr, EProduto _produto)
        {
            _produto.Identificador = int.Parse(rdr["identificador"].ToString());
            _produto.Descricao = rdr["Descricao"].ToString();
            _produto.Categoria = rdr["Categoria"].ToString();
            _produto.ValorUnitario = decimal.Parse(rdr["ValorUnitario"].ToString());
            _produto.Quantidade = decimal.Parse(rdr["Quantidade"].ToString());
        }
    
    }
}
