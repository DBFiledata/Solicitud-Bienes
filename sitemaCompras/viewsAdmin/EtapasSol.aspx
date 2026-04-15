<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/viewsAdmin/adminUsers.Master" CodeBehind="EtapasSol.aspx.cs" Inherits="sitemaCompras.viewsAdmin.EtapasSol" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link href="../Content/modalStyle.css" rel="stylesheet" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="HiddenField1" runat="server" />
            <ajaxtoolkit:modalpopupextender id="mpeEditarRegistro" runat="server" popupcontrolid="Panel1" targetcontrolid="HiddenField1"
                cancelcontrolid="btnEditarClose" backgroundcssclass="modalBackground">
            </ajaxtoolkit:modalpopupextender>

            <asp:HiddenField ID="HiddenField2" runat="server" />
            <ajaxtoolkit:modalpopupextender id="mpeEliminarRegistro" runat="server" popupcontrolid="Panel2" targetcontrolid="HiddenField2"
                cancelcontrolid="btnEliminarClose" backgroundcssclass="modalBackground">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="Panel1" runat="server" CssClass="modal-dialog embed-responsive" DefaultButton="btnEditar" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Editar etapas de solicitudes</h4>
                        <asp:Button ID="btnEditarClose" OnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <h6>Desea editar la etapa de solicitud?
                                            <div class="form-group">
                                                <asp:Label ID="Label2" runat="server" Text="Label">Modifique el contenido y acepte para realizar el cambio</asp:Label>
                                                <asp:TextBox ID="txtRevision" runat="server"
                                                    ToolTip="📝 Ingrese la informacion de forma entendible &#10;Ingrese el nuevo nombre  que sea asignado al estado actual"
                                                    CssClass="form-control ml-2"></asp:TextBox>
                                            </div>
                                            <asp:Button ID="btnEditar" class="btn btn-primary" runat="server" Text="Aceptar ✔" OnClick="btnEditar_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel2" runat="server" CssClass="modal-dialog embed-responsive" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Eliminar etapas de solicitudes</h4>
                        <asp:Button ID="btnEliminarClose" OnClickOnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <h6>Desea Eliminar esta etapa de solicitud?</h6>
                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server" Text="Label">Nombre:</asp:Label>
                                                <asp:Label ID="lblNombreEliminar" runat="server" CssClass="form-control text-center" Text="Label"></asp:Label>
                                            </div>
                                            <asp:Button ID="btnEliminar" class="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminar_Click"/> <%--OnClick="btnConfirmarEliminarRegistro_Click"--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="viewTabla" runat="server">
                    <div class="card-header bg-light embed-responsive pt-3 pb-3">
                        <h5 class="text-center mr-3">Administracion de etapas de solicitudes</h5>
                        <asp:Button ID="btnAddREg" ToolTip="Agregar" runat="server" Text="+" class="btn close mr-2" Style="margin-top: -36px; background-color: #058c24" OnClick="btnAddREg_Click" />
                    </div>
                    <div class="card-body mt-n2">
                        <div class="form-group table-responsive">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" CssClass="table table-striped table-light table-bordered table table-condensed" AutoGenerateColumns="false" Font-Size="Small" Width="99%" CellSpacing="0" OnRowCommand="GridView1_RowCommand" runat="server" HorizontalAlign="Center">
                                    <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle ForeColor="White" BackColor="Gray" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:BoundField DataField="id" ItemStyle-Width="25%" HeaderText="Id" />
                                        <asp:BoundField DataField="nombre" HeaderText="Etapa" />
                                        <asp:TemplateField HeaderText="Procesos" ItemStyle-Width="25%">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" CommandName="editar" Text="📝" CssClass="btn btn-link" ToolTip="Editar" runat="server" />
                                                <asp:Button ID="Button2" CommandName="eliminar" Text="❌" CssClass="btn btn-link" ToolTip="eliminar" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:View>

                <asp:View ID="viewRegistro" runat="server">
                    <div class="container py-1 pb-2 col-8">
                        <div class="d-flex align-items-center justify-content-center">
                            <div class="col-md-12  bg-dark mt-4 pb-5 pt-5 rounded">
                                <div class="row mx-4">
                                    <div class="col-md-12 mx-auto">
                                        <div class="card border-secondary">
                                            <div class="card-header">
                                                <h3 class=" text-center">Registro de etapas de solicitudes</h3>
                                            </div>
                                            <div class="card-body">
                                                <div class="form-group d-flex align-items-center justify-content-center">
                                                    <div class="form-group col-12">
                                                        <asp:Label ID="Label7" runat="server" Text="Label">Nombre de etapas</asp:Label>
                                                        <asp:TextBox ID="txtId" CssClass="form-control"
                                                            ToolTip="Introdusca el nombre del nuevo estatus &#10;"
                                                            placeholder="🏛️" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group d-flex align-items-center justify-content-center col-12">
                                                    <asp:Button ID="btnRegistrar" CssClass="btn btn-success btn-lg float-right mr-1 embed-responsive" runat="server" Text="Confirmar ✔" OnClick="btnRegistrar_Click" />
                                                    <asp:Button ID="btnCancelar" CssClass="btn btn-danger btn-lg float-right ml-1 embed-responsive" runat="server" Text="Cancelar ❌" OnClick="btnCancelar_Click" CausesValidation="False" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

