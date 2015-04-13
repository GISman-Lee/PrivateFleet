using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;


public partial class SurveyReport : System.Web.UI.Page
{
    Cls_General objSurveyReport = new Cls_General();
    IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
    #region Variables and Declarations
    static ILog logger = LogManager.GetLogger(typeof(SurveyReport));
    DataTable dt = null;
    double avg = 0;
    int cnt = 0;
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)Master.FindControl("lblHeader")).Text = "Survey Report";

        System.Globalization.CultureInfo cultr = new System.Globalization.CultureInfo("en-au");
        CalenderFrom.Format = cultr.DateTimeFormat.ShortDatePattern;
        CalendarExtenderTodate.Format = cultr.DateTimeFormat.ShortDatePattern;
        string s = "WELCOME";
        s = s.ToUpperInvariant();

        if (!IsPostBack)
        {
            btnGenerateReport.Visible = false;

            reportype1_2.Visible = false;
            reportype2.Visible = false;
            reportype3.Visible = false;
            reportype4.Visible = false;
            reportype41.Visible = false;

            txtFromdate.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy"); txtSearchKey.Text = "";
            drpServeyDealer.SelectedValue = "";

            pnlSR.Visible = true;
            pnlServayCustomerlist.Visible = false;
            pnlSurvey.Visible = false;
            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvAll.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvIndi.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvIndiConsultant.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());

            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;

        }

    }

    #region Method

    /// <summary>
    /// Binding Contact person Dropdown
    /// </summary>
    private void BindContactPerson()
    {
        Cls_PrimaryContact objPrimaryContact = new Cls_PrimaryContact();
        try
        {
            DataTable dtPrimaryContact = objPrimaryContact.GetAllPrimaryContacts();

            DataView dv = dtPrimaryContact.DefaultView;
            dv.RowFilter = "primaryContactFor<>'VDT'";
            dtPrimaryContact = dv.ToTable();
            if (dtPrimaryContact != null && dtPrimaryContact.Rows.Count > 0)
            {
                drpServeyDealer.DataSource = dtPrimaryContact;
                drpServeyDealer.DataTextField = "Name";
                drpServeyDealer.DataValueField = "ID";
                drpServeyDealer.DataBind();

                drpServeyDealer.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                //No record Found..
            }
        }
        catch (Exception ex)
        {
            logger.Error("BindContactPerson Error - " + ex.Message);
        }
        finally
        {
            objSurveyReport = null;

        }
    }

    private void FillDealer()
    {
        Cls_General objSR = new Cls_General();
        try
        {
            dt = objSR.GetAllDealersForSR();
            ddlName.DataSource = dt;
            ddlName.DataTextField = "Dealer";
            ddlName.DataValueField = "ID";
            ddlName.DataBind();

            if (ddlName.Items.Count == 0)
            {
                ddlName.Items.Insert(0, new ListItem("- No Dealer Found -", "0"));
            }
            else
            {
                ddlName.Items.Insert(0, new ListItem("Select All", "0"));
            }



            ddlDealer.DataSource = dt;
            ddlDealer.DataTextField = "Dealer";
            ddlDealer.DataValueField = "ID";
            ddlDealer.DataBind();

            if (ddlDealer.Items.Count == 0)
            {
                ddlDealer.Items.Insert(0, new ListItem("- No Dealer Found -", "0"));
            }
            else
            {
                ddlDealer.Items.Insert(0, new ListItem("Select All", "0"));
            }





        }
        catch (Exception Ex)
        {
            logger.Error("GetAllActiveRoles Function :" + Ex.Message);
        }
        finally
        {
            objSR = null;
            dt = null;
        }

    }

    private void FillConsultant()
    {
        Cls_UserMaster objUserMaster = new Cls_UserMaster();
        try
        {

            dt = objUserMaster.GetAllConsultants();
            if (dt.Rows.Count > 1)
            {
                ddlName.DataSource = dt;
                ddlName.DataTextField = "Name";
                ddlName.DataValueField = "ID";
                ddlName.DataBind();



            }
            ddlName.Items.Insert(0, new ListItem("Select All", "0"));
        }
        catch (Exception ex)
        {
            logger.Error("Error in Fill Consultant" + ex.Message);
        }
        finally
        {
            objUserMaster = null;
            dt = null;
        }
    }

    private void FillRadioBut()
    {
        try
        {

            DataTable dt = objSurveyReport.FillRadioBut();
            if (dt.Rows.Count > 1 && dt != null)
            {
                radio_Consultant.DataSource = dt;
                radio_Consultant.DataTextField = "Text";
                radio_Consultant.DataValueField = "ID";
                radio_Consultant.DataBind();

                radio_AdminService.DataSource = dt;
                radio_AdminService.DataTextField = "Text";
                radio_AdminService.DataValueField = "ID";
                radio_AdminService.DataBind();

                radio_DealerService.DataSource = dt;
                radio_DealerService.DataTextField = "Text";
                radio_DealerService.DataValueField = "ID";
                radio_DealerService.DataBind();

                radio_Overall.DataSource = dt;
                radio_Overall.DataTextField = "Text";
                radio_Overall.DataValueField = "ID";
                radio_Overall.DataBind();

            }
        }
        catch (Exception ex)
        {
            logger.Error("Error in Fill Consultant" + ex.Message);
        }
    }

    private void FillConsultant_ForModal()
    {
        Cls_UserMaster objUserMaster = new Cls_UserMaster();
        try
        {

            dt = objUserMaster.GetAllConsultants();
            if (dt.Rows.Count > 1)
            {
                ddlConsultant.DataSource = dt;
                ddlConsultant.DataTextField = "Name";
                ddlConsultant.DataValueField = "ID";
                ddlConsultant.DataBind();



            }
            ddlConsultant.Items.Insert(0, new ListItem("Select All", "0"));
        }
        catch (Exception ex)
        {
            logger.Error("Error in Fill Consultant" + ex.Message);
        }
        finally
        {
            objUserMaster = null;
            dt = null;
        }
    }

    private void FillMake()
    {
        try
        {

            DataTable dt = objSurveyReport.GetAllMake();
            if (dt.Rows.Count > 1)
            {
                ddlMake.DataSource = dt;
                ddlMake.DataTextField = "Make";
                ddlMake.DataValueField = "ID";
                ddlMake.DataBind();
            }
            ddlMake.Items.Insert(0, new ListItem("--Select--", ""));
        }
        catch (Exception ex)
        {
            logger.Error("Error in Fill Make" + ex.Message);
        }
    }

    private void DefineSortDirection()
    {
        if (ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] != null)
        {
            if (ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString() == Cls_Constants.VIEWSTATE_ASC)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
            }
            else
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            }

        }
    }

    public void ExportToExcelsheet(DataTable dtTemp, string CName)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            if (dtTemp != null)
            {

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    context.Response.Write(Environment.NewLine);
                    context.Response.Write("Feedback From : " + CName);

                    //foreach (DataColumn column in dtTemp.Columns)
                    //{
                    //    if (column.ColumnName.ToLower().Equals("question") || column.ColumnName.ToLower().Equals("detailanswer"))
                    //        context.Response.Write(column.ColumnName + "\t");
                    //}
                    context.Response.Write(Environment.NewLine);

                    foreach (DataRow row in dtTemp.Rows)
                    {
                        switch (Convert.ToInt32(row["QuestionID"]))
                        {
                            case 1:
                                context.Response.Write("----------------------------------------------------");
                                context.Response.Write(Environment.NewLine);
                                context.Response.Write("About Private Fleet - ( Question 1 of 5)");
                                context.Response.Write(Environment.NewLine);
                                context.Response.Write("----------------------------------------------------");
                                break;
                            case 3:
                                context.Response.Write("----------------------------------------------------");
                                context.Response.Write(Environment.NewLine);
                                context.Response.Write("About Consultant - ( Question 2 of 5)");
                                context.Response.Write(Environment.NewLine);
                                context.Response.Write("----------------------------------------------------");
                                break;
                            case 7:
                                context.Response.Write("----------------------------------------------------");
                                context.Response.Write(Environment.NewLine);
                                context.Response.Write("Administration and Updates - (Question 3 of 5)");
                                context.Response.Write(Environment.NewLine);
                                context.Response.Write("----------------------------------------------------");
                                break;
                            case 12:
                                context.Response.Write("----------------------------------------------------");
                                context.Response.Write(Environment.NewLine);
                                context.Response.Write("Dealership - (Question 4 of 5)");
                                context.Response.Write(Environment.NewLine);
                                context.Response.Write("----------------------------------------------------");
                                break;
                            case 17:
                                context.Response.Write("----------------------------------------------------");
                                context.Response.Write(Environment.NewLine);
                                context.Response.Write("Overall - (Question 5 of 5)");
                                context.Response.Write(Environment.NewLine);
                                context.Response.Write("----------------------------------------------------");
                                break;

                        }
                        context.Response.Write(Environment.NewLine);
                        context.Response.Write(Environment.NewLine);
                        for (int i = 0; i < dtTemp.Columns.Count; i++)
                        {
                            if (dtTemp.Columns[i].ColumnName.ToLower().Equals("question"))
                            {
                                context.Response.Write("Question : " + row[i].ToString());
                                context.Response.Write(Environment.NewLine);
                            }
                            if (dtTemp.Columns[i].ColumnName.ToLower().Equals("detailanswer"))
                            {
                                context.Response.Write("Answer   : " + row[i].ToString());
                                context.Response.Write(Environment.NewLine);
                            }

                        }
                        context.Response.Write(Environment.NewLine);
                    }
                }
                Response.ContentType = "application/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=Report.csv");
                context.Response.End();
                context.Response.Clear();
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message + " ExportToExcelsheet. Error" + ex.StackTrace);
        }
    }

    public void ExportToExcelsheet30Days(DataTable dt)
    {
        DataTable UniqueClientID;
        DataTable dtTemp = new DataTable();
        HttpContext context = HttpContext.Current;
        try
        {
            UniqueClientID = dt.DefaultView.ToTable(true, "ClientID");
            dtTemp = new DataTable();
            for (int i = 0; i < UniqueClientID.Rows.Count; i++)
            {
                dtTemp = null;
                DataView dv = new DataView();
                dv = dt.DefaultView;
                dv.RowFilter = "ClientID=" + UniqueClientID.Rows[i]["ClientID"];
                dtTemp = dv.ToTable();

                if (dtTemp != null)
                {
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        context.Response.Write(Environment.NewLine);
                        context.Response.Write(Environment.NewLine);
                        context.Response.Write("==========================================================");
                        context.Response.Write(Environment.NewLine);
                        context.Response.Write("Feedback From : " + dtTemp.Rows[0]["ClientName"]);
                        context.Response.Write(Environment.NewLine);
                        context.Response.Write("Feedback Date : " + dtTemp.Rows[0]["DateComp"]);
                        context.Response.Write(Environment.NewLine);
                        context.Response.Write("==========================================================");

                        //foreach (DataColumn column in dtTemp.Columns)
                        //{
                        //    if (column.ColumnName.ToLower().Equals("question") || column.ColumnName.ToLower().Equals("detailanswer"))
                        //        context.Response.Write(column.ColumnName + "\t");
                        //}
                        context.Response.Write(Environment.NewLine);

                        foreach (DataRow row in dtTemp.Rows)
                        {
                            switch (Convert.ToInt32(row["QuestionID"]))
                            {
                                case 1:
                                    context.Response.Write("----------------------------------------------------");
                                    context.Response.Write(Environment.NewLine);
                                    context.Response.Write("About Private Fleet - ( Question 1 of 5)");
                                    context.Response.Write(Environment.NewLine);
                                    context.Response.Write("----------------------------------------------------");
                                    break;
                                case 3:
                                    context.Response.Write("----------------------------------------------------");
                                    context.Response.Write(Environment.NewLine);
                                    context.Response.Write("About Consultant - ( Question 2 of 5)");
                                    context.Response.Write(Environment.NewLine);
                                    context.Response.Write("----------------------------------------------------");
                                    break;
                                case 7:
                                    context.Response.Write("----------------------------------------------------");
                                    context.Response.Write(Environment.NewLine);
                                    context.Response.Write("Administration and Updates - (Question 3 of 5)");
                                    context.Response.Write(Environment.NewLine);
                                    context.Response.Write("----------------------------------------------------");
                                    break;
                                case 12:
                                    context.Response.Write("----------------------------------------------------");
                                    context.Response.Write(Environment.NewLine);
                                    context.Response.Write("Dealership - (Question 4 of 5)");
                                    context.Response.Write(Environment.NewLine);
                                    context.Response.Write("----------------------------------------------------");
                                    break;
                                case 17:
                                    context.Response.Write("----------------------------------------------------");
                                    context.Response.Write(Environment.NewLine);
                                    context.Response.Write("Overall - (Question 5 of 5)");
                                    context.Response.Write(Environment.NewLine);
                                    context.Response.Write("----------------------------------------------------");
                                    break;

                            }
                            context.Response.Write(Environment.NewLine);
                            context.Response.Write(Environment.NewLine);
                            for (int j = 0; j < dtTemp.Columns.Count; j++)
                            {
                                if (dtTemp.Columns[j].ColumnName.ToLower().Equals("question"))
                                {
                                    context.Response.Write("Question : " + row[j].ToString());
                                    context.Response.Write(Environment.NewLine);
                                }
                                if (dtTemp.Columns[j].ColumnName.ToLower().Equals("detailanswer"))
                                {
                                    context.Response.Write("Answer   : " + row[j].ToString());
                                    context.Response.Write(Environment.NewLine);
                                }

                            }
                            context.Response.Write(Environment.NewLine);
                        }
                    }
                }
            }
            Response.ContentType = "application/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=SearchSurveyReport.xls");
            context.Response.End();
            context.Response.Clear();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message + " ExportToExcelsheet. Error" + ex.StackTrace);
        }
    }

    public void ExportToExcelsheet30Days_new(DataTable dt)
    {
        DataTable UniqueClientID;
        DataTable UniqueQuestions;
        DataTable dtTemp = new DataTable();
        HttpContext context = HttpContext.Current;

        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        Excel.Range xlWorkSheet_Range;
        object misValue = System.Reflection.Missing.Value;
        try
        {
            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            UniqueClientID = dt.DefaultView.ToTable(true, "ClientID");
            UniqueQuestions = dt.DefaultView.ToTable(true, "Question");

            xlWorkSheet.Cells[1, 3] = "Question 1 of 5";
            xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[1, 3], xlWorkSheet.Cells[1, 4]);
            xlWorkSheet_Range.Merge(2);
            xlWorkSheet_Range.ColumnWidth = "20";
            xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Center;
            xlWorkSheet_Range.Font.Bold = true;

            xlWorkSheet.Cells[1, 5] = "Question 2 of 5";
            xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[1, 5], xlWorkSheet.Cells[1, 8]);
            xlWorkSheet_Range.Merge(4);
            xlWorkSheet_Range.ColumnWidth = "20";
            xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Center;
            xlWorkSheet_Range.Font.Bold = true;

            xlWorkSheet.Cells[1, 9] = "Question 3 of 5";
            xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[1, 9], xlWorkSheet.Cells[1, 13]);
            xlWorkSheet_Range.Merge(5);
            xlWorkSheet_Range.ColumnWidth = "20";
            xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Center;
            xlWorkSheet_Range.Font.Bold = true;

            xlWorkSheet.Cells[1, 14] = "Question 4 of 5";
            xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[1, 14], xlWorkSheet.Cells[1, 18]);
            xlWorkSheet_Range.Merge(5);
            xlWorkSheet_Range.ColumnWidth = "20";
            xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Center;
            xlWorkSheet_Range.Font.Bold = true;

            xlWorkSheet.Cells[1, 19] = "Question 5 of 5";
            xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[1, 19], xlWorkSheet.Cells[1, 22]);
            xlWorkSheet_Range.Merge(4);
            xlWorkSheet_Range.ColumnWidth = "20";
            xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Center;
            xlWorkSheet_Range.Font.Bold = true;


            xlWorkSheet.Cells[2, 1] = "Feedback From";
            xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 1]);
            xlWorkSheet_Range.ColumnWidth = "20";
            xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Center;
            xlWorkSheet_Range.VerticalAlignment = VerticalAlign.Middle;

            xlWorkSheet.Cells[2, 2] = "Date";
            xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 2], xlWorkSheet.Cells[2, 2]);
            xlWorkSheet_Range.ColumnWidth = "20";
            xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Center;
            xlWorkSheet_Range.VerticalAlignment = VerticalAlign.Middle;

            for (int j = 0; j < UniqueQuestions.Rows.Count; j++)
            {
                xlWorkSheet.Cells[2, j + 3] = UniqueQuestions.Rows[j]["Question"];
                xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[2, j + 3], xlWorkSheet.Cells[2, j + 3]);
                xlWorkSheet_Range.WrapText = true;
                xlWorkSheet_Range.VerticalAlignment = VerticalAlign.Top;
                xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Left;
            }

            xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 22]);
            xlWorkSheet_Range.Activate();
            xlWorkSheet_Range.Application.ActiveWindow.FreezePanes = true;

            dtTemp = new DataTable();
            for (int i = 0; i < UniqueClientID.Rows.Count; i++)
            {
                dtTemp = null;
                DataView dv = new DataView();
                dv = dt.DefaultView;
                dv.RowFilter = "ClientID=" + UniqueClientID.Rows[i]["ClientID"];
                dtTemp = dv.ToTable();

                if (dtTemp != null)
                {
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        xlWorkSheet.Cells[i + 4, 1] = dtTemp.Rows[0]["ClientName"];
                        xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[i + 4, 1], xlWorkSheet.Cells[i + 4, 1]);
                        xlWorkSheet_Range.WrapText = true;
                        xlWorkSheet_Range.VerticalAlignment = VerticalAlign.Top;
                        xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Left;

                        xlWorkSheet.Cells[i + 4, 2] = dtTemp.Rows[0]["DateComp"];
                        xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[i + 4, 2], xlWorkSheet.Cells[i + 4, 2]);
                        xlWorkSheet_Range.WrapText = true;
                        xlWorkSheet_Range.VerticalAlignment = VerticalAlign.Top;
                        xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Left;

                        for (int j = 0; j < dtTemp.Rows.Count; j++)
                        {
                            xlWorkSheet.Cells[i + 4, j + 3] = dtTemp.Rows[j]["DetailAnswer"];
                            xlWorkSheet_Range = xlWorkSheet.get_Range(xlWorkSheet.Cells[i + 4, j + 3], xlWorkSheet.Cells[i + 4, j + 3]);
                            xlWorkSheet_Range.WrapText = true;
                            xlWorkSheet_Range.VerticalAlignment = VerticalAlign.Top;
                            xlWorkSheet_Range.HorizontalAlignment = HorizontalAlign.Left;

                        }
                    }
                }
            }

            string path = Server.MapPath("~/Report/SearchSurveyReport.xls");
            System.IO.File.Delete(path);

            xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            System.IO.FileInfo file = new System.IO.FileInfo(path);
            if (file.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();

                Response.ContentType = "application/Excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=SearchSurveyReport.xls");
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.End();
                Response.Clear();
            }

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message + " ExportToExcelsheet_new Error" + ex.Message + " :: " + ex.StackTrace);
        }
    }

    private void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            System.Windows.Forms.MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
        }
        finally
        {
            GC.Collect();
        }
    }

    public XmlDocument ConvertDataTableToXML(DataTable dtAccessories)
    {

        XmlDocument _XMLDoc = new XmlDocument();

        DataSet ds = new DataSet("Accessoryds");
        DataTable dt = new DataTable("Accessorydt");

        dt = dtAccessories;
        ds.Tables.Add(dt);

        _XMLDoc.LoadXml(ds.GetXml());
        return _XMLDoc;
    }

    #endregion

    #region Events

    #region Button Events

    protected void lnk30Days_Click(object sender, EventArgs e)
    {
        objSurveyReport = new Cls_General();

        string custIds = "";
        try
        {

            DataTable dtTemp = new DataTable();
            dtTemp = (DataTable)ViewState["_SearchResultDT"];

            objSurveyReport.XmlDocument = ConvertDataTableToXML(dtTemp).InnerXml;
            objSurveyReport.Customerid = 0;
            dtTemp = null;
            dtTemp = objSurveyReport.GET_Servay_By_Customerid();

            // Convert to CSV 
            //ExportToExcelsheet30Days(dtTemp);
            ExportToExcelsheet30Days_new(dtTemp);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message + " lnk30Days_Click Error" + ex.Message + " :: " + ex.StackTrace);
        }
        finally
        {

        }
    }

    public void ImageButton1_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(drpSearchCriteria.SelectedValue) == "4")
        {
            pnlSurvey.Visible = false;
            pnlServayCustomerlist.Visible = true;
        }
        else
        {
            pnlSurvey.Visible = false;
            pnlreport1_2.Visible = true;
            pnlReport1_2_3.Visible = true;
            pnlSearch.Visible = true; ;
        }

    }

    public void ImageButton2_Click(object sender, EventArgs e)
    {
        pnlServayCustomerlist.Visible = false;
        pnlSR.Visible = true;
        pnlSearch.Visible = true;
    }

    /// <summary>
    /// 30 Jan 2013 : to hide rating drop down
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlName.SelectedValue == "0")
            {
                reportype42.Visible = true;
                ddlRatingFilter.SelectedValue = "0";
            }
            else
            {
                reportype42.Visible = false;
            }
        }
        catch (Exception ex)
        {
            logger.Error("ddlName_SelectedIndexChanged Error - " + ex.Message + " :: " + ex.StackTrace);
        }
    }

    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt = null;
            DataView dv = null;
            ViewState["_SearchResultDT"] = null;
            if (drpreportypePaging.SelectedValue != "All")
                grdSearchReport1_2.PageSize = Convert.ToInt32(drpreportypePaging.SelectedValue);
            else
                grdSearchReport1_2.PageSize = 20;

            switch (Convert.ToString(drpSearchCriteria.SelectedValue))
            {
                case "1":
                    grdSearchReport1_2.DataSource = null;
                    grdSearchReport1_2.DataBind();
                    ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "name";
                    ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
                    pnlreport1_2.Visible = true;
                    pnlReport1_2_3.Visible = true;
                    objSurveyReport.flag = 0;
                    objSurveyReport.fromdate = DateTime.Parse(txtFromdate.Text.Trim(), culture);
                    objSurveyReport.enddate = DateTime.Parse(txtToDate.Text.Trim(), culture);
                    objSurveyReport.Name = "";
                    dt = objSurveyReport.GetServey();
                    dv = new DataView(dt);
                    dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                    dt = dv.ToTable();
                    ViewState["_SearchResultDT"] = dt;
                    grdSearchReport1_2.DataSource = dt;
                    grdSearchReport1_2.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        trRowToDisp.Visible = true;
                        //lblRepoty123.Visible = true;
                        //drpreportypePaging.Visible = true;
                    }
                    else
                    {
                        trRowToDisp.Visible = false;
                        //lblRepoty123.Visible = false;
                        //drpreportypePaging.Visible = false;
                    }


                    break;
                case "2":
                    grdSearchReport1_2.DataSource = null;
                    grdSearchReport1_2.DataBind();
                    ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "name";
                    ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
                    pnlreport1_2.Visible = true;
                    pnlReport1_2_3.Visible = true;
                    objSurveyReport.flag = 2;
                    objSurveyReport.fromdate = DateTime.Parse(txtFromdate.Text.Trim(), culture);
                    objSurveyReport.enddate = DateTime.Parse(txtToDate.Text.Trim(), culture);
                    objSurveyReport.Name = Convert.ToString(drpServeyDealer.SelectedValue);
                    objSurveyReport.PrimaryContactID = Convert.ToInt32(drpServeyDealer.SelectedValue);
                    dt = objSurveyReport.GetServey_Report2();
                    dv = new DataView(dt);
                    dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                    dt = dv.ToTable();
                    ViewState["_SearchResultDT"] = dt;
                    grdSearchReport1_2.DataSource = dt;
                    grdSearchReport1_2.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        trRowToDisp.Visible = true;
                        //lblRepoty123.Visible = true;
                        //drpreportypePaging.Visible = true;
                    }
                    else
                    {
                        trRowToDisp.Visible = false;
                        //lblRepoty123.Visible = false;
                        //drpreportypePaging.Visible = false;
                    }
                    break;

                case "3":
                    grdSearchReport1_2.DataSource = null;
                    grdSearchReport1_2.DataBind();
                    ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "name";
                    ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
                    pnlreport1_2.Visible = true;
                    pnlReport1_2_3.Visible = true;
                    objSurveyReport.keyword = Convert.ToString(txtSearchKey.Text.Trim());
                    objSurveyReport.fromdate = DateTime.Parse(txtFromdate.Text.Trim(), culture);
                    objSurveyReport.enddate = DateTime.Parse(txtToDate.Text.Trim(), culture);
                    dt = objSurveyReport.GetServey_Report3_ByKeyword();
                    dv = new DataView(dt);
                    dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                    dt = dv.ToTable();
                    ViewState["_SearchResultDT"] = dt;
                    grdSearchReport1_2.DataSource = dt;
                    grdSearchReport1_2.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        trRowToDisp.Visible = true;
                        //lblRepoty123.Visible = true;
                        //drpreportypePaging.Visible = true;
                    }
                    else
                    {
                        trRowToDisp.Visible = false;
                        //lblRepoty123.Visible = false;
                        //drpreportypePaging.Visible = false;
                    }
                    break;

                case "4":

                    pnlSR.Visible = true;
                    GridView gvTemp = new GridView();

                    if (ddlRole.SelectedValue == "2")
                        objSurveyReport.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                    else if (ddlRole.SelectedValue == "3")
                        objSurveyReport.RoleID = Convert.ToInt32(ddlRole.SelectedValue);

                    if (ddlName.SelectedValue == "0")
                        objSurveyReport.ReportID = Convert.ToInt32(ddlName.SelectedValue);
                    else
                        objSurveyReport.ReportID = Convert.ToInt32(ddlName.SelectedValue);
                    objSurveyReport.fromdate = DateTime.Parse(txtFromdate.Text.Trim(), culture);
                    objSurveyReport.enddate = DateTime.Parse(txtToDate.Text.Trim(), culture);
                    objSurveyReport.Rating = Convert.ToInt32(ddlRatingFilter.SelectedValue);

                    ViewState["SurveyReport"] = null;
                    dt = objSurveyReport.getSurveyReport();
                    ViewState["SurveyReport"] = dt;

                    lblOverallR.Visible = false;
                    if (ddlName.SelectedValue != "0")
                    {
                        pnlIndi.Visible = true;
                        pnlAll.Visible = false;

                        if (ddlRole.SelectedValue == "2")
                        {
                            gvTemp = gvIndi;
                            gvIndiConsultant.Visible = false;
                            gvIndi.Visible = true;
                        }
                        else if (ddlRole.SelectedValue == "3")
                        {
                            gvTemp = gvIndiConsultant;
                            gvIndiConsultant.Visible = true;
                            gvIndi.Visible = false;
                        }
                        if (dt.Rows.Count > 0)
                            lblOverallR.Visible = true;
                    }
                    else
                    {
                        pnlIndi.Visible = false;
                        pnlAll.Visible = true;
                        gvTemp = gvAll;
                    }

                    if (dt.Rows.Count > 0)
                    {
                        lblRowsToDisplay.Visible = true;
                        ddl_NoRecords.Visible = true;
                        gvTemp.DataSource = dt;
                        gvTemp.DataBind();
                    }
                    else
                    {
                        lblRowsToDisplay.Visible = false;
                        ddl_NoRecords.Visible = false;
                        gvTemp.DataSource = null; ;
                        gvTemp.DataBind();
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.Error("Generate survey report error - " + ex.Message);
        }
        finally
        {
            objSurveyReport = null;
            //  gvTemp = null;
        }
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlRole.SelectedValue = "0";
            ddlName.Items.Clear();
            dt = null;

            gvAll.DataSource = dt;
            gvAll.DataBind();
            lblRowsToDisplay.Visible = false;
            ddl_NoRecords.Visible = false;
            pnlAll.Visible = true;
            pnlIndi.Visible = false;
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Grid View Events

    protected void gvAll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string rating = ((Label)e.Row.FindControl("lblRating")).Text;
                double per = Math.Round((Convert.ToDouble(rating) * 10), 2);
                string tot = Convert.ToString(((HiddenField)e.Row.FindControl("hdfTotal")).Value);

                AjaxControlToolkit.Rating rat = (AjaxControlToolkit.Rating)e.Row.FindControl("userRating");
                rat.CurrentRating = Convert.ToInt32(Math.Round(Convert.ToDouble(rating)));
                rat.ToolTip = per + " %";

                // ((Label)e.Row.FindControl("lblRating")).Text = Convert.ToString(per) + " %";
                ((Label)e.Row.FindControl("lblRating")).Text = Convert.ToString(Math.Round(Convert.ToDouble(rating), 1)) + "<sub>(" + tot + ")</sub>";
            }

        }
        catch (Exception ex)
        {
            logger.Error("Gv all row data bound error - " + ex.Message);
        }
    }

    protected void gvAll_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            //Swap sort direction
            this.DefineSortDirection();

            DataTable dt = (DataTable)ViewState["SurveyReport"];
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            gvAll.DataSource = dt;
            gvAll.DataBind();

            ViewState["SurveyReport"] = dt;
        }
        catch
        {
            throw;
        }

    }

    protected void gvAll_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAll.PageIndex = e.NewPageIndex;
            gvAll.DataSource = (DataTable)ViewState["SurveyReport"];
            gvAll.DataBind();
        }
        catch (Exception ex)
        {
            logger.Debug("Gv all page index changing error - " + ex.Message);
        }
    }

    protected void gvIndi_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string rating = ((Label)e.Row.FindControl("lblDRating")).Text;
                double per = Math.Round((Convert.ToDouble(rating) * 10), 2);

                AjaxControlToolkit.Rating rat = (AjaxControlToolkit.Rating)e.Row.FindControl("dealerRating");
                rat.CurrentRating = Convert.ToInt32(Math.Round(Convert.ToDouble(rating)));
                rat.ToolTip = per + " %";

                // ((Label)e.Row.FindControl("lblDRating")).Text = Convert.ToString(per) + " %";
                ((Label)e.Row.FindControl("lblDRating")).Text = Convert.ToString(Math.Round(Convert.ToDouble(rating), 1));
                avg += Convert.ToDouble(rating);
                cnt++;
                ((Label)e.Row.Parent.Parent.Parent.Parent.Parent.FindControl("lblOverallR")).Text = " Overall Average Rating of Dealer " + dt.Rows[0]["Name"] + " is : " + Convert.ToString(Math.Round(avg / cnt, 1)) + "<sub>(" + cnt + ")</sub>";
            }

        }
        catch (Exception ex)
        {
            logger.Error("Gv indi row data bound error - " + ex.Message);
        }
    }

    protected void gvIndi_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            //Swap sort direction
            this.DefineSortDirection();

            DataTable dt = (DataTable)ViewState["SurveyReport"];
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            gvIndi.DataSource = dt;
            gvIndi.DataBind();

            ViewState["SurveyReport"] = dt;
        }
        catch
        {
            throw;
        }
    }

    protected void gvIndi_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvIndi.PageIndex = e.NewPageIndex;
            gvIndi.DataSource = (DataTable)ViewState["SurveyReport"];
            gvIndi.DataBind();
        }
        catch (Exception ex)
        {
            logger.Debug("Gv indi page index changing error - " + ex.Message);
        }
    }

    protected void gvIndiConsultant_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string rating = ((Label)e.Row.FindControl("lblCRating")).Text;
                double per = Math.Round((Convert.ToDouble(rating) * 10), 2);

                AjaxControlToolkit.Rating rat = (AjaxControlToolkit.Rating)e.Row.FindControl("consultantRating");
                rat.CurrentRating = Convert.ToInt32(Math.Round(Convert.ToDouble(rating)));
                rat.ToolTip = per + " %";


                // ((Label)e.Row.FindControl("lblCRating")).Text = Convert.ToString(per) + " %";
                ((Label)e.Row.FindControl("lblCRating")).Text = Convert.ToString(Math.Round(Convert.ToDouble(rating), 2));
                avg += Convert.ToDouble(rating);
                cnt++;
                ((Label)e.Row.Parent.Parent.Parent.Parent.Parent.FindControl("lblOverallR")).Text = " Overall Average Rating of Dealer " + dt.Rows[0]["Name"] + " is : " + Convert.ToString(Math.Round(avg / cnt, 1)) + "<sub>(" + cnt + ")</sub>";
            }

        }
        catch (Exception ex)
        {
            logger.Error("Gv indi row data bound error - " + ex.Message);
        }
    }

    protected void gvIndiConsultant_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            //Swap sort direction
            this.DefineSortDirection();

            DataTable dt = (DataTable)ViewState["SurveyReport"];
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            gvIndiConsultant.DataSource = dt;
            gvIndiConsultant.DataBind();

            ViewState["SurveyReport"] = dt;
        }
        catch
        {
            throw;
        }
    }

    protected void gvIndiConsultant_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvIndiConsultant.PageIndex = e.NewPageIndex;
            gvIndiConsultant.DataSource = (DataTable)ViewState["SurveyReport"];
            gvIndiConsultant.DataBind();
        }
        catch (Exception ex)
        {
            logger.Debug("Gv indi page index changing error - " + ex.Message);
        }
    }


    public void gvAll_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strCommandName = "";
        strCommandName = Convert.ToString(e.CommandName);
        if (strCommandName == "Search")
        {
            objSurveyReport.userid = Convert.ToInt32(e.CommandArgument);
            objSurveyReport.fromdate = DateTime.Parse(txtFromdate.Text.Trim(), culture);
            objSurveyReport.enddate = DateTime.Parse(txtToDate.Text.Trim(), culture);
            DataTable dt = null;
            dt = objSurveyReport.GetListof_ServeryCustomer();
            grdServayCustomerList.PageSize = Convert.ToInt32(ddlCustomer.SelectedValue);
            ViewState["_ServayCustomerList"] = dt;
            grdServayCustomerList.DataSource = dt;
            grdServayCustomerList.DataBind();
            if (dt.Rows.Count > 0)
            {
                lblCustoerRowShow.Visible = true;
                ddlCustomer.Visible = true;
            }
            else
            {
                lblCustoerRowShow.Visible = false;
                ddlCustomer.Visible = false;
            }


            pnlSR.Visible = false;
            pnlSearch.Visible = false;
            pnlServayCustomerlist.Visible = true;
            pnlSurvey.Visible = false;
        }
    }

    public void grdServayCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string StrCommandName = "";
            StrCommandName = Convert.ToString(e.CommandName);
            Int32 StrCommandArgutment = 0;
            StrCommandArgutment = Convert.ToInt32(e.CommandArgument);
            if (StrCommandName == "ServayDetail")
            {
                objSurveyReport.Customerid = StrCommandArgutment;
                DataTable dt = null;
                dt = objSurveyReport.GET_Servay_By_Customerid();


                show_servey(dt);


            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message.ToString());
        }

    }

    public void grdServayCustomerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdServayCustomerList.PageIndex = e.NewPageIndex;
        grdServayCustomerList.DataSource = (DataTable)ViewState["_ServayCustomerList"];
        grdServayCustomerList.DataBind();
    }

    public void grdServayCustomerList_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            string strSortExpression = e.SortExpression;

            //Swap sort direction
            this.DefineSortDirection();

            DataTable dt = (DataTable)ViewState["_ServayCustomerList"];
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", strSortExpression, ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            grdServayCustomerList.DataSource = dt;
            grdServayCustomerList.DataBind();

            ViewState["_ServayCustomerList"] = dt;
        }
        catch (Exception ex)
        {

            logger.Error(Convert.ToString(ex.Message));
            throw;
        }

    }


    public void grdSearchReport1_2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string strCommandname = Convert.ToString(e.CommandName);
            int id = Convert.ToInt32(e.CommandArgument);

            if (strCommandname == "ServayDetail")
            {
                objSurveyReport.Customerid = id;
                DataTable dt = null;
                dt = objSurveyReport.GET_Servay_By_Customerid();

                pnlSearch.Visible = false;
                pnlreport1_2.Visible = false;
                pnlReport1_2_3.Visible = false;
                show_servey(dt);


            }
            else if (strCommandname == "DownloadCSV")
            {
                objSurveyReport.Customerid = id;
                DataTable dt = null;
                dt = objSurveyReport.GET_Servay_By_Customerid();

                Label lblCustomername = (Label)((LinkButton)e.CommandSource).Parent.FindControl("lblCustomername");
                // Convert to CSV
                ExportToExcelsheet(dt, lblCustomername.Text);


            }

        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void grdSearchReport1_2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdSearchReport1_2.PageIndex = e.NewPageIndex;
            grdSearchReport1_2.DataSource = (DataTable)ViewState["_SearchResultDT"];
            grdSearchReport1_2.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void grdSearchReport1_2_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            string strSortExpression = e.SortExpression;

            //Swap sort direction
            this.DefineSortDirection();

            DataTable dt = (DataTable)ViewState["_SearchResultDT"];
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", strSortExpression, ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            grdSearchReport1_2.DataSource = dt;
            grdSearchReport1_2.DataBind();

            ViewState["_SearchResultDT"] = dt;
        }
        catch (Exception ex)
        {

            logger.Error(Convert.ToString(ex.Message));
            throw;
        }

    }

    public void grdSearchReport1_2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblConsultantName");
                if (lbl.Text == "")
                {
                    lbl.Text = "---";
                }
                lbl = (Label)e.Row.FindControl("lbldealername");
                if (lbl.Text == "")
                {
                    lbl.Text = "---";
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    #endregion

    #region Drop down

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView temp = new GridView();
        try
        {
            if (pnlAll.Visible)
                temp = gvAll;
            else if (pnlIndi.Visible)
            {
                if (gvIndi.Visible)
                    temp = gvIndi;
                else
                    temp = gvIndiConsultant;
            }

            temp.PageIndex = 0;
            if (ddl_NoRecords.SelectedValue.ToString() == "All")
            {

                temp.DataSource = (DataTable)ViewState["SurveyReport"];
                temp.PageSize = temp.PageCount * temp.Rows.Count;
                temp.DataBind();
            }
            else
            {
                temp.DataSource = (DataTable)ViewState["SurveyReport"];
                temp.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                temp.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            temp = null;
        }
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView temp = new GridView();
        try
        {
            temp = grdServayCustomerList;
            temp.PageIndex = 0;
            if (ddl_NoRecords.SelectedValue.ToString() == "All")
            {

                temp.DataSource = (DataTable)ViewState["_ServayCustomerList"];
                temp.PageSize = temp.PageCount * temp.Rows.Count;
                temp.DataBind();
            }
            else
            {
                temp.DataSource = (DataTable)ViewState["_ServayCustomerList"];
                temp.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                temp.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
        finally
        {
            temp = null;
        }
    }

    public void drpreportypePaging_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView temp = new GridView();
        try
        {

            temp = grdSearchReport1_2;
            temp.PageIndex = 0;
            if (drpreportypePaging.SelectedValue.ToString() == "All")
            {

                temp.DataSource = (DataTable)ViewState["_SearchResultDT"];
                temp.PageSize = temp.PageCount * temp.Rows.Count;
                temp.DataBind();
            }
            else
            {
                temp.DataSource = (DataTable)ViewState["_SearchResultDT"];
                temp.PageSize = Convert.ToInt32(drpreportypePaging.SelectedValue.ToString());
                temp.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
        finally
        {
            temp = null;
        }

    }


    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRole.SelectedValue == "2")
                FillDealer();
            else if (ddlRole.SelectedValue == "3")
                FillConsultant();
            else if (ddlRole.SelectedValue == "0")
            {
                ddlName.Items.Clear();
            }
            reportype42.Visible = true;
            ddlRatingFilter.SelectedValue = "0";
        }
        catch
        {
            throw;
        }
    }

    public void drpSearchCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            reportype1_2.Visible = false;
            reportype2.Visible = false;
            reportype3.Visible = false;
            reportype4.Visible = false;
            reportype41.Visible = false;
            reportype42.Visible = false;

            trRowToDisp.Visible = false;
            //lblRepoty123.Visible = false;
            //drpreportypePaging.Visible = false;

            pnlServayCustomerlist.Visible = false;
            pnlSR.Visible = false;
            pnlreport1_2.Visible = false;
            pnlReport1_2_3.Visible = false;

            txtFromdate.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtSearchKey.Text = "";

            ddlRole.SelectedValue = "0";
            ddlName.Items.Clear();
            btnGenerateReport.Visible = true;
            switch (Convert.ToString(drpSearchCriteria.SelectedValue))
            {
                case "":
                    btnGenerateReport.Visible = false;
                    break;
                case "1":
                    reportype1_2.Visible = true;
                    break;
                case "2":
                    BindContactPerson();
                    drpServeyDealer.SelectedValue = "0";
                    reportype1_2.Visible = true;
                    reportype2.Visible = true;
                    break;
                case "3":
                    reportype1_2.Visible = true;
                    reportype3.Visible = true;
                    break;
                case "4":
                    reportype1_2.Visible = true;
                    reportype4.Visible = true;
                    reportype41.Visible = true;
                    reportype42.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.Error("drpSearchCriteria_SelectedIndexChanged" + Convert.ToString(ex.Message));
        }

    }

    #endregion

    #endregion

    public void show_servey(DataTable dt)
    {
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {

                FillRadioBut();
                FillMake();
                FillDealer();
                lbl_APFQ1_Answer.Text = Convert.ToString(dt.Rows[0]["Answer"]);
                if (lbl_APFQ1_Answer.Text.Trim() == "")
                {
                    lbl_APFQ1_Answer.Text = "----";
                }

                lbl_APFQ2_Answer.Text = Convert.ToString(dt.Rows[1]["Answer"]);
                if (lbl_APFQ2_Answer.Text.Trim() == "")
                {
                    lbl_APFQ2_Answer.Text = "----";
                }

                FillConsultant_ForModal();
                ddlConsultant.SelectedValue = Convert.ToString(dt.Rows[2]["Answer"]);
                lbl_ConsultantQ1_Answer.Text = Convert.ToString(ddlConsultant.SelectedItem.Text);

                if (lbl_ConsultantQ1_Answer.Text.Trim() == "")
                {
                    lbl_ConsultantQ1_Answer.Text = "----";
                }

                if (Convert.ToString(dt.Rows[3]["Answer"]) != "")
                {
                    radio_Consultant.SelectedValue = Convert.ToString(dt.Rows[3]["Answer"]);
                    lbl_ConsultantQ2_Answer.Text = Convert.ToString(radio_Consultant.SelectedItem.Text);
                }
                else
                    lbl_ConsultantQ2_Answer.Text = "----";

                if (lbl_ConsultantQ2_Answer.Text.Trim() == "")
                {
                    lbl_ConsultantQ2_Answer.Text = "----";
                }

                lbl_ConsultantQ3_Answer.Text = Convert.ToString(dt.Rows[4]["Answer"]);
                if (lbl_ConsultantQ3_Answer.Text.Trim() == "")
                {
                    lbl_ConsultantQ3_Answer.Text = "----";
                }

                lbl_ConsultantQ4_Answer.Text = Convert.ToString(dt.Rows[5]["Answer"]);

                if (lbl_ConsultantQ4_Answer.Text.Trim() == "")
                {
                    lbl_ConsultantQ4_Answer.Text = "----";
                }

                if (Convert.ToString(dt.Rows[6]["Answer"]) != "Other")
                {
                    if (ddlAdminContact.Items.Contains(new ListItem(Convert.ToString(dt.Rows[6]["Answer"]))) == true)
                    {
                        ddlAdminContact.SelectedValue = Convert.ToString(dt.Rows[6]["Answer"]);
                        lbl_AdminQ1_Answer.Text = Convert.ToString(ddlAdminContact.SelectedItem.Text);
                    }
                    else
                    {
                        lbl_AdminQ1_Answer.Text = Convert.ToString(dt.Rows[6]["Answer"]);
                    }

                }
                else
                {
                    lbl_AdminQ1_Answer.Text = Convert.ToString(dt.Rows[6]["Answer"]);

                }

                if (lbl_AdminQ1_Answer.Text.Trim() == "")
                {
                    lbl_AdminQ1_Answer.Text = "----";
                }

                if (Convert.ToString(dt.Rows[7]["Answer"]) != "")
                {
                    radio_AdminService.SelectedValue = Convert.ToString(dt.Rows[7]["Answer"]);
                    lbl_AdminQ2_Answer.Text = Convert.ToString(radio_AdminService.SelectedItem.Text);
                }
                else
                    lbl_AdminQ2_Answer.Text = "----";

                if (lbl_AdminQ2_Answer.Text.Trim() == "")
                {
                    lbl_AdminQ2_Answer.Text = "----";
                }

                if (Convert.ToString(dt.Rows[8]["Answer"]) != "")
                {
                    radio_AdminEstumateDelivery.SelectedValue = Convert.ToString(dt.Rows[8]["Answer"]);
                    lbl_AdminQ3_Answer.Text = Convert.ToString(radio_AdminEstumateDelivery.SelectedItem.Text);
                }
                else
                    lbl_AdminQ3_Answer.Text = "";

                if (lbl_AdminQ3_Answer.Text.Trim() == "")
                {
                    lbl_AdminQ3_Answer.Text = "----";
                }

                lbl_AdminQ4_Answer.Text = Convert.ToString(dt.Rows[9]["Answer"]);
                if (lbl_AdminQ4_Answer.Text.Trim() == "")
                {
                    lbl_AdminQ4_Answer.Text = "----";
                }

                lbl_AdminQ5_Answer.Text = Convert.ToString(dt.Rows[10]["Answer"]);
                if (lbl_AdminQ5_Answer.Text.Trim() == "")
                {
                    lbl_AdminQ5_Answer.Text = "----";
                }

                ddlMake.SelectedValue = Convert.ToString(dt.Rows[11]["Answer"]);
                lbl_DealerQ1_Answer.Text = Convert.ToString(ddlMake.SelectedItem.Text);

                if (lbl_DealerQ1_Answer.Text.Trim() == "")
                {
                    lbl_DealerQ1_Answer.Text = "----";
                }

                if (!Convert.ToString(dt.Rows[12]["Answer"]).Equals(String.Empty) && Convert.ToInt32(dt.Rows[12]["Answer"]) > 0)
                {
                    ddlDealer.SelectedValue = Convert.ToString(dt.Rows[12]["Answer"]);
                    lbl_DealerQ2_Answer.Text = Convert.ToString(ddlDealer.SelectedItem.Text);
                }
                else
                    lbl_DealerQ2_Answer.Text = String.Empty;

                if (lbl_DealerQ2_Answer.Text.Trim() == "")
                {
                    lbl_DealerQ2_Answer.Text = "----";
                }

                if (Convert.ToString(dt.Rows[13]["Answer"]) != "")
                {
                    radio_DealerService.SelectedValue = Convert.ToString(dt.Rows[13]["Answer"]);
                    lbl_DealerQ3_Answer.Text = Convert.ToString(radio_DealerService.SelectedItem.Text);
                }
                else
                    lbl_DealerQ3_Answer.Text = "";

                if (lbl_DealerQ3_Answer.Text.Trim() == "")
                {
                    lbl_DealerQ3_Answer.Text = "----";
                }

                if (Convert.ToString(dt.Rows[14]["Answer"]).Trim() != "")
                {
                    radio_DealerSatisfiction.SelectedValue = Convert.ToString(dt.Rows[14]["Answer"]);
                    lbl_DealerQ4_Answer.Text = Convert.ToString(radio_DealerSatisfiction.SelectedItem.Text);
                }
                else
                {
                    lbl_DealerQ4_Answer.Text = "----";
                }

                lbl_DealerQ5_Answer.Text = Convert.ToString(dt.Rows[15]["Answer"]);
                if (lbl_DealerQ5_Answer.Text.Trim() == "")
                {
                    lbl_DealerQ5_Answer.Text = "----";
                }

                if (Convert.ToString(dt.Rows[16]["Answer"]) != "")
                {
                    radio_Overall.SelectedValue = Convert.ToString(dt.Rows[16]["Answer"]);
                    lbl_OverallQ1_Answer.Text = Convert.ToString(radio_Overall.SelectedItem.Text);
                }
                else
                {
                    lbl_OverallQ1_Answer.Text = "----";
                }

                if (lbl_OverallQ1_Answer.Text.Trim() == "")
                {
                    lbl_OverallQ1_Answer.Text = "----";
                }

                if (Convert.ToString(dt.Rows[17]["Answer"]) != "")
                {
                    radio_OverallRecommended.SelectedValue = Convert.ToString(dt.Rows[17]["Answer"]);
                    lbl_OverallQ2_Answer.Text = Convert.ToString(radio_OverallRecommended.SelectedItem.Text);
                }
                else
                {
                    lbl_OverallQ2_Answer.Text = "----";
                }

                if (lbl_OverallQ2_Answer.Text.Trim() == "")
                {
                    lbl_OverallQ2_Answer.Text = "----";
                }

                lbl_OverallQ3_Answer.Text = Convert.ToString(dt.Rows[18]["Answer"]);
                if (lbl_OverallQ3_Answer.Text.Trim() == "")
                {
                    lbl_OverallQ3_Answer.Text = "----";
                }
                lbl_OverallQ4_Answer.Text = Convert.ToString(dt.Rows[19]["Answer"]);
                if (lbl_OverallQ4_Answer.Text.Trim() == "")
                {
                    lbl_OverallQ4_Answer.Text = "----";
                }

                pnlSurvey.Visible = true;
                pnlServayCustomerlist.Visible = false;
            }
        }
    }

}
