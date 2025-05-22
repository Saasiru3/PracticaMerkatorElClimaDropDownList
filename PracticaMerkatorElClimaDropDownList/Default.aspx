<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PracticaMerkatorElClimaDropDownList._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Validar Municipio y Consultar Clima España</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 30px; }
        label { font-weight: bold; }
        .form-control { width: 250px; padding: 5px; margin-bottom: 10px; }
        #imgIcono { margin-top: 10px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Validar Municipio y Consultar Clima (España)</h2>

            <asp:Label ID="lblProvincia" runat="server" Text="Provincia:"></asp:Label><br />
            <asp:DropDownList ID="ddlCiudades" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCiudades_SelectedIndexChanged" CssClass="form-control">
            </asp:DropDownList>
            <br />

            <asp:Label ID="lblMunicipio" runat="server" Text="Municipio:"></asp:Label><br />
            <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control"></asp:TextBox>
            <br />

            <asp:Button ID="btnBuscar" runat="server" Text="Buscar Clima" OnClick="btnBuscar_Click" CssClass="form-control" />
            <br />

            <asp:Label ID="lblResultado" runat="server" EnableHtmlEncode="false"></asp:Label>
            <br />

            <asp:Image ID="imgIcono" runat="server" Visible="false" />
        </div>
    </form>
</body>
</html>
