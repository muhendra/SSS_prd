using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess;
using Newtonsoft.Json;
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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.PerjalananDinas
{
    public partial class PerjalananDinasDoc : BasePage
    {
        protected DataTable branchDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["clientDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["clientDtTable" + this.ViewState["_PageID"]] = value; }
        }

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

        protected DataTable mySPDDOCTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mySPDDOCTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mySPDDOCTable" + this.ViewState["_PageID"]] = value; }
        }

        protected DataSet myds
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myds" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myds" + this.ViewState["_PageID"]] = value; }
        }
        protected string strKey
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]] = value; }
        }
        protected DXSSAction myAction
        {
            get { isValidLogin(false); return (DXSSAction)HttpContext.Current.Session["myAction" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAction" + this.ViewState["_PageID"]] = value; }
        }

        protected ApplicationEntity myApplicationEntity
        {
            get { isValidLogin(false); return (ApplicationEntity)HttpContext.Current.Session["myApplicationEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApplicationEntity" + this.ViewState["_PageID"]] = value; }
        }

        protected DataTable myDetailTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]] = value; }
        }

        protected DataTable myDetailRealisasiTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailRealisasiTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailRealisasiTable" + this.ViewState["_PageID"]] = value; }
        }

        protected DataTable myApprovalTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]] = value; }
        }

        protected DataTable myApprovalRealisasiTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApprovalRealisasiTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApprovalRealisasiTable" + this.ViewState["_PageID"]] = value; }
        }

        protected string sizeText
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["sizeText" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["sizeText" + this.ViewState["_PageID"]] = value; }
        }

        protected string resultFileName
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["resultFileName" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["resultFileName" + this.ViewState["_PageID"]] = value; }
        }

        protected string resultFilePath
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["resultFilePath" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["resultFilePath" + this.ViewState["_PageID"]] = value; }
        }

        protected string resultExtension
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["resultExtension" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["resultExtension" + this.ViewState["_PageID"]] = value; }
        }

        protected Stream myFs
        {
            get { isValidLogin(false); return (Stream)HttpContext.Current.Session["myFs" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myFs" + this.ViewState["_PageID"]] = value; }
        }

        protected int FileDocID
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["FileDocID" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["FileDocID" + this.ViewState["_PageID"]] = value; }
        }

        const string UploadDirectory = "~/Content/UploadControl/";
        string resultFileUrl = String.Empty;
        string name = String.Empty;
        string url = String.Empty;
        long sizeInKilobytes = 0;

        //Generate Doc Key Header
        protected DBRegistry myDBReg;

        public DBRegistry DBReg
        {
            get
            {
                return this.myDBReg;
            }
        }

        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new PerjalananDinasDocKey());
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

                mySPDDOCTable = new DataTable();
                myMainTable = new DataTable();
                myDetailTable = new DataTable();

                myDetailRealisasiTable = new DataTable();
                myApprovalTable = new DataTable();
                myApprovalRealisasiTable = new DataTable();
                branchDtTable = new DataTable();
                myAction = DXSSAction.New;

                myDBReg = DBRegistry.Create(myLocalDBSetting);

                if (this.Request.QueryString["Key"] != null)
                {
                    strKey = this.Request.QueryString["Key"].ToString();
                    string actionType = this.Request.QueryString["Action"].ToString();
                    switch (actionType)
                    {
                        case "Edit":
                            myAction = DXSSAction.Edit;
                            break;
                        case "Approval":
                            myAction = DXSSAction.Approve;
                            break;
                        case "View":
                            myAction = DXSSAction.View;
                            break;
                    }
                    mySPDDOCTable = myLocalDBSetting.GetDataTable("select b.DocNo as 'DocumentNo', a.* from DocumentUploadSPD a left join trxPerjalananDinas b on a.DocKey = b.DocKey where a.DocKey=?", false, strKey);
                    gvMain.DataSource = mySPDDOCTable;
                    gvMain.DataBind();
                }

                //GetMainTable();
                //gvMain.DataBind();
                setuplookupedit();
                BindingMaster();
                BindingListJabatan();
                Accessable();


                if (Convert.ToInt32(this.Request.QueryString["ID"]) != 0)
                {
                    SqlConnection myconn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlLocalConnectionString"].ConnectionString);
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM DocumentUploadSPD WHERE ID=@ID", myconn);
                    sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(this.Request.QueryString["ID"]));
                    myconn.Open();
                    SqlDataReader dr = sqlCommand.ExecuteReader(); ;
                    if (dr.Read())
                    {
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.ContentType = dr["Ext"].ToString();
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + dr["SPDNo"].ToString() + "/" + dr["Type"].ToString() + dr["Ext"].ToString());
                        HttpContext.Current.Response.Charset = "";
                        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        HttpContext.Current.Response.BinaryWrite((byte[])dr["FileDoc"]);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                    myconn.Close();
                }
            }
        }

        private void SetApplication(ApplicationEntity ApplicationEntity)
        {
            if (this.myApplicationEntity != ApplicationEntity)
            {
                if (ApplicationEntity != null)
                {
                    this.myApplicationEntity = ApplicationEntity;
                }
                myds = myApplicationEntity.myDataSet;
                myds.Tables[1].DefaultView.Sort = "DtlKey";
                gvDetail.DataBind();
                setuplookupedit();
                BindingMaster();
            }
        }

        private void setuplookupedit()
        {
            if (myAction == DXSSAction.New)
            {
                //generate dockey
                strKey = DocKeyUniqueKey().ToString();

                myDetailTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasDetailBudget where DtlKey = 0", false);
                gvDetail.DataSource = myDetailTable;
                gvDetail.DataBind();

                myApprovalTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DtlKey = 0", false);
                //insert head of user
                AddHeadOfUser();
                gvApproval.DataSource = myApprovalTable;
                gvApproval.DataBind();

                mySPDDOCTable = myLocalDBSetting.GetDataTable("select b.DocNo as 'DocumentNo', a.* from DocumentUploadSPD a left join trxPerjalananDinas b on a.DocKey = b.DocKey where a.DocKey=?", false, strKey);
                gvMain.DataSource = mySPDDOCTable;
                gvMain.DataBind();
            }
        }

        private void BindingMaster()
        {
            if (myAction == DXSSAction.New)
            {

                string ssql = "select top 1 a.CODE,a.DESCS,b.C_NAME from SYS_TBLEMPLOYEE a inner join SYS_COMPANY b on a.C_CODE=b.C_CODE WHERE a.CODE=?";
                DataTable dtUser = myDBSetting.GetDataTable(ssql, false, UserID);

                nik_emp.Value = dtUser.Rows[0]["CODE"];
                txtName.Value = dtUser.Rows[0]["DESCS"];
                spd_charge.Value = dtUser.Rows[0]["C_NAME"];
                //gvCharge.Value = dtUser.Rows[0]["C_NAME"];
                toPNJ.Value = 0;

                //branchDtTable = GetListBranch();
                //gvCharge.DataSource = branchDtTable;
                //gvCharge.DataBind();
            }

            if (myAction == DXSSAction.View || myAction == DXSSAction.Approve)
            {
                DataTable dtSPDDocPnj = getViewSPDDoc(strKey);
                DataRow dr = dtSPDDocPnj.Rows[0];

                SPDNo.Value = dr["DocNo"];
                txtName.Value = dr["Name"];
                nik_emp.Value = dr["NIK"];
                pos_emp3.Value = dr["Jabatan"];

                spd_dept.Value = dr["Dept"];
                pos_emp.Value = dr["Jabatan"];
                spd_charge.Value = dr["PembebananBiaya"];

                negaraasal2.Value = dr["FromTujuan"];
                status_spd.Value = dr["Status"];
                DocNo.Value = dr["DocNo"];

                tglKeberangkatan2.Value = dr["StartDate2"];
                tglKepulangan2.Value = dr["EndDate2"];
                mandays2.Value = dr["JumlahHari2"];
                byvehicle2.Value = dr["Kendaraan2"];

                DataTable dtlpnj = getViewDtlRel(strKey, "Pengajuan");
                DataRow dtp = dtlpnj.Rows[0];

                tglKeberangkatan.Value = dtp["StartDate"];
                tglKepulangan.Value = dtp["EndDate"];
                mandays1.Value = dtp["JumlahHari"];

                negaraasal1.Value = dtp["FromTujuan"];
                negaratujuan1.Value = dtp["Tujuan"];
                byvehicle1.Value = dtp["Kendaraan"];

                if (status_spd.Value.ToString() == "NEW" || status_spd.Value.ToString() == "REQUEST APPROVAL" || status_spd.Value.ToString() == "REJECT PENGAJUAN")
                {
                    myDetailTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasDetailBudget where DocKey=?", false, strKey);
                    gvDetail.DataSource = myDetailTable;
                    gvDetail.DataBind();

                    myApprovalTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DocKey=?", false, strKey);
                    gvApproval.DataSource = myApprovalTable;
                    gvApproval.DataBind();

                    toPNJ.Value = dr["TotalPengajuan"];

                }

                if (status_spd.Value.ToString() == "REALIZATION APPROVAL" || status_spd.Value.ToString() == "COMPLETE" || status_spd.Value.ToString() == "REJECT REALISASI" || status_spd.Value.ToString() == "ON REVIEW BY HRD")
                {
                    myDetailTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasDetailBudget where DocKey=? and TypeSPD='Pengajuan Approval'", false, strKey);
                    gvDetail.DataSource = myDetailTable;
                    gvDetail.DataBind();

                    myApprovalTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DocKey=? and TypeApproval='Pengajuan Approval'", false, strKey);
                    gvApproval.DataSource = myApprovalTable;
                    gvApproval.DataBind();

                    string ssql1 = "select * from trxPerjalananDinasDetailBudget where DocKey=? and TypeSPD='Pengajuan Realisasi'";
                    myDetailRealisasiTable = myLocalDBSetting.GetDataTable(ssql1, false, strKey);
                    gvRealisasiPerjalananDinas.DataSource = myDetailRealisasiTable;
                    gvRealisasiPerjalananDinas.DataBind();

                    myApprovalRealisasiTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DocKey=? and TypeApproval='Pengajuan Realisasi'", false, strKey);
                    gvApprovalRealisasi.DataSource = myApprovalRealisasiTable;
                    gvApprovalRealisasi.DataBind();

                    DataTable dtlrel = getViewDtlRel(strKey, "Realisasi");
                    DataRow dtt = dtlrel.Rows[0];

                    tglKeberangkatan2.Value = dtt["StartDate"];
                    tglKepulangan2.Value = dtt["EndDate"];
                    mandays2.Value = dtt["JumlahHari"];

                    negaraasal2.Value = dtt["FromTujuan"];
                    negaratujuan2.Value = dtt["Tujuan"];
                    byvehicle2.Value = dtt["Kendaraan"];
                    toPNJ.Value = dr["TotalPengajuan"];
                    toREL.Value = dr["TotalRealisasi"];
                }

                if (status_spd.Value.ToString() == "ON BUSSINESS TRIP")
                {
                    DataTable dtDtlRealisasi = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasDetail where DocKey =? and TypeSPD = 'Realisasi'", false, strKey);

                    if (dtDtlRealisasi.Rows.Count == 0)
                    {
                        string ssql1 = "select * from trxPerjalananDinasDetailBudget where DocKey=?";
                        myDetailTable = myLocalDBSetting.GetDataTable(ssql1, false, strKey);
                        gvDetail.DataSource = myDetailTable;
                        gvDetail.DataBind();

                        myApprovalTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DocKey=? and TypeApproval='Pengajuan Approval'", false, strKey);
                        gvApproval.DataSource = myApprovalTable;
                        gvApproval.DataBind();

                        tglKeberangkatan2.Value = tglKeberangkatan.Value;
                        tglKepulangan2.Value = tglKepulangan.Value;
                        mandays2.Value = mandays1.Value;

                        negaraasal2.Value = negaraasal1.Value;
                        negaratujuan2.Value = negaratujuan1.Value;
                        byvehicle2.Value = byvehicle1.Value;
                        toPNJ.Value = dr["TotalPengajuan"];
                        toREL.Value = dr["TotalPengajuan"];
                    }

                    else
                    {
                        string ssql2 = "select * from trxPerjalananDinasDetailBudget where DocKey=? and TypeSPD='Pengajuan Approval'";
                        myDetailTable = myLocalDBSetting.GetDataTable(ssql2, false, strKey);
                        gvDetail.DataSource = myDetailTable;
                        gvDetail.DataBind();

                        myApprovalTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DocKey=? and TypeApproval='Pengajuan Approval'", false, strKey);
                        gvApproval.DataSource = myApprovalTable;
                        gvApproval.DataBind();

                        string ssql1 = "select * from trxPerjalananDinasDetailBudget where DocKey=? and TypeSPD='Pengajuan Realisasi'";
                        myDetailRealisasiTable = myLocalDBSetting.GetDataTable(ssql1, false, strKey);
                        gvRealisasiPerjalananDinas.DataSource = myDetailRealisasiTable;
                        gvRealisasiPerjalananDinas.DataBind();

                        myApprovalRealisasiTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DocKey=? and TypeApproval = 'Pengajuan Realisasi'", false, strKey);
                        gvApprovalRealisasi.DataSource = myApprovalRealisasiTable;
                        gvApprovalRealisasi.DataBind();

                        DataTable dtlrel = getViewDtlRel(strKey, "Realisasi");
                        DataRow dtt = dtlrel.Rows[0];

                        tglKeberangkatan2.Value = dtt["StartDate"];
                        tglKepulangan2.Value = dtt["EndDate"];
                        mandays2.Value = dtt["JumlahHari"];

                        negaraasal2.Value = dtt["FromTujuan"];
                        negaratujuan2.Value = dtt["Tujuan"];
                        byvehicle2.Value = dtt["Kendaraan"];
                        toPNJ.Value = dr["TotalPengajuan"];
                        toREL.Value = dr["TotalRealisasi"];
                    }
                }

                mySPDDOCTable = myLocalDBSetting.GetDataTable("select b.DocNo as 'DocumentNo', a.* from DocumentUploadSPD a left join trxPerjalananDinas b on a.DocKey = b.DocKey where a.DocKey=?", false, strKey);
                gvMain.DataSource = mySPDDOCTable;
                gvMain.DataBind();
            }

            if (myAction == DXSSAction.Edit)
            {
                DataTable dtSPDDocPnj = getViewSPDDoc(strKey);
                DataRow dr = dtSPDDocPnj.Rows[0];

                DataTable dtUangMakan = myLocalDBSetting.GetDataTable("select BudgetAmount from trxPerjalananDinasDetailBudget where DocKey =? and BudgetDesc = 'Tunjangan Makan Dinas'", false, strKey);
                DataRow dr1 = dtUangMakan.Rows[0];

                DataTable dtUangSaku = myLocalDBSetting.GetDataTable("select BudgetAmount from trxPerjalananDinasDetailBudget where DocKey =? and BudgetDesc = 'Tunjangan Saku Dinas'", false, strKey);
                DataRow dr2 = dtUangSaku.Rows[0];

                tunjanganMakan.Value = dr1["BudgetAmount"];
                uangSaku.Value = dr2["BudgetAmount"];

                txtName.Value = dr["Name"];
                nik_emp.Value = dr["NIK"];
                pos_emp.Value = dr["Jabatan"];
                SPDNo.Value = dr["DocNo"];

                pos_emp3.Value = dr["Jabatan"];
                negaraasal2.Value = dr["FromTujuan"];
                spd_charge.Value = dr["PembebananBiaya"];

                spd_dept.Value = dr["Dept"];
                status_spd.Value = dr["Status"];
                negaratujuan2.Value = dr["Tujuan2"];

                tglKeberangkatan2.Value = dr["StartDate2"];
                tglKepulangan2.Value = dr["EndDate2"];
                mandays2.Value = dr["JumlahHari2"];

                byvehicle2.Value = dr["Kendaraan2"];
                mandays3.Value = dr["JumlahHari"];

                DataTable dtlpnj = getViewDtlRel(strKey, "Pengajuan");
                DataRow dtp = dtlpnj.Rows[0];

                tglKeberangkatan.Value = dtp["StartDate"];
                tglKepulangan.Value = dtp["EndDate"];
                mandays1.Value = dtp["JumlahHari"];

                negaraasal1.Value = dtp["FromTujuan"];
                negaratujuan1.Value = dtp["Tujuan"];
                byvehicle1.Value = dtp["Kendaraan"];
                //toREL.Value = 0;

                if (pos_emp.Value.ToString() == "Non-OFFICER")
                {
                    pos_emp2.Value = 1;
                }
                if (pos_emp.Value.ToString() == "OFFICER")
                {
                    pos_emp2.Value = 2;
                }
                if (pos_emp.Value.ToString() == "SPV/ASST.MANAGER")
                {
                    pos_emp2.Value = 3;
                }
                if (pos_emp.Value.ToString() == "MANAGER/SENIOR MANAGER")
                {
                    pos_emp2.Value = 4;
                }
                if (pos_emp.Value.ToString() == "GM/VP")
                {
                    pos_emp2.Value = 5;
                }
                if (pos_emp.Value.ToString() == "SVP/EVP")
                {
                    pos_emp2.Value = 6;
                }
                if (pos_emp.Value.ToString() == "DIREKTUR")
                {
                    pos_emp2.Value = 7;
                }

                if (status_spd.Value.ToString() == "NEW")
                {
                    //myDetailTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasDetailBudget where DocKey=? and TypeSPD='Pengajuan Approval'", false, strKey);
                    string ssql = "select * from trxPerjalananDinasDetailBudget where DocKey=? and TypeSPD='Pengajuan Approval' and BudgetDesc not in('Tunjangan Makan Dinas','Tunjangan Saku Dinas')";
                    myDetailTable = myLocalDBSetting.GetDataTable(ssql, false, strKey);
                    gvDetail.DataSource = myDetailTable;
                    gvDetail.DataBind();

                    myApprovalTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DocKey=? and TypeApproval = 'Pengajuan Approval'", false, strKey);
                    gvApproval.DataSource = myApprovalTable;
                    gvApproval.DataBind();

                    toPNJ.Value = dr["TotalPengajuan"];
                }

                if (status_spd.Value.ToString() == "ON BUSSINESS TRIP" || status_spd.Value.ToString() == "ON REVIEW BY HRD")
                {
                    DataTable dtDtlRealisasi = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasDetail where DocKey =? and TypeSPD = 'Realisasi'", false, strKey);

                    if (dtDtlRealisasi.Rows.Count == 0)
                    {
                        string ssql1 = "select * from trxPerjalananDinasDetailBudget where DocKey=?";
                        myDetailTable = myLocalDBSetting.GetDataTable(ssql1, false, strKey);
                        gvDetail.DataSource = myDetailTable;
                        gvDetail.DataBind();

                        myApprovalTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DocKey=?", false, strKey);
                        gvApproval.DataSource = myApprovalTable;
                        gvApproval.DataBind();

                        string ssql = "select * from trxPerjalananDinasDetailBudget where Dockey=? and BudgetDesc not in('Tunjangan Makan Dinas','Tunjangan Saku Dinas')";
                        myDetailRealisasiTable = myLocalDBSetting.GetDataTable(ssql, false, strKey);
                        gvRealisasiPerjalananDinas.DataSource = myDetailRealisasiTable;
                        gvRealisasiPerjalananDinas.DataBind();

                        myApprovalRealisasiTable = myLocalDBSetting.GetDataTable("select DtlKey, DocKey, 'Pengajuan Realisasi' AS TypeApproval, Seq, NIK, Nama, Jabatan, '' AS IsDecision, '' AS DecisionState, null AS DecisionDate, DecisionNote, Email from trxPerjalananDinasApprovalList where DocKey=?", false, strKey);
                        gvApprovalRealisasi.DataSource = myApprovalRealisasiTable;
                        gvApprovalRealisasi.DataBind();

                        tglKeberangkatan2.Value = tglKeberangkatan.Value;
                        tglKepulangan2.Value = tglKepulangan.Value;
                        mandays2.Value = mandays1.Value;

                        negaraasal2.Value = negaraasal1.Value;
                        negaratujuan2.Value = negaratujuan1.Value;
                        byvehicle2.Value = byvehicle1.Value;

                        toPNJ.Value = dr["TotalPengajuan"];
                        toREL.Value = dr["TotalPengajuan"];
                    }

                    else
                    {
                        string ssql2 = "select * from trxPerjalananDinasDetailBudget where DocKey=? and TypeSPD='Pengajuan Approval'";
                        myDetailTable = myLocalDBSetting.GetDataTable(ssql2, false, strKey);
                        gvDetail.DataSource = myDetailTable;
                        gvDetail.DataBind();

                        myApprovalTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DocKey=? and TypeApproval='Pengajuan Approval'", false, strKey);
                        gvApproval.DataSource = myApprovalTable;
                        gvApproval.DataBind();

                        string ssql1 = "select * from trxPerjalananDinasDetailBudget where DocKey=? and TypeSPD='Pengajuan Realisasi' and BudgetDesc not in('Tunjangan Makan Dinas','Tunjangan Saku Dinas')";
                        myDetailRealisasiTable = myLocalDBSetting.GetDataTable(ssql1, false, strKey);
                        gvRealisasiPerjalananDinas.DataSource = myDetailRealisasiTable;
                        gvRealisasiPerjalananDinas.DataBind();

                        myApprovalRealisasiTable = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasApprovalList where DocKey=? and TypeApproval = 'Pengajuan Realisasi'", false, strKey);
                        gvApprovalRealisasi.DataSource = myApprovalRealisasiTable;
                        gvApprovalRealisasi.DataBind();

                        DataTable dtlrel = getViewDtlRel(strKey, "Realisasi");
                        DataRow dtt = dtlrel.Rows[0];

                        tglKeberangkatan2.Value = dtt["StartDate"];
                        tglKepulangan2.Value = dtt["EndDate"];
                        mandays2.Value = dtt["JumlahHari"];

                        negaraasal2.Value = dtt["FromTujuan"];
                        negaratujuan2.Value = dtt["Tujuan"];
                        byvehicle2.Value = dtt["Kendaraan"];
                        toPNJ.Value = dr["TotalPengajuan"];
                        toREL.Value = dr["TotalRealisasi"];
                    }
                }

                mySPDDOCTable = myLocalDBSetting.GetDataTable("select b.DocNo as 'DocumentNo', a.* from DocumentUploadSPD a left join trxPerjalananDinas b on a.DocKey = b.DocKey where a.DocKey=?", false, strKey);
                gvMain.DataSource = mySPDDOCTable;
                gvMain.DataBind();
            }
        }

        private void BindingListJabatan()
        {
            if (myAction == DXSSAction.New)
            {
                DataTable obj = myLocalDBSetting.GetDataTable("select id_jabatan as Value, jabatan_detail as Text from MasterJabatan", false);
                //DataTable dt = (DataTable)JsonConvert.DeserializeObject(obj.ToString(), (typeof(DataTable)));

                pos_emp.DataSource = obj;
                pos_emp.DataBind();

                obj.Dispose();
            }
        }

        private void Accessable()
        {
            ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUploadDocument").Visible = false;
            ASPxFormLayout.FindItemOrGroupByName("LayoutGroupDocumentLibrary").Visible = false;
            ASPxFormLayout.FindItemOrGroupByName("pos_emp3").Visible = false;
            ASPxFormLayout.FindItemOrGroupByName("totalRel").Visible = false;
            ASPxFormLayout.FindItemOrGroupByName("mandaysShdw").Visible = false;
            ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUploadDoc").Visible = false;
            ASPxFormLayout.FindItemOrGroupByName("tbLayoutRealisasi").Visible = false;
            pos_emp2.Visible = false;

            toPNJ.ClientEnabled = false;
            toPNJ.ForeColor = System.Drawing.Color.Black;
            toPNJ.BackColor = System.Drawing.Color.Transparent;

            toPNJ.ReadOnly = true;

            toREL.ClientEnabled = false;
            toREL.ForeColor = System.Drawing.Color.Black;
            toREL.BackColor = System.Drawing.Color.Transparent;

            toREL.ReadOnly = true;
            if (myAction == DXSSAction.New)
            {
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupHeader").Width = Unit.Percentage(50.0);
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupRencana").Width = Unit.Percentage(50.0);
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupRealisasi").Visible = false;

                ASPxFormLayout.FindItemOrGroupByName("DocNo").Visible = false;
                ASPxFormLayout.FindItemOrGroupByName("status_spd").Visible = false;
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUploadDoc").Visible = true;

                tglKeberangkatan2.ClientEnabled = false;
                tglKeberangkatan2.BackColor = System.Drawing.Color.Transparent;

                negaratujuan2.ClientEnabled = false;
                negaratujuan2.BackColor = System.Drawing.Color.Transparent;

                mandays2.ClientEnabled = false;
                mandays2.BackColor = System.Drawing.Color.Transparent;

                tglKepulangan2.ClientEnabled = false;
                tglKepulangan2.BackColor = System.Drawing.Color.Transparent;

                byvehicle2.ClientEnabled = false;
                byvehicle2.BackColor = System.Drawing.Color.Transparent;

                ASPxFormLayout.FindItemOrGroupByName("SPDNo").Visible = false;

                status_spd.Visible = false;
                DocNo.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;

                btnSubmit.ClientVisible = true;
                spd_charge.ClientEnabled = true;
            }

            if (myAction == DXSSAction.View)
            {
                if (status_spd.Value.ToString() == "NEW" || status_spd.Value.ToString() == "REQUEST APPROVAL")
                {
                    ASPxFormLayout.FindItemOrGroupByName("tbLayoutRealisasi").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupHeader").Width = Unit.Percentage(50.0);
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupRencana").Width = Unit.Percentage(50.0);
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupRealisasi").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("status_spd").Visible = false;

                    tglKeberangkatan2.ClientEnabled = false;
                    tglKeberangkatan2.BackColor = System.Drawing.Color.Transparent;

                    negaratujuan2.ClientEnabled = false;
                    negaratujuan2.BackColor = System.Drawing.Color.Transparent;

                    mandays2.ClientEnabled = false;
                    mandays2.BackColor = System.Drawing.Color.Transparent;

                    tglKepulangan2.ClientEnabled = false;
                    tglKepulangan2.BackColor = System.Drawing.Color.Transparent;

                    byvehicle2.ClientEnabled = false;
                    byvehicle2.BackColor = System.Drawing.Color.Transparent;

                    ASPxFormLayout.FindItemOrGroupByName("SPDNo").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("tbLayoutRealisasi").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("totalRel").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("pos_emp2").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("emptyLay4").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("empt_sspd").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("DocNo").Visible = false;

                    status_spd.Visible = false;
                    DocNo.Visible = false;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }

                else
                {
                    ASPxFormLayout.FindItemOrGroupByName("tbLayoutRealisasi").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("totalRel").Visible = true;
                }

                status_spd.Visible = false;
                DocNo.Visible = false;

                txtName.ClientEnabled = false;
                //txtName.BackColor = System.Drawing.Color.Transparent;
                txtName.ForeColor = System.Drawing.Color.Black;

                SPDNo.ClientEnabled = true;
                //SPDNo.BackColor = System.Drawing.Color.Transparent;
                SPDNo.ForeColor = System.Drawing.Color.Black;

                negaraasal1.ClientEnabled = false;
                //negaraasal1.BackColor = System.Drawing.Color.Transparent;
                negaraasal1.ForeColor = System.Drawing.Color.Black;

                negaraasal2.ClientEnabled = false;
                //negaraasal2.BackColor = System.Drawing.Color.Transparent;
                negaraasal2.ForeColor = System.Drawing.Color.Black;

                nik_emp.ClientEnabled = false;
                //nik_emp.BackColor = System.Drawing.Color.Transparent;
                nik_emp.ForeColor = System.Drawing.Color.Black;

                pos_emp.ClientEnabled = false;
                //pos_emp.BackColor = System.Drawing.Color.Transparent;
                pos_emp.ForeColor = System.Drawing.Color.Black;

                spd_charge.ClientEnabled = false;
                //spd_charge.BackColor = System.Drawing.Color.Transparent;
                spd_charge.ForeColor = System.Drawing.Color.Black;

                spd_dept.ClientEnabled = false;
                //spd_dept.BackColor = System.Drawing.Color.Transparent;
                spd_dept.ForeColor = System.Drawing.Color.Black;

                tglKeberangkatan.ClientEnabled = false;
                //tglKeberangkatan.BackColor = System.Drawing.Color.Transparent;
                tglKeberangkatan.ForeColor = System.Drawing.Color.Black;

                negaratujuan1.ClientEnabled = false;
                //negaratujuan1.BackColor = System.Drawing.Color.Transparent;
                negaratujuan1.ForeColor = System.Drawing.Color.Black;

                mandays1.ClientEnabled = false;
                //mandays1.BackColor = System.Drawing.Color.Transparent;
                mandays1.ForeColor = System.Drawing.Color.Black;

                tglKepulangan.ClientEnabled = false;
                //tglKepulangan.BackColor = System.Drawing.Color.Transparent;
                tglKepulangan.ForeColor = System.Drawing.Color.Black;

                tglKeberangkatan2.ClientEnabled = false;
                //tglKeberangkatan2.BackColor = System.Drawing.Color.Transparent;
                tglKeberangkatan2.ForeColor = System.Drawing.Color.Black;

                tglKepulangan2.ClientEnabled = false;
                //tglKepulangan2.BackColor = System.Drawing.Color.Transparent;
                tglKepulangan2.ForeColor = System.Drawing.Color.Black;

                byvehicle1.ClientEnabled = false;
                //byvehicle1.BackColor = System.Drawing.Color.Transparent;
                byvehicle1.ForeColor = System.Drawing.Color.Black;

                negaratujuan2.ClientEnabled = false;
                //negaratujuan2.BackColor = System.Drawing.Color.Transparent;
                negaratujuan2.ForeColor = System.Drawing.Color.Black;

                mandays2.ClientEnabled = false;
                //mandays2.BackColor = System.Drawing.Color.Transparent;
                mandays2.ForeColor = System.Drawing.Color.Black;

                byvehicle2.ClientEnabled = false;
                //byvehicle2.BackColor = System.Drawing.Color.Transparent;
                byvehicle2.ForeColor = System.Drawing.Color.Black;

                calculate_spd.Visible = false;

                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupTunjangan").Visible = false;

                ASPxFormLayout.FindItemOrGroupByName("tbLayoutGroupDetail");

                gvDetail.Columns["ClmnCommand"].Visible = false;

                gvApproval.Columns["colNo"].Visible = false;
                gvApproval.Columns["ClmnCommand"].Visible = false;

                btnSave.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;

                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUploadDocument").Visible = true;
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupDocumentLibrary").Visible = true;

                gvRealisasiPerjalananDinas.Columns["ClmnCommand"].Visible = false;

                gvApprovalRealisasi.Columns["colNo"].Visible = false;
                gvApprovalRealisasi.Columns["ClmnCommand"].Visible = false;
            }

            if (myAction == DXSSAction.Edit)
            {

                if (status_spd.Value.ToString() == "NEW")
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupHeader").Width = Unit.Percentage(50.0);
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupRencana").Width = Unit.Percentage(50.0);
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupRealisasi").Visible = false;

                    ASPxFormLayout.FindItemOrGroupByName("emptyLay4").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("DocNo").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("SPDNo").Visible = true;

                    ASPxFormLayout.FindItemOrGroupByName("pos_emp").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("pos_emp3").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUploadDocument").Visible = true;

                    gvDetail.Columns["ClmnCommand"].Visible = true;

                    gvApproval.Columns["colNo"].Visible = true;
                    gvApproval.Columns["ClmnCommand"].Visible = true;

                    txtName.ClientEnabled = false;
                    txtName.BackColor = System.Drawing.Color.Transparent;
                    txtName.ForeColor = System.Drawing.Color.Black;

                    nik_emp.ClientEnabled = false;
                    nik_emp.BackColor = System.Drawing.Color.Transparent;
                    nik_emp.ForeColor = System.Drawing.Color.Black;

                    pos_emp3.ClientEnabled = false;
                    pos_emp3.BackColor = System.Drawing.Color.Transparent;
                    pos_emp3.ForeColor = System.Drawing.Color.Black;

                    spd_charge.ClientEnabled = false;
                    spd_charge.BackColor = System.Drawing.Color.Transparent;
                    spd_charge.ForeColor = System.Drawing.Color.Black;

                    spd_dept.ClientEnabled = false;
                    spd_dept.BackColor = System.Drawing.Color.Transparent;
                    spd_dept.ForeColor = System.Drawing.Color.Black;

                    status_spd.Visible = false;
                    DocNo.Visible = false;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;

                    btnSubmit.ClientVisible = true;
                }

                else
                {
                    ASPxFormLayout.FindItemOrGroupByName("tbLayoutRealisasi").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("totalRel").Visible = true;
                    //ASPxFormLayout.FindItemOrGroupByName("mandaysShdw").Visible = true;
                    //ASPxFormLayout.FindItemOrGroupByName("Mandays1").Visible = false;
                    status_spd.Visible = false;
                    DocNo.Visible = false;
                    pos_emp2.Visible = false;

                    mandays3.ClientEnabled = false;
                    mandays3.BackColor = System.Drawing.Color.Transparent;
                    mandays3.ForeColor = System.Drawing.Color.Black;

                    txtName.ClientEnabled = false;
                    txtName.BackColor = System.Drawing.Color.Transparent;
                    txtName.ForeColor = System.Drawing.Color.Black;

                    nik_emp.ClientEnabled = false;
                    nik_emp.BackColor = System.Drawing.Color.Transparent;
                    nik_emp.ForeColor = System.Drawing.Color.Black;

                    pos_emp3.ClientEnabled = false;
                    pos_emp3.BackColor = System.Drawing.Color.Transparent;
                    pos_emp3.ForeColor = System.Drawing.Color.Black;

                    spd_charge.ClientEnabled = false;
                    spd_charge.BackColor = System.Drawing.Color.Transparent;
                    spd_charge.ForeColor = System.Drawing.Color.Black;

                    SPDNo.ClientEnabled = false;
                    SPDNo.BackColor = System.Drawing.Color.Transparent;
                    SPDNo.ForeColor = System.Drawing.Color.Black;

                    negaraasal1.ClientEnabled = false;
                    negaraasal1.BackColor = System.Drawing.Color.Transparent;
                    negaraasal1.ForeColor = System.Drawing.Color.Black;

                    spd_dept.ClientEnabled = false;
                    spd_dept.BackColor = System.Drawing.Color.Transparent;
                    spd_dept.ForeColor = System.Drawing.Color.Black;

                    tglKeberangkatan.ClientEnabled = false;
                    tglKeberangkatan.BackColor = System.Drawing.Color.Transparent;
                    tglKeberangkatan.ForeColor = System.Drawing.Color.Black;

                    negaratujuan1.ClientEnabled = false;
                    negaratujuan1.BackColor = System.Drawing.Color.Transparent;
                    negaratujuan1.ForeColor = System.Drawing.Color.Black;

                    mandays1.ClientEnabled = false;
                    mandays1.BackColor = System.Drawing.Color.Transparent;
                    mandays1.ForeColor = System.Drawing.Color.Black;

                    mandays2.ClientEnabled = false;
                    mandays2.BackColor = System.Drawing.Color.Transparent;
                    mandays2.ForeColor = System.Drawing.Color.Black;

                    tglKepulangan.ClientEnabled = false;
                    tglKepulangan.BackColor = System.Drawing.Color.Transparent;
                    tglKepulangan.ForeColor = System.Drawing.Color.Black;

                    byvehicle1.ClientEnabled = false;
                    byvehicle1.BackColor = System.Drawing.Color.Transparent;
                    byvehicle1.ForeColor = System.Drawing.Color.Black;

                    btnSubmit.ClientVisible = true;
                }

                calculate_spd.Visible = false;

                gvDetail.Columns["ClmnCommand"].Visible = false;
                gvApproval.Columns["colNo"].Visible = true;
                gvApproval.Columns["ClmnCommand"].Visible = false;

                if (status_spd.Value.ToString() == "NEW")
                {
                    gvDetail.Columns["ClmnCommand"].Visible = true;
                    gvApproval.Columns["colNo"].Visible = true;
                    gvApproval.Columns["ClmnCommand"].Visible = true;
                }

                //btnSave.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;

                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUploadDocument").Visible = true;
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupDocumentLibrary").Visible = true;
                ASPxFormLayout.FindItemOrGroupByName("pos_emp").Visible = false;
                ASPxFormLayout.FindItemOrGroupByName("pos_emp3").Visible = true;
            }

            if (myAction == DXSSAction.Approve)
            {
                if (status_spd.Value.ToString() == "REQUEST APPROVAL")
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupHeader").Width = Unit.Percentage(50.0);
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupRencana").Width = Unit.Percentage(50.0);
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupRealisasi").Visible = false;

                    ASPxFormLayout.FindItemOrGroupByName("emptyLay4").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("DocNo").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("SPDNo").Visible = true;

                    ASPxFormLayout.FindItemOrGroupByName("pos_emp").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("pos_emp3").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUploadDocument").Visible = true;

                    ASPxFormLayout.FindItemOrGroupByName("pos_emp2").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("emptyLay4").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("empt_sspd").Visible = false;
                    ASPxFormLayout.FindItemOrGroupByName("DocNo").Visible = false;

                    txtName.ClientEnabled = false;
                    txtName.BackColor = System.Drawing.Color.Transparent;
                    txtName.ForeColor = System.Drawing.Color.Black;

                    nik_emp.ClientEnabled = false;
                    nik_emp.BackColor = System.Drawing.Color.Transparent;
                    nik_emp.ForeColor = System.Drawing.Color.Black;

                    pos_emp3.ClientEnabled = false;
                    pos_emp3.BackColor = System.Drawing.Color.Transparent;
                    pos_emp3.ForeColor = System.Drawing.Color.Black;

                    spd_charge.ClientEnabled = false;
                    spd_charge.BackColor = System.Drawing.Color.Transparent;
                    spd_charge.ForeColor = System.Drawing.Color.Black;

                    spd_dept.ClientEnabled = false;
                    spd_dept.BackColor = System.Drawing.Color.Transparent;
                    spd_dept.ForeColor = System.Drawing.Color.Black;
                }

                if (status_spd.Value.ToString() == "REALIZATION APPROVAL")
                {
                    ASPxFormLayout.FindItemOrGroupByName("tbLayoutRealisasi").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("totalRel").Visible = true;
                }

                status_spd.Visible = false;
                DocNo.Visible = false;

                txtName.ClientEnabled = false;
                txtName.BackColor = System.Drawing.Color.Transparent;
                txtName.ForeColor = System.Drawing.Color.Black;

                nik_emp.ClientEnabled = false;
                nik_emp.BackColor = System.Drawing.Color.Transparent;
                nik_emp.ForeColor = System.Drawing.Color.Black;

                pos_emp.ClientEnabled = false;
                pos_emp.BackColor = System.Drawing.Color.Transparent;
                pos_emp.ForeColor = System.Drawing.Color.Black;

                spd_charge.ClientEnabled = false;
                spd_charge.BackColor = System.Drawing.Color.Transparent;
                spd_charge.ForeColor = System.Drawing.Color.Black;

                spd_dept.ClientEnabled = false;
                spd_dept.BackColor = System.Drawing.Color.Transparent;
                spd_dept.ForeColor = System.Drawing.Color.Black;

                tglKeberangkatan.ClientEnabled = false;
                tglKeberangkatan.BackColor = System.Drawing.Color.Transparent;
                tglKeberangkatan.ForeColor = System.Drawing.Color.Black;

                SPDNo.ClientEnabled = false;
                SPDNo.BackColor = System.Drawing.Color.Transparent;
                SPDNo.ForeColor = System.Drawing.Color.Black;

                negaraasal1.ClientEnabled = false;
                negaraasal1.BackColor = System.Drawing.Color.Transparent;
                negaraasal1.ForeColor = System.Drawing.Color.Black;

                negaraasal2.ClientEnabled = false;
                negaraasal2.BackColor = System.Drawing.Color.Transparent;
                negaraasal2.ForeColor = System.Drawing.Color.Black;

                negaratujuan1.ClientEnabled = false;
                negaratujuan1.BackColor = System.Drawing.Color.Transparent;
                negaratujuan1.ForeColor = System.Drawing.Color.Black;

                mandays1.ClientEnabled = false;
                mandays1.BackColor = System.Drawing.Color.Transparent;
                mandays1.ForeColor = System.Drawing.Color.Black;

                tglKepulangan.ClientEnabled = false;
                tglKepulangan.BackColor = System.Drawing.Color.Transparent;
                tglKepulangan.ForeColor = System.Drawing.Color.Black;

                tglKeberangkatan2.ClientEnabled = false;
                tglKeberangkatan2.BackColor = System.Drawing.Color.Transparent;
                tglKeberangkatan2.ForeColor = System.Drawing.Color.Black;

                tglKepulangan2.ClientEnabled = false;
                tglKepulangan2.BackColor = System.Drawing.Color.Transparent;
                tglKepulangan2.ForeColor = System.Drawing.Color.Black;

                byvehicle1.ClientEnabled = false;
                byvehicle1.BackColor = System.Drawing.Color.Transparent;
                byvehicle1.ForeColor = System.Drawing.Color.Black;

                negaratujuan2.ClientEnabled = false;
                negaratujuan2.BackColor = System.Drawing.Color.Transparent;
                negaratujuan2.ForeColor = System.Drawing.Color.Black;

                mandays2.ClientEnabled = false;
                mandays2.BackColor = System.Drawing.Color.Transparent;
                mandays2.ForeColor = System.Drawing.Color.Black;

                byvehicle2.ClientEnabled = false;
                byvehicle2.BackColor = System.Drawing.Color.Transparent;
                byvehicle2.ForeColor = System.Drawing.Color.Black;
                calculate_spd.Visible = false;

                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupTunjangan").Visible = false;
                ASPxFormLayout.FindItemOrGroupByName("tbLayoutGroupDetail");
                gvDetail.Columns["ClmnCommand"].Visible = false;

                gvApproval.Columns["colNo"].Visible = false;
                gvApproval.Columns["ClmnCommand"].Visible = false;
                gvRealisasiPerjalananDinas.Columns["ClmnCommand"].Visible = false;

                gvApprovalRealisasi.Columns["colNo"].Visible = false;
                gvApprovalRealisasi.Columns["ClmnCommand"].Visible = false;
                btnSave.Visible = false;

                ASPxFormLayout.FindItemOrGroupByName("spaceBtn").Width = Unit.Percentage(60);
                ASPxFormLayout.FindItemOrGroupByName("btnSubmit").Visible = false;
                ASPxFormLayout.FindItemOrGroupByName("btnSave").Visible = false;

                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUploadDocument").Visible = true;
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupDocumentLibrary").Visible = true;
            }
        }

        protected void Calculate_spd(object sender, EventArgs e)
        {
            DateTime tanggalMasuk, tanggalKeluar;
            object kodeJabatan;
            try
            {
                kodeJabatan = pos_emp.Value;
                //kodeJabatan = pos_emp2.Value;
                object obj = myLocalDBSetting.ExecuteScalar("select nominal, id_tunjangan, tunjangan_detail from MasterBiayaTunjangan where id_jabatan=? order by id_tunjangan ASC FOR JSON AUTO", kodeJabatan);
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(obj.ToString(), (typeof(DataTable)));

                tanggalMasuk = Convert.ToDateTime(tglKeberangkatan.Value);
                tanggalKeluar = Convert.ToDateTime(tglKepulangan.Value);

                if (tanggalKeluar < tanggalMasuk)
                {
                    return;
                }
                else
                {
                    TimeSpan waktu = tanggalKeluar - tanggalMasuk;
                    int longDays = Convert.ToInt32(waktu.TotalDays);
                    tunjanganMakan.Value = Convert.ToInt32(dt.Rows[0]["Nominal"].ToString()) * longDays;
                    uangSaku.Value = Convert.ToInt32(dt.Rows[1]["Nominal"].ToString()) * longDays;
                    mandays1.Value = waktu.TotalDays;
                    return;
                }
            }
            catch (Exception Ekawodkao)
            {
                return;
            }
        }

        protected decimal getCalculateTunjangan(string typeTunjangan, int kodeJabatan)
        {
            DateTime tanggalMasuk, tanggalKeluar;
            tanggalMasuk = Convert.ToDateTime(tglKeberangkatan.Value);
            tanggalKeluar = Convert.ToDateTime(tglKepulangan.Value);
            int dtHour, dtMinute;
            decimal res = 0;
            decimal pengurangUM = 0;
            decimal pengurangUS = 0;

            if (typeTunjangan == "UangMakan")
            {
                DataTable dtUM = myLocalDBSetting.GetDataTable("select top 1 nominal from MasterBiayaTunjangan where id_jabatan = ? and tunjangan_detail = 'Tunjangan satu kali makan (dalam negeri)'", false, kodeJabatan);
                if (dtUM.Rows.Count > 0)
                {
                    DataRow dr = dtUM.Rows[0];
                    decimal dVal = Convert.ToDecimal(dr["nominal"]);

                    //3x per 1 hari
                    decimal dValHarian = dVal * 3;

                    TimeSpan waktu = tanggalKeluar.Date - tanggalMasuk.Date;
                    int longDays = Convert.ToInt32(waktu.TotalDays) + 1;

                    res = dValHarian * longDays;

                    //cek jam keberangkatan
                    dtHour = tanggalMasuk.Hour;
                    dtMinute = tanggalMasuk.Minute;

                    pengurangUM += getPengurangUM(dtHour, dtMinute, dVal, false);

                    //cek jam kepulangan
                    dtHour = tanggalKeluar.Hour;
                    dtMinute = tanggalKeluar.Minute;

                    pengurangUM += getPengurangUM(dtHour, dtMinute, dVal, true);
                    res = res - pengurangUM;

                }
            }

            if (typeTunjangan == "UangSaku")
            {
                DataTable dtUS = myLocalDBSetting.GetDataTable("select top 1 nominal from MasterBiayaTunjangan where id_jabatan = ? and tunjangan_detail = 'Uang saku per hari (dalam negeri)'", false, kodeJabatan);
                if (dtUS.Rows.Count > 0)
                {
                    DataRow dr = dtUS.Rows[0];
                    decimal dVal = Convert.ToDecimal(dr["nominal"]);

                    TimeSpan waktu = tanggalKeluar.Date - tanggalMasuk.Date;
                    int longDays = Convert.ToInt32(waktu.TotalDays) + 1;

                    res = dVal * longDays;

                    //cek jam keberangkatan
                    dtHour = tanggalMasuk.Hour;
                    dtMinute = tanggalMasuk.Minute;

                    pengurangUS += getPengurangUS(dtHour, dtMinute, dVal, false);

                    //cek jam kepulangan
                    dtHour = tanggalKeluar.Hour;
                    dtMinute = tanggalKeluar.Minute;

                    pengurangUS += getPengurangUS(dtHour, dtMinute, dVal, true);
                    res = res - pengurangUS;
                }
            }

            return res;
        }

        protected decimal getCalculateTunjangan2(string typeTunjangan, int kodeJabatan)
        {
            DateTime tanggalMasuk, tanggalKeluar;
            tanggalMasuk = Convert.ToDateTime(tglKeberangkatan2.Value);
            tanggalKeluar = Convert.ToDateTime(tglKepulangan2.Value);
            int dtHour, dtMinute;
            decimal res = 0;
            decimal pengurangUM = 0;
            decimal pengurangUS = 0;

            if (typeTunjangan == "UangMakan")
            {
                DataTable dtUM = myLocalDBSetting.GetDataTable("select top 1 nominal from MasterBiayaTunjangan where id_jabatan = ? and tunjangan_detail = 'Tunjangan satu kali makan (dalam negeri)'", false, kodeJabatan);
                if (dtUM.Rows.Count > 0)
                {
                    DataRow dr = dtUM.Rows[0];
                    decimal dVal = Convert.ToDecimal(dr["nominal"]);

                    //3x per 1 hari
                    decimal dValHarian = dVal * 3;

                    TimeSpan waktu = tanggalKeluar.Date - tanggalMasuk.Date;
                    int longDays = Convert.ToInt32(waktu.TotalDays) + 1;

                    res = dValHarian * longDays;

                    //cek jam keberangkatan
                    dtHour = tanggalMasuk.Hour;
                    dtMinute = tanggalMasuk.Minute;

                    pengurangUM += getPengurangUM(dtHour, dtMinute, dVal, false);

                    //cek jam kepulangan
                    dtHour = tanggalKeluar.Hour;
                    dtMinute = tanggalKeluar.Minute;

                    pengurangUM += getPengurangUM(dtHour, dtMinute, dVal, true);
                    res = res - pengurangUM;

                }
            }

            if (typeTunjangan == "UangSaku")
            {
                DataTable dtUS = myLocalDBSetting.GetDataTable("select top 1 nominal from MasterBiayaTunjangan where id_jabatan = ? and tunjangan_detail = 'Uang saku per hari (dalam negeri)'", false, kodeJabatan);
                if (dtUS.Rows.Count > 0)
                {
                    DataRow dr = dtUS.Rows[0];
                    decimal dVal = Convert.ToDecimal(dr["nominal"]);

                    TimeSpan waktu = tanggalKeluar.Date - tanggalMasuk.Date;
                    int longDays = Convert.ToInt32(waktu.TotalDays) + 1;

                    res = dVal * longDays;

                    //cek jam keberangkatan
                    dtHour = tanggalMasuk.Hour;
                    dtMinute = tanggalMasuk.Minute;

                    pengurangUS += getPengurangUS(dtHour, dtMinute, dVal, false);

                    //cek jam kepulangan
                    dtHour = tanggalKeluar.Hour;
                    dtMinute = tanggalKeluar.Minute;

                    pengurangUS += getPengurangUS(dtHour, dtMinute, dVal, true);
                    res = res - pengurangUS;
                }
            }

            return res;
        }

        protected decimal getPengurangUM(int dtHour, int dtMinute, decimal nominal, bool isEndDate)
        {
            decimal totalPengurang = 0;
            // untuk keberangkatan
            if (isEndDate != true)
            {
                if (dtHour > 8)
                {
                    totalPengurang += nominal;
                }

                if (dtHour > 14)
                {
                    totalPengurang += nominal;
                }

                if (dtHour > 21)
                {
                    totalPengurang += nominal;
                }
            }
            else //untuk kepulangan
            {
                if (dtHour < 8)
                {
                    totalPengurang += nominal;
                }

                if (dtHour < 14)
                {
                    totalPengurang += nominal;
                }

                if (dtHour < 21)
                {
                    totalPengurang += nominal;
                }
            }

            return totalPengurang;
        }

        protected decimal getPengurangUS(int dtHour, int dtMinute, decimal nominal, bool isEndDate)
        {
            decimal totalPengurang = 0;
            if (isEndDate != true) // untuk keberangkatan
            {
                if (dtHour > 13)
                {
                    totalPengurang += (nominal / 2);
                }
            }
            else //untuk kepulangan
            {
                if (dtHour < 13)
                {
                    totalPengurang += (nominal / 2);
                }
            }
            return totalPengurang;
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string urlsave = "";
            urlsave = "~/Transactions/Application/PerjalananDinas/PerjalananDinasList.aspx";
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
                case "ON_LOAD":
                    break;
                case "SAVE_CONFIRM":
                    //object paramValue2 = callbackParam[2];
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to save this data?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "SUBMIT_CONFIRM":
                    //object paramValue2 = callbackParam[2];
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to submit this data?";
                    cplMain.JSProperties["cplblActionButton"] = "SUBMIT";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "SAVE":
                    Save(SaveAction.Save, false);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
                case "SUBMIT":
                    //Submit(SaveAction.Submit);
                    Save(SaveAction.Save, true);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SUBMIT";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
                case "APPROVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to approve this?";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    if (ErrorInField(out strmessageError, SaveAction.Approve))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "APPROVE":
                    Approve(SaveAction.Approve);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been approve...";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
                case "REJECT_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to reject this?";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    if (ErrorInField(out strmessageError, SaveAction.Reject))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "REJECT":
                    Reject(SaveAction.Reject);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been reject...";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;

                case "UPLOADCONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to upload this document?";
                    cplMain.JSProperties["cplblActionButton"] = "UPLOAD";
                    break;

                case "UPLOAD":
                    SaveUploadDocSPD(documentTypeSPD.Text.ToString(), mmNotes.Text, SPDNo.Text);

                    mySPDDOCTable = myLocalDBSetting.GetDataTable("select b.DocNo as 'DocumentNo', a.* from DocumentUploadSPD a left join trxPerjalananDinas b on a.DocKey = b.DocKey where a.DocKey=?", false, strKey);
                    gvMain.DataSource = mySPDDOCTable;

                    cplMain.JSProperties["cpAlertMessage"] = "Upload success...";
                    cplMain.JSProperties["cplblActionButton"] = "UPLOAD";
                    //ASPxWebControl.RedirectOnCallback(urlsave);
                    break;

                case "CALCULATE":
                    decimal decUM = 0;
                    decimal decUS = 0;
                    decimal decTP = 0;
                    double dJmlHari = 0;

                    if (myAction == DXSSAction.New)
                    {
                        if (pos_emp.Value != null)
                        {
                            if (tglKeberangkatan.Value != null && tglKepulangan.Value != null)
                            {
                                DateTime tanggalMasuk, tanggalKeluar;
                                tanggalMasuk = Convert.ToDateTime(tglKeberangkatan.Value);
                                tanggalKeluar = Convert.ToDateTime(tglKepulangan.Value);

                                //if (tanggalKeluar < DateTime.Now)
                                //{
                                //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "ERROR" + "');", true);
                                //    return;
                                //}

                                if (tanggalKeluar >= tanggalMasuk)
                                {
                                    dJmlHari = (tanggalKeluar.Date - tanggalMasuk.Date).TotalDays + 1;
                                    decUM = getCalculateTunjangan("UangMakan", Convert.ToInt32(pos_emp.Value));
                                    decUS = getCalculateTunjangan("UangSaku", Convert.ToInt32(pos_emp.Value));
                                    decTP = Convert.ToInt32(decUM) + Convert.ToInt32(decUS);

                                    if (myDetailTable.Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in myDetailTable.Rows)
                                        {
                                            decTP += Convert.ToDecimal(dr["BudgetAmount"]);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (myAction == DXSSAction.Edit)
                    {
                        if (status_spd.Value.ToString() == "NEW" && pos_emp2.Value != null)
                        {
                            if (tglKeberangkatan.Value != null && tglKepulangan.Value != null)
                            {
                                DateTime tanggalMasuk2, tanggalKeluar2;
                                tanggalMasuk2 = Convert.ToDateTime(tglKeberangkatan.Value);
                                tanggalKeluar2 = Convert.ToDateTime(tglKepulangan.Value);

                                if (tanggalKeluar2 >= tanggalMasuk2)
                                {
                                    dJmlHari = (tanggalKeluar2.Date - tanggalMasuk2.Date).TotalDays + 1;
                                    decUM = getCalculateTunjangan("UangMakan", Convert.ToInt32(pos_emp2.Value));
                                    decUS = getCalculateTunjangan("UangSaku", Convert.ToInt32(pos_emp2.Value));
                                    decTP = Convert.ToInt32(decUM) + Convert.ToInt32(decUS);

                                    if (myDetailTable.Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in myDetailTable.Rows)
                                        {
                                            decTP += Convert.ToDecimal(dr["BudgetAmount"]);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    cplMain.JSProperties["cpT_MANDAYS"] = dJmlHari.ToString();
                    cplMain.JSProperties["cpT_MAKAN"] = decUM.ToString();
                    cplMain.JSProperties["cpT_UANGSAKU"] = decUS.ToString();
                    cplMain.JSProperties["cpT_TOTALPENGAJUAN"] = decTP.ToString();
                    break;

                case "CALCULATE_REALISASI":

                    decimal decUM2 = 0;
                    decimal decUS2 = 0;
                    decimal decTP2 = 0;
                    double dJmlHari2 = 0;
                    string strTypeCalc2 = "";

                    if (tglKeberangkatan2.Value != null && tglKepulangan2.Value != null)
                    {
                        DateTime tanggalMasuk2, tanggalKeluar2;
                        tanggalMasuk2 = Convert.ToDateTime(tglKeberangkatan2.Value);
                        tanggalKeluar2 = Convert.ToDateTime(tglKepulangan2.Value);
                        if (tanggalKeluar2 >= tanggalMasuk2)
                        {
                            dJmlHari2 = (tanggalKeluar2.Date - tanggalMasuk2.Date).TotalDays + 1;
                            strTypeCalc2 = "Realisasi";
                            decUM2 = getCalculateTunjangan2("UangMakan", Convert.ToInt32(pos_emp2.Value));
                            decUS2 = getCalculateTunjangan2("UangSaku", Convert.ToInt32(pos_emp2.Value));
                            decTP2 = Convert.ToInt32(decUM2) + Convert.ToInt32(decUS2);

                            if (myDetailRealisasiTable.Rows.Count > 0)
                            {
                                foreach (DataRow dr2 in myDetailRealisasiTable.Rows)
                                {
                                    decTP2 += Convert.ToDecimal(dr2["BudgetAmount"]);
                                }
                            }
                        }
                    }

                    cplMain.JSProperties["cpT_MANDAYS"] = dJmlHari2.ToString();
                    cplMain.JSProperties["cpT_TypeCalc"] = strTypeCalc2;
                    cplMain.JSProperties["cpT_MAKAN"] = decUM2.ToString();
                    cplMain.JSProperties["cpT_UANGSAKU"] = decUS2.ToString();
                    cplMain.JSProperties["cpT_TOTALREALISASI"] = decTP2.ToString();
                    break;
            }
        }

        private bool Save(SaveAction saveAction, bool isSubmit)
        {
            bool bSave = true;

            if (myAction == DXSSAction.New)
            {
                //string urlsave = "";
                //urlsave = "~/Transactions/Application/PerjalananDinas/PerjalananDinasList.aspx";
                string strDocNo = getDocNo();
                InsertRequest();
                InsertDtlSPD_Pnj("Pengajuan");
                updateTotalPnj();
                addCalculateTunjangan("Pengajuan Approval");
                foreach (DataRow dr in myDetailTable.Rows)
                {
                    InsertDtlBudget(dr, "Pengajuan Approval");
                }
                foreach (DataRow dr in myApprovalTable.Rows)
                {
                    insertApproval(dr, "Pengajuan Approval", "");
                }

                //Insert direktur approval
                //myDBSetting.ExecuteNonQuery("exec dbo.spSmileSupport_fillApprovalDirectorByID '" + UserID.ToString() + "','','" + strKey + "'");
                DataTable dtApprovalDir = myDBSetting.GetDataTable("exec dbo.spSmileSupport_getApprovalDirectorByID ?,?,?", false, UserID, "", strKey);
                if(dtApprovalDir.Rows.Count > 0)
                {
                    foreach(DataRow drAppDir in dtApprovalDir.Rows)
                    {
                        insertApproval(drAppDir, "Pengajuan Approval", "");
                    }
                    //DataRow drAppDir = dtApprovalDir.Rows[0];
                    //insertApproval(drAppDir, "Pengajuan Approval", "");
                }

                SaveUploadDocSPD("Document Pengajuan SPD", "", SPDNo.Text);

                if (isSubmit)
                {
                    UpdateApprovalDecision();
                    UpdateApproval("REQUEST APPROVAL");

                    //email notif
                    DataTable dtsendEmail = getViewApprovalEmail(strKey, "Pengajuan Approval");
                    DataRow dre = dtsendEmail.Rows[0];

                    if (dre["Email"].ToString() != null)
                    {
                        SendMailNotif(dre["Email"].ToString(), dre["Nama"].ToString(), dre["DocNo"].ToString());
                    }
                }
                //ASPxWebControl.RedirectOnCallback(urlsave);
            }

            if (myAction == DXSSAction.Edit)
            {
                if (status_spd.Value.ToString() == "NEW")
                {
                    UpdateMainSPD_Pnj();
                    UpdateDtlSPD_Pnj();
                    updateTotalPnj();
                    DeleteDtlBudget();
                    addCalculateTunjangan("Pengajuan Approval");
                    foreach (DataRow dr in myDetailTable.Rows)
                    {
                        InsertDtlBudget(dr, "Pengajuan Approval");
                    }

                    DeleteApproval();
                    foreach (DataRow dr in myApprovalTable.Rows)
                    {
                        insertApproval(dr, "Pengajuan Approval", "");
                    }

                    if (isSubmit)
                    {
                        UpdateApprovalDecision();
                        UpdateApproval("REQUEST APPROVAL");

                        //email notif
                        DataTable dtsendEmail = getViewApprovalEmail(strKey, "Pengajuan Approval");
                        DataRow dre = dtsendEmail.Rows[0];

                        if (dre["Email"].ToString() != null)
                        {
                            SendMailNotif(dre["Email"].ToString(), dre["Nama"].ToString(), dre["DocNo"].ToString());
                        }
                    }
                }

                if (status_spd.Value.ToString() == "ON BUSSINESS TRIP")
                {
                    DataTable dtDtlRealisasi = myLocalDBSetting.GetDataTable("select * from trxPerjalananDinasDetail where DocKey =? and TypeSPD = 'Realisasi'", false, strKey);
                    if (dtDtlRealisasi.Rows.Count == 0)
                    {
                        InsertDtlRelSPD_Pnj("Realisasi");
                        updateTotalRel();
                        addCalculateTunjanganRel("Pengajuan Realisasi");
                        foreach (DataRow dr in myDetailRealisasiTable.Rows)
                        {
                            InsertDtlBudget(dr, "Pengajuan Realisasi");
                        }
                        foreach (DataRow dr in myApprovalRealisasiTable.Rows)
                        {
                            insertApproval(dr, "Pengajuan Realisasi", "");
                        }
                        if (isSubmit)
                        {
                            UpdateApprovalDecision();
                            UpdateApproval("ON REVIEW BY HRD");

                            //Email HR
                            //string strEmailHR = "ermina.lie@mncgroup.com";
                            //string strNameHR = "ERMAWATI LIE";
                            SendMailNotifHRD(DocNo.Text, "OnReview");
                        }
                    }

                    else
                    {
                        UpdateDtlRelSPD_Pnj();
                        updateTotalRel();
                        DeleteDtlRelBudget();
                        addCalculateTunjanganRel("Pengajuan Realisasi");
                        foreach (DataRow dr in myDetailRealisasiTable.Rows)
                        {
                            InsertDtlBudget(dr, "Pengajuan Realisasi");
                        }
                        DeleteApprovalRel();
                        foreach (DataRow dr in myApprovalRealisasiTable.Rows)
                        {
                            insertApproval(dr, "Pengajuan Realisasi", "");
                        }
                        if (isSubmit)
                        {
                            UpdateApprovalDecision();
                            UpdateApproval("ON REVIEW BY HRD");

                            //Email HR
                            string strEmailHR = "ermina.lie@mncgroup.com";
                            string strNameHR = "ERMAWATI LIE";
                            SendMailNotif(strEmailHR, strNameHR, DocNo.Text);
                        }
                    }
                }

                if (status_spd.Value.ToString() == "ON REVIEW BY HRD")
                {
                    updateTotalRel();
                    DeleteDtlRelBudget();
                    addCalculateTunjanganRel("Pengajuan Realisasi");
                    foreach (DataRow dr in myDetailRealisasiTable.Rows)
                    {
                        InsertDtlBudget(dr, "Pengajuan Realisasi");
                    }
                    DeleteApprovalRel();
                    foreach (DataRow dr in myApprovalRealisasiTable.Rows)
                    {
                        insertApproval(dr, "Pengajuan Realisasi", "");
                    }
                    if (isSubmit)
                    {
                        UpdateApprovalDecision();
                        UpdateApproval("REALIZATION APPROVAL");

                        //email notif
                        DataTable dtsendEmail = getViewApprovalEmail(strKey, "Pengajuan Realisasi");
                        DataRow dre = dtsendEmail.Rows[0];

                        if (dre["Email"].ToString() != null)
                        {
                            SendMailNotif(dre["Email"].ToString(), dre["Nama"].ToString(), dre["DocNo"].ToString());
                        }
                    }
                }
            }

            return bSave;
        }

        private bool Submit(SaveAction saveAction)
        {
            bool bSave = true;

            if (myAction == DXSSAction.New)
            {
                UpdateApprovalDecision();
                UpdateApproval("REQUEST APPROVAL");
                //email notif
                //DataTable dtsendEmail = getViewApprovalEmail(strKey);
                //DataRow dre = dtsendEmail.Rows[0];

                //if (dre["Email"].ToString() != null)
                //{
                //    SendMailNotif(dre["Email"].ToString(), dre.["Nama"].ToString());
                //}

            }

            if (myAction == DXSSAction.Edit)
            {
                if (status_spd.Value.ToString() == "NEW")
                {
                    UpdateApprovalDecision();
                    UpdateApproval("REQUEST APPROVAL");
                    //email notif
                    //DataTable dtsendEmail = getViewApprovalEmail(strKey);
                    //DataRow dre = dtsendEmail.Rows[0];

                    //if (dre["Email"].ToString() != null)
                    //{
                    //    SendMailNotif(dre["Email"].ToString(), dre.["Nama"].ToString());
                    //}
                }
            }

            return bSave;
        }

        private bool Approve(SaveAction saveAction)
        {
            bool bSave = true;

            if (status_spd.Text == "REQUEST APPROVAL")
            {
                UpdateApprovalRelease("Pengajuan Approval", "APPROVE", "");
                DataTable dtSPDPeng = getViewApprovalPengajuan(strKey, "Pengajuan Approval");
                DataRow dr = dtSPDPeng.Rows[0];
                if (dr["DecisionState"].ToString() == "APPROVE" && dr["IsDecision"].ToString() == "T")
                {
                    UpdateApproval("ON BUSSINESS TRIP");
                    SendMailNotifHRD(dr["DocNo"].ToString(),"PrePayment");

                }else
                {
                    //email notif
                    DataTable dtsendEmail = getViewApprovalEmail(strKey, "Pengajuan Approval");
                    DataRow dre = dtsendEmail.Rows[0];

                    if (dre["Email"] != null)
                    {
                        SendMailNotif(dre["Email"].ToString(), dre["Nama"].ToString(), dre["DocNo"].ToString());
                    }
                    
                    if (dre["Seq"].ToString() == "1")
                    {
                        dbsetting.ExecuteNonQuery("exec[dbo].[SP_MNCL_Email_InfoSPD] ?", dre["DocNo"].ToString());
                    }
                }
            }

            else
            {
                UpdateApprovalRelease("Pengajuan Realisasi", "APPROVE", "");
                DataTable dtSPDPeng = getViewApprovalPengajuan(strKey, "Pengajuan Realisasi");
                DataRow dr = dtSPDPeng.Rows[0];
                if (dr["DecisionState"].ToString() == "APPROVE" && dr["IsDecision"].ToString() == "T")
                {
                    UpdateApproval("COMPLETE");
                    SendMailNotifHRD(dr["DocNo"].ToString(), "Realization");
                }
                else
                {
                    //email notif
                    DataTable dtsendEmail = getViewApprovalEmail(strKey, "Pengajuan Realisasi");
                    DataRow dre = dtsendEmail.Rows[0];

                    if (dre["Email"] != null)
                    {
                        SendMailNotif(dre["Email"].ToString(), dre["Nama"].ToString(), dre["DocNo"].ToString());
                    }
                }
            }


            return bSave;
        }

        private bool Reject(SaveAction saveAction)
        {
            bool bSave = true;

            if (status_spd.Text == "REQUEST APPROVAL")
            {
                UpdateApprovalRelease("Pengajuan Approval", "REJECTED", "");
                UpdateApproval("REJECT PENGAJUAN");

                //DataTable dtSPDPeng = getViewApprovalPengajuan(strKey, "Pengajuan Approval");
                //DataRow dr = dtSPDPeng.Rows[0];

                //if (dr["DecisionState"].ToString() == "REJECTED")
                //{
                //    UpdateApproval("REJECT PENGAJUAN");
                //}
            }
            else
            {
                UpdateApprovalRelease("Pengajuan Realisasi", "REJECTED", "");
                UpdateApproval("REJECT REALISASI");

                //DataTable dtSPDPeng = getViewApprovalPengajuan(strKey, "Pengajuan Realisasi");
                //DataRow dr = dtSPDPeng.Rows[0];
                //if (dr["DecisionState"].ToString() == "REJECTED" && dr["IsDecision"].ToString() == "T")
                //{
                //    UpdateApproval("REJECT REALISASI");
                //}
            }

            return bSave;
        }

        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";

            if (myAction == DXSSAction.New)
            {
                if (myFs == null)
                {
                    errorF = true;
                    strmessageError = "Upload Document is empty !";
                }

                if(myDetailTable.Rows.Count == 0)
                {
                    errorF = true;
                    strmessageError = "Rincian Anggaran is empty !";
                }

                if (myApprovalTable.Rows.Count == 0)
                {
                    errorF = true;
                    strmessageError = "Approval table is empty !";
                }
            }

            return errorF;
        }

        protected void gvDetail_Init(object sender, EventArgs e)
        {

        }

        protected void gvDetail_InitNewRow(object sender, EventArgs e)
        {

        }

        protected void gvDetail_DataBinding(object sender, EventArgs e)
        {
            //(sender as ASPxGridView).DataSource = myds.Tables[1];
            (sender as ASPxGridView).DataSource = myDetailTable;
        }

        protected void addCalculateTunjangan(string typespd)
        {
            int i;
            i = gvDetail.VisibleRowCount;
            //here
            myDetailTable.Rows.Add(i, strKey, i, typespd, "Tunjangan Perjalanan Dinas", "Tunjangan Makan Dinas", tunjanganMakan.Value, DBNull.Value);
            myDetailTable.Rows.Add(i + 1, strKey, i + 1, typespd, "Tunjangan Perjalanan Dinas", "Tunjangan Saku Dinas", uangSaku.Value, DBNull.Value);
        }

        protected void addCalculateTunjanganRel(string typespd)
        {
            int i;
            i = gvDetail.VisibleRowCount;
            //here
            myDetailRealisasiTable.Rows.Add(i, strKey, i, typespd, "Tunjangan Perjalanan Dinas", "Tunjangan Makan Dinas", tunjanganMakan.Value, DBNull.Value);
            myDetailRealisasiTable.Rows.Add(i + 1, strKey, i + 1, typespd, "Tunjangan Perjalanan Dinas", "Tunjangan Saku Dinas", uangSaku.Value, DBNull.Value);
        }


        protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["TypeBudget"] == null) throw new Exception("Column 'Tipe Tunjangan' is mandatory.");
            if (e.NewValues["BudgetDesc"] == null) throw new Exception("Column 'Description' is mandatory.");
            if (e.NewValues["BudgetAmount"] == null) throw new Exception("Column 'Amount' is mandatory.");

            if (StrErrorMsg == "")
            {
                if (myAction == DXSSAction.New || myAction == DXSSAction.Edit)
                {
                    gvDetail.JSProperties["cpCmd"] = "INSERT";

                    int i = gvDetail.VisibleRowCount;

                    string dtlTypeBudget = e.NewValues["TypeBudget"].ToString();
                    string dtlBudgetDesc = e.NewValues["BudgetDesc"].ToString();
                    double dtlBudgetAmount = Convert.ToDouble(e.NewValues["BudgetAmount"]);

                    myDetailTable.Rows.Add(i, strKey, i, "", dtlTypeBudget, dtlBudgetDesc, dtlBudgetAmount, DBNull.Value);


                    decimal vNetAmountTotal = decimal.Parse(toPNJ.Value.ToString());
                    //decimal totUM_US = Convert.ToInt32(tunjanganMakan.Value) + Convert.ToInt32(uangSaku.Value);
                    vNetAmountTotal += decimal.Parse(e.NewValues["BudgetAmount"].ToString());
                    //vNetAmountTotal = totUM_US + Convert.ToInt32(vNetAmountTotal);
                    gvDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;
                }
            }
        }

        protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["TypeBudget"] == null) throw new Exception("Column 'Tipe Tunjangan' is mandatory.");
            if (e.NewValues["BudgetDesc"] == null) throw new Exception("Column 'Description' is mandatory.");
            if (e.NewValues["BudgetAmount"] == null) throw new Exception("Column 'Amount' is mandatory.");
            if (StrErrorMsg == "")
            {
                if (myAction == DXSSAction.New || myAction == DXSSAction.Edit)
                {
                    gvDetail.JSProperties["cpCmd"] = "UPDATE";
                    int editingRowVisibleIndex = gvDetail.EditingRowVisibleIndex;
                    int id = (int)gvDetail.GetRowValues(editingRowVisibleIndex, "DtlKey");
                    //DataRow dr = myDetailTable.Rows.Find(id);

                    var searchExpression = "DtlKey = " + id.ToString();
                    DataRow[] foundRow = myDetailTable.Select(searchExpression);
                    foreach (DataRow dr in foundRow)
                    {
                        dr["TypeBudget"] = Convert.ToString(e.NewValues["TypeBudget"]);
                        dr["BudgetDesc"] = Convert.ToString(e.NewValues["BudgetDesc"]);
                        dr["BudgetAmount"] = Convert.ToString(e.NewValues["BudgetAmount"]);
                    }

                    decimal vNetAmountTotal = decimal.Parse(toPNJ.Value.ToString());
                    //decimal totUM_US = Convert.ToInt32(tunjanganMakan.Value) + Convert.ToInt32(uangSaku.Value);
                    //vNetAmountTotal = totUM_US + Convert.ToInt32(vNetAmountTotal);
                    vNetAmountTotal -= decimal.Parse(e.OldValues["BudgetAmount"].ToString());
                    vNetAmountTotal += decimal.Parse(e.NewValues["BudgetAmount"].ToString());
                    gvDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;
                }
            }
        }

        protected void gvDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            if (myAction == DXSSAction.New || myAction == DXSSAction.Edit)
            {
                gvDetail.JSProperties["cpCmd"] = "DELETE";
                int id = (int)e.Keys["DtlKey"];
                //DataRow dr = myDetailTable.Rows.Find(id);

                var searchExpression = "DtlKey = " + id.ToString();
                DataRow[] foundRow = myDetailTable.Select(searchExpression);

                foreach (DataRow dr in foundRow)
                {
                    myDetailTable.Rows.Remove(dr);
                }

                decimal vNetAmountTotal = decimal.Parse(toPNJ.Value.ToString());
                vNetAmountTotal -= decimal.Parse(e.Values["BudgetAmount"].ToString());
                gvDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }

        protected void gvDetail_CustomCallback(object sender, EventArgs e)
        {

        }

        protected void gvDetail_AutoFilterCellEditorInitialize(object sender, EventArgs e)
        {

        }

        protected void gvDetail_CustomColumnDisplayText(object sender, EventArgs e)
        {

        }

        protected void InsertRequest()
        {
            int getStrKey = Convert.ToInt32(strKey);
            string strDocNo = getDocNo();
            string ssql = @"EXEC INSERT_HEADER_PERJALANAN_DINAS @Name,@NIK,@Jabatan,@Tujuan,@PembebananBiaya,@Dept,@DocKey,@DocNo,@CRE_BY,@MOD_BY,@FromTujuan";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(ssql);
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar);
            sqlParameter1.Value = txtName.Text;
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@NIK", SqlDbType.VarChar);
            sqlParameter2.Value = nik_emp.Text;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Jabatan", SqlDbType.VarChar);
            sqlParameter3.Value = pos_emp.Text;
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@Tujuan", SqlDbType.VarChar);
            sqlParameter4.Value = negaratujuan1.Text;
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@PembebananBiaya", SqlDbType.VarChar);
            sqlParameter5.Value = spd_charge.Text;
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Dept", SqlDbType.VarChar);
            sqlParameter6.Value = spd_dept.Text;
            sqlParameter6.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
            sqlParameter7.Value = strKey;
            sqlParameter7.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@DocNo", SqlDbType.VarChar);
            sqlParameter8.Value = strDocNo;
            sqlParameter8.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@CRE_BY", SqlDbType.VarChar);
            sqlParameter9.Value = txtName.Value;
            sqlParameter9.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@MOD_BY", SqlDbType.VarChar);
            sqlParameter10.Value = txtName.Value;
            sqlParameter10.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@FromTujuan", SqlDbType.VarChar);
            sqlParameter11.Value = negaraasal1.Value == null ? DBNull.Value : negaraasal1.Value;
            sqlParameter11.Direction = ParameterDirection.Input;
            sqlCommand.ExecuteNonQuery();
            myconn.Close();
        }

        protected void InsertDtlSPD_Pnj(string TypeSPD)
        {
            string ssql = @"INSERT INTO [SSS].[dbo].[trxPerjalananDinasDetail] ([DocKey],[TypeSPD],[StartDate],[EndDate],[JumlahHari],[Kendaraan],[Tujuan],[FromTujuan])
                                    VALUES(@DocKey,@TypeSPD,@StartDate,@EndDate,@JumlahHari,@Kendaraan,@Tujuan,@FromTujuan)";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(ssql);
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
            sqlParameter1.Value = strKey;
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@TypeSPD", SqlDbType.VarChar);
            sqlParameter2.Value = TypeSPD;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime);
            sqlParameter3.Value = tglKeberangkatan.Value;
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime);
            sqlParameter4.Value = tglKepulangan.Value;
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@JumlahHari", SqlDbType.Int);
            sqlParameter5.Value = mandays1.Value;
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Kendaraan", SqlDbType.VarChar);
            sqlParameter6.Value = byvehicle1.Value == null ? DBNull.Value : byvehicle1.Value;
            sqlParameter6.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@Tujuan", SqlDbType.VarChar);
            sqlParameter7.Value = negaratujuan1.Value == null ? DBNull.Value : negaratujuan1.Value;
            sqlParameter7.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@FromTujuan", SqlDbType.VarChar);
            sqlParameter8.Value = negaraasal1.Value == null ? DBNull.Value : negaraasal1.Value;
            sqlParameter8.Direction = ParameterDirection.Input;

            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        protected void InsertDtlRelSPD_Pnj(string TypeSPD)
        {
            string ssql = @"INSERT INTO [SSS].[dbo].[trxPerjalananDinasDetail] ([DocKey],[TypeSPD],[StartDate],[EndDate],[JumlahHari],[Kendaraan],[Tujuan],[FromTujuan])
                                    VALUES(@DocKey,@TypeSPD,@StartDate,@EndDate,@JumlahHari,@Kendaraan,@Tujuan,@FromTujuan)";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(ssql);
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
            sqlParameter1.Value = strKey;
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@TypeSPD", SqlDbType.VarChar);
            sqlParameter2.Value = TypeSPD;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime);
            sqlParameter3.Value = tglKeberangkatan2.Value;
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime);
            sqlParameter4.Value = tglKepulangan2.Value;
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@JumlahHari", SqlDbType.Int);
            sqlParameter5.Value = mandays2.Value;
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Kendaraan", SqlDbType.VarChar);
            sqlParameter6.Value = byvehicle2.Value == null ? DBNull.Value : byvehicle2.Value;
            sqlParameter6.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@Tujuan", SqlDbType.VarChar);
            sqlParameter7.Value = negaratujuan2.Value == null ? DBNull.Value : negaratujuan2.Value;
            sqlParameter7.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@FromTujuan", SqlDbType.VarChar);
            sqlParameter8.Value = negaraasal2.Value == null ? DBNull.Value : negaraasal2.Value;
            sqlParameter8.Direction = ParameterDirection.Input;

            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        protected void UpdateMainSPD_Pnj()
        {
            string ssql = "UPDATE trxPerjalananDinas SET Tujuan=?, FromTujuan=? WHERE DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, negaratujuan1.Text, negaraasal1.Text, strKey);
        }

        protected void UpdateMainSPD_Rel()
        {
            string ssql = "UPDATE trxPerjalananDinas SET Tujuan=?, FromTujuan=? WHERE DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, negaratujuan2.Text, negaraasal2.Text, strKey);
        }

        protected void UpdateDtlSPD_Pnj()
        {
            string ssql = "UPDATE trxPerjalananDinasDetail SET StartDate=?, EndDate=?, JumlahHari=?, Kendaraan=?, Tujuan =?, FromTujuan=? WHERE DocKey=? and TypeSPD = 'Pengajuan'";
            myLocalDBSetting.ExecuteNonQuery(ssql, tglKeberangkatan.Value, tglKepulangan.Value, mandays1.Value, byvehicle1.Value, negaratujuan1.Text, negaraasal1.Text, strKey);
        }

        protected void UpdateApprovalDecision()
        {
            string ssql = "UPDATE trxPerjalananDinasApprovalList SET IsDecision='F' WHERE DocKey=? and ISNULL(IsDecision,'')<>'T'";
            myLocalDBSetting.ExecuteNonQuery(ssql, strKey);
        }

        protected void UpdateDtlRelSPD_Pnj()
        {
            string ssql = "UPDATE trxPerjalananDinasDetail SET StartDate=?, EndDate=?, JumlahHari=?, Kendaraan=?, Tujuan =?, FromTujuan=? WHERE DocKey=? and TypeSPD = 'Realisasi'";
            myLocalDBSetting.ExecuteNonQuery(ssql, tglKeberangkatan2.Value, tglKepulangan2.Value, mandays2.Value, byvehicle2.Value, negaratujuan2.Text, negaraasal2.Text, strKey);
        }

        protected void updateTotalPnj()
        {
            string ssql = "UPDATE trxPerjalananDinas SET TotalPengajuan=? WHERE DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, toPNJ.Value, strKey);
        }

        protected void updateTotalRel()
        {
            string ssql = "UPDATE trxPerjalananDinas SET TotalRealisasi=? WHERE DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, toREL.Value, strKey);
        }

        protected void InsertDtlSPD_Rel(string TypeSPD)
        {
            string ssql = @"INSERT INTO [SSS].[dbo].[trxPerjalananDinasDetail] ([DocKey],[TypeSPD],[StartDate],[EndDate],[JumlahHari],[Kendaraan])
                                    VALUES(@DocKey,@TypeSPD,@StartDate,@EndDate,@JumlahHari,@Kendaraan)";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(ssql);
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
            sqlParameter1.Value = strKey;
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@TypeSPD", SqlDbType.VarChar);
            sqlParameter2.Value = TypeSPD;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime);
            sqlParameter3.Value = tglKeberangkatan2.Value == null ? DBNull.Value : tglKeberangkatan2.Value;
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime);
            sqlParameter4.Value = tglKepulangan2.Value == null ? DBNull.Value : tglKepulangan2.Value;
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@JumlahHari", SqlDbType.Int);
            sqlParameter5.Value = mandays2.Value == null ? DBNull.Value : mandays2.Value;
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Kendaraan", SqlDbType.VarChar);
            sqlParameter6.Value = byvehicle2.Value == null ? "" : byvehicle2.Value;
            sqlParameter6.Direction = ParameterDirection.Input;

            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        protected void InsertDtlBudget(DataRow dataRows, string TypeSPD)
        {
            string ssql = @"INSERT INTO [SSS].[dbo].[trxPerjalananDinasDetailBudget] ([DocKey],[Seq],[TypeSPD],[TypeBudget],[BudgetDesc],[BudgetAmount])
                                    VALUES(@DocKey,@Seq,@TypeSPD,@TypeBudget,@BudgetDesc,@BudgetAmount)";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(ssql);
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
            sqlParameter1.Value = dataRows.Field<int>("DocKey");
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@Seq", SqlDbType.Int);
            sqlParameter2.Value = dataRows.Field<int>("Seq");
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@TypeSPD", SqlDbType.VarChar);
            sqlParameter3.Value = TypeSPD;
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@TypeBudget", SqlDbType.VarChar);
            sqlParameter4.Value = dataRows.Field<string>("TypeBudget");
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@BudgetDesc", SqlDbType.VarChar);
            sqlParameter5.Value = dataRows.Field<string>("BudgetDesc");
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@BudgetAmount", SqlDbType.Decimal);
            sqlParameter6.Value = dataRows.Field<Decimal>("BudgetAmount");
            sqlParameter6.Direction = ParameterDirection.Input;

            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        protected void DeleteDtlBudget()
        {
            string ssql = "DELETE trxPerjalananDinasDetailBudget where TypeSPD='Pengajuan Approval' and DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, strKey);
        }

        protected void DeleteApproval()
        {
            string ssql = "DELETE trxPerjalananDinasApprovalList where TypeApproval='Pengajuan Approval' and DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, strKey);
        }

        protected void DeleteDtlRelBudget()
        {
            string ssql = "DELETE trxPerjalananDinasDetailBudget where TypeSPD='Pengajuan Realisasi' and DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, strKey);
        }

        protected void DeleteApprovalRel()
        {
            string ssql = "DELETE trxPerjalananDinasApprovalList where TypeApproval='Pengajuan Realisasi' and DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, strKey);
        }

        protected void DeleteUS_UM()
        {
            string ssql = "WITH  del AS(SELECT TOP 2 *FROM trxPerjalananDinasDetailBudget where DocKey=? and TypeBudget = 'Tunjangan Perjalanan Dinas') DELETE FROM del";
            myLocalDBSetting.ExecuteNonQuery(ssql, strKey);
        }

        protected void insertApproval(DataRow dataRow, string TypeApproval, string isDecision)
        {
            string ssql = @"insert into [SSS].[dbo].[trxPerjalananDinasApprovalList] ([DocKey],[TypeApproval],[Seq],[NIK],[Nama],[Jabatan],[IsDecision],[DecisionState],[DecisionDate],[DecisionNote],[Email])
                        values(@DocKey,@TypeApproval,@Seq,@NIK,@Nama,@Jabatan,@IsDecision,@DecisionState,@DecisionDate,@DecisionNote,@Email)";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(ssql);
            sqlCommand.Connection = myconn;
            myconn.Open();
            //i, strkey, "pengajuan", i , dtlnik , dtlnama, dtljabatan, "t", dbnull.value, dbnull.value, dbnull.value, dbnull.value
            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
            sqlParameter1.Value = dataRow.Field<int>("DocKey");
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@TypeApproval", SqlDbType.VarChar);
            sqlParameter2.Value = TypeApproval;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Seq", SqlDbType.Int);
            sqlParameter3.Value = dataRow.Field<int>("Seq");
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@NIK", SqlDbType.VarChar);
            sqlParameter4.Value = dataRow.Field<string>("NIK");
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Nama", SqlDbType.VarChar);
            sqlParameter5.Value = dataRow.Field<string>("Nama");
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Jabatan", SqlDbType.VarChar);
            sqlParameter6.Value = dataRow.Field<string>("Jabatan");
            sqlParameter6.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@IsDecision", SqlDbType.VarChar);
            sqlParameter7.Value = isDecision;
            sqlParameter7.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@DecisionState", SqlDbType.VarChar);
            sqlParameter8.Value = dataRow.Field<string>("DecisionState") == null ? "" : dataRow.Field<string>("DecisionState");
            sqlParameter8.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@DecisionDate", SqlDbType.DateTime);
            //sqlParameter9.Value = dataRow.Field<DateTime>("DecisionDate");
            sqlParameter9.Value = DBNull.Value;
            sqlParameter9.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@DecisionNote", SqlDbType.VarChar);
            sqlParameter10.Value = dataRow.Field<string>("DecisionNote") == null ? "" : dataRow.Field<string>("DecisionNote");
            sqlParameter10.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar);
            sqlParameter11.Value = dataRow.Field<string>("Email") == null ? "" : dataRow.Field<string>("Email");
            sqlParameter11.Direction = ParameterDirection.Input;
            sqlCommand.ExecuteNonQuery();
            myconn.Close();
        }

        protected void insertApprovalRealisasi(DataRow dataRow)
        {
            string ssql = @"insert into [SSS].[dbo].[trxPerjalananDinasApprovalList] ([DocKey],[TypeApproval],[Seq],[NIK],[Nama],[Jabatan],[IsDecision],[DecisionState],[DecisionDate],[DecisionNote],[Email])
                        values(@DocKey,@TypeApproval,@Seq,@NIK,@Nama,@Jabatan,@IsDecision,@DecisionState,@DecisionDate,@DecisionNote,@Email)";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(ssql);
            sqlCommand.Connection = myconn;
            myconn.Open();
            //i, strkey, "pengajuan", i , dtlnik , dtlnama, dtljabatan, "t", dbnull.value, dbnull.value, dbnull.value, dbnull.value
            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
            sqlParameter1.Value = dataRow.Field<int>("DocKey");
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@TypeApproval", SqlDbType.VarChar);
            sqlParameter2.Value = dataRow.Field<string>("TypeApproval");
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Seq", SqlDbType.Int);
            sqlParameter3.Value = dataRow.Field<int>("Seq");
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@NIK", SqlDbType.VarChar);
            sqlParameter4.Value = dataRow.Field<string>("NIK");
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Nama", SqlDbType.VarChar);
            sqlParameter5.Value = dataRow.Field<string>("Nama");
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Jabatan", SqlDbType.VarChar);
            sqlParameter6.Value = dataRow.Field<string>("Jabatan");
            sqlParameter6.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@IsDecision", SqlDbType.VarChar);
            sqlParameter7.Value = dataRow.Field<string>("IsDecision");
            sqlParameter7.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@DecisionState", SqlDbType.VarChar);
            sqlParameter8.Value = dataRow.Field<string>("DecisionState") == null ? "" : dataRow.Field<string>("DecisionState");
            sqlParameter8.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@DecisionDate", SqlDbType.DateTime);
            sqlParameter9.Value = dataRow.Field<DateTime>("DecisionDate");
            sqlParameter9.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@DecisionNote", SqlDbType.VarChar);
            sqlParameter10.Value = dataRow.Field<string>("DecisionNote") == null ? "" : dataRow.Field<string>("DecisionNote");
            sqlParameter10.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar);
            sqlParameter11.Value = dataRow.Field<string>("Email") == null ? "" : dataRow.Field<string>("Email");
            sqlParameter11.Direction = ParameterDirection.Input;

            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        //ganti disini
        private bool SaveUploadDocSPD(string docType, string docNote, string docNo)
        {
            bool bSave = true;
            BinaryReader br = new BinaryReader(myFs);
            Byte[] bytes = br.ReadBytes((Int32)myFs.Length);

            SqlConnection myconn = new SqlConnection(localdbsetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                string sQuery = @"INSERT INTO DocumentUploadSPD
                                    VALUES (@DocKey, @Type, @Ext, @Notes, @CreatedBy, @CreatedDateTime, @FileSize, @FileDoc, @SPDNo)";
                SqlCommand sqlCommand = new SqlCommand(sQuery);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;
                sqlCommand.Transaction = trans;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = strKey;

                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("Type", SqlDbType.NVarChar);
                sqlParameter2.Value = docType;

                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Ext", SqlDbType.NVarChar);
                sqlParameter3.Value = resultExtension;

                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@Notes", SqlDbType.NVarChar);
                sqlParameter4.Value = docNote;

                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar);
                sqlParameter5.Value = UserID;

                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime);
                sqlParameter6.Value = localdbsetting.GetServerTime();

                SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@FileSize", SqlDbType.NVarChar);
                sqlParameter7.Value = sizeText;

                SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@FileDoc", SqlDbType.Binary);
                sqlParameter8.Value = bytes;

                SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@SPDNo", SqlDbType.NVarChar);
                sqlParameter9.Value = docNo;

                sqlCommand.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
            return bSave;
        }

        private void SendMailNotif(string emailTo, string employeename, string docNo)
        {
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(@"sp_SSS_SendMail");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = myconn;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@profile_name", SqlDbType.NVarChar);
                sqlParameter1.Value = "SQLMelisa";

                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@recipients", SqlDbType.NVarChar);
                sqlParameter2.Value = emailTo;

                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@copy_recipients", SqlDbType.NVarChar);
                sqlParameter3.Value = "";

                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@body", SqlDbType.NVarChar);
                sqlParameter4.Value = "Hi " + employeename + ", this Business Trip Form with no : " + docNo + " need your approval, please login to Smile Support as soon as posible. Thankyou!";
                //mynextapprovalrow["NIK"]
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@subject", SqlDbType.NVarChar);
                sqlParameter5.Value = "Approval Business Trip - Notification";

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

        private void SendMailNotifHRD(string docNo, string typeApproval)
        {
            string bodyTextEmail = "";
            string subjectTextEmail = "";
            string emailAddress = "";
            if (typeApproval == "PrePayment")
            {
                bodyTextEmail = "Hi HR MNC Leasing, this Business Trip Form with no : " + docNo + " pre payment approval is complete. Thankyou!";
                subjectTextEmail = "Pre Payment Business Trip - Notification";
                //emailAddress = "hr.mncleasing@mncgroup.com";
                emailAddress = "hr.mncleasing@mncgroup.com;khristiana.biringkanae@mncgroup.com";
            }
            else if(typeApproval == "OnReview")
            {
                bodyTextEmail = "Hi HR MNC Leasing, this Business Trip Realization form with no : " + docNo + " need your review. Thankyou!";
                subjectTextEmail = "Review Business Trip Realization - Notification";
                emailAddress = "ermina.lie@mncgroup.com";
            }
            else
            {
                bodyTextEmail = "Hi HR MNC Leasing, this Business Trip Form with no : " + docNo + " realization approval is complete. Thankyou!";
                subjectTextEmail = "Realization Business Trip - Notification";
                emailAddress = "hr.mncleasing@mncgroup.com";
            }

            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(@"sp_SSS_SendMail");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = myconn;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@profile_name", SqlDbType.NVarChar);
                sqlParameter1.Value = "SQLMelisa";

                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@recipients", SqlDbType.NVarChar);
                sqlParameter2.Value = emailAddress;

                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@copy_recipients", SqlDbType.NVarChar);
                sqlParameter3.Value = "";

                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@body", SqlDbType.NVarChar);
                sqlParameter4.Value = bodyTextEmail;
                //mynextapprovalrow["NIK"]
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@subject", SqlDbType.NVarChar);
                sqlParameter5.Value = subjectTextEmail;

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

        protected void UpdateApproval(string Status)
        {
            string ssql = "UPDATE trxPerjalananDinas SET [Status]=? WHERE DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, Status, strKey);
        }

        protected void getUpdateDtl(string DocKey)
        {
            string ssql = "select * from trxPerjalananDinasDetail where TypeSPD = 'Realisasi' and DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, strKey);
        }

        protected void UpdateApprovalRelease(string TypeApproval, string decState, string strNote)
        {
            string ssql = "UPDATE trxPerjalananDinasApprovalList SET IsDecision='T', DecisionState=?, DecisionNote=?, DecisionDate=Getdate() WHERE DocKey=? and NIK=? and TypeApproval=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, decState, strNote, strKey, UserID, TypeApproval);
        }

        protected DataTable getViewPerjalananDinas(string docKey)
        {
            string ssql = @"select * from [dbo].[trxPerjalananDinas]";

            DataTable res = new DataTable();
            res = myLocalDBSetting.GetDataTable(ssql, false, docKey);

            return res;
        }

        protected DataTable getDataApproval(string value)
        {
            DataTable res = new DataTable();
            string ssql = "select * from [SSS].[dbo].[trxPerjalananDinasApprovalList] where DocKey = ?";
            res = myLocalDBSetting.GetDataTable(ssql, false, value);

            return res;
        }

        protected void gvApproval_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myApprovalTable;
        }

        protected void gvApproval_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["Nama"] == null) throw new Exception("Column 'Nama' is mandatory.");
            if (e.NewValues["NIK"] == null) throw new Exception("Column 'Nik' is mandatory.");
            if (e.NewValues["Jabatan"] == null) throw new Exception("Column 'Jabatan' is mandatory.");
            if (StrErrorMsg == "")
            {
                if (myAction == DXSSAction.New)
                {
                    gvApproval.JSProperties["cpCmd"] = "INSERT";

                    int i = gvApproval.VisibleRowCount;

                    string dtlNIK = e.NewValues["NIK"].ToString();
                    string dtlNama = e.NewValues["Nama"].ToString();
                    string dtlJabatan = e.NewValues["Jabatan"].ToString();
                    string dtlEmail = e.NewValues["Email"].ToString();

                    myApprovalTable.Rows.Add(i, strKey, "Pengajuan Approval", i, dtlNIK, dtlNama, dtlJabatan, "", DBNull.Value, DBNull.Value, DBNull.Value, dtlEmail);

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;
                }

                if (myAction == DXSSAction.Edit)
                {
                    gvApproval.JSProperties["cpCmd"] = "INSERT";

                    int i = gvApproval.VisibleRowCount;

                    string dtlNIK = e.NewValues["NIK"].ToString();
                    string dtlNama = e.NewValues["Nama"].ToString();
                    string dtlJabatan = e.NewValues["Jabatan"].ToString();
                    string dtlEmail = e.NewValues["Email"].ToString();

                    myApprovalTable.Rows.Add(DBNull.Value, strKey, "Pengajuan Approval", i + 1, dtlNIK, dtlNama, dtlJabatan, "", DBNull.Value, DateTime.Now, DBNull.Value, dtlEmail);

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;
                }
            }
        }

        protected void gvApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["Nama"] == null) throw new Exception("Column 'Nama' is mandatory.");
            if (e.NewValues["NIK"] == null) throw new Exception("Column 'Nik' is mandatory.");
            if (e.NewValues["Jabatan"] == null) throw new Exception("Column 'Jabatan' is mandatory.");

            if (StrErrorMsg == "")
            {
                if (myAction == DXSSAction.New || myAction == DXSSAction.Edit)
                {
                    gvApproval.JSProperties["cpCmd"] = "UPDATE";
                    int editingRowVisibleIndex = gvApproval.EditingRowVisibleIndex;
                    int id = (int)gvApproval.GetRowValues(editingRowVisibleIndex, "DtlKey");

                    DataRow dr = myApprovalTable.Rows.Find(id);

                    var searchExpression = "DtlKey = " + id.ToString();
                    DataRow[] foundRow = myApprovalTable.Select(searchExpression);
                    foreach (DataRow dr2 in foundRow)
                    {
                        dr["NIK"] = Convert.ToString(e.NewValues["NIK"]);
                        dr["Nama"] = Convert.ToString(e.NewValues["Nama"]);
                        dr["Jabatan"] = Convert.ToString(e.NewValues["Jabatan"]);
                        dr["Email"] = Convert.ToString(e.NewValues["Email"]);
                    }

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;
                }

            }
        }

        protected void gvApproval_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            if (myAction == DXSSAction.New || myAction == DXSSAction.Edit)
            {
                gvApproval.JSProperties["cpCmd"] = "DELETE";
                int id = (int)e.Keys["DtlKey"];
                //DataRow dr = myDetailTable.Rows.Find(id);

                var searchExpression = "DtlKey = " + id.ToString();
                DataRow[] foundRow = myApprovalTable.Select(searchExpression);
                foreach (DataRow dr in foundRow)
                {
                    myApprovalTable.Rows.Remove(dr);
                }

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }

        protected void gvApproval_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void gvApproval_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Nama")
            {
                isValidLogin(false);
                if (Page.IsCallback)
                {
                    DataTable myitem = new DataTable();
                    myitem = myDBSetting.GetDataTable("SELECT CODE as NIK, DESCS as Nama, EMAIL as Email, '' as Jabatan FROM SYS_TBLEMPLOYEE", false);
                    ASPxComboBox cmb = (ASPxComboBox)e.Editor;
                    cmb.DataSource = myitem;
                    cmb.DataBindItems();
                }
            }
        }

        protected void gvApproval_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }

        protected void gvApproval_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }

        protected string getDocNo()
        {
            string res = "";

            DataRow[] myrowDocNo = myLocalDBSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='SPD'", "", DataViewRowState.CurrentRows);
            if (myrowDocNo != null)
            {
                try
                {
                    res = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myDBSetting.GetServerTime());
                }
                catch (Exception ex)
                {
                    myLocalDBSetting.Rollback();
                }
                myLocalDBSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType = 'SPD'");
            }

            return res;
        }

        protected DataTable getViewSPDDoc(string DocKey)
        {
            string ssql = @"select top 1 a.*,b.*,c.* from trxPerjalananDinas a 
                            left join dbo.trxPerjalananDinasDetail b on a.DocKey = b.DocKey
                            left join (select top 1 b.DocKey, DtlKey,StartDate as StartDate2,EndDate as EndDate2,JumlahHari as JumlahHari2,Kendaraan as Kendaraan2, b.Tujuan as Tujuan2 
                            from trxPerjalananDinasDetail a
                            left join trxPerjalananDinas b on a.DocKey = b.DocKey 
                            where TypeSPD = 'Realisasi' order by DtlKey desc)c on a.DocKey = c.DocKey
                            where a.DocKey=?";

            DataTable res = new DataTable();
            res = myLocalDBSetting.GetDataTable(ssql, false, DocKey);
            res.Dispose();
            return res;
        }

        protected DataTable getViewDtlRel(string DocKey, string typespd)
        {
            string ssql = @"select * from trxPerjalananDinasDetail where DocKey=? and TypeSPD=?";

            DataTable res = new DataTable();
            res = myLocalDBSetting.GetDataTable(ssql, false, DocKey, typespd);
            res.Dispose();
            return res;
        }

        protected DataTable getdetailBudget(string DocKey)
        {
            string ssql = @"select * from trxPerjalananDinasDetailBudget where DocKey=?";
            DataTable res = new DataTable();
            res = myLocalDBSetting.GetDataTable(ssql, false, DocKey);
            res.Dispose();
            return res;
        }

        protected DataTable GetListBranch()
        {
            string ssql = @"select C_CODE as [Kode_Cabang],C_NAME as [Nama_Cabang] from SYS_COMPANY";
            DataTable res = new DataTable();
            res = myDBSetting.GetDataTable(ssql, false);
            res.Dispose();
            return res;
        }

        protected DataTable getViewApprovalPengajuan(string DocKey, string TypeApproval)
        {
            //string ssql = @"SELECT TOP 1 * FROM SSS.dbo.trxPerjalananDinasApprovalList WHERE DocKey=? and TypeApproval=? ORDER BY Seq DESC";
            string ssql = @"SELECT TOP 1 b.*, a.DocNo FROM SSS.dbo.trxPerjalananDinas a inner join SSS.dbo.trxPerjalananDinasApprovalList b on a.DocKey = b.DocKey WHERE a.DocKey=? and b.TypeApproval=? ORDER BY b.Seq DESC";

            DataTable res = new DataTable();
            res = myLocalDBSetting.GetDataTable(ssql, false, DocKey, TypeApproval);

            return res;
        }

        protected DataTable getViewApprovalEmail(string DocKey, string typeApproval)
        {
            //string ssql = @"SELECT TOP 1 * FROM SSS.dbo.trxPerjalananDinasApprovalList WHERE DocKey=? and IsDecision = 'F' and TypeApproval=? ORDER BY Seq ASC";

            string ssql = "SELECT TOP 1 b.*, a.DocNo FROM SSS.dbo.trxPerjalananDinas a inner join SSS.dbo.trxPerjalananDinasApprovalList b on a.DocKey = b.DocKey WHERE a.DocKey=? and b.IsDecision = 'F' and b.TypeApproval=? ORDER BY b.Seq ASC";

            DataTable res = new DataTable();
            res = myLocalDBSetting.GetDataTable(ssql, false, DocKey, typeApproval);

            return res;
        }

        protected void UploadCtrl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            sizeText = "";
            resultExtension = Path.GetExtension(e.UploadedFile.FileName);
            resultFileName = "SPDDoc_" + Path.ChangeExtension(Path.GetRandomFileName(), resultExtension);
            resultFileUrl = UploadDirectory + resultFileName;
            resultFilePath = MapPath(resultFileUrl);
            e.UploadedFile.SaveAs(resultFilePath);
            name = e.UploadedFile.FileName;
            url = ResolveClientUrl(resultFileUrl);
            sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
            sizeText = sizeInKilobytes.ToString() + " KB";
            e.CallbackData = resultFileName;
            myFs = e.UploadedFile.FileContent;
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
                        FileDocID = System.Convert.ToInt32(obj);
                    }
                    ASPxWebControl.RedirectOnCallback(string.Format("PerjalananDinasDoc.aspx?ID=" + FileDocID.ToString()));

                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = mySPDDOCTable;
        }

        protected void gvRealisasiPerjalananDinas_Init(object sender, EventArgs e)
        {

        }

        protected void gvRealisasiPerjalananDinas_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }

        protected void gvRealisasiPerjalananDinas_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myDetailRealisasiTable;
        }

        protected void gvRealisasiPerjalananDinas_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["TypeBudget"] == null) throw new Exception("Column 'Tipe Tunjangan' is mandatory.");
            if (e.NewValues["BudgetDesc"] == null) throw new Exception("Column 'Description' is mandatory.");
            if (e.NewValues["BudgetAmount"] == null) throw new Exception("Column 'Amount' is mandatory.");

            if (StrErrorMsg == "")
            {
                if (myAction == DXSSAction.New || myAction == DXSSAction.Edit)
                {
                    gvRealisasiPerjalananDinas.JSProperties["cpCmd"] = "INSERT";

                    int i = gvRealisasiPerjalananDinas.VisibleRowCount;

                    string dtlTypeBudget = e.NewValues["TypeBudget"].ToString();
                    string dtlBudgetDesc = e.NewValues["BudgetDesc"].ToString();
                    double dtlBudgetAmount = Convert.ToDouble(e.NewValues["BudgetAmount"]);

                    myDetailRealisasiTable.Rows.Add(i, strKey, i, "", dtlTypeBudget, dtlBudgetDesc, dtlBudgetAmount, DBNull.Value);

                    decimal vNetAmountTotal = decimal.Parse(toREL.Value.ToString());
                    vNetAmountTotal += decimal.Parse(e.NewValues["BudgetAmount"].ToString());
                    gvRealisasiPerjalananDinas.JSProperties["cpTotal2"] = (vNetAmountTotal).ToString();

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;

                }
            }
        }

        protected void gvRealisasiPerjalananDinas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["TypeBudget"] == null) throw new Exception("Column 'Tipe Tunjangan' is mandatory.");
            if (e.NewValues["BudgetDesc"] == null) throw new Exception("Column 'Description' is mandatory.");
            if (e.NewValues["BudgetAmount"] == null) throw new Exception("Column 'Amount' is mandatory.");

            if (StrErrorMsg == "")
            {
                if (myAction == DXSSAction.New || myAction == DXSSAction.Edit)
                {
                    gvRealisasiPerjalananDinas.JSProperties["cpCmd"] = "UPDATE";
                    int editingRowVisibleIndex = gvRealisasiPerjalananDinas.EditingRowVisibleIndex;
                    int id = (int)gvRealisasiPerjalananDinas.GetRowValues(editingRowVisibleIndex, "DtlKey");
                    //DataRow dr = myDetailTable.Rows.Find(id);

                    var searchExpression = "DtlKey = " + id.ToString();
                    DataRow[] foundRow = myDetailRealisasiTable.Select(searchExpression);
                    foreach (DataRow dr in foundRow)
                    {
                        dr["TypeBudget"] = Convert.ToString(e.NewValues["TypeBudget"]);
                        dr["BudgetDesc"] = Convert.ToString(e.NewValues["BudgetDesc"]);
                        dr["BudgetAmount"] = Convert.ToString(e.NewValues["BudgetAmount"]);
                    }

                    decimal vNetAmountTotal = decimal.Parse(toREL.Value.ToString());
                    vNetAmountTotal -= decimal.Parse(e.OldValues["BudgetAmount"].ToString());
                    vNetAmountTotal += decimal.Parse(e.NewValues["BudgetAmount"].ToString());
                    gvRealisasiPerjalananDinas.JSProperties["cpTotal2"] = (vNetAmountTotal).ToString();

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;
                }
            }
        }

        protected void gvRealisasiPerjalananDinas_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            if (myAction == DXSSAction.New || myAction == DXSSAction.Edit)
            {
                gvRealisasiPerjalananDinas.JSProperties["cpCmd"] = "DELETE";
                int id = (int)e.Keys["DtlKey"];
                //DataRow dr = myDetailTable.Rows.Find(id);

                var searchExpression = "DtlKey = " + id.ToString();
                DataRow[] foundRow = myDetailRealisasiTable.Select(searchExpression);
                foreach (DataRow dr in foundRow)
                {
                    myDetailRealisasiTable.Rows.Remove(dr);
                }

                decimal vNetAmountTotal = decimal.Parse(toREL.Value.ToString());
                vNetAmountTotal -= decimal.Parse(e.Values["BudgetAmount"].ToString());
                gvRealisasiPerjalananDinas.JSProperties["cpTotal2"] = (vNetAmountTotal).ToString();

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }

        protected void gvRealisasiPerjalananDinas_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void gvRealisasiPerjalananDinas_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }

        protected void gvRealisasiPerjalananDinas_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {

        }

        protected void gvApprovalRealisasi_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myApprovalRealisasiTable;
        }

        protected void gvApprovalRealisasi_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["Nama"] == null) throw new Exception("Column 'Nama' is mandatory.");
            if (e.NewValues["NIK"] == null) throw new Exception("Column 'Nik' is mandatory.");
            if (e.NewValues["Jabatan"] == null) throw new Exception("Column 'Jabatan' is mandatory.");
            if (StrErrorMsg == "")
            {
                if (myAction == DXSSAction.New)
                {
                    gvApprovalRealisasi.JSProperties["cpCmd"] = "INSERT";

                    int i = gvApprovalRealisasi.VisibleRowCount;

                    string dtlNIK = e.NewValues["NIK"].ToString();
                    string dtlNama = e.NewValues["Nama"].ToString();
                    string dtlJabatan = e.NewValues["Jabatan"].ToString();
                    string dtlEmail = e.NewValues["Email"].ToString();

                    myApprovalRealisasiTable.Rows.Add(i, strKey, "Pengajuan Realisasi", i, dtlNIK, dtlNama, dtlJabatan, "", DBNull.Value, DBNull.Value, DBNull.Value, dtlEmail);

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;
                }

                if (myAction == DXSSAction.Edit)
                {
                    gvApprovalRealisasi.JSProperties["cpCmd"] = "INSERT";

                    int i = gvApprovalRealisasi.VisibleRowCount;

                    string dtlNIK = e.NewValues["NIK"].ToString();
                    string dtlNama = e.NewValues["Nama"].ToString();
                    string dtlJabatan = e.NewValues["Jabatan"].ToString();
                    string dtlEmail = e.NewValues["Email"].ToString();

                    myApprovalRealisasiTable.Rows.Add(DBNull.Value, strKey, "Pengajuan Realisasi", i + 1, dtlNIK, dtlNama, dtlJabatan, "", DBNull.Value, DateTime.Now, DBNull.Value, dtlEmail);

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;
                }
            }
        }

        protected void gvApprovalRealisasi_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["Nama"] == null) throw new Exception("Column 'Nama' is mandatory.");
            if (e.NewValues["NIK"] == null) throw new Exception("Column 'Nik' is mandatory.");
            if (e.NewValues["Jabatan"] == null) throw new Exception("Column 'Jabatan' is mandatory.");
            if (StrErrorMsg == "")
            {
                if (myAction == DXSSAction.New || myAction == DXSSAction.Edit)
                {
                    gvApprovalRealisasi.JSProperties["cpCmd"] = "UPDATE";
                    int editingRowVisibleIndex = gvApproval.EditingRowVisibleIndex;
                    int id = (int)gvApprovalRealisasi.GetRowValues(editingRowVisibleIndex, "DtlKey");

                    DataRow dr = myApprovalRealisasiTable.Rows.Find(id);

                    var searchExpression = "DtlKey = " + id.ToString();
                    DataRow[] foundRow = myApprovalRealisasiTable.Select(searchExpression);
                    foreach (DataRow dr2 in foundRow)
                    {
                        dr["NIK"] = Convert.ToString(e.NewValues["NIK"]);
                        dr["Nama"] = Convert.ToString(e.NewValues["Nama"]);
                        dr["Jabatan"] = Convert.ToString(e.NewValues["Jabatan"]);
                        dr["Email"] = Convert.ToString(e.NewValues["Email"]);
                    }

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;
                }

            }
        }

        protected void gvApprovalRealisasi_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            if (myAction == DXSSAction.New || myAction == DXSSAction.Edit)
            {
                gvApprovalRealisasi.JSProperties["cpCmd"] = "DELETE";
                int id = (int)e.Keys["DtlKey"];
                //DataRow dr = myDetailTable.Rows.Find(id);

                var searchExpression = "DtlKey = " + id.ToString();
                DataRow[] foundRow = myApprovalRealisasiTable.Select(searchExpression);
                foreach (DataRow dr in foundRow)
                {
                    myApprovalRealisasiTable.Rows.Remove(dr);
                }

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }

        protected void gvApprovalRealisasi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void gvApprovalRealisasi_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }

        protected void gvApprovalRealisasi_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Nama")
            {
                isValidLogin(false);
                if (Page.IsCallback)
                {
                    DataTable myitem = new DataTable();
                    myitem = myDBSetting.GetDataTable("SELECT CODE as NIK, DESCS as Nama, EMAIL as Email, '' as Jabatan FROM SYS_TBLEMPLOYEE", false);
                    ASPxComboBox cmb = (ASPxComboBox)e.Editor;
                    cmb.DataSource = myitem;
                    cmb.DataBindItems();
                }
            }
        }

        protected void gvApprovalRealisasi_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {

        }

        protected void AddHeadOfUser()
        {
            string ssql = @"select top 1
            	a.HEAD, 
            	a.HEAD_DESCS,
            	c.USERGROUPDESC,
            	d.EMAIL
            from SYS_TBLEMPLOYEE a
            left join MASTER_USER_COMPANY_GROUP b on a.HEAD = b.USER_ID
            left join MASTER_GROUP c on b.GROUP_CODE = c.USERGROUP
            left join SYS_TBLEMPLOYEE d on a.HEAD = d.CODE and d.ISACTIVE = 1
            where a.CODE = ? and a.HEAD not in ('1906024','1907003','U0106546')";
            DataTable dtHeadUser = myDBSetting.GetDataTable(ssql, false, UserID);
            if (dtHeadUser.Rows.Count > 0)
            {
                DataRow drHead = dtHeadUser.Rows[0];
                myApprovalTable.Rows.Add(0, strKey, "Pengajuan Approval", 0, drHead["HEAD"], drHead["HEAD_DESCS"], drHead["USERGROUPDESC"], "", DBNull.Value, DBNull.Value, DBNull.Value, drHead["EMAIL"]);
            }
        }

        protected void gvCharge_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = branchDtTable;
        }
    }
}