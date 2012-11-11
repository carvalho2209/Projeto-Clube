using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidade;

namespace Apresentacao.Paginas
{
    public partial class Cad_Movimentacao : System.Web.UI.Page
    {

        public List<EItemMovimentacao> ListaItens {
            get { return (List<EItemMovimentacao>)Session["Lista"]; } 
            set { Session["Lista"] = value; } 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //CarregarProduto();
                    //CarregarGridVazia();
                    CarregarGridAssociado();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            try
            {
                RetornarPagina();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region eventos das grids
        protected void grdPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("style", "cursor:pointer;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((System.Web.UI.Control)sender, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void grdPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow linhaSelecionada = this.grdPesquisa.SelectedRow;
            string x = linhaSelecionada.Cells[1].Text;

            NAssociado nAssociado = new NAssociado();
            EAssociado Associado = new EAssociado();

            Associado = nAssociado.Consultar(int.Parse(x));
            txtAssociado.Text = Associado.nome;
            hdfIdAssociado.Value = Associado.identificador.ToString();

            //EAssociado associado = new NAssociado().Consultar(int.Parse(x));

            btnPesquisar_ModalPopupExtender.Hide();

        }

        protected void grdPesquisa_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grdPesquisa.Columns[0].Visible = false;
            }
        }

        protected void grdPesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPesquisa.PageIndex = e.NewPageIndex;
                CarregarGridAssociado();
                btnPesquisar_ModalPopupExtender.Show();
            }
            catch (Exception)
            {
                throw;
            }
        }




        protected void grdItemMovimentacao_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("style", "cursor:pointer;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((System.Web.UI.Control)sender, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void grdItemMovimentacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow linhaSelecionada = this.grdItemMovimentacao.SelectedRow;
            string x = linhaSelecionada.Cells[1].Text;

            btnPesquisar_ModalPopupExtender.Hide();

        }

        protected void grdItemMovimentacao_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grdItemMovimentacao.Columns[0].Visible = false;
            }
        }

        protected void grdItemMovimentacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdItemMovimentacao.PageIndex = e.NewPageIndex;
                CarregarGridAssociado();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion eventos das grids

        private void RetornarPagina()
        {
            //Retorna para a pagina de pesquisa
            Response.Redirect("~/Default.aspx");
        }

        private void CarregarProduto()
        {
            NProduto ngProduto = new NProduto();
            List<EProduto> lstRetorno = ngProduto.Listar();

            ddlProduto.DataSource = lstRetorno;
            ddlProduto.DataBind();
        }

        private void CarregarGridVazia()
        {
            ListaItens = null;
            grdItemMovimentacao.DataSource = ListaItens;
            grdItemMovimentacao.DataBind();
        }

        private void CarregarGridAssociado()
        {
            //Carregar a grid
            NAssociado nAssociado = new NAssociado();
            EAssociado eAssociado = new EAssociado();
            List<EAssociado> lstRetorno = nAssociado.Listar(eAssociado);

            grdPesquisa.DataSource = lstRetorno;
            grdPesquisa.DataBind();
        }

    }
}