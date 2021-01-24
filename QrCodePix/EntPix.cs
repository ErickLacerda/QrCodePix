using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QrCodePix
{
    public class EntPix
    {
        //Variaveis com código padrão da informção
        public static string Id_Payload_Format_Indicator => "00";
        public static string Id_Point_Of_Initiation_Method => "01";
        public static string Id_Merchant_Account_Information => "26";
        public static string Id_Merchant_Account_Infomation_Gui => "00";
        public static string Id_Merchant_Account_Infomation_Key => "01";
        public static string Id_Merchant_Account_Information_Description => "02";
        public static string Id_Merchant_Account_Information_Url => "25";
        public static string Id_Merchant_Category_Code => "52";
        public static string Id_Transaction_Currency => "53";
        public static string Id_Transaction_Amount => "54";
        public static string Id_Country_Code => "58";
        public static string Id_Merchant_Name => "59";
        public static string Id_Merchant_City => "60";
        public static string Id_Additional_Field_Template => "62";
        public static string Id_Additional_Field_Template_TxId => "05";
        public static string Id_CRC16 => "63";

        //Chave Pix
        public string pixKey { get; set; }
        //Tipo de Chave Pix
        public string typeKey { get; set; }
        //Descrição do pagamento
        public string description { get; set; }
        //Nome do titular da conta
        public string merchantName { get; set; }
        //Cidade do titular da conta
        public string merchantCity { get; set; }
        //ID da transação
        public string txId { get; set; }
        //Valor da transação
        public double amount { get; set; }
    }
}