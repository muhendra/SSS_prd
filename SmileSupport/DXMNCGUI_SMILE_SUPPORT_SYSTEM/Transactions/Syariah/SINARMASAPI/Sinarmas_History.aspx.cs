using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.SINARMASAPI
{
    public partial class Sinarmas_History : BasePage
    {

        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }

        protected int PDF64
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["PDF64" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["PDF64" + this.ViewState["_PageID"]] = value; }
        }

        protected string resultFileName
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["resultFileName" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["resultFileName" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myMainTable = new DataTable();
                GetMainTable();
                gvMain.DataBind();

                if (Convert.ToInt32(this.Request.QueryString["id"]) != 0)
                {
                    SqlConnection myconn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM trxResultSINARMAS WHERE id=@id", myconn);
                    sqlCommand.Parameters.AddWithValue("@id", Convert.ToInt32(this.Request.QueryString["id"]));
                    myconn.Open();
                    SqlDataReader dr = sqlCommand.ExecuteReader(); ;
                    if (dr.Read())
                    {
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.ContentType = ".pdf";
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "POLIS SINARMAS_" + dr["POLICY_NO"].ToString() + ".pdf");
                        HttpContext.Current.Response.Charset = "";
                        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        HttpContext.Current.Response.BinaryWrite((byte[])dr["PDF64"]);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                    myconn.Close();
                }
            }
        }

        const string UploadDirectory = "~/Content/UploadControl/";
        string resultFileUrl = String.Empty;
        string name = String.Empty;
        string url = String.Empty;
        long sizeInKilobytes = 0;

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }

        protected void gvMain_Init(object sender, EventArgs e)
        {

        }

        protected void GetMainTable()
        {
            string ssql = @"SELECT b.NAME,(CONVERT(varchar(10),dateadd(month,-1,c.DUEDATE), 110)) AS StartDate,(CONVERT(varchar(10),dateadd(year,5, dateadd(month,-1,c.DUEDATE)), 110)) AS EndDate,
                            a.id,a.LSAGREE_ID,a.POLICY_NO,replace(convert(varchar,cast(floor(a.PREMIUM) as money),1), '.00', '') AS PREMIUM,CONVERT(decimal(18,2),(a.COMMISION)) 
                            AS COMMISION,CONVERT(decimal(18,2),(a.ADMINFEE)) as ADMINFEE,CONVERT(decimal(18,2),(a.DISCOUNT))AS DISCOUNT,a.PDF64,a.USER_ID,a.CREATED_DATE
                            FROM [dbo].[trxResultSINARMAS] a 
                            LEFT JOIN LS_AGREEMENT b ON a.LSAGREE_ID = b.LSAGREE COLLATE SQL_Latin1_General_CP1_CI_AS
                            LEFT JOIN LS_AMORTSCHEDULE c ON b.LSAGREE = c.LSAGREE and c.PERIOD = '1'
                            ORDER BY a.CREATED_DATE DESC";
            myMainTable = dbsetting.GetDataTable(ssql, false);
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadAtt();
        }

        private bool DownloadAtt()
        {
            bool bDownloadAtt = true;
            FileInfo file = new FileInfo(MapPath(UploadDirectory + resultFileName));
            if (file.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "text/plain";
                Response.Flush();
                Response.TransmitFile(file.FullName);
                Response.End();
            }
            return bDownloadAtt;
        }

        protected void gvMain_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "GridbtnDownload")
            {
                try
                {
                    object obj = gvMain.GetRowValues(e.VisibleIndex, gvMain.KeyFieldName);
                    if (obj != null && obj != DBNull.Value)
                    {
                        PDF64 = System.Convert.ToInt32(obj);
                    }
                    ASPxWebControl.RedirectOnCallback(string.Format("Sinarmas_History.aspx?ID=" + PDF64.ToString()));

                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }
    }
}