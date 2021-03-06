using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.Mitra
{
    public partial class MitraMaint : BasePage
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
        protected DataTable mytableSearch
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mytableSearch" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mytableSearch" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable mytableDetailSearch
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mytableDetailSearch" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mytableDetailSearch" + this.ViewState["_PageID"]] = value; }
        }
        protected MitraDB myMitraDB
        {
            get { isValidLogin(false); return (MitraDB)HttpContext.Current.Session["myMitraDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMitraDB" + this.ViewState["_PageID"]] = value; }
        }
        protected MitraEntity myMitraEntity
        {
            get { isValidLogin(false); return (MitraEntity)HttpContext.Current.Session["myMitraEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMitraEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected IContainer components
        {
            get { isValidLogin(false); return (IContainer)HttpContext.Current.Session["components" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["components" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
                mytableSearch = new DataTable();
                mytableDetailSearch = new DataTable();
                this.myMitraDB = MitraDB.Create(myDBSetting, myLocalDBSetting, dbsession);
                if (!accessright.IsAccessibleByUserID(UserID, "WLC_VIEW_ALL"))
                {
                    mytableSearch = this.myMitraDB.LoadBrowseTable(false, myDBSession.LoginUserID);
                }
                else
                {
                    mytableSearch = this.myMitraDB.LoadBrowseTable(true, myDBSession.LoginUserID);
                }
                gvMain.DataBind();
                accessable();

                if (this.Request.QueryString["MCode"] != null)
                {
                    int indexVal = gvMain.FindVisibleIndexByKeyValue(this.Request.QueryString["MCode"]);
                    gvMain.FocusedRowIndex = indexVal;
                }
                else
                {
                    gvMain.FocusedRowIndex = -1;
                }
            }
        }
        protected void OpenData(DXSSAction Action)
        {
            string updatedQueryString = "";
            try
            {
                DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                if (myrow != null)
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();
                    myMitraEntity = myMitraDB.Edit(Convert.ToInt32(myrow["Mkey"]), Action);
                    Response.Redirect("~/Transactions/Syariah/Mitra/MitraEntry.aspx" + updatedQueryString);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
        }
        protected void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose();
        }
        protected void accessable()
        {
            if (!accessright.IsAccessibleByUserID(UserID, "TICKET_CAN_GRAB"))
            {

            }
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                myMitraEntity = myMitraDB.Entity(DXSSType.MTR);
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                updatedQueryString = "?" + nameValues.ToString();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
            Response.Redirect("~/Transactions/Syariah/Mitra/MitraEntry.aspx" + updatedQueryString);
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            OpenData(DXSSAction.View);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            OpenData(DXSSAction.Edit);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                isValidLogin();
                if (!accessright.IsAccessibleByUserID(UserID, "WLC_VIEW_ALL"))
                {
                    mytableSearch = this.myMitraDB.LoadBrowseTable(false, myDBSession.LoginUserID);
                }
                else
                {
                    mytableSearch = this.myMitraDB.LoadBrowseTable(true, myDBSession.LoginUserID);
                }
                gvMain.DataSource = mytableSearch;
                gvMain.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = mytableSearch;
        }

        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }

        protected void gvMain_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string updatedQueryString = "";
            ASPxGridView gridView = (ASPxGridView)sender;
            string[] callbackParam = e.Parameters.ToString().Split(';');
            (sender as ASPxGridView).JSProperties["cpNewWindowUrl"] = null;
            gvMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            if (callbackParam.Length > 1)
            {
                object paramName = callbackParam[0].ToUpper();
                object paramValue = callbackParam[1].ToUpper();
                gridView.JSProperties["cplblmessageError"] = "";
                gridView.JSProperties["cplblActionButton"] = "";

                switch (callbackParam[0].ToUpper())
                {
                    case "INDEX":
                        int ivisibleindex = 0;
                        ivisibleindex = System.Convert.ToInt16(paramValue);
                        gvMain.JSProperties["cpVisibleIndex"] = ivisibleindex;
                        gridView.KeyFieldName = "MCode";
                        break;
                    case "OPEN":
                        break;
                    case "REFRESH":
                        try
                        {
                            isValidLogin();
                            if (!accessright.IsAccessibleByUserID(UserID, "WLC_VIEW_ALL"))
                            {
                                mytableSearch = this.myMitraDB.LoadBrowseTable(false, myDBSession.LoginUserID);
                            }
                            else
                            {
                                mytableSearch = this.myMitraDB.LoadBrowseTable(true, myDBSession.LoginUserID);
                            }
                            gvMain.DataSource = mytableSearch;
                            gvMain.DataBind();
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                            return;
                        }
                        break;
                    case "DOUBLECLICK":
                        try
                        {
                            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                            if (myrow != null)
                            {
                                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                                nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                                updatedQueryString = "?" + nameValues.ToString();
                                myMitraEntity = myMitraDB.Edit(Convert.ToInt32(myrow["MCode"]), DXSSAction.Edit);
                                ASPxWebControl.RedirectOnCallback("~/Transactions/Syariah/Mitra/MitraEntry.aspx" + updatedQueryString);
                            }
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                            return;
                        }
                        break;
                }
            }
        }

        protected void gvMain_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "Active?")
            {
                e.DisplayText = System.Convert.ToString(e.Value) == "T" ? "YES" : "NO";
            }
        }
    }
}