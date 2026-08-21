namespace sitemaCompras.viewsAdmin
{
    using System;
    using System.Data;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using sitemaCompras.Controlers;

    public partial class Auditoria : Page
    {
        private BaseDatos odbusuario;

        protected ScriptManager ScriptManager1;
        protected UpdatePanel UpdatePanel1;
        protected TextBox txtUsuario;
        protected TextBox txtSolicitud;
        protected DropDownList dlAccion;
        protected TextBox txtDesde;
        protected TextBox txtHasta;
        protected Button btnFiltrar;
        protected Button btnLimpiar;
        protected Label lblResumen;
        protected GridView GridView1;

        private void Page_PreInit(object sender, EventArgs e)
        {
            // Mismo esquema que el resto del panel admin: type 1 usa superAdmin.master
            // (sidebar completo) y type 7 usa adminUsers.master.
            if (!Page.IsPostBack)
            {
                if ((string)Session["type"] == "1")
                {
                    MasterPageFile = "~/viewsAdmin/superAdmin.master";
                }
                else if ((string)Session["type"] == "7")
                {
                    MasterPageFile = "~/viewsAdmin/adminUsers.master";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }

            // Guarda de acceso: solo perfiles administrativos (tipo 1 y 7).
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/login.aspx");
                return;
            }
            if ((string)Session["type"] != "1" && (string)Session["type"] != "7")
            {
                Session.Abandon();
                Session.Clear();
                FormsAuthentication.SignOut();
                Response.Redirect("~/login.aspx");
                return;
            }

            CargarAcciones();
            GuardarFiltros("", null, "", null, null);
            BindGrid(0);
        }

        /// <summary>
        /// Carga el catálogo de acciones en el dropdown, con "Todas" al inicio.
        /// </summary>
        private void CargarAcciones()
        {
            try
            {
                odbusuario = new BaseDatos();
                DataSet ds = odbusuario.listaAccionesAuditoria();
                dlAccion.Items.Clear();
                if (ds != null && ds.Tables.Count > 0)
                {
                    dlAccion.DataSource = ds.Tables[0];
                    dlAccion.DataTextField = "descripcion";
                    dlAccion.DataValueField = "clave";
                    dlAccion.DataBind();
                }
                dlAccion.Items.Insert(0, new ListItem("— Todas —", ""));
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "Auditoria.CargarAcciones", "carga de catálogo");
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = string.IsNullOrWhiteSpace(txtUsuario.Text) ? "" : txtUsuario.Text.Trim();

                int? solicitud = null;
                int parsedSol;
                if (!string.IsNullOrWhiteSpace(txtSolicitud.Text) && int.TryParse(txtSolicitud.Text.Trim(), out parsedSol))
                    solicitud = parsedSol;

                string accion = dlAccion.SelectedValue ?? "";

                DateTime? desde = null;
                DateTime parsedDesde;
                if (!string.IsNullOrWhiteSpace(txtDesde.Text) && DateTime.TryParse(txtDesde.Text, out parsedDesde))
                    desde = parsedDesde.Date;

                DateTime? hasta = null;
                DateTime parsedHasta;
                if (!string.IsNullOrWhiteSpace(txtHasta.Text) && DateTime.TryParse(txtHasta.Text, out parsedHasta))
                    hasta = parsedHasta.Date.AddDays(1).AddSeconds(-1); // incluir todo el día seleccionado

                GuardarFiltros(usuario, solicitud, accion, desde, hasta);
                BindGrid(0);
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "Auditoria.btnFiltrar_Click", "grid/button handler");
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo aplicar el filtro');", true);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtSolicitud.Text = "";
            txtDesde.Text = "";
            txtHasta.Text = "";
            if (dlAccion.Items.Count > 0)
                dlAccion.SelectedIndex = 0;
            GuardarFiltros("", null, "", null, null);
            BindGrid(0);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGrid(e.NewPageIndex);
        }

        /// <summary>
        /// Persiste los criterios de filtro en ViewState para que la paginación
        /// pueda re-consultar sin depender del estado de los controles.
        /// </summary>
        private void GuardarFiltros(string usuario, int? solicitud, string accion, DateTime? desde, DateTime? hasta)
        {
            ViewState["f_usuario"] = usuario;
            ViewState["f_solicitud"] = solicitud;
            ViewState["f_accion"] = accion;
            ViewState["f_desde"] = desde;
            ViewState["f_hasta"] = hasta;
        }

        private void BindGrid(int pageIndex)
        {
            try
            {
                string usuario = ViewState["f_usuario"] as string ?? "";
                int? solicitud = ViewState["f_solicitud"] as int?;
                string accion = ViewState["f_accion"] as string ?? "";
                DateTime? desde = ViewState["f_desde"] as DateTime?;
                DateTime? hasta = ViewState["f_hasta"] as DateTime?;

                odbusuario = new BaseDatos();
                DataSet ds = odbusuario.filtroAuditoria(usuario, solicitud, accion, desde, hasta);

                int total = (ds != null && ds.Tables.Count > 0) ? ds.Tables[0].Rows.Count : 0;
                GridView1.PageIndex = pageIndex;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                lblResumen.Text = total + " registro(s) encontrado(s).";
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "Auditoria.BindGrid", "consulta de auditoría");
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al consultar la auditoría');", true);
            }
        }
    }
}
