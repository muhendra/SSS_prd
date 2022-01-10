using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SINARMAS.Models
{
    public class ResultReqPolisSNM
    {
        public string PolicyNo { get; set; }
        public string CaseID { get; set; }
        public string Discount { get; set; }
        public string PDF64 { get; set; }
        public string HTTPCode { get; set; }
        public decimal Premium { get; set; }
        public decimal AdminFee { get; set; }
        public string StampReceipts { get; set; }
        public string FeedbackMessage { get; set; }
        public string IdTransaction { get; set; }
        public string PDF64_Attachment { get; set; }
        public string StatusPenerbitan { get; set; }
        public decimal Commision { get; set; }
        public decimal PaymentTotal { get; set; }
        public decimal SumPaymentTotal { get; set; }
        public string ConID { get; set; }
        public string StampPolicy { get; set; }
    }
}