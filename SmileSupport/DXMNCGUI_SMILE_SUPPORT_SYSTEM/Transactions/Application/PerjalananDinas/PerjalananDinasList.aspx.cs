using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.PerjalananDinas
{
    public partial class PerjalananDinasList : BasePage
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
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myApprovalTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myRealisasiTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myRealisasiTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myRealisasiTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDetailTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                myMainTable = new DataTable();
                myDetailTable = new DataTable();
                myApprovalTable = new DataTable();
                myRealisasiTable = new DataTable();
                GetMainTable();
                gvMain.DataBind();
                BindingMaster();

                GetApprovalTable();
                gvApprovalList.DataBind();

                if (myApprovalTable.Rows.Count > 0)
                {
                    btnApprovalList.Text += " (" + Convert.ToString(myApprovalTable.Rows.Count) + ")";
                }

                if (myRealisasiTable.Rows.Count > 0)
                {
                    btnRealisasiList.Text += " (" + Convert.ToString(myRealisasiTable.Rows.Count) + ")";
                }
            }
        }

        protected void GetMainTable()
        {
            string ssql = @"SELECT DISTINCT a.* FROM trxPerjalananDinas a
                            LEFT JOIN trxPerjalananDinasApprovalList b ON a.DocKey = b.DocKey
                            WHERE a.NIK = ISNULL(?,'') OR b.NIK = ISNULL(?,'')
                            ORDER BY a.CRE_DATE DESC";
            int IsUserAdmin = IsSuperAdmin();

            if(IsUserAdmin == 1)
            {
                ssql = @"SELECT DISTINCT a.* FROM trxPerjalananDinas a
                            LEFT JOIN trxPerjalananDinasApprovalList b ON a.DocKey = b.DocKey
                            ORDER BY a.CRE_DATE DESC";
            }
            
            myMainTable = myLocalDBSetting.GetDataTable(ssql, false, UserID, UserID);
        }
        
        private void BindingMaster()
        {

        }

        protected void gvMain_Init(object sender, EventArgs e)
        {

        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerjalananDinasDoc.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            GetMainTable();
            string updatedQueryString = "";
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            if (myrow != null)
            {
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("Key", myrow["DocKey"].ToString());
                updatedQueryString = "?" + nameValues.ToString();
                nameValues.Set("Action", "View");
                updatedQueryString = "?" + nameValues.ToString();
                Response.Redirect("PerjalananDinasDoc.aspx" + updatedQueryString);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                return;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            string ssql = "select top 1 a.CODE,a.DESCS,a.ADDRESS from SYS_TBLEMPLOYEE a inner join SYS_COMPANY b on a.C_CODE=b.C_CODE WHERE a.CODE=?";
            DataTable dtUser = myDBSetting.GetDataTable(ssql, false, UserID);
            if (myrow != null)
            {
                if (myrow["NIK"].ToString() == UserID)
                {
                    if (myrow["Status"].ToString() == "NEW" || myrow["Status"].ToString() == "ON BUSSINESS TRIP")
                    {

                        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                        nameValues.Set("Key", myrow["DocKey"].ToString());
                        updatedQueryString = "?" + nameValues.ToString();

                        nameValues.Set("Action", "Edit");
                        updatedQueryString = "?" + nameValues.ToString();
                        Response.Redirect("PerjalananDinasDoc.aspx" + updatedQueryString);
                    }

                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Hanya SPD Dengan Status NEW & ON BUSSINESS TRIP yang dapat Diubah." + "');", true);
                        return;
                    }
                }

                else if (dtUser.Rows[0]["CODE"].ToString() == "1906013" && myrow["Status"].ToString() == "ON REVIEW BY HRD")
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", myrow["DocKey"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();

                    nameValues.Set("Action", "Edit");
                    updatedQueryString = "?" + nameValues.ToString();
                    Response.Redirect("PerjalananDinasDoc.aspx" + updatedQueryString);
                }

                else if (dtUser.Rows[0]["CODE"].ToString() == "2009023" && myrow["Status"].ToString() == "ON REVIEW BY HRD")
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", myrow["DocKey"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();

                    nameValues.Set("Action", "Edit");
                    updatedQueryString = "?" + nameValues.ToString();
                    Response.Redirect("PerjalananDinasDoc.aspx" + updatedQueryString);
                }

                else if (dtUser.Rows[0]["CODE"].ToString() == "2111048" && myrow["Status"].ToString() == "ON REVIEW BY HRD")
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", myrow["DocKey"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();

                    nameValues.Set("Action", "Edit");
                    updatedQueryString = "?" + nameValues.ToString();
                    Response.Redirect("PerjalananDinasDoc.aspx" + updatedQueryString);
                }

                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Cannot Edit this row.." + "');", true);
                    return;
                }
            }

            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                return;
            }
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string updatedQueryString = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlLocalDBSetting localdbsetting = this.myLocalDBSetting;

            string strmessageError = "";

            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];

            switch (callbackParam[0].ToUpper())
            {
                case "REFRESH":
                    break;
                case "SHOW":
                    try
                    {
                        DataRow myapprovalrow = gvApprovalList.GetDataRow(gvApprovalList.FocusedRowIndex);

                        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                        nameValues.Set("Key", myapprovalrow["DocKey"].ToString());
                        updatedQueryString = "?" + nameValues.ToString();

                        nameValues.Set("Action", "Approval");
                        updatedQueryString = "?" + nameValues.ToString();
                        //Response.Redirect("RequestReleaseDoc.aspx" + updatedQueryString);
                        ASPxWebControl.RedirectOnCallback("~/Transactions/Application/PerjalananDinas/PerjalananDinasDoc.aspx" + updatedQueryString);
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        return;
                    }
                    break;
                case "SHOW_REALISASI":
                    try
                    {
                        DataRow myapprovalrow = gvRealisasiApprovalList.GetDataRow(gvRealisasiApprovalList.FocusedRowIndex);

                        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                        nameValues.Set("Key", myapprovalrow["DocKey"].ToString());
                        updatedQueryString = "?" + nameValues.ToString();

                        nameValues.Set("Action", "Approval");
                        updatedQueryString = "?" + nameValues.ToString();
                        //Response.Redirect("RequestReleaseDoc.aspx" + updatedQueryString);
                        ASPxWebControl.RedirectOnCallback("~/Transactions/Application/PerjalananDinas/PerjalananDinasDoc.aspx" + updatedQueryString);
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        return;
                    }
                    break;
            }
        }

        protected void gvApprovalList_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myApprovalTable;
        }

        protected void gvApprovalList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void btnApprovalList_Click(object sender, EventArgs e)
        {
            GetApprovalTable();
            gvApprovalList.DataSource = myApprovalTable;
            gvApprovalList.DataBind();

            apcApproval.ShowOnPageLoad = true;
        }

        protected void GetApprovalTable()
        {
            string ssql = "exec dbo.spSmileSupport_GetApprovalListPerjalananDinas ?, ?";

            myApprovalTable = myDBSetting.GetDataTable(ssql, false, "Pengajuan Approval", UserID);

            myRealisasiTable = myDBSetting.GetDataTable(ssql, false, "Realisasi Approval", UserID);
        }

        protected void gvRealisasiApprovalList_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myRealisasiTable;
        }

        protected void gvRealisasiApprovalList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void btnRealisasiList_Click(object sender, EventArgs e)
        {
            GetApprovalTable();
            gvRealisasiApprovalList.DataSource = myRealisasiTable;
            gvRealisasiApprovalList.DataBind();

            apcRealisasi.ShowOnPageLoad = true;
        }

        protected int IsSuperAdmin()
        {
            int res = 0;
            string ssql = "select * from AccessRight where CMDid IN ('IS_SUPER_ADMIN','SPD_SHOW_ALL') and NIK =?";
            DataTable dtIsADMIN = myLocalDBSetting.GetDataTable(ssql, false, this.UserID);
            if(dtIsADMIN.Rows.Count > 0)
            {
                res = 1;
            }

            return res;
        }
    }
}