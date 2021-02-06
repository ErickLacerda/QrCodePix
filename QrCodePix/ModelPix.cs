using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.Presentation;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace QrCodePix
{
    public class ModelPix
    {
        //Método responsável por gerar a string do PayLoad
        public string GerarStringPayLoad(DadosPix dadosPix)
        {
            EntPix entPix = ToEntity(dadosPix);

            string payLoad = CalcularValores(EntPix.Id_Payload_Format_Indicator, "01")
                           + CalcularValores(EntPix.Id_Merchant_Account_Information, GetMerchantAccountInformation(entPix.pixKey))
                           + CalcularValores(EntPix.Id_Merchant_Category_Code, "0000")
                           + CalcularValores(EntPix.Id_Transaction_Currency, "986")
                           + CalcularValores(EntPix.Id_Transaction_Amount, entPix.amount.ToString("F2").Replace(",", "."))
                           + CalcularValores(EntPix.Id_Country_Code, "BR")
                           + CalcularValores(EntPix.Id_Merchant_Name, entPix.merchantName)
                           + CalcularValores(EntPix.Id_Merchant_City, entPix.merchantCity)
                           + CalcularValores(EntPix.Id_Additional_Field_Template, GetAdditionalFieldTemplate())
                           + EntPix.Id_CRC16 + "04";

            return payLoad + CalcularCRC16(payLoad);
        }

        //Método responsável por receber dados para geração de string payload
        public EntPix ToEntity(DadosPix dadosPix)
        {
            return new EntPix()
            {
                merchantName = dadosPix.Nome,
                amount = dadosPix.Valor,
                merchantCity = dadosPix.Cidade,
                typeKey = dadosPix.TipoChave,
                pixKey = dadosPix.ChavePix
            };
        }

        //Método responsável por retornar valor completo de um objeto do payload
        public string CalcularValores(string id, string valor)
        {
            string tamanho = valor.Length.ToString().PadLeft(2, '0');

            return id + tamanho + valor;
        }

        //Método responsável por retornar os valores do campo adicional
        public string GetAdditionalFieldTemplate()
        {
            string txId = CalcularValores(EntPix.Id_Additional_Field_Template_TxId, "***");

            return txId;
        }

        //Método responsável por retornar os valores completos da informação da conta
        public string GetMerchantAccountInformation(string chavePix)
        {
            string gui = CalcularValores(EntPix.Id_Merchant_Account_Infomation_Gui, "br.gov.bcb.pix");
            string pixKey = CalcularValores(EntPix.Id_Merchant_Account_Infomation_Key, chavePix);
            string description = CalcularValores(EntPix.Id_Merchant_Account_Information_Description, "Pix");

            return gui + pixKey + description;
        }

        //Método responsável por calcular o CRC16 do PayLoad
        public string CalcularCRC16(string payLoad)
        {
            Encoding enc = Encoding.UTF8;

            var bytes = enc.GetBytes(payLoad);

            var crcTable = new List<int> {
                0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7, 0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef,
                0x1231, 0x0210, 0x3273, 0x2252, 0x52b5, 0x4294, 0x72f7, 0x62d6, 0x9339, 0x8318, 0xb37b, 0xa35a, 0xd3bd, 0xc39c, 0xf3ff, 0xe3de,
                0x2462, 0x3443, 0x0420, 0x1401, 0x64e6, 0x74c7, 0x44a4, 0x5485, 0xa56a, 0xb54b, 0x8528, 0x9509, 0xe5ee, 0xf5cf, 0xc5ac, 0xd58d,
                0x3653, 0x2672, 0x1611, 0x0630, 0x76d7, 0x66f6, 0x5695, 0x46b4, 0xb75b, 0xa77a, 0x9719, 0x8738, 0xf7df, 0xe7fe, 0xd79d, 0xc7bc,
                0x48c4, 0x58e5, 0x6886, 0x78a7, 0x0840, 0x1861, 0x2802, 0x3823, 0xc9cc, 0xd9ed, 0xe98e, 0xf9af, 0x8948, 0x9969, 0xa90a, 0xb92b,
                0x5af5, 0x4ad4, 0x7ab7, 0x6a96, 0x1a71, 0x0a50, 0x3a33, 0x2a12, 0xdbfd, 0xcbdc, 0xfbbf, 0xeb9e, 0x9b79, 0x8b58, 0xbb3b, 0xab1a,
                0x6ca6, 0x7c87, 0x4ce4, 0x5cc5, 0x2c22, 0x3c03, 0x0c60, 0x1c41, 0xedae, 0xfd8f, 0xcdec, 0xddcd, 0xad2a, 0xbd0b, 0x8d68, 0x9d49,
                0x7e97, 0x6eb6, 0x5ed5, 0x4ef4, 0x3e13, 0x2e32, 0x1e51, 0x0e70, 0xff9f, 0xefbe, 0xdfdd, 0xcffc, 0xbf1b, 0xaf3a, 0x9f59, 0x8f78,
                0x9188, 0x81a9, 0xb1ca, 0xa1eb, 0xd10c, 0xc12d, 0xf14e, 0xe16f, 0x1080, 0x00a1, 0x30c2, 0x20e3, 0x5004, 0x4025, 0x7046, 0x6067,
                0x83b9, 0x9398, 0xa3fb, 0xb3da, 0xc33d, 0xd31c, 0xe37f, 0xf35e, 0x02b1, 0x1290, 0x22f3, 0x32d2, 0x4235, 0x5214, 0x6277, 0x7256,
                0xb5ea, 0xa5cb, 0x95a8, 0x8589, 0xf56e, 0xe54f, 0xd52c, 0xc50d, 0x34e2, 0x24c3, 0x14a0, 0x0481, 0x7466, 0x6447, 0x5424, 0x4405,
                0xa7db, 0xb7fa, 0x8799, 0x97b8, 0xe75f, 0xf77e, 0xc71d, 0xd73c, 0x26d3, 0x36f2, 0x0691, 0x16b0, 0x6657, 0x7676, 0x4615, 0x5634,
                0xd94c, 0xc96d, 0xf90e, 0xe92f, 0x99c8, 0x89e9, 0xb98a, 0xa9ab, 0x5844, 0x4865, 0x7806, 0x6827, 0x18c0, 0x08e1, 0x3882, 0x28a3,
                0xcb7d, 0xdb5c, 0xeb3f, 0xfb1e, 0x8bf9, 0x9bd8, 0xabbb, 0xbb9a, 0x4a75, 0x5a54, 0x6a37, 0x7a16, 0x0af1, 0x1ad0, 0x2ab3, 0x3a92,
                0xfd2e, 0xed0f, 0xdd6c, 0xcd4d, 0xbdaa, 0xad8b, 0x9de8, 0x8dc9, 0x7c26, 0x6c07, 0x5c64, 0x4c45, 0x3ca2, 0x2c83, 0x1ce0, 0x0cc1,
                0xef1f, 0xff3e, 0xcf5d, 0xdf7c, 0xaf9b, 0xbfba, 0x8fd9, 0x9ff8, 0x6e17, 0x7e36, 0x4e55, 0x5e74, 0x2e93, 0x3eb2, 0x0ed1, 0x1ef0
            };

            var crc = 0xFFFF;

            for (var i = 0; i < bytes.Length; i++)
            {
                var c = bytes[i];
                var j = (c ^ (crc >> 8)) & 0xFF;
                crc = crcTable[j] ^ (crc << 8);
            }

            var answer = ((crc ^ 0) & 0xFFFF);
            var hex = ConverterNumParaHex(answer);

            return hex;
        }

        //Método responsável por converter o retorno do calculo CRC16 para Hexadecimal
        private static string ConverterNumParaHex(int n)
        {
            return n.ToString("X4").ToUpper();
        }

        //Método responsável por gerar o QRCode
        public string GerarQRCodeLogo(string qrCode, string diretorio)
        {
            ZXing.BarcodeWriter barcodeWriter = new ZXing.BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions() { Width = 450, Height = 450, Margin = 0, PureBarcode = false };

            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = new BitmapRenderer { Foreground = Color.DarkSlateGray};
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            Bitmap bitmap = barcodeWriter.Write(qrCode);
            Bitmap logo = new Bitmap(diretorio + "\\arquivos\\logo-pix-icone.png");
            Graphics g = Graphics.FromImage(bitmap);

            Rectangle rectangle = new Rectangle(160, 160, 130, 130);

            g.DrawImage(logo, rectangle);

            Image imageQRCode;

            imageQRCode = bitmap;

            Random r = new Random();

            string pathQRCode = diretorio + "\\arquivos\\QRCode" + r.Next(000000, 999999) + ".png";
            string pathTemp = diretorio + "\\arquivos\\QRCode" + r.Next(000000, 999999) + ".png";
            imageQRCode.Save(pathTemp, System.Drawing.Imaging.ImageFormat.Png);

            return pathTemp;
        }
    }
}