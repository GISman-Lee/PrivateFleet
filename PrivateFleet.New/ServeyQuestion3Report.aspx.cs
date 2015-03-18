using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;
using System.Text;

public partial class ServeyQuestion3Report : System.Web.UI.Page
{
    #region Private Variables
    static ILog logger = LogManager.GetLogger(typeof(ServeyQuestion3Report));
    Cls_General objSurveyReport = null;
    DataTable dtPrimaryContact = new DataTable();
    DataSet dsServeyRpt = null;
    IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
    #endregion

    #region Events

    /// <summary>
    /// Page Load Event : Conrtact Person Dropdown is bibded here.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ((Label)Master.FindControl("lblHeader")).Text = "Survey Question:3 Report";

            System.Globalization.CultureInfo cultr = new System.Globalization.CultureInfo("en-au");
            CalenderFrom.Format = cultr.DateTimeFormat.ShortDatePattern;
            CalendarExtenderTodate.Format = cultr.DateTimeFormat.ShortDatePattern;

            if (!IsPostBack)
            {
                txtFromdate.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                BindContactPerson();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Error - " + ex.Message);
        }
    }

    /// <summary>
    /// All Servey reports are generated here.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        objSurveyReport = new Cls_General();
        dsServeyRpt = new DataSet();
        Int32 _contactPersonId = 0;
        pnlReport.Visible = true;
        DateTime _fromdate, _enddate;

        try
        {
            if (Convert.ToInt32(ddlContactPerson.SelectedValue) > 0)
            {
                _contactPersonId = Convert.ToInt32(ddlContactPerson.SelectedValue);

                _fromdate = DateTime.Parse(txtFromdate.Text.Trim(), culture);
                _enddate = DateTime.Parse(txtToDate.Text.Trim(), culture);
                dsServeyRpt = objSurveyReport.GetServeyReportForQuestn3(_contactPersonId, _fromdate, _enddate);
            }
            else
            {
                _contactPersonId = 0;
                _fromdate = DateTime.Parse(txtFromdate.Text.Trim(), culture);
                _enddate = DateTime.Parse(txtToDate.Text.Trim(), culture);
                dsServeyRpt = objSurveyReport.GetServeyReportForQuestn3(_contactPersonId, _fromdate, _enddate);
            }

            ViewState["dsServeyDtls"] = dsServeyRpt;
            if (dsServeyRpt != null && dsServeyRpt.Tables.Count > 0)
            {
                lblHdrQ7.Visible = true;
                lblHdrQ8.Visible = true;
                lblHdrQ9.Visible = true;
                lblQ18All.Visible = true;
                BindGrdQ7(dsServeyRpt, _contactPersonId);
                BindGrdQ8(dsServeyRpt, _contactPersonId);
                BindGrdQ9(dsServeyRpt, _contactPersonId);
                BindGrdQ18(dsServeyRpt, _contactPersonId);
            }
            else
            {
                //No record Found..
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnGenerateReport_Click Error - " + ex.Message);
        }
    }

    /// <summary>
    /// RowDataBound Event of Grid :1 :For Question 7.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdQ7_1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                if (!string.IsNullOrEmpty(lblTotal_Q7_1.Text))
                {
                    Label lblPercentg = (Label)e.Row.FindControl("lblPercentage");
                    Label lblCount = (Label)e.Row.FindControl("lblCount");

                    Double _cnt = Convert.ToDouble(lblCount.Text.Trim());

                    Double _total = Convert.ToDouble(lblTotal_Q7_1.Text.Trim());
                    Double _per = 0;
                    Panel pnlInner = (Panel)e.Row.FindControl("pnlInner");
                    Panel pnlPer = (Panel)e.Row.FindControl("pnlPer");
                    Int32 intPerCentage = 0;

                    if (_total > 0)
                    {
                        _per = (_cnt / _total);
                        intPerCentage = Convert.ToInt32(_per * 100);
                        lblPercentg.Text = Convert.ToString(_per.ToString("P"));
                        pnlInner.Style.Add("width", intPerCentage.ToString() + "%");
                    }
                    else
                    {
                        pnlInner.Style.Add("width", intPerCentage.ToString() + "%");
                    }
                }
                else
                {
                    logger.Debug("grdQ7_1_RowDataBound_DeBug: lblCount.Text is Empty.");
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("grdQ7_1_RowDataBound Error - " + ex.Message);
        }
    }

    /// <summary>
    /// RowDataBound event of Grid 1: Question 8:displaying details of particular Contact person.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdQ8_1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                Label lblTotalQ8_1 = (Label)e.Row.FindControl("lblTotalQ8_1");

                if (!string.IsNullOrEmpty(lblTotalQ8_1.Text.Trim()))
                {
                    Double _total;
                    Double _cnt = 0, _per = 0;
                    _total = Convert.ToDouble(lblTotalQ8_1.Text.Trim());

                    Label lblPercentg = (Label)e.Row.FindControl("lblPercentage");
                    Label lblCount = (Label)e.Row.FindControl("lblCount");
                    Panel pnlInner = (Panel)e.Row.FindControl("pnlInner");

                    if (_total > 0)
                    {
                        _cnt = Convert.ToDouble(lblCount.Text.Trim());
                        _total = Convert.ToDouble(lblTotalQ8_1.Text.Trim());
                        _per = 0;

                        _per = (_cnt / _total);
                        lblPercentg.Text = Convert.ToString(_per.ToString("P"));

                        Int32 intPerCentage = Convert.ToInt32(_per * 100);
                        pnlInner.Style.Add("width", intPerCentage.ToString() + "%");
                    }
                    else if (_total == 0)
                    {
                        lblPercentg.Text = Convert.ToString(_per.ToString("P"));
                        pnlInner.Style.Add("width", 0 + "%");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("grdQ8_1_RowDataBound Error - " + ex.Message);
        }
        finally
        {

        }
    }

    /// <summary>
    /// RowDataBound event of Grid 2: Question 8:displaying details of All Contact person.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdQ8_all_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                Label lblTotalQ8ALL = (Label)e.Row.FindControl("lblTotalQ8ALL");

                if (!string.IsNullOrEmpty(lblTotalQ8ALL.Text.Trim()))
                {
                    Double _total;
                    Double _cnt = 0, _per = 0;
                    _total = Convert.ToDouble(lblTotalQ8ALL.Text.Trim());

                    Label lblExellentPer = (Label)e.Row.FindControl("lblExellentPer");
                    Label lblExcellent = (Label)e.Row.FindControl("lblExcellent");

                    Label lblVGoodPer = (Label)e.Row.FindControl("lblVGoodPer");
                    Label lblVeryGood = (Label)e.Row.FindControl("lblVeryGood");

                    Label lblAvgPer = (Label)e.Row.FindControl("lblAvgPer");
                    Label lblAverage = (Label)e.Row.FindControl("lblAverage");

                    Label lblPoorPer = (Label)e.Row.FindControl("lblPoorPer");
                    Label lblPoor = (Label)e.Row.FindControl("lblPoor");

                    Label lblVPoorPer = (Label)e.Row.FindControl("lblVPoorPer");
                    Label lblVeryPoor = (Label)e.Row.FindControl("lblVeryPoor");
                    Panel pnlInner_E = (Panel)e.Row.FindControl("pnlInner_E");

                    Panel pnlInner_VG = (Panel)e.Row.FindControl("pnlInner_VG");
                    Panel pnlInner_AVG = (Panel)e.Row.FindControl("pnlInner_AVG");

                    Panel pnlInner_P = (Panel)e.Row.FindControl("pnlInner_P");
                    Panel pnlInner_VP = (Panel)e.Row.FindControl("pnlInner_VP");

                    if (_total > 0)
                    {
                        _cnt = Convert.ToDouble(lblExcellent.Text.Trim());
                        _per = (_cnt / _total);
                        lblExellentPer.Text = Convert.ToString(_per.ToString("P"));

                        Int32 intPerCentage = Convert.ToInt32(_per * 100);
                        pnlInner_E.Style.Add("width", intPerCentage.ToString() + "%");

                        _cnt = 0; _per = 0;
                        _cnt = Convert.ToDouble(lblVeryGood.Text.Trim());
                        _per = (_cnt / _total);
                        lblVGoodPer.Text = Convert.ToString(_per.ToString("P"));
                        Int32 intPerCentage1 = Convert.ToInt32(_per * 100);
                        pnlInner_VG.Style.Add("width", intPerCentage1.ToString() + "%");

                        _cnt = 0; _per = 0;
                        _cnt = Convert.ToDouble(lblAverage.Text.Trim());
                        _per = (_cnt / _total);
                        lblAvgPer.Text = Convert.ToString(_per.ToString("P"));

                        Int32 intPerCentage2 = Convert.ToInt32(_per * 100);
                        pnlInner_AVG.Style.Add("width", intPerCentage2.ToString() + "%");

                        _cnt = 0; _per = 0;
                        _cnt = Convert.ToDouble(lblPoor.Text.Trim());
                        _per = (_cnt / _total);
                        lblPoorPer.Text = Convert.ToString(_per.ToString("P"));

                        Int32 intPerCentage3 = Convert.ToInt32(_per * 100);
                        pnlInner_P.Style.Add("width", intPerCentage3.ToString() + "%");

                        _cnt = 0; _per = 0;
                        _cnt = Convert.ToDouble(lblVeryPoor.Text.Trim());
                        _per = (_cnt / _total);
                        lblVPoorPer.Text = Convert.ToString(_per.ToString("P"));

                        Int32 intPerCentage4 = Convert.ToInt32(_per * 100);
                        pnlInner_VP.Style.Add("width", intPerCentage4.ToString() + "%");
                    }
                    else if (_total == 0)
                    {
                        lblExellentPer.Text = Convert.ToString(_per.ToString("P"));
                        pnlInner_E.Style.Add("width", 0 + "%");

                        lblVGoodPer.Text = Convert.ToString(_per.ToString("P"));
                        pnlInner_VG.Style.Add("width", 0 + "%");

                        lblAvgPer.Text = Convert.ToString(_per.ToString("P"));
                        pnlInner_AVG.Style.Add("width", 0 + "%");

                        lblPoorPer.Text = Convert.ToString(_per.ToString("P"));
                        pnlInner_P.Style.Add("width", 0 + "%");

                        lblVPoorPer.Text = Convert.ToString(_per.ToString("P"));
                        pnlInner_VP.Style.Add("width", 0 + "%");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("grdQ8_1_RowDataBound Error - " + ex.Message);
        }
        finally
        {

        }
    }

    /// <summary>
    /// RowDataBound event of Grid 1: Question 9:displaying details of All Contact person.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdQ9_1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                Label lblTotalQ9_1 = (Label)e.Row.FindControl("lblTotalQ9_1");

                if (!string.IsNullOrEmpty(lblTotalQ9_1.Text.Trim()))
                {
                    Double _total;
                    Double _cnt = 0, _per = 0;
                    _total = Convert.ToDouble(lblTotalQ9_1.Text.Trim());

                    Label lblPercentg = (Label)e.Row.FindControl("lblPercentage");
                    Label lblCount = (Label)e.Row.FindControl("lblCount");
                    Panel pnlInner = (Panel)e.Row.FindControl("pnlInner");

                    if (_total > 0)
                    {
                        _cnt = Convert.ToDouble(lblCount.Text.Trim());
                        _per = (_cnt / _total);
                        lblPercentg.Text = Convert.ToString(_per.ToString("P"));

                        Int32 intPerCentage = Convert.ToInt32(_per * 100);
                        pnlInner.Style.Add("width", intPerCentage.ToString() + "%");
                    }
                    else if (_total == 0)
                    {
                        lblPercentg.Text = Convert.ToString(_per.ToString("P"));
                        pnlInner.Style.Add("width", 0 + "%");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("grdQ9_1_RowDataBound Error - " + ex.Message);
        }
    }

    /// <summary>
    /// RowDataBound event of Grid 2: Question 9:displaying details of All Contact person.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdQ9All_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                Label lblTotalQ9All = (Label)e.Row.FindControl("lblTotalQ9All");
                Double _cnt = 0, _total, _per = 0;

                if (!string.IsNullOrEmpty(lblTotalQ9All.Text.Trim()))
                {
                    _total = Convert.ToDouble(lblTotalQ9All.Text.Trim());

                    Label lblYesPer = (Label)e.Row.FindControl("lblYesPer");
                    Label lblYesCount = (Label)e.Row.FindControl("lblYesCount");

                    Label lblNoPer = (Label)e.Row.FindControl("lblNoPer");
                    Label lblNoCount = (Label)e.Row.FindControl("lblNoCount");


                    Panel pnlInner_Yes = (Panel)e.Row.FindControl("pnlInner_Yes");
                    Panel pnlInner_No = (Panel)e.Row.FindControl("pnlInner_No");

                    if (_total > 0)
                    {
                        _cnt = Convert.ToDouble(lblYesCount.Text.Trim());

                        _per = (_cnt / _total);
                        lblYesPer.Text = Convert.ToString(_per.ToString("P"));
                        Int32 intPerCentageYes = Convert.ToInt32(_per * 100);
                        pnlInner_Yes.Style.Add("width", intPerCentageYes.ToString() + "%");


                        _cnt = 0; _per = 0;
                        _cnt = Convert.ToDouble(lblNoCount.Text.Trim());
                        _per = (_cnt / _total);
                        lblNoPer.Text = Convert.ToString(_per.ToString("P"));

                        Int32 intPerCentageNo = Convert.ToInt32(_per * 100);
                        pnlInner_No.Style.Add("width", intPerCentageNo.ToString() + "%");
                    }
                    else if (_total == 0)
                    {
                        lblYesPer.Text = Convert.ToString(_per.ToString("P"));
                        lblNoPer.Text = Convert.ToString(_per.ToString("P"));

                        pnlInner_Yes.Style.Add("width", 0 + "%");
                        pnlInner_No.Style.Add("width", 0 + "%");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("grdQ8_1_RowDataBound Error - " + ex.Message);
        }
        finally
        {

        }
    }

    /// <summary>
    /// Paging for grid of Question 7.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdQ7_1_OnPaging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (ViewState["dsServeyDtls"] != null)
            {
                DataSet _dsServeyRpt = (DataSet)ViewState["dsServeyDtls"];
                grdQ7_1.PageIndex = e.NewPageIndex;
                if (ddlContactPerson.SelectedIndex > 0)
                {
                    BindGrdQ7(_dsServeyRpt, Convert.ToInt32(ddlContactPerson.SelectedValue));
                }
                else
                {
                    BindGrdQ7(_dsServeyRpt, 0);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("grdQ7_1_OnPaging Error - " + ex.Message);
        }
    }

    #endregion

    #region Methods
    /// <summary>
    /// Binding Contact person Dropdown
    /// </summary>
    private void BindContactPerson()
    {
        Cls_PrimaryContact objPrimaryContact = new Cls_PrimaryContact();
        try
        {
            dtPrimaryContact = objPrimaryContact.GetAllPrimaryContacts();
            DataView dv = dtPrimaryContact.DefaultView;
            dv.RowFilter = "primaryContactFor<>'VDT'";
            dtPrimaryContact = dv.ToTable();
            if (dtPrimaryContact != null && dtPrimaryContact.Rows.Count > 0)
            {
                ListItem lstItm = new ListItem("----------All----------", "-1");

                ddlContactPerson.DataSource = dtPrimaryContact;
                ddlContactPerson.DataBind();
                ddlContactPerson.Items.Insert(0, lstItm);
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
            dtPrimaryContact = null;
        }
    }

    /// <summary>
    /// Binding Grid for Question : 7
    /// </summary>
    /// <param name="ContactPersonId"></param>
    private void BindGrdQ7(DataSet _dsServeyRpt, Int32 ContactPersonId)
    {
        try
        {
            //Binding OverAll Result...
            lblTotal_Q7_1.Visible = true;
            lblTotalAns_Q7_1.Visible = true;
            Int32 TotalCntQ7_1 = 0;
            if (_dsServeyRpt != null && _dsServeyRpt.Tables.Count > 0)
            {
                if (_dsServeyRpt.Tables[0] != null && _dsServeyRpt.Tables[0].Rows.Count > 0)
                {
                    TotalCntQ7_1 = Convert.ToInt32(_dsServeyRpt.Tables[0].Compute("SUM(Count)", ""));
                    lblTotal_Q7_1.Text = Convert.ToString(TotalCntQ7_1);
                }
                else
                {
                    return;
                }
                if (ContactPersonId == 0)
                {
                    if (_dsServeyRpt.Tables[0] != null && _dsServeyRpt.Tables[0].Rows.Count > 0)
                    {
                        grdQ7_1.DataSource = _dsServeyRpt.Tables[0];
                        grdQ7_1.DataBind();
                    }
                    else
                    {
                        grdQ7_1.DataSource = null;
                        grdQ7_1.DataBind();
                    }
                }
                else if (ContactPersonId > 0)
                {
                    DataView dv = new DataView();
                    dv = _dsServeyRpt.Tables[0].DefaultView;
                    dv.RowFilter = "ContactPersonId=" + ContactPersonId;

                    if (dv != null && dv.Count > 0)
                    {
                        grdQ7_1.DataSource = dv;
                        grdQ7_1.DataBind();
                    }
                    else
                    {
                        grdQ7_1.DataSource = null;
                        grdQ7_1.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("BindGrdQ7 Error - " + ex.Message);
        }
    }

    /// <summary>
    /// Binding Grid for Question : 8
    /// </summary>
    /// <param name="ContactPersonId"></param>
    private void BindGrdQ8(DataSet _dsServeyRpt, Int32 ContactPersonId)
    {
        try
        {
            if (_dsServeyRpt != null && _dsServeyRpt.Tables.Count > 0)
            {
                if (ContactPersonId == 0)
                {
                    grdQ8All.Visible = true;
                    grdQ8_1.Visible = false;
                    if (_dsServeyRpt.Tables[1] != null && _dsServeyRpt.Tables[1].Rows.Count > 0)
                    {
                        grdQ8All.DataSource = _dsServeyRpt.Tables[1];
                        grdQ8All.DataBind();
                    }
                    else
                    {
                        grdQ8All.DataSource = null;
                        grdQ8All.DataBind();
                    }
                }
                else if (ContactPersonId > 0)
                {
                    DataView dv = new DataView();
                    if (_dsServeyRpt.Tables[1] != null && _dsServeyRpt.Tables[1].Rows.Count > 0)
                    {
                        dv = _dsServeyRpt.Tables[1].DefaultView;
                        dv.RowFilter = "ContactPersonId=" + ContactPersonId;

                        if (dv != null && dv.Count > 0)
                        {
                            //-------------Creating Temp Table ----------
                            DataTable dt1 = new DataTable();

                            DataTable dt2 = new DataTable();
                            dt2 = dv.ToTable();

                            DataColumn dcName = new DataColumn("Text");
                            DataColumn dcPerCentage = new DataColumn("Count");
                            DataColumn dcTotal = new DataColumn("Total");

                            dt1.Columns.Add(dcName);
                            dt1.Columns.Add(dcPerCentage);
                            dt1.Columns.Add(dcTotal);


                            DataRow dr = dt1.NewRow();
                            dr["Text"] = dt2.Columns[2].ColumnName.ToString();
                            dr["Count"] = dt2.Rows[0][2].ToString();
                            dr["Total"] = dt2.Rows[0][7].ToString();
                            dt1.Rows.Add(dr);

                            DataRow dr1 = dt1.NewRow();
                            dr1["Text"] = dt2.Columns[3].ColumnName.ToString();
                            dr1["Count"] = dt2.Rows[0][3].ToString();
                            dr1["Total"] = dt2.Rows[0][7].ToString();

                            dt1.Rows.Add(dr1);

                            DataRow dr2 = dt1.NewRow();
                            dr2["Text"] = dt2.Columns[4].ColumnName.ToString();
                            dr2["Count"] = dt2.Rows[0][4].ToString();
                            dr2["Total"] = dt2.Rows[0][7].ToString();

                            dt1.Rows.Add(dr2);

                            DataRow dr3 = dt1.NewRow();
                            dr3["Text"] = dt2.Columns[5].ColumnName.ToString();
                            dr3["Count"] = dt2.Rows[0][5].ToString();
                            dr3["Total"] = dt2.Rows[0][7].ToString();

                            dt1.Rows.Add(dr3);

                            DataRow dr4 = dt1.NewRow();
                            dr4["Text"] = dt2.Columns[6].ColumnName.ToString();
                            dr4["Count"] = dt2.Rows[0][6].ToString();
                            dr4["Total"] = dt2.Rows[0][7].ToString();

                            dt1.Rows.Add(dr4);

                            grdQ8_1.DataSource = dt1;
                            grdQ8_1.DataBind();
                            grdQ8All.Visible = false;
                            grdQ8_1.Visible = true;
                        }
                        else
                        {
                            grdQ8_1.DataSource = null;
                            grdQ8_1.DataBind();
                        }
                    }
                    else
                    {
                        grdQ8_1.DataSource = null;
                        grdQ8_1.DataBind();
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("BindGrdQ8 Error - " + ex.Message);
        }
        finally
        {
        }
    }

    /// <summary>
    /// Binding Grid for Question : 9
    /// </summary>
    /// <param name="ContactPersonId"></param>
    private void BindGrdQ9(DataSet _dsServeyRpt, Int32 ContactPersonId)
    {
        try
        {
            if (_dsServeyRpt != null && _dsServeyRpt.Tables.Count > 0)
            {
                if (ContactPersonId == 0)
                {
                    if (_dsServeyRpt.Tables[2] != null && _dsServeyRpt.Tables[2].Rows.Count > 0)
                    {
                        grdQ9All.DataSource = _dsServeyRpt.Tables[2];
                        grdQ9All.DataBind();
                        grdQ9_1.Visible = false;
                        grdQ9All.Visible = true;
                    }
                    else
                    {
                        grdQ9All.DataSource = null;
                        grdQ9All.DataBind();
                    }
                }
                else if (ContactPersonId > 0)
                {
                    DataView dv = new DataView();
                    if (_dsServeyRpt.Tables[2] != null && _dsServeyRpt.Tables[2].Rows.Count > 0)
                    {
                        dv = _dsServeyRpt.Tables[2].DefaultView;
                        dv.RowFilter = "ContactPersonId=" + ContactPersonId;

                        if (dv != null && dv.Count > 0)
                        {
                            //-------------Creating Temp Table ----------
                            DataTable dt1 = new DataTable();

                            DataTable dt2 = new DataTable();
                            dt2 = dv.ToTable();

                            DataColumn dcName = new DataColumn("Answer");
                            DataColumn dcPerCentage = new DataColumn("Count");
                            DataColumn dcTotal = new DataColumn("Total");

                            dt1.Columns.Add(dcName);
                            dt1.Columns.Add(dcPerCentage);
                            dt1.Columns.Add(dcTotal);

                            DataRow dr = dt1.NewRow();
                            dr["Answer"] = dt2.Columns[2].ColumnName.ToString();
                            dr["Count"] = dt2.Rows[0][2].ToString();
                            dr["Total"] = dt2.Rows[0][4].ToString();

                            dt1.Rows.Add(dr);

                            DataRow dr1 = dt1.NewRow();
                            dr1["Answer"] = dt2.Columns[3].ColumnName.ToString();
                            dr1["Count"] = dt2.Rows[0][3].ToString();
                            dr1["Total"] = dt2.Rows[0][4].ToString();

                            dt1.Rows.Add(dr1);
                            grdQ9_1.DataSource = dt1;
                            grdQ9_1.DataBind();

                            grdQ9_1.Visible = true;
                            grdQ9All.Visible = false;
                        }
                        else
                        {
                            grdQ9_1.DataSource = null;
                            grdQ9_1.DataBind();
                        }
                    }
                    else
                    {
                        grdQ9_1.DataSource = null;
                        grdQ9_1.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("BindGrdQ9 Error - " + ex.Message);
        }
    }


    /// <summary>
    /// Binding Grid for Question : 18
    /// </summary>
    /// <param name="ContactPersonId"></param>
    private void BindGrdQ18(DataSet _dsServeyRpt, Int32 ContactPersonId)
    {
        try
        {
            if (_dsServeyRpt != null && _dsServeyRpt.Tables.Count > 0)
            {
                if (_dsServeyRpt.Tables[3] != null && _dsServeyRpt.Tables[3].Rows.Count > 0)
                {
                    grdQ18All.DataSource = _dsServeyRpt.Tables[3];
                    grdQ18All.DataBind();
                    grdQ18All.Visible = true;
                }
                else
                {
                    grdQ18All.Visible = false ;
                }       
            }
        }
        catch (Exception ex)
        {
            logger.Error("BindGrdQ9 Error - " + ex.Message);
        }
    }
    /// <summary>
    /// RowDataBound event of Grid : Question 18:displaying details of All Contact person.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdQ18All_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                Label lblTotalQ18All = (Label)e.Row.FindControl("lblTotalQ18All");
                Double _cnt = 0, _total, _per = 0;

                if (!string.IsNullOrEmpty(lblTotalQ18All.Text.Trim()))
                {
                    _total = Convert.ToDouble(lblTotalQ18All.Text.Trim());

                    Label lblYesPer = (Label)e.Row.FindControl("lblYesPer");
                    Label lblYesCount = (Label)e.Row.FindControl("lblYesCount");

                    Label lblNoPer = (Label)e.Row.FindControl("lblNoPer");
                    Label lblNoCount = (Label)e.Row.FindControl("lblNoCount");

                    Panel pnlInner_Yes = (Panel)e.Row.FindControl("pnlInner_Yes");
                    Panel pnlInner_No = (Panel)e.Row.FindControl("pnlInner_No");

                    if (_total > 0)
                    {
                        _cnt = Convert.ToDouble(lblYesCount.Text.Trim());

                        _per = (_cnt / _total);
                        lblYesPer.Text = Convert.ToString(_per.ToString("P"));
                        Int32 intPerCentageYes = Convert.ToInt32(_per * 100);
                        pnlInner_Yes.Style.Add("width", intPerCentageYes.ToString() + "%");


                        _cnt = 0; _per = 0;
                        _cnt = Convert.ToDouble(lblNoCount.Text.Trim());
                        _per = (_cnt / _total);
                        lblNoPer.Text = Convert.ToString(_per.ToString("P"));

                        Int32 intPerCentageNo = Convert.ToInt32(_per * 100);
                        pnlInner_No.Style.Add("width", intPerCentageNo.ToString() + "%");
                    }
                    else if (_total == 0)
                    {
                        lblYesPer.Text = Convert.ToString(_per.ToString("P"));
                        lblNoPer.Text = Convert.ToString(_per.ToString("P"));

                        pnlInner_Yes.Style.Add("width", 0 + "%");
                        pnlInner_No.Style.Add("width", 0 + "%");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("grdQ18All_Row_Databound Error - " + ex.Message);
        }
        finally
        {

        }
    }

    #endregion
}
