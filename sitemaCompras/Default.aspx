<%@ Page Title="Compras" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="sitemaCompras._Default" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title><%: Page.Title %> - Asamblea Nacional</title>
    <!--Logo de tituo de pagina-->
    <link href="userscontent/img/logoAsamblea.png" rel="shortcut icon" type="image/x-icon" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/hojaestilo1.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body class="fondo">
    <form id="form1" runat="server">
        <div class="modal-dialog text-center">
            <div class="col-sm-9 holaestilo1">
                <div class="modal-content">
                    <div class="col-12 user-img">
                        <img class="imagen" src="userscontent/img/logoAsamblea.png" />
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="El usuario no cuenta con perfil autorizado para ingresar a la aplicacion" ForeColor="Red"></asp:Label>
                    <asp:Button ID="Button1" runat="server" Text="Regresar" CssClass="btn btn-warning mt-3 mb-3 mr-4 ml-4 pr-5 pl-5" OnClick="Button1_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
