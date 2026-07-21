<%@ Page Language="C#" AutoEventWireup="true" Title="Plantilla Solicitud" MasterPageFile="~/viewsAdmin/superAdmin.master" CodeBehind="PlantillaSolicitud.aspx.cs" Inherits="sitemaCompras.viewsAdmin.PlantillaSolicitud" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid mt-3">
                <div class="row">
                    <div class="col-12">
                        <div class="card border-dark mb-4">
                            <div class="card-header bg-light">
                                <h5 class="text-center mb-0">Gestión de Plantilla de Solicitud</h5>
                            </div>

                            <div class="card-body bg-light">
                                <!-- Información sobre marcadores -->
                                <div class="alert alert-info">
                                    <h6><strong>Instrucciones:</strong></h6>
                                    <p>Suba un archivo <strong>.docx</strong> (Microsoft Word) o <strong>.xlsx</strong> (Microsoft Excel) que será usado como plantilla para generar las Solicitudes de Bienes y Servicios.
                                    La plantilla debe contener los siguientes marcadores de texto que serán reemplazados automáticamente:</p>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <ul class="mb-0">
                                                <li><code>{{FECHA}}</code> - Fecha de la solicitud</li>
                                                <li><code>{{DIR_ADMIN}}</code> - Director administrativo</li>
                                                <li><code>{{SOLICITANTE}}</code> - Funcionario responsable</li>
                                                <li><code>{{UNIDAD}}</code> - Unidad solicitante</li>
                                                <li><code>{{DESCRIPCION_USO}}</code> - Uso de bienes/servicios</li>
                                            </ul>
                                        </div>
                                        <div class="col-md-6">
                                            <ul class="mb-0">
                                                <li><code>{{CHK_BIENES}}</code> - Checkbox Bienes</li>
                                                <li><code>{{CHK_SERVICIOS}}</code> - Checkbox Servicios</li>
                                                <li><code>{{CHK_REEMPLAZO}}</code> - Checkbox Reemplazo</li>
                                                <li><code>{{CHK_OBRA}}</code> - Checkbox Obra</li>
                                                <li><code>{{CHK_NUEVO}}</code> - Checkbox Nuevo</li>
                                                <li><code>{{TABLA_RENGLONES}}</code> - Tabla de renglones</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <!-- Sección de subida -->
                                <div class="card border-primary mb-4">
                                    <div class="card-header bg-primary text-white">
                                        <strong>Subir nueva plantilla</strong>
                                    </div>
                                    <div class="card-body">
                                        <div class="form-group d-flex align-items-end">
                                            <div class="col-md-7">
                                                <label>Archivo de plantilla (.docx / .xlsx):</label>
                                                <asp:FileUpload ID="FileUploadPlantilla" CssClass="form-control" runat="server" accept=".docx,.xlsx" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button ID="btnSubirPlantilla" CssClass="btn btn-success form-control" 
                                                    runat="server" Text="Subir y Activar" OnClick="btnSubirPlantilla_Click" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="btnGenerarDefault" CssClass="btn btn-warning form-control" 
                                                    runat="server" Text="Generar Default" OnClick="btnGenerarDefault_Click"
                                                    ToolTip="Genera una plantilla base idéntica al formato original del PDF" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Plantilla activa -->
                                <div class="card border-success mb-4">
                                    <div class="card-header bg-success text-white">
                                        <strong>Plantilla Activa</strong>
                                    </div>
                                    <div class="card-body">
                                        <asp:Label ID="lblPlantillaActiva" runat="server" CssClass="form-control bg-light" 
                                            Text="No hay plantilla activa. Se usará la plantilla por defecto del sistema."></asp:Label>
                                        <asp:Label ID="lblFechaPlantilla" runat="server" CssClass="text-muted small mt-1" Text=""></asp:Label>
                                    </div>
                                </div>

                                <!-- Historial de plantillas -->
                                <div class="card border-secondary">
                                    <div class="card-header bg-secondary text-white">
                                        <strong>Historial de plantillas</strong>
                                    </div>
                                    <div class="card-body">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gridPlantillas" CssClass="table table-striped table-light table-bordered table-condensed" 
                                                AutoGenerateColumns="false" Font-Size="Small" Width="100%" CellSpacing="0" 
                                                runat="server" HorizontalAlign="Center" EmptyDataText="No se han subido plantillas aún.">
                                                <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle ForeColor="White" BackColor="Gray" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <Columns>
                                                    <asp:BoundField DataField="idPlantilla" HeaderText="#" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="nombreArchivo" HeaderText="Nombre del Archivo" ItemStyle-Width="30%" />
                                                    <asp:BoundField DataField="fechaSubida" HeaderText="Fecha de Subida" ItemStyle-Width="20%" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                    <asp:BoundField DataField="subidoPor" HeaderText="Subido Por" ItemStyle-Width="20%" />
                                                    <asp:TemplateField HeaderText="Estado" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <%# Convert.ToBoolean(Eval("activa")) ? "Activa" : "Inactiva" %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubirPlantilla" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
