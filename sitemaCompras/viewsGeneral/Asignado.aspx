<%@ Page Language="C#" AutoEventWireup="true" Title="Asignadas" MasterPageFile="~/viewsGeneral/NestedMasterCotizador.master" CodeBehind="Asignado.aspx.cs" Inherits="sitemaCompras.viewsGeneral.Asignado" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--<script type="text/javascript">
        function scrollToBottom() {
            var panel = document.getElementById('<%= panelChat.ClientID %>');
            panel.scrollTop = panel.scrollHeight;
        }
    </script>--%>
    <link href="../Content/modalStyle.css" rel="stylesheet" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeEditarArchivo" runat="server" PopupControlID="panelEditar" TargetControlID="HiddenField1"
                CancelControlID="btnEditarClose" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField2" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeEliminarArchivo" runat="server" PopupControlID="panelEliminarArchivo" TargetControlID="HiddenField2"
                CancelControlID="btnEliminarArchivoClose" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField3" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mppeRegistrarArchivo" runat="server" PopupControlID="panelRegistrarArchivo" TargetControlID="HiddenField3"
                CancelControlID="btnRegistrarArchivoClose" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField4" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeEliminarRenglon" runat="server" PopupControlID="panelEliminarRenglon" TargetControlID="HiddenField4"
                CancelControlID="btnEliminarRenglonClose" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField5" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeEditRenglon" runat="server" PopupControlID="panelEditarRenglon" TargetControlID="HiddenField5"
                CancelControlID="btnEditarRenglonClose" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField6" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeEliminarSolicitud" runat="server" PopupControlID="panelEliminarSolicitud" TargetControlID="HiddenField6"
                CancelControlID="btnEliminarSolicitudSolicitud" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField7" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeEstatusEspeciales" runat="server" PopupControlID="panelEstatusEspeciales" TargetControlID="HiddenField7"
                CancelControlID="btnCloseEstatusEspeciales" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField8" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeEtapas" runat="server" PopupControlID="panelEtapa" TargetControlID="HiddenField8"
                CancelControlID="btnCloseEtapa" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField9" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeComfirmarProceso" runat="server" PopupControlID="panelConfirmarProceso" TargetControlID="HiddenField9"
                CancelControlID="closeConfirmarProceso" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField10" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeCrearProveedor" runat="server" PopupControlID="panelCrearProveedor" TargetControlID="HiddenField10"
                CancelControlID="closeCrearProveedor" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField11" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeCotizadores" runat="server" PopupControlID="panelAsignarCotizador" TargetControlID="HiddenField11"
                CancelControlID="closeAsignarCotizador" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField12" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeConfirmarCotizador" runat="server" PopupControlID="panelConfirmarCotizador" TargetControlID="HiddenField12"
                CancelControlID="closeConfirmarCotizador" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField13" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeConfirmarAtencion" runat="server" PopupControlID="panelConfirmar" TargetControlID="HiddenField13"
                CancelControlID="closeConfirmarAtencion" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField14" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeRechazarAtencion" runat="server" PopupControlID="PanleRechazarAtencion" TargetControlID="HiddenField14"
                CancelControlID="closeRechazarAtencion" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanleRechazarAtencion" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Rechazar Asignacion</h4>
                        <asp:Button ID="closeRechazarAtencion" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-2">
                        <div id="">
                            <div class="row">
                                <div class="col-md-12 mx-auto">
                                    <div class="card border-secondary">
                                        <div class="card-body mt-n3">
                                            <asp:Panel ID="panel9" runat="server" Visible="true">
                                                <div class="form-group">
                                                    <asp:Label ID="Label35" runat="server" Text="Label">Rechazar la atencion del siguente registro: </asp:Label>
                                                    <asp:Label ID="lblRechazarAtencionReg" runat="server" Text="Label"></asp:Label>
                                                </div>
                                            </asp:Panel>
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <asp:Button ID="btnRechazarAtencion" OnClick="btnRechazar_Click" class="btn btn-danger form-control" runat="server" Text="Rechazar ✖️" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelConfirmar" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Confirmar Atencion</h4>
                        <asp:Button ID="closeConfirmarAtencion" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-2">
                        <div id="">
                            <div class="row">
                                <div class="col-md-12 mx-auto">
                                    <div class="card border-secondary">
                                        <div class="card-body mt-n3">
                                            <asp:Panel ID="panel8" runat="server" Visible="true">
                                                <div class="form-group">
                                                    <asp:Label ID="Label32" runat="server" Text="Label">Confirmar la atencion del siguente registro: </asp:Label>
                                                    <asp:Label ID="lblConfirmarAtencionReg" runat="server" Text="Label"></asp:Label>
                                                </div>
                                            </asp:Panel>
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <asp:Button ID="btnConfirmarAtencion" OnClick="btnConfirmar_Click" class="btn btn-primary form-control" runat="server" Text="Confirmar ✔️" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelAsignarCotizador" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Cotizadores</h4>
                        <asp:Button ID="closeAsignarCotizador" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-n3">
                        <div id="">
                            <div class="row">
                                <div class="col-md-12 mx-auto">
                                    <div class="card border-secondary">
                                        <div class="card-body mt-2">
                                            <asp:Panel ID="panelListaCotizadores" runat="server" Visible="true">
                                                <%-- botones generados de la lista de cotizadores --%>

                                                <asp:DropDownList ID="dlistCotizadores" DataValueField="id" DataTextField="nombres" CssClass="form-control" ToolTip="Escoja el cotizador" runat="server">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <asp:Button ID="Button4" OnClick="btnConfirmarCotizador_Click" class="btn btn-primary form-control" runat="server" Text="Confirmar" />
                                            </div>
                                        </div>
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Label ID="Label34" runat="server" Text="" Visiblefalse="false" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelConfirmarCotizador" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Confirmar cotizador</h4>
                        <asp:Button ID="closeConfirmarCotizador" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-2">
                        <div id="">
                            <div class="row">
                                <div class="col-md-12 mx-auto">
                                    <div class="card border-secondary">
                                        <div class="card-body mt-n3">
                                            <asp:Panel ID="panel4" runat="server" Visible="true">
                                                <div class="form-group">
                                                    <asp:Label ID="Label36" runat="server" Text="Label">Cotizador Seleccionado:</asp:Label>
                                                    <asp:Label ID="lblCotizadorSelect" runat="server" Text="Label"></asp:Label>
                                                </div>
                                            </asp:Panel>
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <asp:Button ID="btnConfirmarCotizador" OnClick="btnConfirmarCotizador_Click" class="btn btn-primary form-control" runat="server" Text="Confirmar" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel ID="panelCrearProveedor" runat="server" CssClass="modal-dialog embed-responsive" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Crear Proveedor</h4>
                        <asp:Button ID="closeCrearProveedor" OnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <div class="form-group">
                                                <asp:Label ID="Label40" runat="server" Text="Nombre:"></asp:Label>
                                                <asp:TextBox ID="txtNombreProveedorCrear" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:Label ID="Label41" runat="server" Text="Correo:"></asp:Label>
                                                <asp:TextBox ID="txtCorreoProveedorCrear" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                                                <asp:Label ID="Label42" runat="server" Text="Telefono:"></asp:Label>
                                                <asp:TextBox ID="txtTelefonoProveedorCrear" CssClass="form-control" TextMode="Phone" runat="server"></asp:TextBox>

                                            </div>
<%--                                            <asp:Button ID="btnCrearProveedor" class="btn btn-primary" runat="server" Text="Registrar ✔" OnClick="btnCrearProveedor_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelRegistrarArchivo" runat="server" CssClass="modal-dialog embed-responsive" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Registrar Archivo</h4>
                        <asp:Button ID="btnRegistrarArchivoClose" OnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <div class="form-group">
                                                <asp:Label ID="Label20" runat="server" CssClass="mt-2" Text="Label">Archivo:</asp:Label>
                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                                <asp:Label ID="Label18" runat="server" CssClass="mt-2" Text="Label">Nombre del archivo:</asp:Label>
                                                <asp:TextBox ID="txtNombreArchivo" runat="server"
                                                    ToolTip="📝 nombre que sera asignado al archivo" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <asp:Button ID="btnRegistrarArchivo" class="btn btn-primary" runat="server" Text="Aceptar ✔" OnClick="btnRegistrarArchivo_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelEditarRenglon" runat="server" CssClass="modal-dialog embed-responsive" align="center" Style="display: none; min-width: 1125px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Editar Renglón</h4>
                        <asp:Button ID="btnEditarRenglonClose" OnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="d-flex align-items-center justify-content-center mt-1 col-12">
                        <div class="form-group col-1">
                            <asp:Label ID="Label8" runat="server" Text="Renglon:"></asp:Label>
                            <asp:TextBox ID="txtRenglonEdit" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-1">
                            <asp:Label ID="Label19" runat="server" Text="Cantidad:"></asp:Label>
                            <asp:TextBox ID="txtCantidadEdit" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-1">
                            <asp:Label ID="Label21" runat="server" Text="Unidad:"></asp:Label>
                            <asp:TextBox ID="txtUnidadContentEdit" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-4">
                            <asp:Label ID="Label22" runat="server" Text="Descripcion:"></asp:Label>
                            <asp:TextBox ID="txtDecsContentEdit" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-2">
                            <asp:Label ID="Label23" runat="server" Text="Costo unitario:"></asp:Label>
                             <asp:TextBox ID="txtCostoEdit" CssClass="form-control" step="any" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-1">
                            <asp:Button ID="btnActualizarRenglon" ToolTip="Actualizar" runat="server" Text="✔️" ForeColor="Black" Font-Size="Large" class="btn btn-success btn-outline-danger mt-4" OnClick="btnActualizarRenglon_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelEditar" runat="server" CssClass="modal-dialog embed-responsive" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Editar Archivo</h4>
                        <asp:Button ID="btnEditarClose" OnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <h6 class="mt-2">Desea editar el archivo?
                                            <div class="form-group">
                                                <asp:Label ID="Label14" runat="server" CssClass="mt-2" Text="Label">Nombre:</asp:Label>
                                                <asp:TextBox ID="txtEditNombre" runat="server"
                                                    ToolTip="📝 Ingrese la informacion de forma entendible &#10;Ingrese el nuevo nombre  que sera asignado al Dir. Adm." CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label15" runat="server" CssClass="mt-2" Text="Label">Apellido:</asp:Label>
                                                <asp:TextBox ID="txtEditApellido" runat="server"
                                                    ToolTip="📝 Ingrese la informacion de forma entendible &#10;Ingrese el nuevo apellido que sera asignado al Dir. Adm." CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label16" runat="server" CssClass="mt-2" Text="Label">Unidad Administrativa</asp:Label>
                                                <asp:DropDownList ID="dListEditEstado" CssClass="form-control" ToolTip="🏛️ Escoja el estado de el Dir. Adm" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                                <asp:Button ID="btnEditar" class="btn btn-primary" runat="server" Text="Aceptar ✔" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelEliminarArchivo" runat="server" CssClass="modal-dialog embed-responsive" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Eliminar Archivo</h4>
                        <asp:Button ID="btnEliminarArchivoClose" OnClickOnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <asp:Label ID="lblElimiarReg" runat="server" Font-Size="30px" Text="Label">Desea Eliminar este archivo?</asp:Label>
                                            <div class="form-group">
                                                <asp:Label ID="Label17" runat="server" Text="Label">Nombre:</asp:Label>
                                                <asp:Label ID="lblDeleteNombreArchivo" runat="server" CssClass="form-control text-center" Text="Label"></asp:Label>
                                            </div>
                                            <asp:Button ID="btnEliminarArchivo" class="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminarArchivo_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelEliminarRenglon" runat="server" CssClass="modal-dialog embed-responsive" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Eliminar Renglon</h4>
                        <asp:Button ID="btnEliminarRenglonClose" OnClickOnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <asp:Label ID="Label5" runat="server" Font-Size="30px" Text="Label">Desea Eliminar este renglon?</asp:Label>
                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" Text="Label">#:</asp:Label>
                                                <asp:Label ID="lblRenglon" runat="server" CssClass="form-control text-center" Text="Label"></asp:Label>
                                            </div>
                                            <asp:Button ID="btnEliminarRenglon" class="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminarRenglon_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelEliminarSolicitud" runat="server" CssClass="modal-dialog embed-responsive" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Eliminar Solicitud</h4>
                        <asp:Button ID="btnEliminarSolicitudSolicitud" OnClickOnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <asp:Label ID="Label27" runat="server" Font-Size="30px" Text="Label">Desea Eliminar esta solicitud?</asp:Label>
                                            <div class="form-group">
                                                <asp:Label ID="Label28" runat="server" Text="Label">#:</asp:Label>
                                                <asp:Label ID="lblEliminarSolicitud" runat="server" CssClass="form-control text-center" Text="Label"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label29" runat="server" Text="Label">Titulo:</asp:Label>
                                                <asp:Label ID="lblTituloEliminar" runat="server" CssClass="form-control text-center" Text="Label"></asp:Label>
                                            </div>
                                            <asp:Button ID="btnEliminarSolicitud" class="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminarSolicitud_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelEstatusEspeciales" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Estatus Especiales</h4>
                        <asp:Button ID="btnCloseEstatusEspeciales" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-n3">
                        <div id="">
                            <div class="row">
                                <div class="col-md-12 mx-auto">
                                    <div class="card border-secondary">
                                        <div class="card-body mt-2">
                                            <asp:Panel ID="panel6" runat="server" Visible="true">
                                                <div class="form-group">
                                                    <asp:Button ID="btnEDocUnidad" CommandArgument="8" OnCommand="Estatus_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="E. Doc. Unidad" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnEDocProveedor" CommandArgument="9" OnCommand="Estatus_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="E. Doc. Proveedor" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnPrecioR" CommandArgument="10" OnCommand="Estatus_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Precio Referencia" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnCotizacionL" CommandArgument="11" OnCommand="Estatus_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Cotizacion en Línea" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnBloqueo" CommandArgument="12" OnCommand="Estatus_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Bloqueo" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnRechazado" CommandArgument="7" OnCommand="Estatus_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Rechazado" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="Button5" CommandArgument="17" OnCommand="Estatus_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Reserva de partida" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="Button6" CommandArgument="18" OnCommand="Estatus_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Anulado" />
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Label ID="Label30" runat="server" Text="" Visiblefalse="false" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel ID="panelEtapa" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Siguiente</h4>
                        <asp:Button ID="btnCloseEtapa" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-2">
                        <div id="">
                            <div class="row">
                                <div class="col-md-12 mx-auto">
                                    <div class="card border-secondary">
                                        <div class="card-body mt-1">
                                            <asp:Panel ID="panel5" runat="server" Visible="true">
                                                <div class="form-group">
                                                    <asp:Button ID="btnUnidad" CommandArgument="1" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Unidad" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnDirAdm" CommandArgument="2" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Dirrección Adm." />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnpresupuesto" CommandArgument="3" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Presupuesto" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnLegal" CommandArgument="4" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Legal" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnSap" CommandArgument="5" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="SAP" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnControlF" CommandArgument="6" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Control físcal" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnPresidencia" CommandArgument="7" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Presidencia" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnVPresidencia" CommandArgument="10" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Vice presidencia" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnSecretaria" CommandArgument="11" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Secretaria general" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnCompras" CommandArgument="9" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Compras" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnContrataciones" CommandArgument="12" OnCommand="Etapa_Command"
                                                        CssClass="btn btn-dark form-control text-center" runat="server" Text="Contrataciones" />
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Label ID="Label31" runat="server" Text="" Visiblefalse="false" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelConfirmarProceso" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Confirmar</h4>
                        <asp:Button ID="closeConfirmarProceso" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-2">
                        <div id="">
                            <div class="row">
                                <div class="col-md-12 mx-auto">
                                    <div class="card border-secondary">
                                        <div class="card-body mt-n3">
                                            <asp:Panel ID="panel7" runat="server" Visible="true">
                                                <div class="form-group">
                                                    <asp:Label ID="Label33" runat="server" Text="Label">Comentario:</asp:Label>
                                                    <asp:TextBox ID="txtComentario" CssClass="form-control ml-3" TextMode="MultiLine" Width="94%" Height="75px" runat="server"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <asp:Button ID="btnConfirmarPorceso" OnClick="btnConfirmarPorceso_Click" class="btn btn-primary form-control" runat="server" Text="Confirmar" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="ViewTabla" runat="server">
                    <div class="table-responsive">
                        <div class="card-header bg-light embed-responsive pt-3 pb-3">
                            <h5 class="text-center mr-3">Solicitudes asignadas</h5>
                        </div>
                        <div class="card-body mt-n2">
                            <div class="form-group table-responsive">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"
                                        CssClass="table table-striped table-light table-bordered table table-condensed" AutoGenerateColumns="false" Font-Size="Small" Width="99%" CellSpacing="0" HorizontalAlign="Center">
                                        <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle ForeColor="White" BackColor="Gray" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>

                                            <asp:BoundField DataField="#" ItemStyle-Width="5%" HeaderText="Id" />
                                            <asp:BoundField DataField="SB" ItemStyle-Width="5%" HeaderText="SB" />
                                            <asp:BoundField DataField="Titulo" ItemStyle-Width="25%" HeaderText="Titulo" />
                                            <asp:BoundField DataField="F-Sol" ItemStyle-Width="8%" HeaderText="F-Sol" />
                                            <asp:BoundField DataField="etapa" ItemStyle-Width="8%" HeaderText="Etapa" />
                                            <asp:BoundField DataField="F-Etapa" ItemStyle-Width="8%" HeaderText="F-Etapa" />
                                            <asp:BoundField DataField="estatus" ItemStyle-Width="8%" HeaderText="Estatus" />
                                            <asp:BoundField DataField="Resposable" ItemStyle-Width="10%" HeaderText="Solicitante" />
                                            <asp:BoundField DataField="Unidad" ItemStyle-Width="10%" HeaderText="Unidad" />
                                            <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" />

                                            <asp:TemplateField HeaderText="Procesos" ItemStyle-Width="5%">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CommandName="editar" Text="📝" CssClass="btn btn-link" ToolTip="Editar" runat="server" />
                                                    <asp:Button ID="Button2" CommandName="verDoc" Text="👁‍🗨" CssClass="btn btn-link" ToolTip="Ver documento" runat="server" />
                                                    <asp:Button ID="Button3" CommandName="reasignar" Text="🔄" CssClass="btn btn-link" ToolTip="Reasignar Cotizador" runat="server" />
                                                    <asp:Button ID="Button4" CommandName="Confirmar" Text="✔️" CssClass="btn btn-link" ToolTip="Confirmar" runat="server" />
                                                    <asp:Button ID="Button5" CommandName="Rechazar" Text="❌" CssClass="btn btn-link" ToolTip="Rechazar" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>

                <asp:View ID="viewDescripciones" runat="server">
                    <div class="card-body col-12 d-row bg-light">
                        <div class="card-body col-12 d-flex bg-light" style="margin-bottom: -35px; margin-top: -20px;">
                            <div class=" col-12 border border-dark form-group">
                                <div class="card-header bg-light h1">
                                    <h6 class=" text-center">Generales</h6>
                                </div>

                                <%--campos de procesos de edicion--%>
                                <asp:Panel ID="panelInfoGeneral" Enabled="false" runat="server">
                                <div id="divNumEditar" class="form-group d-flex align-items-center justify-content-center mt-2" runat="server" visible="true">
                                    <div class="form-group col-4">
                                        <asp:Label ID="lblFicha" runat="server" Text="Unidad:"></asp:Label>
                                        <asp:Label ID="txtUnidad" CssClass="form-control" Enabled="false" runat="server"></asp:Label>
                                    </div>
                                    <div class="form-group col-4">
                                        <asp:Label ID="lblEtapa" runat="server" Text="Titulo:"></asp:Label>
                                        <asp:TextBox ID="txtTitulo" CssClass="form-control" Enabled="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-4">
                                        <asp:Label ID="Label1" runat="server" Text="Solicitante:"></asp:Label>
                                        <asp:Label ID="txtSolicitante" CssClass="form-control" Enabled="false" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div id="div1" class="form-group d-flex align-items-center justify-content-center mt-1" runat="server" visible="true">
                                    <div class="form-group col-4">
                                        <asp:Label ID="Label9" runat="server" Text="Creador:"></asp:Label>
                                        <asp:Label ID="txtSolicitantePrincipal" CssClass="form-control" Enabled="false" runat="server"></asp:Label>
                                    </div>
                                    <div class="form-group col-4">
                                        <asp:Label ID="Label2" runat="server" Text="SB:"></asp:Label>
                                        <asp:TextBox ID="txtSB" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-4">
                                        <asp:Label ID="Label3" runat="server" Text="Fecha:"></asp:Label>
                                        <asp:TextBox ID="txtFecha" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group d-flex  mt-1">
                                    <div class="form-group col-9">
                                        <asp:Label ID="Label4" runat="server" Text="Descripcion:"></asp:Label>
                                        <asp:TextBox ID="txtDescripcion" ToolTip="Especifique el uso de los bienes o servicios requeridos"
                                            CssClass="form-control" TextMode="MultiLine" Height="182px" runat="server"></asp:TextBox>
                                        <%--Width="105%"--%>
                                    </div>
                                    <div class="form-group col-2">
                                        <asp:Label ID="Label47" runat="server" Text="Solicitud:"></asp:Label>
                                        <asp:CheckBoxList ID="checkTipoSol"
                                            CellPadding="1"
                                            CssClass="form-control w-auto h-auto"
                                            RepeatColumns="1"
                                            RepeatDirection="Vertical"
                                            RepeatLayout="Table"
                                            TextAlign="Right"
                                            runat="server">
                                            <asp:ListItem Value="1">Bienes</asp:ListItem>
                                            <asp:ListItem Value="2">Servicios</asp:ListItem>
                                            <asp:ListItem Value="3">Reemplazo</asp:ListItem>
                                            <asp:ListItem Value="4">Obra</asp:ListItem>
                                            <asp:ListItem Value="5">Nuevo</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <%--<div class="col-1 row align-items-end" style="height: 205px;">
                                        <div class="col">
                                            <asp:Button ID="btnActualizar" ToolTip="Actualizar" runat="server" Text="💾" class="btn" Style="background-color: green" OnClick="btnActualizar_Click" />
                                        </div>
                                    </div>--%>
                                </div>
                                </asp:Panel>

                                

                                <%--<div class=" card-body col-12 form-group">
                                    <div class="form-group d-flex align-items-center justify-content-center mt-1">
                                        <div class="form-group col-6">
                                            <asp:Button ID="btnVerDoc1" class="btn btn-info form-control" runat="server" Text="Ver 👁️" OnClick="btnVerDoc_Click" />
                                        </div>
                                        <div class="form-group col-6">
                                            <asp:Button ID="btnEnviar" class="btn btn-primary form-control" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
                                        </div>
                                    </div>
                                </div>--%>


                                <div class="card-header bg-light d-flex align-items-center justify-content-center">
                                    <asp:Label class="text-center " ID="headNameCotizador" runat="server">Cotizador:</asp:Label>
                                </div>
                                <div id="div2" class="form-group d-flex align-items-center justify-content-center mt-1" runat="server" visible="true">
                                    <div class="form-group col-6">
                                        <asp:Label ID="Label7" runat="server" Text="Proveedor:"></asp:Label>
                                        <asp:Label ID="lblProveedor" CssClass="form-control" runat="server"></asp:Label>
                                    </div>
                                    <div class="form-group col-2">
                                        <asp:Label ID="Label24" runat="server" Text="Precio:"></asp:Label>
                                        <asp:Label ID="lblPrecioProveedor" CssClass="form-control" runat="server"></asp:Label>
                                    </div>
                                    <div class="form-group col-2">
                                        <asp:Label ID="Label25" runat="server" Text="Resolución:"></asp:Label>
                                        <asp:Label ID="lblResolucion" CssClass="form-control" runat="server"></asp:Label>
                                    </div>
                                    <div class="form-group col-2">
                                        <asp:Label ID="Label26" runat="server" Text="Ord.Compra:"></asp:Label>
                                        <asp:Label ID="lblOrdenCompra" CssClass="form-control" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <%--<asp:Panel ID="panelAsignarProveedor" runat="server">
                                    <div class="card-header bg-light h1 mt-n3">
                                        <h6 class=" text-center">Cotizador</h6>
                                    </div>
                                    <div id="div7" class="form-group d-flex align-items-center justify-content-center mt-1" runat="server" visible="true">
                                        <div class="form-group col-4">
                                            <asp:Label ID="Label32" runat="server" Text="Proveedor:"></asp:Label>
                                            <asp:DropDownList ID="dlistProveedor" CssClass="form-control" DataValueField="id" DataTextField="nombre" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="form-group col-2">
                                            <asp:Label ID="Label35" runat="server" Text="Precio:"></asp:Label>
                                            <asp:TextBox ID="txtPrecioProveedor" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-2">
                                            <asp:Label ID="Label37" runat="server" Text="Resolución:"></asp:Label>
                                            <asp:TextBox ID="txtResolucion" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-2">
                                            <asp:Label ID="Label39" runat="server" Text="Ord.Compra:"></asp:Label>
                                            <asp:TextBox ID="txtOrdenCompra" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-2">
                                            <asp:LinkButton ID="lbActualizarProveedor" OnClick="lbActualizarProveedor_Click" Style="background-color: green" Visible="true" class="btn mt-4" ToolTip="Actualizar informacion del registro" runat="server">
                                                <asp:Image ID="Image1" ImageUrl="~/userscontent/icon/iconUpdate.png" Width="25px" Height="25px" runat="server" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbCrearProveedor" OnClick="lbCrearProveedor_Click" Style="background-color: green" class="btn mt-4" ToolTip="Crear nuevo proveedor" runat="server">
                                                <asp:Image ID="Image3" ImageUrl="~/userscontent/icon/añadirContacto.png" Width="25px" Height="25px" runat="server" />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>--%>

                                <asp:Panel ID="panelVer" runat="server">
                                    <div class="form-group d-flex align-items-center justify-content-center mt-1">

                                        <div class="form-group col-12">
                                            <asp:Button ID="Button7" class="btn btn-info form-control" runat="server" Text="Ver 👁️" OnClick="btnVerDoc_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="panelBtnsCotizador" runat="server">

                                    <div class=" card-body col-12 form-group">
                                        <div class="form-group d-flex align-items-center justify-content-center mt-1">
                                            <div class="form-group col-6">
                                                <asp:Button ID="btnConfirmar" class="btn btn-success form-control" runat="server" Text="Confirmar ✔️" OnClick="btnConfirmar_Click"/>
                                            </div>
                                            <div class="form-group col-6">
                                                <asp:Button ID="btnRechazar" class="btn btn-danger form-control" runat="server" Text="Rechazar ✖️" OnClick="btnRechazar_Click"/>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="panelBtnsOthers" runat="server">

                                    <div class=" card-body col-12 form-group">
                                        <div class="form-group d-flex align-items-center justify-content-center mt-1">
                                            <div class="form-group col-3">
                                                <asp:Button ID="btnSiguienteEtapa" class="btn btn-primary form-control" runat="server" Text="Enviar ✉️" OnClick="btnSiguienteEtapa_Click" />
                                            </div>
                                            <div class="form-group col-3">
                                                <asp:Button ID="btnVerDoc" class="btn btn-info form-control" runat="server" Text="Ver 👁️" OnClick="btnVerDoc_Click" />
                                            </div>
                                            <div class="form-group col-3">
                                                <asp:Button ID="btnAsignar" class="btn btn-success form-control" runat="server" Text="Asignar 📌" OnClick="btnAsignar_Click" />
                                            </div>
                                            <div class="form-group col-3">
                                                <asp:Button ID="btnEstatusEspeciales" class="btn btn-danger form-control" runat="server" Text="Estatus Especiales 📑" OnClick="btnEstatusEspeciales_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>

                        <div>
                            <div class="card-body col-12 d-flex  bg-light" style="margin-bottom: -35px;">
                                <div class=" col-12 border border-dark form-group mt-2">
                                    <div class="card-header bg-light h1">
                                        <h6 class=" text-center">Contenido</h6>
                                    </div>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <%--<div id="div4" class="form-group d-flex align-items-center justify-content-center mt-1 col-12" runat="server" visible="true">

                                            <div class="form-group col-1">
                                                <asp:Label ID="Label13" runat="server" Text="Renglon:"></asp:Label>
                                                <asp:TextBox ID="txtRenglon" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-2">
                                                <asp:Label ID="Label9" runat="server" Text="Cantidad:"></asp:Label>
                                                <asp:TextBox ID="txtCantidad" CssClass="form-control"  runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-2">
                                                <asp:Label ID="Label10" runat="server" Text="Unidad:"></asp:Label>
                                                <asp:TextBox ID="txtUnidadContent" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-4">
                                                <asp:Label ID="Label11" runat="server" Text="Descripcion:"></asp:Label>
                                                <asp:TextBox ID="txtDecsContent" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-2">
                                                <asp:Label ID="Label12" runat="server" Text="Costo unitario:"></asp:Label>
                                                <asp:TextBox ID="txtCosto" CssClass="form-control" step="any" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-1">
                                                <asp:LinkButton ID="linkAddRenglon" OnClick="btnAddRenglon_Click" class="btn btn-success btn-outline-danger mt-4" ToolTip="Agregar" runat="server">
                                                    <asp:Image ID="Image4" ImageUrl="~/userscontent/icon/crear.png" Width="25px" Height="25px" runat="server" />
                                                </asp:LinkButton>
                                            </div>
                                        </div>--%>

                                        <div id="div3" class="form-group d-flex align-items-center justify-content-center mt-1" runat="server">
                                            <div class="form-group table-responsive">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gridContenido" CssClass="table table-striped table-light table-bordered table table-condensed" AutoGenerateColumns="false" Font-Size="Small" Width="99%" CellSpacing="0" OnRowCommand="gridContenido_RowCommand" runat="server" HorizontalAlign="Center">
                                                        <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <HeaderStyle ForeColor="White" BackColor="Gray" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <Columns>
                                                            <asp:BoundField DataField="idDetalles_Solicitudes" ItemStyle-Width="8%" HeaderText="Id" />
                                                            <asp:BoundField DataField="renglones" ItemStyle-Width="7%" HeaderText="RENGLÓN" />
                                                            <asp:BoundField DataField="cantidades" ItemStyle-Width="7%" HeaderText="CANTIDAD" />
                                                            <asp:BoundField DataField="unidades" ItemStyle-Width="7%" HeaderText="UNIDAD" />
                                                            <asp:BoundField DataField="descripciones" HeaderText="DESCRIPCIÓN" />
                                                            <asp:BoundField DataField="unitario" ItemStyle-Width="12%" HeaderText="P.UNITARIO" />
                                                            <asp:BoundField DataField="total" ItemStyle-Width="12%" HeaderText="TOTAL" />
                                                            <%--<asp:TemplateField HeaderText="Procesos" ItemStyle-Width="11%">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:Button ID="Button1" CommandName="editarRenglon" Text="📝" CssClass="btn btn-link" ToolTip="Editar" runat="server" />
                                                                    <asp:Button ID="Button2" CommandName="eliminarRenglon" Text="❌" CssClass="btn btn-link" ToolTip="eliminar" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>


                            <div class="card-body col-12 d-flex  bg-light" style="margin-bottom: -35px;">
                                <div class=" col-12 border border-dark form-group">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <div class="card-header bg-light h1">
                                            <asp:Button ID="btnAddREg" ToolTip="Agregar" runat="server" Text="📂" class="btn btn-success btn-outline-danger position-absolute mt-n1" OnClick="btnAddREg_Click" />
                                            <h6 class=" text-center">Archivos de solicitud</h6>

                                        </div>
                                        <div id="div5" class="form-group d-flex align-items-center justify-content-center mt-4" runat="server" visible="true">
                                            <div class="form-group table-responsive">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gridArchivo" OnRowCommand="gridArchivo_RowCommand" OnRowDataBound="gridArchivo_RowDataBound"
                                                        CssClass="table table-striped table-light table-bordered table table-condensed" AutoGenerateColumns="false" Font-Size="Small" Width="99%" CellSpacing="0" runat="server" HorizontalAlign="Center">
                                                        <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <HeaderStyle ForeColor="White" BackColor="Gray" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <Columns>
                                                            <asp:BoundField DataField="#Ref" ItemStyle-Width="8%" HeaderText="#Ref" />
                                                            <asp:BoundField DataField="Archivo" HeaderText="Archivo" />
                                                            <asp:BoundField DataField="Doc" HeaderText="Doc" />
                                                            <asp:TemplateField HeaderText="Procesos" ItemStyle-Width="10%">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:Button ID="Button1" CommandName="verDoc" Text="👁‍🗨" CssClass="btn btn-link" ToolTip="Ver documento" runat="server" />
                                                                    <asp:Button ID="Button2" CommandName="eliminarArchivo" Text="❌" CssClass="btn btn-link" ToolTip="eliminar" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="card-body col-12 d-flex  bg-light" style="margin-bottom: -35px;">
                                <div class=" col-12 border border-dark form-group">
                                    <asp:Panel ID="panelMensajes" runat="server">
                                        <asp:UpdatePanel ID="uPanelChat" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="card-header bg-light h1">

                                                    <asp:LinkButton ID="lbtnContorlMensajes" CommandArgument="1" OnCommand="btnPanelMensajes_Click" CssClass="btn btn-success btn-outline-danger position-absolute mt-n1" ToolTip="Mensajes" runat="server">
                                                        <%--OnClientClick="ScrollToBottom();"--%>
                                                        <asp:Image ID="imgEncendido" ImageUrl="~/userscontent/icon/botón-de-encendido.png" Width="25px" Height="25px" runat="server" />
                                                    </asp:LinkButton>

                                                    <h6 class=" text-center">Mensajes y alertas</h6>
                                                </div>
                                                <div id="div6" class="form-group d-flex align-items-center justify-content-center mt-4" runat="server" visible="true">
                                                    <div class="form-group table-responsive">
                                                        <div class="table-responsive">
                                                            <div class="container">
                                                                <div class="row clearfix">
                                                                    <div class="col-12">
                                                                        <div>
                                                                            <asp:Panel ID="chatArea" class="chat" runat="server" DefaultButton="linkBMsj" Enabled="false">
                                                                                <div class="chat-message clearfix">
                                                                                    <asp:Panel ID="ChatHistory" class="chat-history" runat="server">
                                                                                        <asp:Panel ID="panelChat" class="m-b-0 overflow-auto" Height="250px" runat="server" ScrollBars="Vertical">
                                                                                            <%--chat content--%>
                                                                                        </asp:Panel>
                                                                                    </asp:Panel>
                                                                                    <div class="chat-message clearfix">
                                                                                        <div class="input-group mb-0">
                                                                                            <asp:TextBox ID="txtMsj" class="form-control" placeholder="Escriba su mensaje..." runat="server"></asp:TextBox>
                                                                                            <div class="input-group-prepend">
                                                                                                <asp:LinkButton ID="linkBMsj" ToolTip="Enviar Mensaje" class="input-group-text" OnClick="linkBMsj_Click" runat="server">
                                                                                                    <%--OnClientClick="ScrollToBottom();"--%>
                                                                                                    <asp:Image ID="Image2" ImageUrl="~/userscontent/icon/boton-enviado.png" Width="25px" Height="25px" runat="server" />
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:View>

                <asp:View ID="viewPDF" runat="server">
                    <div class="card-header bg-white">
                        <h3 class=" text-center " style="color: #275EAD">Contenido del documento</h3>
                        <asp:Button ID="btnCloseContent" OnCommand="btnCloseContent_Command" CssClass=" btn btn-danger mb-n2" 
                            ToolTip="Cerrar documento" runat="server" Text="X"  /> <%--OnClick="btnCloseContent_Click"--%>
                    </div>

                    <asp:Literal ID="ltEmbed" runat="server" />


                </asp:View>

            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnRegistrarArchivo" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
