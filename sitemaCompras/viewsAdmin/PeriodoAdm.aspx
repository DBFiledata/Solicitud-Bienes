<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/viewsAdmin/superAdmin.Master" CodeBehind="PeriodoAdm.aspx.cs" Inherits="sitemaCompras.viewsAdmin.PeriodoAdm" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link href="../Content/modalStyle.css" rel="stylesheet" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="HiddenField1" runat="server" />
            <ajaxtoolkit:modalpopupextender id="mpeEditarRegistro" runat="server" popupcontrolid="Panel1" targetcontrolid="HiddenField1"
                cancelcontrolid="btnEditarClose" backgroundcssclass="modalBackground">
            </ajaxtoolkit:modalpopupextender>

            <!-- Modal Editar Periodo -->
            <asp:Panel ID="Panel1" runat="server" CssClass="modal-dialog embed-responsive" DefaultButton="btnEditar" align="center" Style="display: none; max-width: 600px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title text-center">Editar Periodo Administrativo</h4>
                        <asp:Button ID="btnEditarClose" OnClick="limpiarModalClick" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body">
                                        <div class="form-group text-left">
                                            <div class="form-group">
                                                <label>Descripción:</label>
                                                <asp:TextBox ID="txtEditDescripcion" runat="server" CssClass="form-control" placeholder="Ej: 2025-2026"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Fecha de Inicio:</label>
                                                <asp:TextBox ID="txtEditFechaInicio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Fecha de Fin:</label>
                                                <asp:TextBox ID="txtEditFechaFin" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </div>
                                            <div class="form-group form-check">
                                                <asp:CheckBox ID="chkEditVigente" runat="server" CssClass="form-check-input" />
                                                <label class="form-check-label" for="<%= chkEditVigente.ClientID %>">¿Establecer como periodo vigente (actual)?</label>
                                            </div>
                                            <asp:Button ID="btnEditar" class="btn btn-primary btn-block mt-3" runat="server" Text="Guardar Cambios ✔" OnClick="btnEditar_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <!-- MultiView para gestión de Periodos -->
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="viewTabla" runat="server">
                    <div class="card-header bg-light embed-responsive pt-3 pb-3">
                        <h5 class="text-center mr-3">Gestión de Periodos Administrativos</h5>
                        <asp:Button ID="btnAddREg" ToolTip="Agregar Nuevo Periodo" runat="server" Text="+" class="btn close mr-2" Style="margin-top: -36px; background-color: #058c24" OnClick="btnAddREg_Click" />
                    </div>
                    <div class="card-body mt-n2">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" CssClass="table table-striped table-light table-bordered table-condensed text-center" AutoGenerateColumns="false" Font-Size="Small" Width="100%" CellSpacing="0" OnRowCommand="GridView1_RowCommand" runat="server" HorizontalAlign="Center">
                                <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle ForeColor="White" BackColor="Gray" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="idPeriodo" HeaderText="ID" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                    <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha de Inicio" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="fecha_fin" HeaderText="Fecha de Fin" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField HeaderText="Vigente" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVigente" runat="server" 
                                                Text='<%# Convert.ToInt32(Eval("vigente")) == 1 ? "⭐ VIGENTE" : "No" %>' 
                                                CssClass='<%# Convert.ToInt32(Eval("vigente")) == 1 ? "badge badge-success p-2" : "text-muted" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acción" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" CommandName="editar" CommandArgument='<%# Eval("idPeriodo") %>' Text="📝 Editar" CssClass="btn btn-sm btn-info" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <!-- Panel de Asignación de Visibilidad a Usuarios -->
                    <div class="card mt-4">
                        <div class="card-header bg-dark text-white pt-3 pb-3">
                            <h5 class="text-center">Habilitación Individual de Periodos Anteriores</h5>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-6 offset-md-3 text-center">
                                    <label class="font-weight-bold">Seleccione el periodo histórico a habilitar:</label>
                                    <asp:DropDownList ID="ddlPeriodos" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriodos_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <small class="text-muted d-block mt-1">
                                        Los usuarios seleccionados abajo tendrán visibilidad de las solicitudes de este periodo además del vigente.
                                    </small>
                                </div>
                            </div>

                            <div class="table-responsive">
                                <asp:GridView ID="GridViewUsuarios" CssClass="table table-striped table-bordered table-condensed text-center" AutoGenerateColumns="false" Font-Size="Small" Width="100%" runat="server" HorizontalAlign="Center">
                                    <HeaderStyle ForeColor="White" BackColor="#343a40" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Habilitar" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkHabilitado" runat="server" />
                                                <asp:HiddenField ID="hdnUsuarioId" runat="server" Value='<%# Eval("idUsuaruios") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idUsuaruios" HeaderText="Cédula / Usuario" />
                                        <asp:TemplateField HeaderText="Nombre Completo">
                                            <ItemTemplate>
                                                <%# Eval("nombres") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="unidad" HeaderText="Unidad Administrativa" />
                                        <asp:BoundField DataField="tipo" HeaderText="Rol / Tipo" />
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="text-center mt-3">
                                <asp:Button ID="btnGuardarAsignaciones" runat="server" Text="Guardar Asignaciones ✔" CssClass="btn btn-success px-5" OnClick="btnGuardarAsignaciones_Click" />
                            </div>
                        </div>
                    </div>
                </asp:View>

                <!-- Vista Registro de Periodo -->
                <asp:View ID="viewRegistro" runat="server">
                    <div class="container py-3 col-8">
                        <div class="d-flex align-items-center justify-content-center">
                            <div class="col-md-12 bg-dark mt-2 pb-4 pt-4 rounded">
                                <div class="row mx-2">
                                    <div class="col-md-12 mx-auto">
                                        <div class="card border-secondary">
                                            <div class="card-header bg-secondary text-white">
                                                <h3 class="text-center">Crear Nuevo Periodo Administrativo</h3>
                                            </div>
                                            <div class="card-body text-left">
                                                <div class="form-group">
                                                    <label class="font-weight-bold">Descripción del Periodo:</label>
                                                    <asp:TextBox ID="txtDescripcion" CssClass="form-control" placeholder="Ej: 2026-2027" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label class="font-weight-bold">Fecha de Inicio:</label>
                                                    <asp:TextBox ID="txtFechaInicio" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label class="font-weight-bold">Fecha de Fin:</label>
                                                    <asp:TextBox ID="txtFechaFin" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group form-check mb-4">
                                                    <asp:CheckBox ID="chkRegVigente" runat="server" CssClass="form-check-input" />
                                                    <label class="form-check-label" for="<%= chkRegVigente.ClientID %>">¿Establecer como periodo vigente (actual)?</label>
                                                </div>

                                                <div class="form-group d-flex align-items-center justify-content-center">
                                                    <asp:Button ID="btnRegistrar" CssClass="btn btn-success btn-lg flex-fill mr-2" runat="server" Text="Confirmar ✔" OnClick="btnRegistrar_Click" />
                                                    <asp:Button ID="btnCancelar" CssClass="btn btn-danger btn-lg flex-fill ml-2" runat="server" Text="Cancelar ❌" OnClick="btnCancelar_Click" CausesValidation="False" />
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
