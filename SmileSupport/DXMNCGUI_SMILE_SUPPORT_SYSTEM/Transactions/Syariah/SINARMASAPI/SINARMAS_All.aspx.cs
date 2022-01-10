using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SINARMAS;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.SINARMASAPI
{
    public partial class SINARMAS_All : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlLocalDBSetting myLocalDBSetting
        {
            get { isValidLogin(false); return (SqlLocalDBSetting)HttpContext.Current.Session["myLocalDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myLocalDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable clientDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["clientDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["clientDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable tmpDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["tmpDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["tmpDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable debDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["debDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["debDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable detailDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["detailDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["detailDtTable" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
                clientDtTable = new DataTable();
                tmpDtTable = new DataTable();
                debDtTable = new DataTable();
                detailDtTable = new DataTable();

                clientDtTable = GetListClient();
                gvClient.DataSource = clientDtTable;
                gvClient.DataBind();

                tmpDtTable = GetTempData("");
                gvTempData.DataSource = tmpDtTable;
                gvTempData.DataBind();

                detailDtTable = null;
                gvDetailresponse.DataSource = detailDtTable;
                gvDetailresponse.DataBind();

            }

            if (!IsCallback)
            {

            }
        }

        DataTable GetListClient()
        {
            string ssql = "SELECT DISTINCT b.LSAGREE, a.[NAME] AS [Client Name], a.INKTP AS [No KTP], a.[NPWP], a.INBORNDT AS [Tgl Lahir], a.INBORNPLC AS [Tempat Lahir], a.ADDRESS1 AS [Alamat] FROM [dbo].[SYS_CLIENT] a INNER JOIN LS_AGREEMENT b ON a.CLIENT=b.LESSEE WHERE a.CLIENT like 'P%' AND b.LSAGREE COLLATE SQL_Latin1_General_CP1_CI_AS NOT IN (SELECT a.LSAGREE_ID FROM trxResultSINARMAS a INNER JOIN LS_AGREEMENT b ON a.LSAGREE_ID = b.LSAGREE COLLATE SQL_Latin1_General_CP1_CI_AS) ORDER BY a.[NAME]";
            //string ssql = @"SELECT b.LSAGREE, a.[NAME] AS [Client Name], a.INKTP AS [No KTP], a.[NPWP], a.INBORNDT AS [Tgl Lahir], a.INBORNPLC AS [Tempat Lahir], a.ADDRESS1 AS [Alamat] 
            //                FROM [dbo].[SYS_CLIENT] a with(NOLOCK)
            //                INNER JOIN LS_AGREEMENT b with(NOLOCK) ON a.CLIENT=b.LESSEE
            //                INNER JOIN LS_AMORTSCHEDULE d with(NOLOCK) on b.LSAGREE = d.LSAGREE
            //                LEFT JOIN LS_ASSETVEHICLE c with(NOLOCK) on b.LSAGREE = c.LSAGREE
            //                WHERE a.CLIENT like 'P%'
            //                AND b.PRODUCT_FACILITY_CODE = 112
            //                AND d.period = 1
            //                AND c.SUM_INSURED is not null 
            //                AND b.LSAGREE NOT IN ( SELECT LSAGREE_ID FROM trxResultSINARMAS with(NOLOCK)  )	
            //                AND b.LSAGREE NOT IN ( select AGREENO FROM ls_insregister with(NOLOCK) where INSPOLICY IS NOT NULL )
            //                ORDER BY a.NAME";
            DataTable resDT = new DataTable();
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(ssql);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;

                SqlDataReader reader = sqlCommand.ExecuteReader();
                resDT.Load(reader);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }

            return resDT;
        }

        protected void gvTempData_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = tmpDtTable;
            (sender as ASPxGridView).FocusedRowIndex = -1;
        }

        protected void gvDebresponse_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = debDtTable;
        }

        protected void gvDetailresponse_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = detailDtTable;
        }

        protected async void btnRequestConfirm_Click(object sender, EventArgs e)
        {
            DataRow myrow = gvTempData.GetDataRow(gvTempData.FocusedRowIndex);

            if (myrow != null)
            {
                string duedate = myrow["st_date"].ToString();
                string clientcode = myrow["CLIENT"].ToString();
                var dtFind = from row in tmpDtTable.AsEnumerable()
                             where row.Field<string>("CLIENT") == clientcode
                             select row;
                var dtreqPOL = dtFind.CopyToDataTable();
                API_SINARMAS APIClass = new API_SINARMAS();
                var reqPol = await APIClass.RequestPolis(dtreqPOL);

                if (duedate == "")
                {
                    apcalert.Text = "Tidak dapat melanjutkan pembukaan Polis, Start Date / End Date Kosong !";
                    apcalert.ShowOnPageLoad = true;
                    return;
                }

                if (reqPol.Substring(0, 6) == "Sukses")
                {
                    //func isi grid
                    detailDtTable = GetDetailResponse();
                    gvDetailresponse.DataSource = detailDtTable;
                    gvDetailresponse.DataBind();

                    //func kosong grid atas
                    tmpDtTable = null;
                    gvTempData.DataSource = tmpDtTable;
                    gvTempData.DataBind();
                }

                else if (reqPol.Substring(0, 6) == "Failed")
                {
                    apcalert.Text = "Invalid Parameter, Failed to fetch data/ Request server error.";
                    apcalert.ShowOnPageLoad = true;
                }

                else
                {
                    apcalert.Text = reqPol;
                    apcalert.ShowOnPageLoad = true;
                }
            }

            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                return;

            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void btnTemplate_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }

        protected void gvClient_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = clientDtTable;
        }

        DataTable GetTempData(string value)
        {
            string ssql = "exec [dbo].[spMNCL_getClient_SINARMAS] '" + value + "'";
            DataTable resDT = new DataTable();
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(ssql);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;

                SqlDataReader reader = sqlCommand.ExecuteReader();
                resDT.Load(reader);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }

            return resDT;
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            string ccode = "";

            switch (callbackParam[0].ToUpper())
            {
                case "LOAD":
                    ccode = callbackParam[1].ToString();
                    tmpDtTable = GetTempData(ccode);
                    break;
            }
        }

        protected DataTable GetDetailResponse()
        {
            DataTable res = new DataTable();
            //string ssql = "select top 1 * from [trxResultSINARMAS] where [USER_ID]=? and POLICY_NO=? order by CREATED_DATE desc";
            //string ssql = "select LSAGREE_ID,POLICY_NO,CONVERT(decimal(18,2),(PREMIUM)) as PREMIUM,CONVERT(decimal(18,2),(COMMISION)) as COMMISION,CONVERT(decimal(18,2),(ADMINFEE)) as ADMINFEE,CONVERT(decimal(18,2),(DISCOUNT))as DISCOUNT,USER_ID,CREATED_DATE from [trxResultSINARMAS] where [USER_ID]=? and POLICY_NO=? order by CREATED_DATE desc";
            string ssql = @"SELECT top 1 b.NAME,(CONVERT(varchar(10),dateadd(month,-1,c.DUEDATE), 110)) AS StartDate,(CONVERT(varchar(10),dateadd(year,5, dateadd(month,-1,c.DUEDATE)), 110)) AS EndDate,
                            a.LSAGREE_ID,a.POLICY_NO,CONVERT(decimal(18,2),(a.PREMIUM)) AS PREMIUM,CONVERT(decimal(18,2),(a.COMMISION)) 
                            AS COMMISION,CONVERT(decimal(18,2),(a.ADMINFEE)) as ADMINFEE,CONVERT(decimal(18,2),(a.DISCOUNT))AS DISCOUNT,a.USER_ID,a.CREATED_DATE
                            FROM [dbo].[trxResultSINARMAS] a 
                            LEFT JOIN LS_AGREEMENT b ON a.LSAGREE_ID = b.LSAGREE COLLATE SQL_Latin1_General_CP1_CI_AS
                            LEFT JOIN LS_AMORTSCHEDULE c ON b.LSAGREE = c.LSAGREE and c.PERIOD = '1'
                            where [USER_ID]=?
                            ORDER BY a.CREATED_DATE DESC";
            res = myDBSetting.GetDataTable(ssql, false, UserID);

            return res;
        }

        protected void btnClearConfirm_Click(object sender, EventArgs e)
        {

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/Syariah/SINARMASAPI/SINARMAS_All.aspx");
        }
    }
}