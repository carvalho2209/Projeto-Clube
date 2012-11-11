using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidade;
using Persistencia;

namespace Negocio
{
    public class NProduto
    {
        //Declara o objeto da camada de persistência
        PProduto pProduto = new PProduto();

        public EProduto Incluir(EProduto produto)
        {
            try
            {
                //Chama o método de inclusão
                produto = pProduto.Incluir(produto);

                //Retorna o objeto com o identificador preenchido
                return produto;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public bool Alterar(EProduto produto)
        {
            try
            {
                //Chama o método alterar da classe de persistencia
                return pProduto.Alterar(produto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Excluir(int identificador)
        {
            try
            {
                return pProduto.Excluir(identificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public EProduto Consultar(int identificador)
        {
            try
            {
                return pProduto.Consultar(identificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EProduto> Listar(EProduto produto)
        {
            try
            {
                return pProduto.Listar(produto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EProduto> Listar()
        {
            try
            {
                EProduto produto = new EProduto();
                return this.Listar(produto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
