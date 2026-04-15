<%@ Page Language="C#" AutoEventWireup="true" Title="Autorizaciones" MasterPageFile="~/viewsGeneral/NestedMasterAprobador.master" CodeBehind="AutorizacionAprobador.aspx.cs" Inherits="sitemaCompras.viewsGeneral.AutorizacionAprobador" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link href="../Content/modalStyle.css" rel="stylesheet" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="HiddenField1" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeEditarRegistro" runat="server" PopupControlID="Panel1" TargetControlID="HiddenField1"
                CancelControlID="btnEditarClose" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField2" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeEliminarRegistro" runat="server" PopupControlID="Panel2" TargetControlID="HiddenField2"
                CancelControlID="btnEliminarClose" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="Panel1" runat="server" CssClass="modal-dialog embed-responsive" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Editar usuario</h4>
                        <asp:Button ID="btnEditarClose" OnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <h6 class="mt-2">Desea editar Permiso de aprobación al usuario?
                                            <div class="form-group">
                                                <asp:Label ID="Label2" runat="server" CssClass="mt-2" Text="Label">Nombre:</asp:Label>
                                                <asp:Label ID="txtEditNombre" runat="server"
                                                    ToolTip="Nombre  de Usuario" CssClass="form-control"></asp:Label>
                                                <asp:Label ID="Label1" runat="server" CssClass="mt-5 text-center" Text="Label">Autorización:</asp:Label>
                                                <%--<asp:CheckBoxList ID="checkTipoAut"
                                                    CellPadding="12"
                                                    CellSpacing="5"
                                                    CssClass="form-control w-auto h-auto p-4"
                                                    RepeatColumns="2"
                                                    RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow"
                                                    TextAlign="Right"
                                                    runat="server">
                                                    <asp:ListItem Value="1">No Autorizado</asp:ListItem>
                                                    <asp:ListItem Value="2">Autorizado</asp:ListItem>
                                                </asp:CheckBoxList>--%>
                                                <asp:DropDownList ID="dListTipoAut" CssClass="form-control" ToolTip="🏛️ Escoja la autorizacion a asignar" runat="server">
                                                    <asp:ListItem Text="No Autorizado" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Autorizado" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                                <asp:Button ID="btnEditar" class="btn btn-primary" runat="server" Text="Aceptar ✔" OnClick="btnEditar_Click" />
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
                        <h4 class="modal-title text-center">Eliminar usuario</h4>
                        <asp:Button ID="btnEliminarClose" OnClickOnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <asp:Label ID="lblElimiarReg" runat="server" Font-Size="30px" Text="Label">Desea Eliminar este usuario?</asp:Label>
                                            <div class="form-group">
                                                <asp:Label ID="Label7" runat="server" Text="Label">Id:</asp:Label>
                                                <asp:Label ID="lblDelId" runat="server" CssClass="form-control text-center" Text="Label"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Text="Label">Nombre:</asp:Label>
                                                <asp:Label ID="lblDeleteNombre" runat="server" CssClass="form-control text-center" Text="Label"></asp:Label>
                                                <asp:Label ID="Label9" runat="server" Text="Label">Dir. Adm:</asp:Label>
                                                <asp:Label ID="lblDelDirAdm" runat="server" CssClass="form-control text-center" Text="Label"></asp:Label>
                                            </div>
                                            <asp:Button ID="btnEliminar" class="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
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
                        <h5 class=" mr-3 text-center">Administración de autirización de usuarios</h5>
                    </div>
                    <div class="form-group embed-responsive m-1">
                        <asp:GridView ID="GridView1" CssClass="table table-striped table-responsive table-light table-bordered table table-condensed" AutoGenerateColumns="false" Font-Size="Small" CellSpacing="0" OnRowCommand="GridView1_RowCommand" runat="server" HorizontalAlign="Center">
                            <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle ForeColor="White" BackColor="Gray" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="idUsuaruios" ItemStyle-Width="15%" HeaderText="Id" />
                                <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                                
                                <asp:BoundField DataField="autorizacion" ItemStyle-Width="7%" HeaderText="Autorización" />
                                <asp:TemplateField HeaderText="Procesos" ItemStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" CommandName="editar" Text="🔏" CssClass="btn btn-link" ToolTip="Editar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>

            </asp:MultiView>
        </ContentTemplate>
        <%--<triggers>
            <asp:PostBackTrigger ControlID="checkTipoAut" />
        </triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
