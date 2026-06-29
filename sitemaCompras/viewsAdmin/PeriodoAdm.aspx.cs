using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using sitemaCompras.Controlers;

namespace sitemaCompras.viewsAdmin
{
    public partial class PeriodoAdm : Page
    {
        private BaseDatos odbusuario;

        private void Page_PreInit(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if ((string)Session["type"] == "1")
                {
                    MasterPageFile = "~/viewsAdmin/superAdmin.master";
                }
                else
                {
                    // Si no es tipo 1, no debe ver esto.
                    Session.Abandon();
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (Convert.ToInt32(Session["type"]) == 1)
            {
                if (!Page.IsPostBack)
                {
                    CargarPeriodos();
                    CargarUsuarios();
                    CargarComboPeriodos();
                    MultiView1.ActiveViewIndex = 0;
                }
            }
            else
            {
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
        }

        private void CargarPeriodos()
        {
            odbusuario = new BaseDatos();
            DataSet ds = odbusuario.dListaPeriodosAdministrativos();
            if (ds != null && ds.Tables.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }

        private void CargarUsuarios()
        {
            odbusuario = new BaseDatos();
            DataSet ds = odbusuario.filtroUsuarios("", "", "", null, null, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                // Excluimos superAdmin y admin de usuarios
                dv.RowFilter = "tipo <> 'Super Administrador' AND tipo <> 'Administrador de Usuarios'";
                GridViewUsuarios.DataSource = dv;
                GridViewUsuarios.DataBind();
            }
        }

        private void CargarComboPeriodos()
        {
            odbusuario = new BaseDatos();
            DataSet ds = odbusuario.dListaPeriodosAdministrativos();
            ddlPeriodos.Items.Clear();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string id = row["idPeriodo"].ToString();
                    string desc = row["descripcion"].ToString();
                    string vigente = Convert.ToInt32(row["vigente"]) == 1 ? " (VIGENTE)" : "";
                    
                    ddlPeriodos.Items.Add(new ListItem(desc + vigente, id));
                }
                
                // Seleccionar el periodo vigente por defecto en el ddl si existe
                DataRow[] vigentes = ds.Tables[0].Select("vigente = 1");
                if (vigentes.Length > 0)
                {
                    ddlPeriodos.SelectedValue = vigentes[0]["idPeriodo"].ToString();
                }
                
                MarcarUsuariosAsignados();
            }
            else
            {
                ddlPeriodos.Items.Add(new ListItem("No hay periodos creados", ""));
            }
        }

        private void MarcarUsuariosAsignados()
        {
            if (string.IsNullOrEmpty(ddlPeriodos.SelectedValue))
                return;

            int idPeriodo = Convert.ToInt32(ddlPeriodos.SelectedValue);
            odbusuario = new BaseDatos();
            DataSet dsAsignados = odbusuario.listaUsuariosPeriodos(idPeriodo);

            // Desmarcar todos primero
            foreach (GridViewRow row in GridViewUsuarios.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkHabilitado");
                if (chk != null) chk.Checked = false;
            }

            if (dsAsignados != null && dsAsignados.Tables.Count > 0 && dsAsignados.Tables[0].Rows.Count > 0)
            {
                DataTable dtAsignados = dsAsignados.Tables[0];
                foreach (GridViewRow row in GridViewUsuarios.Rows)
                {
                    HiddenField hdnId = (HiddenField)row.FindControl("hdnUsuarioId");
                    CheckBox chk = (CheckBox)row.FindControl("chkHabilitado");

                    if (hdnId != null && chk != null)
                    {
                        string cedula = hdnId.Value;
                        DataRow[] found = dtAsignados.Select("idUsuaruios = '" + cedula.Replace("'", "''") + "'");
                        if (found.Length > 0)
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }
        }

        protected void ddlPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            MarcarUsuariosAsignados();
        }

        protected void btnAddREg_Click(object sender, EventArgs e)
        {
            txtDescripcion.Text = "";
            txtFechaInicio.Text = "";
            txtFechaFin.Text = "";
            chkRegVigente.Checked = false;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtFechaInicio.Text) || string.IsNullOrEmpty(txtFechaFin.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Por favor complete todos los campos.');", true);
                return;
            }

            DateTime inicio = Convert.ToDateTime(txtFechaInicio.Text);
            DateTime fin = Convert.ToDateTime(txtFechaFin.Text);

            if (inicio >= fin)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('La fecha de inicio debe ser menor a la fecha de fin.');", true);
                return;
            }

            odbusuario = new BaseDatos();
            bool ok = odbusuario.gestionarPeriodosAdministrativos(0, txtDescripcion.Text, inicio, fin, chkRegVigente.Checked, 1);

            if (ok)
            {
                CargarPeriodos();
                CargarComboPeriodos();
                MultiView1.ActiveViewIndex = 0;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Periodo creado exitosamente.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Ocurrió un error al crear el periodo.');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                int idPeriodo = Convert.ToInt32(e.CommandArgument);
                Session["idDoc"] = idPeriodo; // Guardar id temporal en la sesión como se hace en el resto del sistema

                odbusuario = new BaseDatos();
                DataSet ds = odbusuario.datosPeriodoAdministrativo(idPeriodo);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    txtEditDescripcion.Text = row["descripcion"].ToString();
                    txtEditFechaInicio.Text = Convert.ToDateTime(row["fecha_inicio"]).ToString("yyyy-MM-dd");
                    txtEditFechaFin.Text = Convert.ToDateTime(row["fecha_fin"]).ToString("yyyy-MM-dd");
                    chkEditVigente.Checked = Convert.ToInt32(row["vigente"]) == 1;

                    mpeEditarRegistro.Show();
                }
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Session["idDoc"] == null) return;

            int idPeriodo = Convert.ToInt32(Session["idDoc"]);
            
            if (string.IsNullOrEmpty(txtEditDescripcion.Text) || string.IsNullOrEmpty(txtEditFechaInicio.Text) || string.IsNullOrEmpty(txtEditFechaFin.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Por favor complete todos los campos.');", true);
                mpeEditarRegistro.Show();
                return;
            }

            DateTime inicio = Convert.ToDateTime(txtEditFechaInicio.Text);
            DateTime fin = Convert.ToDateTime(txtEditFechaFin.Text);

            if (inicio >= fin)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('La fecha de inicio debe ser menor a la fecha de fin.');", true);
                mpeEditarRegistro.Show();
                return;
            }

            odbusuario = new BaseDatos();
            bool ok = odbusuario.gestionarPeriodosAdministrativos(idPeriodo, txtEditDescripcion.Text, inicio, fin, chkEditVigente.Checked, 2);

            if (ok)
            {
                CargarPeriodos();
                CargarComboPeriodos();
                mpeEditarRegistro.Hide();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Periodo modificado exitosamente.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Ocurrió un error al modificar el periodo.');", true);
                mpeEditarRegistro.Show();
            }
        }

        protected void btnGuardarAsignaciones_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPeriodos.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Debe seleccionar un periodo válido.');", true);
                return;
            }

            int idPeriodo = Convert.ToInt32(ddlPeriodos.SelectedValue);
            odbusuario = new BaseDatos();

            bool algunError = false;

            foreach (GridViewRow row in GridViewUsuarios.Rows)
            {
                HiddenField hdnId = (HiddenField)row.FindControl("hdnUsuarioId");
                CheckBox chk = (CheckBox)row.FindControl("chkHabilitado");

                if (hdnId != null && chk != null)
                {
                    string cedula = hdnId.Value;
                    
                    // Si está marcado, lo asignamos (INSERT IGNORE)
                    // Si está desmarcado, lo removemos (DELETE)
                    int proceso = chk.Checked ? 1 : 3;
                    bool ok = odbusuario.gestionarUsuariosPeriodos(cedula, idPeriodo, proceso);
                    if (!ok)
                    {
                        algunError = true;
                    }
                }
            }

            if (!algunError)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Asignaciones de periodo actualizadas correctamente.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Se actualizaron las asignaciones, pero se reportó algún inconveniente.');", true);
            }
            
            MarcarUsuariosAsignados();
        }

        protected void limpiarModalClick(object sender, EventArgs e)
        {
            mpeEditarRegistro.Hide();
        }
    }
}
