namespace sitemaCompras.viewsGeneral
{
    using System;
    using System.Data;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using sitemaCompras.Controlers;

    /// <summary>
    /// Bandeja propia del Solicitante: reúne las solicitudes que todavía no entraron al flujo
    /// (borradores guardados con "Por enviar") y las que fueron devueltas para corregir
    /// ("Subsanar"), que son exactamente los dos estados en los que la solicitud le pertenece y
    /// puede editarla. Desde aquí se retoma la edición en RegistroSolicitud.aspx, que es donde vive
    /// el formulario completo; esta vista solo lista y deriva, para no duplicar esa pantalla.
    /// </summary>
    public partial class MisSolicitudes : Page
    {
    	/// <summary>"Por enviar" (16): borrador guardado, aún fuera del flujo.</summary>
    	private const int EstatusBorrador = 16;

    	/// <summary>"Subsanar" (6): devuelta al Solicitante para que la corrija y la reenvíe.</summary>
    	private const int EstatusSubsanar = 6;

    	private BaseDatos odbusuario;

    	protected Label lblId;

    	protected TextBox txtId;

    	protected Label lblTitulo;

    	protected TextBox txtTituloF;

    	protected Button btnFiltrar;

    	protected Button btnLimpiar;

    	protected GridView GridView1;

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		if (Page.IsPostBack)
    		{
    			return;
    		}
    		if (!User.Identity.IsAuthenticated)
    		{
    			Response.Redirect("~/Login.aspx");
    			return;
    		}
    		// Misma puerta que RegistroSolicitud.aspx: solo quienes pueden crear solicitudes.
    		if (((string)Session["type"] != "2") & ((string)Session["type"] != "3"))
    		{
    			Session.Abandon();
    			Session.Clear();
    			FormsAuthentication.SignOut();
    			Response.Redirect("~/login.aspx");
    			return;
    		}
    		CargarMisSolicitudes(null, "");
    	}

    	/// <summary>
    	/// Lista las solicitudes propias en los dos estados editables. Se consulta una vez por estado
    	/// porque filtroSolicitudes admite un solo estatus por llamada, y se unen los resultados.
    	/// </summary>
    	private void CargarMisSolicitudes(int? id, string titulo)
    	{
    		try
    		{
    			string usuario = Convert.ToString(Session["id"]);

    			odbusuario = new BaseDatos();
    			DataSet dsBorradores = odbusuario.filtroSolicitudes(id, "", "", titulo, EstatusBorrador, null, usuario, null, null, null, null, null, null);

    			odbusuario = new BaseDatos();
    			DataSet dsDevueltas = odbusuario.filtroSolicitudes(id, "", "", titulo, EstatusSubsanar, null, usuario, null, null, null, null, null, null);

    			DataTable dt = null;
    			if (dsBorradores != null && dsBorradores.Tables.Count > 0)
    			{
    				dt = dsBorradores.Tables[0];
    			}
    			if (dsDevueltas != null && dsDevueltas.Tables.Count > 0)
    			{
    				if (dt == null)
    				{
    					dt = dsDevueltas.Tables[0];
    				}
    				else
    				{
    					dt.Merge(dsDevueltas.Tables[0]);
    				}
    			}

    			if (dt != null)
    			{
    				// La columna de id se llama "#", por lo que hay que escaparla para ordenar.
    				dt.DefaultView.Sort = "[#] DESC";
    				GridView1.DataSource = dt.DefaultView;
    			}
    			else
    			{
    				GridView1.DataSource = null;
    			}
    			GridView1.DataBind();
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "MisSolicitudes.CargarMisSolicitudes");
    			ErrorHandler.ShowUserError(this, "cargar sus solicitudes", ex.Message);
    		}
    	}

    	protected void btnFiltrar_Click(object sender, EventArgs e)
    	{
    		int valor;
    		int? id = (!string.IsNullOrWhiteSpace(txtId.Text) && int.TryParse(txtId.Text.Trim(), out valor)) ? new int?(valor) : ((int?)null);
    		CargarMisSolicitudes(id, txtTituloF.Text.Trim());
    	}

    	protected void btnLimpiar_Click(object sender, EventArgs e)
    	{
    		txtId.Text = string.Empty;
    		txtTituloF.Text = string.Empty;
    		CargarMisSolicitudes(null, "");
    	}

    	/// <summary>
    	/// Deriva la edición a RegistroSolicitud.aspx, que valida por su cuenta que la solicitud sea
    	/// del usuario y siga en un estado editable antes de cargarla.
    	/// </summary>
    	protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    	{
    		if (e.CommandName != "editarSol")
    		{
    			return;
    		}
    		try
    		{
    			GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    			string seleccion = gvr.Cells[0].Text.Trim();
    			int idSolicitud;
    			if (!int.TryParse(seleccion, out idSolicitud) || idSolicitud <= 0)
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo identificar la solicitud seleccionada.');", addScriptTags: true);
    				return;
    			}
    			Response.Redirect("RegistroSolicitud.aspx?id=" + idSolicitud);
    		}
    		catch (System.Threading.ThreadAbortException)
    		{
    			// Response.Redirect siempre lanza esta excepción; no es un error.
    			throw;
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "MisSolicitudes.GridView1_RowCommand");
    			ErrorHandler.ShowUserError(this, "abrir la solicitud", ex.Message);
    		}
    	}
    }
}
