using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SINARMAS.Models;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualBasic;
using System.IO;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SINARMAS
{
    public class API_SINARMAS : BasePage
    {
        protected Stream myFs
        {
            get { isValidLogin(false); return (Stream)HttpContext.Current.Session["myFs" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myFs" + this.ViewState["_PageID"]] = value; }
        }

        string secretkey = ConfigurationManager.AppSettings["sinarmas_secretkey"].ToString();
        Uri Polis_SINARMAS = new Uri(ConfigurationManager.AppSettings["sinarmas_uri"].ToString());
        string Polis_User = ConfigurationManager.AppSettings["userauth_sinarmas"].ToString();
        string Polis_Pwd = ConfigurationManager.AppSettings["pwdauth_sinarmas"].ToString();

        public async Task<string> RequestPolis(DataTable dtPol)
        {
            string strResult = "";
            string LSAGREEID = "";

            foreach (DataRow item in dtPol.Rows)
            {
                LSAGREEID = item["LSAGREE"].ToString();

                SinarmasReq mdlReq = new SinarmasReq();
                NbWorkPage mdlNBW = new NbWorkPage();
                Quotation mdlQtn = new Quotation();

                CustomerP mdlCstP = new CustomerP();
                AddressList mdlAdrs = new AddressList();
                AsmTelfax mdlFax = new AsmTelfax();

                PersonList mdlPerson = new PersonList();
                AsmCoverage mdlCov = new AsmCoverage();
                AsmCoverage mdlCov2 = new AsmCoverage();
                AsmHeir mdlHeir = new AsmHeir();
                Policy mdlPol = new Policy();

                var lstTelFax = new List<AsmTelfax>();
                var listCvrg = new List<AsmCoverage>();
                //var listCvrg2 = new List<AsmCoverage>();
                var listHeir = new List<AsmHeir>();
                var listQtn = new List<Quotation>();

                var listCstP = new List<CustomerP>();
                var listpol = new List<Policy>();
                var listaddrs = new List<AddressList>();
                var listprs = new List<PersonList>();

                ResultReqPolisSNM messageResult = new ResultReqPolisSNM();

                foreach (DataRow dr in dtPol.Rows)
                {
                    //Quotation
                    mdlQtn.BusinessCode = item["BusinessCode"].ToString();
                    mdlQtn.BusinessName = item["BusinessName"].ToString();
                    mdlQtn.GroupPanel = item["GroupPanel"].ToString();
                    mdlQtn.AccessCode = item["AccessCode"].ToString();

                    //Customer_P
                    mdlCstP.PyFirstName = item["pyFirstName"].ToString();
                    mdlCstP.PyLastName = item["PyLastName"].ToString();
                    mdlCstP.PyCity = item["PyCity"].ToString();
                    mdlCstP.AsmDateOfBirth = item["AsmDateOfBirth"].ToString();
                    mdlCstP.AsmGender = item["AsmGender"].ToString();
                    mdlCstP.AsmidCard = item["ASMIDCard"].ToString();
                    mdlCstP.PyCompany = item["pyCompany"].ToString();

                    //Policy
                    mdlPol.StartDateTime = item["StartDateTime"].ToString();
                    mdlPol.EndDateTime = item["EndDateTime"].ToString();
                    mdlPol.QqName = item["QQName"].ToString();
                    mdlPol.CustomerType = item["CustomerType"].ToString();
                    mdlPol.TheInsured = item["TheInsured"].ToString();
                    mdlPol.IdTransaction = item["IdTransaction"].ToString();
                    mdlPol.TripType = item["TripType"].ToString();
                    mdlPol.Currency = item["Currency"].ToString();
                    mdlPol.TypeOfPacket = item["TypeOfPacket"].ToString();
                    mdlPol.StatusPenerbitan = item["StatusPenerbitan"].ToString();

                    //AddressList
                    mdlAdrs.AsmAddress = item["ASMAddress"].ToString();
                    mdlAdrs.AsmAddressType = item["AsmAddressType"].ToString();
                    mdlAdrs.AsmZipCode = item["AsmZipCode"].ToString();

                    //Telfax(in AddressList)
                    mdlFax.TelfaxCode = item["TelFaxCode"].ToString();
                    mdlFax.TelfaxNumber = item["TelFaxNumber"].ToString();
                    mdlFax.TelfaxType = item["TelFaxType"].ToString();

                    //mdlAdrs.AsmTelfax.Add(mdlFax);
                    lstTelFax.Add(mdlFax);
                    mdlAdrs.AsmTelfax = lstTelFax;

                    //PersonList
                    mdlPerson.AsmDateOfBirth = item["ASMDateOfBirth_P"].ToString();
                    mdlPerson.AsmHeight = item["ASMHeight"].ToString();
                    mdlPerson.AsmidCard = item["ASMIDCard"].ToString();
                    mdlPerson.AsmJobName = item["ASMJobName"].ToString();
                    mdlPerson.AsmLeftHanded = item["ASMLeftHanded"].ToString();
                    mdlPerson.AsmParticipantStatus = item["ASMParticipantStatus"].ToString();
                    mdlPerson.AsmWeight = item["ASMWeight"].ToString();
                    mdlPerson.StartDateTime = item["StartDateTime_P"].ToString();
                    mdlPerson.EndDateTime = item["EndDateTime_P"].ToString();
                    mdlPerson.PyFullName = item["pyFullName"].ToString();

                    //Coverage-PersonList #1
                    mdlCov.Coverage = item["Coverage"].ToString();
                    mdlCov.CoverageNote = item["CoverageNote"].ToString();
                    mdlCov.DiscountPercentage = item["DiscountPercentage"].ToString();
                    mdlCov.CalculateMethod = item["CalculateMethod"].ToString();
                    mdlCov.Rate = item["Rate"].ToString();
                    mdlCov.Tsi = item["TSI"].ToString();
                    //listCvrg[0] = mdlCov;
                    listCvrg.Add(mdlCov);

                    //Coverage-PersonList #2
                    mdlCov2.Coverage = item["Coverage2"].ToString();
                    mdlCov2.CoverageNote = item["CoverageNote2"].ToString();
                    mdlCov2.DiscountPercentage = item["DiscountPercentage2"].ToString();
                    mdlCov2.CalculateMethod = item["CalculateMethod2"].ToString();
                    mdlCov2.Rate = item["Rate2"].ToString();
                    mdlCov2.Tsi = item["TSI2"].ToString();
                    //listCvrg[1] = mdlCov2;
                    listCvrg.Add(mdlCov2);

                    //mdlPerson.AsmCoverage[0] = mdlCov;
                    //mdlPerson.AsmCoverage[1] = mdlCov2;

                    mdlPerson.AsmCoverage = listCvrg;

                    //lstTelFax.Add(mdlFax);
                    //mdlAdrs.AsmTelfax = lstTelFax;

                    //ASMHeir-PersonList
                    mdlHeir.AsmDateOfBirth = item["ASMDateOfBirth_H"].ToString();
                    mdlHeir.AsmGender = item["ASMGender_P"].ToString();
                    mdlHeir.AsmHeirPercentage = item["ASMHeirPercentage"].ToString();
                    mdlHeir.AsmRelationName = item["ASMRelationName"].ToString();
                    mdlHeir.PyFullName = item["pyFullName_H"].ToString();
                    //mdlPerson.AsmHeir.Add(mdlHeir);

                    listHeir.Add(mdlHeir);
                    mdlPerson.AsmHeir = listHeir;

                    //Final Add To Header
                    mdlNBW.Quotation = mdlQtn;
                    mdlNBW.CustomerP = mdlCstP;
                    mdlNBW.Policy = mdlPol;
                    //mdlNBW.AddressList.Add(mdlAdrs);
                    //mdlNBW.PersonList.Add(mdlPerson);
                    var lstAdrs = new List<AddressList>();
                    lstAdrs.Add(mdlAdrs);
                    mdlNBW.AddressList = lstAdrs;

                    var lstPrsn = new List<PersonList>();
                    lstPrsn.Add(mdlPerson);
                    mdlNBW.PersonList = lstPrsn;

                    mdlReq.NbWorkPage = mdlNBW;
                    //var lstAll = new List<SinarmasReq>();
                    //lstAll.Add(mdlNBW);
                    //mdlReq.NbWorkPage = lstAll;

                }

                string jsonString = JsonConvert.SerializeObject(mdlReq);
                string stringResponse = String.Empty;

                try
                {
                    var response = new HttpResponseMessage();
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    HttpClientHandler clientHandler = new HttpClientHandler();
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", secretkey);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    response = await client.PostAsync(Polis_SINARMAS, httpContent);
                    stringResponse = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    messageResult = JsonConvert.DeserializeObject<ResultReqPolisSNM>(stringResponse);

                    if (messageResult.HTTPCode == "200")
                    {
                        //strResult = messageResult.PolicyNo.ToString();
                        strResult = messageResult.FeedbackMessage.ToString();
                        InsertResultSINARMAS(messageResult, LSAGREEID);
                    }

                    else if (messageResult.HTTPCode == "400")
                    {
                        strResult = messageResult.FeedbackMessage.ToString();
                        //InsertLogData(Polis_SINARMAS.ToString(), stringResponse, 0, jsonString, strResult, "sysadmin");
                    }

                    else if (messageResult.FeedbackMessage == "Failed to fetch data/ Request server error.")
                    {
                        strResult = messageResult.FeedbackMessage.ToString();
                    }

                    else
                    {
                        strResult += "- Error connecting to API";
                        //InsertLogData(Polis_SINARMAS.ToString(), stringResponse, 0, jsonString, strResult, "sysadmin");
                    }
                }

                catch (Exception ex)
                {
                    strResult = "ERROR: " + messageResult.FeedbackMessage;
                    //InsertLogData(Polis_SINARMAS.ToString(), stringResponse, 1, jsonString, strResult, "sysadmin");
                }

            }
            return strResult;
        }

        public void InsertLogData(string api_url, string result, int is_error, string postval, string retval, string user)
        {
            //api_url = api_url.Replace("https://", "https:///");
            result = result.Replace("'", "");
            postval = postval.Replace("'", "");

            //string api_url = request.Url.Scheme + @":/" + request.ApplicationPath + @"/api/InfoAgreement";
            string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            string ssql = "INSERT INTO [dbo].[API_Log] SELECT '" + api_url + "', '" + result + "', '" + retval + "'," + is_error + ",'" +
                UserID + "',GETDATE(),'" + postval + "'";
            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(ssql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void InsertResultSINARMAS(ResultReqPolisSNM messageResult, string LSAGREE)
        {
            if (messageResult != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
                SqlConnection myconn = new SqlConnection(connString);
                string pdf_en = messageResult.PDF64;
                //var barr = Encoding.ASCII.GetBytes(pdf_en);
                byte[] imageBytes = Convert.FromBase64String(pdf_en);
                //byte[] bytes = System.Convert.FromBase64String(stringInBase64);
                Policy mdlPol = new Policy();
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[trxResultSINARMAS]([ADMINFEE],[SUM_PAYMENT_TOTAL],[POLICY_NO],[STATUS_PENERBITAN],[STAMP_POLICY],[CON_ID],[IDTRANSACTION],[STAMP_RECEIPT],[FEEDBACK_MESSAGE],[PAYMENT_TOTAL],[DISCOUNT],[CASEID],[HTTP_CODE],[PDF_ATTACHMENT],[PDF64],[COMMISION],[PREMIUM],[USER_ID],[CREATED_DATE],[LSAGREE_ID])
                                                        VALUES (@ADMINFEE,@SUM_PAYMENT_TOTAL,@POLICY_NO,@STATUS_PENERBITAN,@STAMP_POLICY,@CON_ID,@IDTRANSACTION,@STAMP_RECEIPT,@FEEDBACK_MESSAGE,@PAYMENT_TOTAL,@DISCOUNT,@CASEID,@HTTP_CODE,@PDF_ATTACHMENT,@PDF64,@COMMISION,@PREMIUM,@USER_ID,@CREATED_DATE,@LSAGREE_ID)");
                sqlCommand.Connection = myconn;
                myconn.Open();
                try
                {
                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@ADMINFEE", SqlDbType.Decimal);
                    sqlParameter1.Value = messageResult.AdminFee;

                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@SUM_PAYMENT_TOTAL", SqlDbType.Decimal);
                    sqlParameter2.Value = messageResult.SumPaymentTotal;

                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@POLICY_NO", SqlDbType.VarChar);
                    sqlParameter3.Value = messageResult.PolicyNo;

                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@STATUS_PENERBITAN", SqlDbType.VarChar);
                    sqlParameter4.Value = messageResult.StatusPenerbitan;

                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@STAMP_POLICY", SqlDbType.VarChar);
                    sqlParameter5.Value = messageResult.StampPolicy;

                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@CON_ID", SqlDbType.VarChar);
                    sqlParameter6.Value = messageResult.ConID;

                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@IDTRANSACTION", SqlDbType.VarChar);
                    sqlParameter7.Value = messageResult.IdTransaction;

                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@STAMP_RECEIPT", SqlDbType.VarChar);
                    sqlParameter8.Value = messageResult.StampReceipts;

                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@FEEDBACK_MESSAGE", SqlDbType.VarChar);
                    sqlParameter9.Value = messageResult.FeedbackMessage;

                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@PAYMENT_TOTAL", SqlDbType.Decimal);
                    sqlParameter10.Value = messageResult.PaymentTotal;

                    SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@DISCOUNT", SqlDbType.Decimal);
                    sqlParameter11.Value = messageResult.Discount;

                    SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@CASEID", SqlDbType.VarChar);
                    sqlParameter12.Value = messageResult.CaseID;

                    SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@HTTP_CODE", SqlDbType.VarChar);
                    sqlParameter13.Value = messageResult.HTTPCode;

                    SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@PDF_ATTACHMENT", SqlDbType.VarChar);
                    sqlParameter14.Value = DBNull.Value;
                    //messageResult.PDF64_Attachment;

                    SqlParameter sqlParameter15 = sqlCommand.Parameters.Add("@PDF64", SqlDbType.Binary);
                    sqlParameter15.Value = imageBytes;

                    SqlParameter sqlParameter16 = sqlCommand.Parameters.Add("@COMMISION", SqlDbType.Decimal);
                    sqlParameter16.Value = messageResult.Commision;

                    SqlParameter sqlParameter17 = sqlCommand.Parameters.Add("@PREMIUM", SqlDbType.Decimal);
                    sqlParameter17.Value = messageResult.Premium;

                    SqlParameter sqlParameter18 = sqlCommand.Parameters.Add("@USER_ID", SqlDbType.VarChar);
                    sqlParameter18.Value = UserID;

                    SqlParameter sqlParameter19 = sqlCommand.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);
                    sqlParameter19.Value = DateTime.Now;

                    SqlParameter sqlParameter20 = sqlCommand.Parameters.Add("@LSAGREE_ID", SqlDbType.VarChar);
                    sqlParameter20.Value = LSAGREE;

                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    myconn.Close();
                }
            }
        }

    }
}