<%@ Page Language="C#" AutoEventWireup="true" Title="Busqueda de Solicitudes" MasterPageFile="~/viewsAdmin/superAdmin.master" CodeBehind="busquedaDocumentos.aspx.cs" Inherits="sitemaCompras.viewsAdmin.busquedaDocumentos" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link href="../Content/modalStyle.css" rel="stylesheet" />
    <style>
        .table-responsive .table th,
        .table-responsive .table td {
            word-wrap: break-word;
            overflow-wrap: break-word;
        }
        .table-responsive .table td.word-break-cell {
            word-break: break-word;
        }
    </style>
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
            <ajaxToolkit:ModalPopupExtender ID="mpeReporteFecha" runat="server" PopupControlID="panelReporteFecha" TargetControlID="HiddenField13"
                CancelControlID="btnCloseRFecha" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField14" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeReplicaResolucion" runat="server" PopupControlID="PanelReplica" TargetControlID="HiddenField14"
                CancelControlID="closeReplica" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField15" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeResultadoReplica" runat="server" PopupControlID="PanelReplicaResultado" TargetControlID="HiddenField15"
                CancelControlID="closeReplicaResultado" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField16" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeTransferir" runat="server" PopupControlID="panelTransferir" TargetControlID="HiddenField16"
                CancelControlID="btnCloseTransferir" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField17" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeConfirmarTransferir" runat="server" PopupControlID="panelConfirmarTransferir" TargetControlID="HiddenField17"
                CancelControlID="btnCloseConfirmarTransferir" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelConfirmarTransferir" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Transferir</h4>
                        <asp:Button ID="btnCloseConfirmarTransferir" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-2">
                        <div class="row">
                            <div class="col-md-12 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <asp:Panel ID="panel10" runat="server" Visible="true">
                                            <div class="form-group">
                                                <asp:Label ID="Label75" runat="server" Text="Seleccione la copia a la se va transferir"></asp:Label>
                                            </div>
                                            <div class="form-group mt-n4">
                                                <asp:DropDownList ID="dlistReplicaSB" DataValueField="id" DataTextField="replica" CssClass="form-control" ToolTip="Escoja la copia" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </asp:Panel>
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Button ID="btnTransferirReplica" OnClick="btnTransferirReplica_Click" class="btn btn-primary form-control" runat="server" Text="Confirmar transferencia" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelTransferir" runat="server" align="center" Style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; display: flex; justify-content: center; align-items: center;">
                <div class="modal-content mt-5" style="width: 1100px;">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Transferencia Renglones</h4>
                        <asp:Button ID="btnCloseTransferir" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-n3">
                        <div id="div9" class="form-group d-flex align-items-center justify-content-center mt-1" runat="server">
                            <div class="form-group table-responsive">
                                <div class="table-responsive">
                                    <asp:GridView ID="gridContenidoTransferir" CssClass="table table-striped table-light table-bordered table table-condensed" AutoGenerateColumns="false" Font-Size="Small" Width="99%" CellSpacing="0" OnRowCommand="gridContenidoTransferir_RowCommand" runat="server" HorizontalAlign="Center">
                                        <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle ForeColor="White" BackColor="Gray" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="idDetalles_Solicitudes" ItemStyle-Width="8%" HeaderText="Id" />
                                            <asp:BoundField DataField="renglones" ItemStyle-Width="7%" HeaderText="RENGL&#211;N" />
                                            <asp:BoundField DataField="cantidades" ItemStyle-Width="7%" HeaderText="CANTIDAD" />
                                            <asp:BoundField DataField="unidades" ItemStyle-Width="7%" HeaderText="UNIDAD" />
                                            <asp:BoundField DataField="descripciones" HeaderText="DESCRIPCI&#211;N" />
                                            <asp:BoundField DataField="unitario" ItemStyle-Width="12%" HeaderText="P.UNITARIO" />
                                            <asp:BoundField DataField="total" ItemStyle-Width="12%" HeaderText="TOTAL" />
                                            <asp:TemplateField HeaderText="Procesos" ItemStyle-Width="11%">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:Button ID="btnTransferir" CommandName="transferirRenglon" Text="&#x2197;&#xFE0F;" CssClass="btn btn-link" ToolTip="Transferir" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="PanelReplicaResultado" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Crear replica</h4>
                        <asp:Button ID="closeReplicaResultado" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-2">
                        <div class="row">
                            <div class="col-md-12 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <asp:Panel ID="panel9" runat="server" Visible="true">
                                            <div class="form-group">
                                                <asp:Label ID="msjReplicaResultado" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="Label76" runat="server" Text=""></asp:Label><br />
                                            </div>
                                            <div class="form-group mt-n4">
                                                <asp:Label ID="lblResultadoReplica" CssClass="h6" Font-Size="XX-Large" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="PanelReplica" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Crear replica</h4>
                        <asp:Button ID="closeReplica" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body mt-2">
                        <div class="row">
                            <div class="col-md-12 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <asp:Panel ID="panel8" runat="server" Visible="true">
                                            <div class="form-group">
                                                <asp:Label ID="msjReplica" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="idReplicaresolucion" runat="server" Text=""></asp:Label><br />
                                            </div>
                                            <div class="form-group mt-n4">
                                                <asp:Label ID="lblCantidad" CssClass="h6" Font-Size="X-Small" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Button ID="btnConfirmarReplica" OnClick="btnConfirmarReplica_Click1" class="btn btn-primary form-control" runat="server" Text="Confirmar" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelReporteFecha" runat="server" CssClass="modal-dialog embed-responsive" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">Reporte por Fecha</h4>
                        <asp:Button ID="btnCloseRFecha" OnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-6 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <div class="form-group">
                                                <asp:Label ID="Label55" runat="server" Text="Fecha Unidad:"></asp:Label>
                                                <asp:Label ID="lblFUnidad" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label72" runat="server" Text="Fecha SB:"></asp:Label>
                                                <asp:Label ID="lblFSB" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label56" runat="server" Text="Fecha Dir. ADM:"></asp:Label>
                                                <asp:Label ID="lblFDirAdm" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label57" runat="server" Text="Fecha Presupuesto:"></asp:Label>
                                                <asp:Label ID="lblFPresupuesto" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label61" runat="server" Text="Fecha Legal:"></asp:Label>
                                                <asp:Label ID="lblFLega" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label63" runat="server" Text="Fecha SAP:"></asp:Label>
                                                <asp:Label ID="lblFSAP" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label65" runat="server" Text="Fecha C. Fiscal:"></asp:Label>
                                                <asp:Label ID="lblFCFiscal" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label62" runat="server" Text="Fecha presidencia:"></asp:Label>
                                                <asp:Label ID="lblFPresidencia" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label66" runat="server" Text="Fecha vicepresidencia:"></asp:Label>
                                                <asp:Label ID="lblFVPresidencia" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label68" runat="server" Text="Fecha secretaria:"></asp:Label>
                                                <asp:Label ID="lblFSecretaria" CssClass="form-control" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <div class="form-group">
                                                <asp:Label ID="Label70" runat="server" Text="Fecha compras:"></asp:Label>
                                                <asp:Label ID="lblFCompras" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label64" runat="server" Text="Fecha contratarciones:"></asp:Label>
                                                <asp:Label ID="lblFontrataciones" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label58" runat="server" Text="F. E. Doc. unidad:"></asp:Label>
                                                <asp:Label ID="lblFEDocU" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label59" runat="server" Text="F. E. Doc. Proveedor:"></asp:Label>
                                                <asp:Label ID="lblFECocP" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label60" runat="server" Text="Fecha Precio R.:"></asp:Label>
                                                <asp:Label ID="lblFPrecioR" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label67" runat="server" Text="Fecha cotizacion en L.:"></asp:Label>
                                                <asp:Label ID="lblFCLinea" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label71" runat="server" Text="Fecha bloqueo:"></asp:Label>
                                                <asp:Label ID="lblFBloqueo" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label73" runat="server" Text="Fecha rechazado:"></asp:Label>
                                                <asp:Label ID="lblFRechazado" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label69" runat="server" Text="Fecha reserva P.:"></asp:Label>
                                                <asp:Label ID="lblFReservaP" CssClass="form-control" runat="server"></asp:Label>
                                                <asp:Label ID="Label74" runat="server" Text="Fecha anulado:"></asp:Label>
                                                <asp:Label ID="lblFAnulado" CssClass="form-control" runat="server"></asp:Label>
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
                        <div class="row">
                            <div class="col-md-12 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-2">
                                        <asp:Panel ID="panelListaCotizadores" runat="server" Visible="true">
                                            <asp:DropDownList ID="dlistCotizadores" DataValueField="id" DataTextField="nombres" CssClass="form-control" ToolTip="Escoja el cotizador" runat="server">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Button ID="Button4" OnClick="btnConfirmarCotizador_Click" class="btn btn-primary form-control" runat="server" Text="Confirmar" />
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="Label34" runat="server" Text="" ForeColor="Red"></asp:Label>
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
                                            <asp:Button ID="btnCrearProveedor" class="btn btn-primary" runat="server" Text="Registrar &#x2714;" OnClick="btnCrearProveedor_Click" />
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
                                                <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <asp:Button ID="btnRegistrarArchivo" class="btn btn-primary" runat="server" Text="Aceptar &#x2714;" OnClick="btnRegistrarArchivo_Click" />
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
                        <h4 class="modal-title text-center">Editar Rengl&#243;n</h4>
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
                            <asp:Button ID="btnActualizarRenglon" ToolTip="Actualizar" runat="server" Text="&#x2714;&#xFE0F;" ForeColor="Black" Font-Size="Large" class="btn btn-success btn-outline-danger mt-4" OnClick="btnActualizarRenglon_Click" />
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
                                                <asp:TextBox ID="txtEditNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label15" runat="server" CssClass="mt-2" Text="Label">Apellido:</asp:Label>
                                                <asp:TextBox ID="txtEditApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label16" runat="server" CssClass="mt-2" Text="Label">Unidad Administrativa</asp:Label>
                                                <asp:DropDownList ID="dListEditEstado" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                                <asp:Button ID="btnEditar" class="btn btn-primary" runat="server" Text="Aceptar &#x2714;" />
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
                        <asp:Button ID="btnEliminarArchivoClose" runat="server" Text="x" class="close" aria-hidden="true" />
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
                        <asp:Button ID="btnEliminarRenglonClose" runat="server" Text="x" class="close" aria-hidden="true" />
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
                        <asp:Button ID="btnEliminarSolicitudSolicitud" runat="server" Text="x" class="close" aria-hidden="true" />
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
                        <div class="row">
                            <div class="col-md-12 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-2">
                                        <asp:Panel ID="panel6" runat="server" Visible="true">
                                            <div class="form-group">
                                                <asp:Button ID="btnEDocUnidad" CommandArgument="8" OnCommand="Estatus_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="E. Doc. Unidad" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnEDocProveedor" CommandArgument="9" OnCommand="Estatus_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="E. Doc. Proveedor" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnPrecioR" CommandArgument="10" OnCommand="Estatus_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Precio Referencia" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnCotizacionL" CommandArgument="11" OnCommand="Estatus_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Cotizacion en L&#237;nea" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnBloqueo" CommandArgument="12" OnCommand="Estatus_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Bloqueo" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnRechazado" CommandArgument="7" OnCommand="Estatus_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Rechazado" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="Button5" CommandArgument="17" OnCommand="Estatus_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Reserva de partida" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="Button6" CommandArgument="18" OnCommand="Estatus_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Anulado" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="Label30" runat="server" Text="" ForeColor="Red"></asp:Label>
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
                        <div class="row">
                            <div class="col-md-12 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-1">
                                        <asp:Panel ID="panel5" runat="server" Visible="true">
                                            <div class="form-group"><asp:Button ID="btnUnidad" CommandArgument="1" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Unidad" /></div>
                                            <div class="form-group"><asp:Button ID="btnDirAdm" CommandArgument="2" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Dirreci&#243;n Adm." /></div>
                                            <div class="form-group"><asp:Button ID="btnpresupuesto" CommandArgument="3" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Presupuesto" /></div>
                                            <div class="form-group"><asp:Button ID="btnLegal" CommandArgument="4" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Legal" /></div>
                                            <div class="form-group"><asp:Button ID="btnSap" CommandArgument="5" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="SAP" /></div>
                                            <div class="form-group"><asp:Button ID="btnControlF" CommandArgument="6" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Control f&#237;scal" /></div>
                                            <div class="form-group"><asp:Button ID="btnPresidencia" CommandArgument="7" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Presidencia" /></div>
                                            <div class="form-group"><asp:Button ID="btnVPresidencia" CommandArgument="10" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Vice presidencia" /></div>
                                            <div class="form-group"><asp:Button ID="btnSecretaria" CommandArgument="11" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Secretaria general" /></div>
                                            <div class="form-group"><asp:Button ID="btnCompras" CommandArgument="9" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Compras" /></div>
                                            <div class="form-group"><asp:Button ID="btnContrataciones" CommandArgument="12" OnCommand="Etapa_Command" CssClass="btn btn-dark form-control text-center" runat="server" Text="Contrataciones" /></div>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="Label31" runat="server" Text="" ForeColor="Red"></asp:Label>
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
            </asp:Panel>

            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="ViewTabla" runat="server">
                    <div class="table-responsive">
                        <div class="card-header bg-light embed-responsive pt-3 pb-3">
                            <h5 class="text-center mr-3">Busqueda de solicitudes</h5>
                        </div>
                        <div id="desplegableFiltro" class="col-12 align-items-center bg-white modal-body">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnFiltrar">
                                <div class="col-12 d-flex flex-row">
                                    <div class="col-4">
                                        <div class="bg-white collapse-inner rounded mt-n2">
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <div class="form-group col-6">
                                                    <asp:Label ID="Label38" runat="server" Text="Label">Id de solicitud: </asp:Label>
                                                    <asp:TextBox ID="txtId" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-6">
                                                    <asp:Label ID="Label43" runat="server" Text="Label">SB: </asp:Label>
                                                    <asp:TextBox ID="txtSolicitudBien" CssClass="form-control" runat="server" ToolTip="Numero ref. de Solicitud de bienes"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="bg-white collapse-inner rounded mt-n2">
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <div class="form-group col-12">
                                                    <asp:Label ID="Label44" runat="server" Text="Label">Titulo: </asp:Label>
                                                    <asp:TextBox ID="txtTituloF" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="bg-white collapse-inner rounded mt-n2">
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <div class="form-group col-12">
                                                    <asp:Label ID="Label54" runat="server" Text="Label">Resoluci&#243;n: </asp:Label>
                                                    <asp:TextBox ID="txtNumResolucion" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 d-flex flex-row">
                                    <div id="divEstEta" class="col-4" runat="server">
                                        <div class="bg-white collapse-inner rounded mt-n4">
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <div class="form-group col-6">
                                                    <asp:Label ID="Label49" runat="server" Text="Label">Estatus: </asp:Label>
                                                    <asp:DropDownList ID="dlistEstatus" DataValueField="id" DataTextField="nombre" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-6">
                                                    <asp:Label ID="Label48" runat="server" Text="Label">Etapa: </asp:Label>
                                                    <asp:DropDownList ID="dListEtapa" DataValueField="id" DataTextField="nombre" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divProCot" class="col-4" runat="server">
                                        <div class="bg-white collapse-inner rounded mt-n4">
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <div class="form-group col-6">
                                                    <asp:Label ID="Label45" runat="server" Text="Label">Cotizador: </asp:Label>
                                                    <asp:DropDownList ID="dlistCotizador" DataValueField="id" DataTextField="nombres" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-6">
                                                    <asp:Label ID="Label46" runat="server" Text="Label">proveedor: </asp:Label>
                                                    <asp:DropDownList ID="dlistProveedorF" DataValueField="id" DataTextField="nombre" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divUniUser" class="col-4" runat="server">
                                        <div class="bg-white collapse-inner rounded mt-n4">
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <div class="form-group col-6">
                                                    <asp:Label ID="Label50" runat="server" Text="Label">Unidad: </asp:Label>
                                                    <asp:DropDownList ID="dlistUnidad" CssClass="form-control" Height="32px" DataValueField="id" DataTextField="nombre"
                                                        AutoPostBack="true" runat="server" OnSelectedIndexChanged="dlistUnidad_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-6">
                                                    <asp:Label ID="Label51" runat="server" Text="Label">Usuario: </asp:Label>
                                                    <asp:DropDownList ID="dlistUsuario" Enabled="false" DataValueField="idUsuaruios" DataTextField="nombres" CssClass="form-control" Height="32px" ToolTip="Usuarios por unidad" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 d-flex flex-row">
                                    <div class="col-11">
                                        <div class="bg-white collapse-inner rounded mt-n4">
                                            <div class="form-group d-flex align-items-center justify-content-center">
                                                <div class="form-group col-6">
                                                    <asp:Label ID="Label52" runat="server" Text="Label">Desde</asp:Label>
                                                    <asp:TextBox ID="txtDesde" CssClass="form-control" runat="server" ToolTip="Inicio de rango de busqueda" TextMode="Date"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-6">
                                                    <asp:Label ID="Label53" runat="server" Text="Label">Hasta</asp:Label>
                                                    <asp:TextBox ID="TxtHasta" CssClass="form-control" runat="server" ToolTip="Final de rango de busqueda" TextMode="Date"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-1">
                                        <div class="bg-white collapse-inner form-group mt-n2">
                                            <asp:Button ID="btnFiltrar" CssClass="btn btn-success btn-lg" ToolTip="Aplicar Filtro" runat="server" Text="&#x1F50E;" OnClick="btnFiltrar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
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
                                            <asp:BoundField DataField="Titulo" ItemStyle-Width="25%" HeaderText="Titulo" ItemStyle-CssClass="word-break-cell" />
                                            <asp:BoundField DataField="F-Sol" ItemStyle-Width="8%" HeaderText="F-Sol" />
                                            <asp:BoundField DataField="etapa" ItemStyle-Width="8%" HeaderText="Etapa" />
                                            <asp:BoundField DataField="F-Etapa" ItemStyle-Width="8%" HeaderText="F-Etapa" />
                                            <asp:BoundField DataField="estatus" ItemStyle-Width="8%" HeaderText="Estatus" />
                                            <asp:BoundField DataField="Cotizador" ItemStyle-Width="8%" HeaderText="Cotizador" Visible="false" />
                                            <asp:BoundField DataField="Resposable" ItemStyle-Width="10%" HeaderText="Solicitante" />
                                            <asp:BoundField DataField="Unidad" ItemStyle-Width="10%" HeaderText="Unidad" />
                                            <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" />
                                            <asp:TemplateField HeaderText="Procesos" ItemStyle-Width="5%">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CommandName="editar" Text="&#x1F4DD;" CssClass="btn btn-link" ToolTip="Editar" runat="server" />
                                                    <asp:Button ID="Button7" CommandName="Rfrecha" Text="&#x1F4C6;" CssClass="btn btn-link" ToolTip="Reporte por fecha" runat="server" />
                                                    <asp:Button ID="Button3" CommandName="verDoc" Text="&#x1F441;&#x200D;&#x1F5E8;" CssClass="btn btn-link" ToolTip="Ver documento" runat="server" />
                                                    <asp:Button ID="Button4_del" CommandName="eliminar" Text="&#x274C;" CssClass="btn btn-link" ToolTip="eliminar" runat="server" />
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

                                <asp:Panel ID="panelInfoGeneral1" runat="server">
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
                                </asp:Panel>

                                <asp:Panel ID="panelInfoGeneral2" runat="server">
                                    <div id="div1" class="form-group d-flex align-items-center justify-content-center mt-1" runat="server" visible="true">
                                        <div class="form-group col-6">
                                            <asp:Label ID="Label2" runat="server" Text="SB:"></asp:Label>
                                            <asp:TextBox ID="txtSB" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-6">
                                            <asp:Label ID="Label3" runat="server" Text="Fecha:"></asp:Label>
                                            <asp:TextBox ID="txtFecha" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="panelInfoGeneral3" CssClass="col-12" runat="server">
                                    <div class="form-group d-flex  mt-1">
                                        <asp:Panel ID="panelInfoGeneral3_1" CssClass="form-group col-9" runat="server">
                                            <div class="col-12">
                                                <asp:Label ID="Label4" runat="server" Text="Descripcion:"></asp:Label>
                                                <asp:TextBox ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" Height="182px" maxlength="455" runat="server"></asp:TextBox>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="panelInfoGeneral3_2" CssClass="form-group col-2" runat="server">
                                            <div class="col-12">
                                                <asp:Label ID="Label47" runat="server" Text="Solicitud:"></asp:Label>
                                                <asp:CheckBoxList ID="checkTipoSol" CellPadding="1" CssClass="form-control w-auto h-auto" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table" TextAlign="Right" runat="server">
                                                    <asp:ListItem Value="1">Bienes</asp:ListItem>
                                                    <asp:ListItem Value="2">Servicios</asp:ListItem>
                                                    <asp:ListItem Value="3">Reemplazo</asp:ListItem>
                                                    <asp:ListItem Value="4">Obra</asp:ListItem>
                                                    <asp:ListItem Value="5">Nuevo</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </asp:Panel>

                                        <div class="col-1 row align-items-end" style="height: 205px;">
                                            <div class="col">
                                                <asp:Button ID="btnActualizar" ToolTip="Actualizar" runat="server" Text="&#x1F4BE;" class="btn" Style="background-color: green" OnClick="btnActualizar_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="panelInfoPreveedor" runat="server">
                                    <div class="card-header bg-light d-flex align-items-center justify-content-center">
                                        <asp:Label class="text-center " ID="headNameCotizador" runat="server">Cotizador:</asp:Label>
                                    </div>
                                    <div id="div2" class="form-group d-flex align-items-center justify-content-center mt-1" runat="server" visible="true">
                                        <div class="form-group col-5">
                                            <asp:Label ID="Label7" runat="server" Text="Proveedor:"></asp:Label>
                                            <asp:Label ID="lblProveedor" CssClass="form-control" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-2">
                                            <asp:Label ID="Label24" runat="server" Text="Precio:"></asp:Label>
                                            <asp:Label ID="lblPrecioProveedor" CssClass="form-control" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-2">
                                            <asp:Label ID="Label25" runat="server" Text="Resoluci&#243;n:"></asp:Label>
                                            <asp:Label ID="lblResolucion" CssClass="form-control" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-2">
                                            <asp:Label ID="Label26" runat="server" Text="Ord.Compra:"></asp:Label>
                                            <asp:Label ID="lblOrdenCompra" CssClass="form-control" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-1">
                                            <asp:LinkButton ID="LbReplicaResolucion1" Visible="false" OnClick="LbReplicaResolucion_Click" class="btn btn-danger mt-4" ToolTip="Replicar para resoluci&#243;n" runat="server">
                                                <asp:Image ID="Image10" ImageUrl="~/userscontent/icon/duplicar.png" Width="21px" Height="25px" runat="server" />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="panelAsignarProveedor" runat="server">
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
                                            <asp:Label ID="Label37" runat="server" Text="Resoluci&#243;n:"></asp:Label>
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
                                                <asp:Image ID="Image3" ImageUrl="~/userscontent/icon/a&#241;adirContacto.png" Width="25px" Height="25px" runat="server" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="LbReplicaResolucion" Visible="false" OnClick="LbReplicaResolucion_Click" class="btn btn-danger mt-4" ToolTip="Replicar para resoluci&#243;n" runat="server">
                                                <asp:Image ID="Image6" ImageUrl="~/userscontent/icon/duplicar.png" Width="21px" Height="25px" runat="server" />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="panelBtnVer" runat="server">
                                    <div class=" card-body col-12 form-group">
                                        <div class="form-group d-flex align-items-center justify-content-center mt-1">
                                            <div class="form-group col-12">
                                                <asp:Button ID="btnVerdoc2" class="btn btn-info form-control" runat="server" Text="Ver &#x1F441;&#xFE0F;" OnClick="btnVerDoc_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="panelBtns" runat="server">
                                    <div class=" card-body col-12 form-group">
                                        <div class="form-group d-flex align-items-center justify-content-center mt-1">
                                            <div class="form-group col-6">
                                                <asp:Button ID="btnVerDoc1" class="btn btn-info form-control" runat="server" Text="Ver &#x1F441;&#xFE0F;" OnClick="btnVerDoc_Click" />
                                            </div>
                                            <div class="form-group col-6">
                                                <asp:Button ID="btnEnviar" class="btn btn-primary form-control" runat="server" Text="Enviar &#x2709;&#xFE0F;" OnClick="btnSiguienteEtapa_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="panelBtnsOthers" runat="server">
                                    <div class=" card-body col-12 form-group">
                                        <div class="form-group d-flex align-items-center justify-content-center mt-1">
                                            <div class="form-group col-4">
                                                <asp:Button ID="btnSiguienteEtapa" class="btn btn-primary form-control" runat="server" Text="Env&#237;o de Etapa &#x2709;&#xFE0F;" OnClick="btnSiguienteEtapa_Click" />
                                            </div>
                                            <div class="form-group col-4">
                                                <asp:Button ID="btnVerDoc" class="btn btn-info form-control" runat="server" Text="Ver &#x1F441;&#xFE0F;" OnClick="btnVerDoc_Click" />
                                            </div>
                                            <div class="form-group col-4">
                                                <asp:Button ID="btnEstatusEspeciales" class="btn btn-danger form-control" runat="server" Text="Estatus Especiales &#x1F4D1;" OnClick="btnEstatusEspeciales_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>

                        <div>
                            <div class="card-body col-12 d-flex  bg-light" style="margin-bottom: -35px;">
                                <div class=" col-12 border border-dark form-group mt-2">
                                    <div class="card-header bg-light d-flex align-items-center">
                                        <div class="flex-grow-1">
                                            <h6 class="text-center m-0">Contenido</h6>
                                        </div>
                                        <div class="ml-auto">
                                            <asp:LinkButton ID="LbReplicaSB" Visible="false" OnClick="LbReplicaSB_Click" class="btn btn-danger mt-1" ToolTip="Replicar para SB" runat="server">
                                                <asp:Image ID="Image5" ImageUrl="~/userscontent/icon/copiarFilas.png" Width="21px" Height="25px" runat="server" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="LbTransferRenglones" Visible="false" OnClick="LbTransferRenglones_Click" class="btn btn-info mt-1" ToolTip="Transferir renglones" runat="server">
                                                <asp:Image ID="Image7" ImageUrl="~/userscontent/icon/transferir.png" Width="21px" Height="25px" runat="server" />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <asp:Panel ID="panelAddContenido" runat="server">
                                        <div id="div4" class="form-group d-flex align-items-center justify-content-center mt-1 col-12" runat="server" visible="true">
                                            <div class="form-group col-1">
                                                <asp:Label ID="Label13" runat="server" Text="Renglon:"></asp:Label>
                                                <asp:TextBox ID="txtRenglon" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-2">
                                                <asp:Label ID="Label9" runat="server" Text="Cantidad:"></asp:Label>
                                                <asp:TextBox ID="txtCantidad" CssClass="form-control" runat="server"></asp:TextBox>
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
                                        </div>
                                    </asp:Panel>

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
                                                        <asp:BoundField DataField="renglones" ItemStyle-Width="7%" HeaderText="RENGL&#211;N" />
                                                        <asp:BoundField DataField="cantidades" ItemStyle-Width="7%" HeaderText="CANTIDAD" />
                                                        <asp:BoundField DataField="unidades" ItemStyle-Width="7%" HeaderText="UNIDAD" />
                                                        <asp:BoundField DataField="descripciones" HeaderText="DESCRIPCI&#211;N" />
                                                        <asp:BoundField DataField="unitario" ItemStyle-Width="12%" HeaderText="P.UNITARIO" />
                                                        <asp:BoundField DataField="total" ItemStyle-Width="12%" HeaderText="TOTAL" />
                                                        <asp:TemplateField HeaderText="Procesos" ItemStyle-Width="11%">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="Button8" CommandName="editarRenglon" Text="&#x1F4DD;" CssClass="btn btn-link" ToolTip="Editar" runat="server" />
                                                                <asp:Button ID="Button9" CommandName="eliminarRenglon" Text="&#x274C;" CssClass="btn btn-link" ToolTip="eliminar" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="card-body col-12 d-flex  bg-light" style="margin-bottom: -35px;">
                                <div class=" col-12 border border-dark form-group">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <div class="card-header bg-light h1">
                                            <asp:Button ID="btnAddREg" ToolTip="Agregar" runat="server" Text="&#x1F4C2;" class="btn btn-success btn-outline-danger position-absolute mt-n1" OnClick="btnAddREg_Click" />
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
                                                                    <asp:Button ID="Button10" CommandName="verDoc" Text="&#x1F441;&#x200D;&#x1F5E8;" CssClass="btn btn-link" ToolTip="Ver documento" runat="server" />
                                                                    <asp:Button ID="Button11" CommandName="eliminarArchivo" Text="&#x274C;" CssClass="btn btn-link" ToolTip="eliminar" runat="server" />
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
                                                        <asp:Image ID="imgEncendido" ImageUrl="~/userscontent/icon/bot&#243;n-de-encendido.png" Width="25px" Height="25px" runat="server" />
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
                                                                                        </asp:Panel>
                                                                                    </asp:Panel>
                                                                                    <div class="chat-message clearfix">
                                                                                        <div class="input-group mb-0">
                                                                                            <asp:TextBox ID="txtMsj" class="form-control" placeholder="Escriba su mensaje..." runat="server"></asp:TextBox>
                                                                                            <div class="input-group-prepend">
                                                                                                <asp:LinkButton ID="linkBMsj" ToolTip="Enviar Mensaje" class="input-group-text" OnClick="linkBMsj_Click" runat="server">
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
                        <asp:Button ID="btnCloseContent" CssClass=" btn btn-danger mb-n2" ToolTip="Cerrar documento" runat="server" Text="X" OnCommand="btnCloseContent_Command" />
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
