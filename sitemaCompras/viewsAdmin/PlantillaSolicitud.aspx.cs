namespace sitemaCompras.viewsAdmin
{
    using System;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using sitemaCompras.Controlers;

    public partial class PlantillaSolicitud : Page
    {
        private BaseDatos odbusuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else if ((string)Session["type"] != "1")
                {
                    Session.Abandon();
                    Session.Clear();
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/login.aspx");
                }
                CargarPlantillaActiva();
                CargarHistorial();
            }
        }

        /// <summary>
        /// Sube un nuevo archivo .docx o .xlsx como plantilla activa.
        /// </summary>
        protected void btnSubirPlantilla_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUploadPlantilla.HasFile)
                {
                    string extension = Path.GetExtension(FileUploadPlantilla.FileName).ToLower();
                    if (extension != ".docx" && extension != ".xlsx")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Pop",
                            "alert('Solo se permiten archivos .docx (Word) o .xlsx (Excel)');", true);
                        return;
                    }

                    // Generar nombre único para el archivo, preservando la extensión real.
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string nombreArchivo = "plantilla_" + timestamp + extension;
                    string rutaRelativa = "~/DocumentsPlantillas/" + nombreArchivo;
                    string rutaFisica = HttpContext.Current.Server.MapPath(rutaRelativa);

                    // Asegurar que el directorio existe
                    string directorio = Path.GetDirectoryName(rutaFisica);
                    if (!Directory.Exists(directorio))
                    {
                        Directory.CreateDirectory(directorio);
                    }

                    // Guardar el archivo
                    FileUploadPlantilla.SaveAs(rutaFisica);

                    // Registrar en la base de datos
                    odbusuario = new BaseDatos();
                    odbusuario.guardarPlantillaSolicitud(
                        FileUploadPlantilla.FileName,
                        rutaRelativa,
                        Convert.ToString(Session["id"])
                    );

                    ScriptManager.RegisterStartupScript(this, GetType(), "Pop",
                        "alert('Plantilla subida y activada correctamente. Las próximas solicitudes usarán esta plantilla.');", true);

                    CargarPlantillaActiva();
                    CargarHistorial();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Pop",
                        "alert('Debe seleccionar un archivo .docx o .xlsx');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "PlantillaSolicitud.btnSubirPlantilla_Click", "subir plantilla docx");
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop",
                    "alert('Error al subir la plantilla: " + ex.Message.Replace("'", "") + "');", true);
            }
        }

        /// <summary>
        /// Genera la plantilla por defecto que replica el formato original del PDF.
        /// </summary>
        protected void btnGenerarDefault_Click(object sender, EventArgs e)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string nombreArchivo = "plantilla_default_" + timestamp + ".docx";
                string rutaRelativa = "~/DocumentsPlantillas/" + nombreArchivo;
                string rutaFisica = HttpContext.Current.Server.MapPath(rutaRelativa);

                // Rutas a las imágenes del sistema
                string logoPath = HttpContext.Current.Server.MapPath("~/userscontent/img/logoasamblea.png");
                string headerPath = HttpContext.Current.Server.MapPath("~/userscontent/img/header.png");
                string comprasPath = HttpContext.Current.Server.MapPath("~/userscontent/img/compras.png");

                // Generar la plantilla
                ReporteComprasDocx reporteDocx = new ReporteComprasDocx();
                reporteDocx.CrearPlantillaDefault(rutaFisica, logoPath, headerPath, comprasPath);

                // Registrar en la base de datos
                odbusuario = new BaseDatos();
                odbusuario.guardarPlantillaSolicitud(
                    "Plantilla Default (generada)",
                    rutaRelativa,
                    Convert.ToString(Session["id"])
                );

                ScriptManager.RegisterStartupScript(this, GetType(), "Pop",
                    "alert('Plantilla por defecto generada y activada correctamente.');", true);

                CargarPlantillaActiva();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "PlantillaSolicitud.btnGenerarDefault_Click", "generar plantilla default");
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop",
                    "alert('Error al generar la plantilla: " + ex.Message.Replace("'", "") + "');", true);
            }
        }

        /// <summary>
        /// Carga y muestra la información de la plantilla activa actual.
        /// </summary>
        private void CargarPlantillaActiva()
        {
            try
            {
                odbusuario = new BaseDatos();
                string rutaActiva = odbusuario.obtenerPlantillaActiva();
                if (!string.IsNullOrEmpty(rutaActiva))
                {
                    // Cargar datos completos
                    odbusuario = new BaseDatos();
                    DataSet ds = odbusuario.listaPlantillasSolicitud();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // La primera fila es la más reciente (ORDER BY fechaSubida DESC)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (Convert.ToBoolean(dr["activa"]))
                            {
                                lblPlantillaActiva.Text = "📄 " + Convert.ToString(dr["nombreArchivo"]);
                                lblFechaPlantilla.Text = "Subida el: " + Convert.ToDateTime(dr["fechaSubida"]).ToString("dd/MM/yyyy HH:mm") +
                                    " | Por: " + Convert.ToString(dr["subidoPor"]);
                                return;
                            }
                        }
                    }
                }

                lblPlantillaActiva.Text = "⚠️ No hay plantilla activa. Se usará la plantilla por defecto del sistema.";
                lblFechaPlantilla.Text = "Presione 'Generar Default' para crear una plantilla base.";
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "PlantillaSolicitud.CargarPlantillaActiva", "cargar plantilla activa");
                lblPlantillaActiva.Text = "⚠️ Error al cargar la plantilla activa.";
            }
        }

        /// <summary>
        /// Carga el historial de plantillas en el GridView.
        /// </summary>
        private void CargarHistorial()
        {
            try
            {
                odbusuario = new BaseDatos();
                DataSet ds = odbusuario.listaPlantillasSolicitud();
                if (ds != null && ds.Tables.Count > 0)
                {
                    gridPlantillas.DataSource = ds.Tables[0];
                    gridPlantillas.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "PlantillaSolicitud.CargarHistorial", "cargar historial plantillas");
            }
        }
    }
}
