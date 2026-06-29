<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/viewsAdmin/adminUsers.Master" CodeBehind="Users.aspx.cs" Inherits="sitemaCompras.viewsAdmin.Users" %>

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
                                            <h6 class="mt-2">Desea editar el usuario?
                                            <div class="form-group">
                                                <asp:Label ID="Label15" runat="server" CssClass="mt-2" Text="Label">Id:</asp:Label>
                                                <asp:TextBox ID="txtEditId" runat="server"
                                                    ToolTip="📝 Ingrese la informacion de forma entendible &#10;Ingrese el nuevo Id  que sera asignado al Usuario" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label4" runat="server" CssClass="mt-2" Text="Label">Password:</asp:Label>
                                                <asp:TextBox ID="txtEditPassword" runat="server"
                                                    ToolTip="📝 Ingrese la informacion de forma entendible &#10;Ingrese el nuevo Password  que sera asignado al Usuario" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label2" runat="server" CssClass="mt-2" Text="Label">Nombre:</asp:Label>
                                                <asp:TextBox ID="txtEditNombre" runat="server"
                                                    ToolTip="📝 Ingrese la informacion de forma entendible &#10;Ingrese el nuevo Nombre  que sera asignado al Usuario" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label1" runat="server" CssClass="mt-2" Text="Label">Apellido:</asp:Label>
                                                <asp:TextBox ID="txtEditApellido" runat="server"
                                                    ToolTip="📝 Ingrese la informacion de forma entendible &#10;Ingrese el nuevo Apellido  que sera asignado al Usuario" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label18" runat="server" CssClass="mt-2" Text="Label">Telefono:</asp:Label>
                                                <asp:TextBox ID="txtEditTelefono" runat="server" TextMode="Phone"
                                                    ToolTip="📝 Ingrese la informacion de forma entendible &#10;Ingrese el nuevo Telefono  que sera asignado al Usuario" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label19" runat="server" CssClass="mt-2" Text="Label">Email:</asp:Label>
                                                <asp:TextBox ID="txtEditEmail" runat="server" TextMode="Email"
                                                    ToolTip="📝 Ingrese la informacion de forma entendible &#10;Ingrese el nuevo Email que sera asignado al Usuario" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label5" runat="server" CssClass="mt-2" Text="Label">Unidad Administrativa:</asp:Label>
                                                <asp:DropDownList ID="dlistEditUnidad" DataValueField="id" DataTextField="nombre" CssClass="form-control" ToolTip="🏛️ Escoja la Unidad Adm. de el usuario" runat="server">
                                                </asp:DropDownList>
                                                <asp:Label ID="Label16" runat="server" CssClass="mt-2" Text="Label">Tipo de usuario:</asp:Label>
                                                <asp:DropDownList ID="dListEditTipo" DataValueField="id" DataTextField="nombre" CssClass="form-control" ToolTip="🏛️ Escoja el tipo de usuario" runat="server">
                                                </asp:DropDownList>
                                                <asp:Label ID="Label17" runat="server" CssClass="mt-2" Text="Label">Estado de usuario:</asp:Label>
                                                <asp:DropDownList ID="dListEditEstado" DataValueField="id" DataTextField="nombre" CssClass="form-control" ToolTip="🏛️ Escoja el estado de el usuario" runat="server">
                                                </asp:DropDownList>
                                                <asp:Label ID="LabelEditRolesF" runat="server" CssClass="mt-2" Text="Label">Rol del flujo:</asp:Label>
                                                <asp:DropDownList ID="dListEditRolesF" DataValueField="id" DataTextField="nombre" CssClass="form-control" ToolTip="🔄 Escoja el rol del flujo" runat="server">
                                                </asp:DropDownList>
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
                                            <asp:Button ID="btnEliminar" class="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminar_Click"/>
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
                        <h5 class=" mr-3 text-center">Administracion de usuarios</h5>
                        <asp:Button ID="btnAddREg" ToolTip="Agregar" runat="server" Text="+" class="btn close mr-2" Style="margin-top: -36px; background-color: #058c24" OnClick="btnAddREg_Click" />
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
                                <asp:BoundField DataField="telefonos" ItemStyle-Wrap="false" HeaderText="Telefono" />
                                <asp:BoundField DataField="emails" ItemStyle-Wrap="false" HeaderText="Email" />
                                <asp:BoundField DataField="unidad" HeaderText="Unidad" />
                                <asp:BoundField DataField="tipo" ItemStyle-Width="9%" HeaderText="Tipo" />
                                <asp:BoundField DataField="estatus" ItemStyle-Width="7%" HeaderText="Estado" />
                                <asp:TemplateField HeaderText="Procesos" ItemStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" CommandName="editar" Text="📝" CssClass="btn btn-link" ToolTip="Editar" runat="server" />
                                        <asp:Button ID="Button2" CommandName="eliminar" Text="❌" CssClass="btn btn-link" ToolTip="eliminar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>

                <asp:View ID="viewRegistro" runat="server">
                    <div class="container p-1">
                        <div class="d-flex align-items-center justify-content-center">
                            <div class="col-md-  bg-dark mt-2 p-4 rounded">
                                <div class="row">
                                    <div class="col-md-12 mx-auto">
                                        <div class="card border-secondary">
                                            <div class="card-header">
                                                <h3 class=" text-center">Registro de usuarios</h3>
                                            </div>
                                            <div class="card-body">
                                                <div class="form-group d-flex align-items-center justify-content-center">
                                                    <div class="form-group col-6">
                                                        <asp:Label ID="Label" runat="server" Text="Label">Id</asp:Label>
                                                        <asp:TextBox ID="txtId" CssClass="form-control"
                                                            ToolTip="Introdusca el ID (cedula) &#10;"
                                                            placeholder="📇" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-6">
                                                        <asp:Label ID="Label20" runat="server" Text="Label">Password</asp:Label>
                                                        <asp:TextBox ID="txtpass" CssClass="form-control" TextMode="Password"
                                                            ToolTip="Introdusca el Password &#10;"
                                                            placeholder="🔒" runat="server"></asp:TextBox>
                                                    </div>
                                                    
                                                </div>

                                                <div class="form-group d-flex align-items-center justify-content-center">
                                                    <div class="form-group col-6">
                                                        <asp:Label ID="Label6" runat="server" Text="Label">Nombre</asp:Label>
                                                        <asp:TextBox ID="txtRegistroNombre" CssClass="form-control" placeholder="📝" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-6">
                                                        <asp:Label ID="Label8" runat="server" Text="Label">Apellido</asp:Label>
                                                        <asp:TextBox ID="txtRegistroApellido" CssClass="form-control" placeholder="📝" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group d-flex align-items-center justify-content-center">
                                                    <div class="form-group col-6">
                                                        <asp:Label ID="Label10" runat="server" Text="Label">telefono</asp:Label>
                                                        <asp:TextBox ID="txtRegistrarTelefono" CssClass="form-control pl-3"
                                                            ToolTip="Introdusca el ID o codigo de pocicion &#10;se encuentra en el carnet devajo del nombre"
                                                            placeholder=" 📞 " runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-6">
                                                        <asp:Label ID="Label11" runat="server" Text="Label">Email</asp:Label>
                                                        <asp:TextBox ID="txtRegistrarEmail" CssClass="form-control" ToolTip="Introdusca su email"
                                                            placeholder="📧" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group pl-3">
                                                    <asp:Label ID="Label12" runat="server" Text="Label">Unidad Administrativa</asp:Label>
                                                    <asp:DropDownList ID="dListUnidad" DataValueField="id" DataTextField="nombre"  CssClass="form-control" ToolTip="🏛️ Escoja la unidad administrativa" runat="server" Width="98%">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group pl-3">
                                                    <asp:Label ID="Label13" runat="server" Text="Label">Tipo de usuario</asp:Label>
                                                    <asp:DropDownList ID="dListTipo" DataValueField="id" DataTextField="nombre"  CssClass="form-control" ToolTip="👤 Escoja el tipo de usuario" runat="server" Width="98%">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group pl-3">
                                                    <asp:Label ID="Label14" runat="server" Text="Label">Estado de usuario</asp:Label>
                                                    <asp:DropDownList ID="dListEstado" DataValueField="id" DataTextField="nombre"  CssClass="form-control" ToolTip="👤 Escoja el tipo de usuario" runat="server" Width="98%">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group pl-3">
                                                    <asp:Label ID="LabelRolesF" runat="server" Text="Label">Rol del flujo</asp:Label>
                                                    <asp:DropDownList ID="dListRolesF" DataValueField="id" DataTextField="nombre" CssClass="form-control" ToolTip="🔄 Escoja el rol del flujo" runat="server" Width="98%">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group d-flex align-items-center justify-content-center col-12">
                                                    <asp:Button ID="btnRegistrar" CssClass="btn btn-success btn-lg float-right mr-1 embed-responsive" runat="server" Text="Confirmar ✔" OnClick="btnRegistrar_Click" />
                                                    <asp:Button ID="btnCancelar" CssClass="btn btn-danger btn-lg float-right ml-1 embed-responsive"  runat="server" Text="Cancelar ❌" OnClick="btnCancelar_Click" CausesValidation="False" />
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
