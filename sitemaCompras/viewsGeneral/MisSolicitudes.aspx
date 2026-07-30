<%@ Page Language="C#" AutoEventWireup="true" Title="Mis Solicitudes" MasterPageFile="~/viewsGeneral/NestedMasterSolicitantes.master" CodeBehind="MisSolicitudes.aspx.cs" Inherits="sitemaCompras.viewsGeneral.MisSolicitudes" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <div class="card-body col-12 bg-light">
        <div class="col-12 border border-dark form-group mt-2">
            <div class="card-header bg-light h1">
                <h4>Mis Solicitudes</h4>
                <small class="text-muted">
                    Borradores guardados que aún no ha enviado, y solicitudes devueltas para corregir.
                    Al enviarlas pasan al Aprobador Interno y dejan de aparecer en esta lista.
                </small>
            </div>

            <div class="card-body col-12">
                <div class="form-group d-flex align-items-end">
                    <div class="col-2">
                        <asp:Label ID="lblId" runat="server" Text="Id" />
                        <asp:TextBox ID="txtId" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-6">
                        <asp:Label ID="lblTitulo" runat="server" Text="Título" />
                        <asp:TextBox ID="txtTituloF" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-2">
                        <asp:Button ID="btnFiltrar" runat="server" Text="Buscar 🔎" CssClass="btn btn-primary form-control" OnClick="btnFiltrar_Click" />
                    </div>
                    <div class="col-2">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary form-control" OnClick="btnLimpiar_Click" />
                    </div>
                </div>

                <div class="form-group mt-3">
                    <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand"
                        CssClass="table table-striped table-light table-bordered table-condensed" AutoGenerateColumns="false"
                        Font-Size="Small" Width="99%" CellSpacing="0" HorizontalAlign="Center"
                        EmptyDataText="No tiene solicitudes pendientes de enviar ni devueltas para corregir.">
                        <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle ForeColor="White" BackColor="Gray" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundField DataField="#" ItemStyle-Width="6%" HeaderText="Id" />
                            <asp:BoundField DataField="SB" ItemStyle-Width="6%" HeaderText="SB" />
                            <asp:BoundField DataField="Titulo" ItemStyle-Width="46%" HeaderText="Título" />
                            <asp:BoundField DataField="F-Sol" ItemStyle-Width="12%" HeaderText="Fecha" />
                            <asp:BoundField DataField="estatus" ItemStyle-Width="18%" HeaderText="Estatus" />
                            <asp:TemplateField HeaderText="Acción" ItemStyle-Width="12%">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditar" runat="server" Text="Editar ✏️" CssClass="btn btn-warning btn-sm"
                                        CommandName="editarSol" ToolTip="Continuar editando esta solicitud" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
