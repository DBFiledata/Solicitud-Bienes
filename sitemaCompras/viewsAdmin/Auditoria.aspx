<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/viewsAdmin/adminUsers.Master" CodeBehind="Auditoria.aspx.cs" Inherits="sitemaCompras.viewsAdmin.Auditoria" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link href="../Content/modalStyle.css" rel="stylesheet" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card-header bg-light embed-responsive pt-3 pb-3">
                <h5 class="text-center">Logs de Auditoría del Sistema</h5>
            </div>

            <div class="card-body mt-n2">

                <!-- Filtros -->
                <div class="card border-secondary mb-3">
                    <div class="card-body">
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <asp:Label ID="Label1" runat="server" Text="Usuario"></asp:Label>
                                <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"
                                    placeholder="ID de usuario"
                                    ToolTip="Filtra por el ID de usuario (dejar vacío = todos)"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-2">
                                <asp:Label ID="Label2" runat="server" Text="N° Solicitud"></asp:Label>
                                <asp:TextBox ID="txtSolicitud" CssClass="form-control" runat="server"
                                    placeholder="Id"
                                    ToolTip="Filtra por el número de solicitud (dejar vacío = todas)"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3">
                                <asp:Label ID="Label3" runat="server" Text="Tipo de acción"></asp:Label>
                                <asp:DropDownList ID="dlAccion" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="form-group col-md-2">
                                <asp:Label ID="Label4" runat="server" Text="Desde"></asp:Label>
                                <asp:TextBox ID="txtDesde" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-2">
                                <asp:Label ID="Label5" runat="server" Text="Hasta"></asp:Label>
                                <asp:TextBox ID="txtHasta" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-12 text-right">
                                <asp:Button ID="btnFiltrar" CssClass="btn btn-primary" runat="server" Text="Filtrar 🔍" OnClick="btnFiltrar_Click" />
                                <asp:Button ID="btnLimpiar" CssClass="btn btn-secondary" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" CausesValidation="False" />
                            </div>
                        </div>
                    </div>
                </div>

                <asp:Label ID="lblResumen" runat="server" CssClass="text-muted small"></asp:Label>

                <!-- Resultados -->
                <div class="form-group table-responsive">
                    <asp:GridView ID="GridView1" runat="server"
                        CssClass="table table-striped table-light table-bordered table-condensed"
                        AutoGenerateColumns="false" Font-Size="Small" Width="99%" CellSpacing="0"
                        HorizontalAlign="Center" AllowPaging="true" PageSize="25"
                        OnPageIndexChanging="GridView1_PageIndexChanging"
                        EmptyDataText="No se encontraron registros para los filtros indicados.">
                        <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle ForeColor="White" BackColor="Gray" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <PagerStyle HorizontalAlign="Center" BackColor="#f8f9fa" />
                        <Columns>
                            <asp:BoundField DataField="fechaHora" HeaderText="Fecha / Hora" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="false" />
                            <asp:BoundField DataField="nombreUsuario" HeaderText="Usuario" />
                            <asp:BoundField DataField="idUsuario" HeaderText="ID" />
                            <asp:BoundField DataField="accionDescripcion" HeaderText="Acción" />
                            <asp:BoundField DataField="idEntidad" HeaderText="Solicitud" />
                            <asp:BoundField DataField="etapaOrigen" HeaderText="Etapa orig." />
                            <asp:BoundField DataField="etapaDestino" HeaderText="Etapa dest." />
                            <asp:BoundField DataField="detalle" HeaderText="Detalle" />
                            <asp:BoundField DataField="ip" HeaderText="IP" />
                            <asp:BoundField DataField="resultado" HeaderText="Resultado" />
                        </Columns>
                    </asp:GridView>
                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
