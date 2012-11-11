<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cad_Movimentacao.aspx.cs" Inherits="Apresentacao.Paginas.Cad_Movimentacao" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .style1
    {
    }
    .style2
    {
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
    <tr>
        <td align="right" class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="style2">
            <asp:Label ID="Label1" runat="server" Text="Associado"></asp:Label>
        </td>
        <td>
            <input id="hdfIdAssociado" type="hidden" runat="server"/>
            <asp:TextBox ID="txtAssociado" runat="server" Width="443px"></asp:TextBox>
            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" />
            <asp:ModalPopupExtender ID="btnPesquisar_ModalPopupExtender" runat="server" 
                BackgroundCssClass="modalBackground" CancelControlID="btnFecharPesquisa" 
                DynamicServicePath="" Enabled="True" PopupControlID="pnlPesquisaAssociado" 
                TargetControlID="btnPesquisar">
            </asp:ModalPopupExtender>
&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="style2">
            <asp:Label ID="Label3" runat="server" Text="Produto"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduto" runat="server" Width="440px" 
                AppendDataBoundItems="True" DataTextField="Descricao" 
                DataValueField="Identificador">
                <asp:ListItem Value="0">Selecione...</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="style2">
            <asp:Label ID="Label4" runat="server" Text="Valor Unitário"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtValorUnitario" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right" class="style2">
            <asp:Label ID="Label5" runat="server" Text="Quantidade"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtQuantidade" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="center" class="style1" colspan="2">
            <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" Width="150px" />
&nbsp;&nbsp;
            <asp:Button ID="btnRetirar" runat="server" Text="Retirar" Width="150px" />
        </td>
    </tr>
    <tr>
        <td align="center" class="style1" colspan="2">
            <asp:GridView ID="grdItemMovimentacao" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="None" Width="567px" 
                EmptyDataText="Não existem produtos para a movimentação." 
                ShowHeaderWhenEmpty="True"
                onpageindexchanging="grdItemMovimentacao_PageIndexChanging" 
                onrowcreated="grdItemMovimentacao_RowCreated" 
                onrowdatabound="grdItemMovimentacao_RowDataBound" 
                onselectedindexchanged="grdItemMovimentacao_SelectedIndexChanged" 
                                >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Produto">
                        <ItemTemplate>
                            <asp:Label ID="lblProduto" runat="server" Text='<%# Eval("Produto.Descricao")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Quantidade" DataField="Quantidade" />
                    <asp:TemplateField HeaderText="Valor Total">
                        <ItemTemplate>
                            <asp:Label ID="lblProduto" runat="server" Text='<%# Eval("ValorTotal")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td align="right" class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" class="style1" colspan="2">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" Width="150px" />
            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" Width="150px" />
            <asp:Button ID="btnFechar" runat="server" Text="Fechar" Width="150px" 
                onclick="btnFechar_Click" />
        </td>
    </tr>
    <tr>
        <td align="right" class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="style2" colspan="2">
            <asp:Panel ID="pnlPesquisaAssociado" runat="server" CssClass="modal" BackColor="White">
                <table style="width:100%;">
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdPesquisa" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" CellPadding="4" CssClass="grid" ForeColor="#333333" 
                                GridLines="None" onpageindexchanging="grdPesquisa_PageIndexChanging" 
                                onrowcreated="grdPesquisa_RowCreated" onrowdatabound="grdPesquisa_RowDataBound" 
                                onselectedindexchanged="grdPesquisa_SelectedIndexChanged" PageSize="5" 
                                Width="471px">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="Identificador" HeaderText="Identificador" />
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                    <asp:BoundField DataField="Telefone" HeaderText="Telefone" />
                                    <asp:TemplateField HeaderText="Tipo do Associado">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" 
                                                Text='<%# Eval("TipoAssociado.Descricao")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnFecharPesquisa" runat="server" Text="Fechar" Width="176px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
</asp:Content>
