<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/viewsGeneral/NestedMasterAprobador.master" Title="Perfil" CodeBehind="Perfil.aspx.cs" Inherits="sitemaCompras.viewsGeneral.Perfil" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container py-1 pb-2 ">
                <div class="d-flex align-items-center justify-content-center">
                    <div class="col-md-12  bg-dark mt-4 pb-5 pt-5 rounded">
                        <div class="row">
                            <div class="col-md-12 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-header">
                                        <h3 class=" text-center">Perfil</h3>
                                    </div>
                                    <div class="card-body">
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <div class="form-group col-4">
                                                <asp:Label ID="Label7" runat="server" Text="Label">Id</asp:Label>
                                                <asp:Label ID="txtId" CssClass="form-control" placeholder="📇" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-4">
                                                <asp:Label ID="Label1" runat="server" Text="Label">Nombre</asp:Label>
                                                <asp:Label ID="txtNombre" CssClass="form-control" placeholder="📝" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-4">
                                                <asp:Label ID="Label2" runat="server" Text="Label">Apellido</asp:Label>
                                                <asp:Label ID="txtApellido" CssClass="form-control" placeholder="📝" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <div class="form-group col-6">
                                                <asp:Label ID="Label9" runat="server" Text="Label">telefono</asp:Label>
                                                <asp:Label ID="txtTelefono" CssClass="form-control pl-3" placeholder=" 📞 " runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-6">
                                                <asp:Label ID="Label3" runat="server" Text="Label">Email</asp:Label>
                                                <asp:Label ID="txtEmail" CssClass="form-control" placeholder="📧" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <div class="form-group col-6">
                                                <asp:Label ID="Label4" runat="server" Text="Label">Unidad Administrativa</asp:Label>
                                            <asp:Label ID="txtUnidad" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                            <div class="form-group col-6">
                                                <asp:Label ID="Label6" runat="server" Text="Label">Tipo de usuario</asp:Label>
                                            <asp:Label ID="txtTipo" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

