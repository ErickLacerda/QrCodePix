using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QrCodePix
{
    public partial class Pix : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGeraPix_Click(object sender, EventArgs e)
        {
            string diretorioQRCode = GerarExibirQrcodePix(GetDadosix());

            imgPix.Src = "\\Arquivos\\" + diretorioQRCode.Split('\\')[diretorioQRCode.Split('\\').Length - 1];
        }

        private DadosPix GetDadosix()
        {
            return new DadosPix()
            {
                Nome = txtNome.Text,
                Cidade = txtCidade.Text,
                Valor = Convert.ToDouble(txtValor.Text),
                ChavePix = txtChavePix.Text,
                TipoChave = "E",
            };
        }

        private string GerarExibirQrcodePix(DadosPix dadosPix)
        {
            string payLoad = new ModelPix().GerarStringPayLoad(dadosPix);

            string diretorio = HttpContext.Current.Server.MapPath("~");

            string caminhoQRCode = new ModelPix().GerarQRCodeLogo(payLoad, diretorio);

            return caminhoQRCode;
        }
    }

    public class DadosPix
    {
        public string Nome { get; set; }
        public string ChavePix { get; set; }
        public double Valor { get; set; }
        public string Cidade { get; set; }
        public string TipoChave { get; set; }
    }
}