namespace sitemaCompras.viewsGeneral
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using sitemaCompras.Controlers;

    public partial class Perfil : Page
    {
    	private BaseDatos odbusuario;

    	protected ScriptManager ScriptManager1;

    	protected UpdatePanel UpdatePanel1;

    	protected Label Label7;

    	protected Label txtId;

    	protected Label Label1;

    	protected Label txtNombre;

    	protected Label Label2;

    	protected Label txtApellido;

    	protected Label Label9;

    	protected Label txtTelefono;

    	protected Label Label3;

    	protected Label txtEmail;

    	protected Label Label4;

    	protected Label txtUnidad;

    	protected Label Label6;

    	protected Label txtTipo;

    	private void Page_PreInit(object sender, EventArgs e)
    	{
    		if (!Page.IsPostBack)
    		{
    			if ((string)Session["type"] == "2")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterSolicitantes.master";
    			}
    			else if ((string)Session["type"] == "3")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterAprobador.master";
    			}
    			else if ((string)Session["type"] == "4")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterRecepcionCompras.master";
    			}
    			else if ((string)Session["type"] == "5")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterCotizador.master";
    			}
    			else if ((string)Session["type"] == "6")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterOtrasRecepciones.master";
    			}
    		}
    	}

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		if (!Page.IsPostBack)
    		{
    			odbusuario = new BaseDatos();
    			DataSet ds = odbusuario.infoUsuarios(Convert.ToString(Session["id"]));
    			txtId.Text = Convert.ToString(Session["id"]);
    			txtNombre.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
    			txtApellido.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
    			txtEmail.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
    			txtTelefono.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
    			txtUnidad.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
    			txtTipo.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
    		}
    	}
    }

}

