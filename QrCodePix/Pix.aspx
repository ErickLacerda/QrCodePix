<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pix.aspx.cs" Inherits="QrCodePix.Pix" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>QR-Code Pix Estático</title>
    <link rel="stylesheet" href="bootstrap-5.0.0-beta1-dist/css/bootstrap.css" />
    <style>
        body {
            background-color: #1C1C1C;
        }

        .divPrincipal {
            width: 25%;
            float: left;
            min-height: 100%;
            position: relative;
            margin: 10% 0 0 20%;
        }

        .divCabecalho {
            height: 60px;
            background-color: #4AD997;
            width: 100%;
            font-family: Century Gothic, sans-serif;
            font-weight: bold;
            text-align: center;
            font-size: 40px;
        }

        .divImg {
            float: right;
            width: 50%;
        }

        .imgQrcode {
            margin: 0 0 0 10%;
            width: 300px;
        }

        .imgLogoPix {
            width: 150px;
            float: initial;
            margin-top: 70%;
        }

        .divRodape {
            height: 30px;
            background-color: #4AD997;
            position: absolute;
            bottom: 0;
            width: 100%;
        }
    </style>
</head>

<body runat="server">
    <form runat="server">

        <div class="divCabecalho">QR-Code Pix Estático</div>

        <div runat="server" class="divPrincipal">
            <div class="input-group mb-3">
                <span class="input-group-text">Nome</span>
                <asp:TextBox runat="server" type="text" ID="txtNome" class="form-control" />
            </div>

            <div class="input-group mb-3">
                <span class="input-group-text">Chave Pix</span>
                <asp:TextBox runat="server" type="text" ID="txtChavePix" class="form-control" />
            </div>

            <div class="input-group mb-3">
                <span class="input-group-text">Cidade</span>
                <asp:TextBox runat="server" type="text" ID="txtCidade" class="form-control" />
            </div>

            <div class="input-group mb-3">
                <span class="input-group-text">Valor</span>
                <asp:TextBox runat="server" type="text" ID="txtValor" class="form-control" />
            </div>

            <div>
                <asp:Button runat="server" Text="Gerar QR-Code" ID="btnGeraPix" OnClick="btnGeraPix_Click" class="btn" Style="background-color: #E9ECEF;" />
            </div>
        </div>

        <div class="divImg">
            <img runat="server" id="imgPix" src="imagens/QRCode_Facil.jpg" alt="QR-Code" class="imgQrcode" />
            <img src="imagens/pix-bc-logo.png" alt="Logo Pix" class="imgLogoPix" />
        </div>

        <div class="divRodape"></div>

    </form>
</body>

</html>
