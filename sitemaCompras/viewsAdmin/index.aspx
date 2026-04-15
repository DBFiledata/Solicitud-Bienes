<%@ Page Language="C#" Title="Inicio" AutoEventWireup="true" MasterPageFile="~/viewsAdmin/adminUsers.Master" CodeBehind="index.aspx.cs" Inherits="sitemaCompras.viewsAdmin.index" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <header class="masthead  bg-white text-dark text-center align-items-stretch h-auto mb-5">
        <div class="container d-flex align-items-center flex-column pb-5">
            <!-- Masthead Avatar Image -->
            <img class="mb-4 mt-5 embed-responsive" style="max-width:650px; max-height:400px; min-height:325px;" src="../userscontent/img/adminImg.png" alt="" />
            <!-- Masthead Heading -->
            <h1 class="masthead-heading text-uppercase mb-0">Le damos la bienvenida</h1>
            <!-- Icon Divider -->
            <div class="divider-custom1 mb-3">
                <div class="divider-custom-line"></div>
                <div class="divider-custom-icon">
                    <i class="fa fa-file-upload"></i>
                    <i class="fa fa-cogs"></i>
                    <i class="fa fa-file-download"></i>
                </div>
                <div class="divider-custom-line"></div>
            </div>
            <!-- Masthead Subheading -->
            <h4 class="masthead-heading mb-5">Sistema para gestion de compras</h4>

        </div>
    </header>
</asp:Content>
