using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mechsoft.GeneralUtilities;
using System.Text;
using log4net;


public partial class Quotation : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(Quotation));
    public static int txtValTabIndex = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            if (Request.QueryString["id"].ToString() != "")
            {
                if (!IsPostBack)
                {
                    ((Label)Master.FindControl("lblHeader")).Text = "Create Quotation";
                    // txtDate.Text = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    txtDealerNotes.Attributes.Add("onkeypress", "return maxLength(event,this);");
                    txtUpNotes.Attributes.Add("onkeypress", "return maxLength(event,this);");

                    if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                        ViewState["RequestId"] = Convert.ToInt32(Request.QueryString["id"]);
                    ViewState["QuotationID"] = Convert.ToInt32(Request.QueryString["QuoteID"]);
                    ViewState["DID"] = Convert.ToInt32(Request.QueryString["DID"]);
                    addDate();

                    Cls_UserMaster objUserMaster = new Cls_UserMaster();
                    objUserMaster.ConsultantID = Convert.ToInt16(Request.QueryString["ConsultantID"].ToString());
                    dlConsultantInfo.DataSource = objUserMaster.GetConsultantBasicInfo();
                    dlConsultantInfo.RepeatColumns = 1;
                    dlConsultantInfo.DataBind();
                }
                #region "Comment"
                // txtBuildComplianceDate.Text = Request.Params[txtBuildComplianceDate.UniqueID];

                //txtBuild.Text = Request.Params[txtBuild.UniqueID];
                //txtDate.Text = Request.Params[txtDate.UniqueID];

                //RangeValEstimatedTimeOfDelivery.MinimumValue = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
                //RangeValEstimatedTimeOfDelivery.MaximumValue = DateTime.UtcNow.Date.AddDays(365).ToString("MM/dd/yyyy");

                //logger.Debug(txtEstimatedTimeOfDelivery.Text);
                //logger.Debug(RangeValEstimatedTimeOfDelivery.MinimumValue);
                //logger.Debug(RangeValEstimatedTimeOfDelivery.MaximumValue);

                //pan1.DefaultButton = "btnDefault"; 

                #endregion

                txtEstimatedTimeOfDelivery.Text = Request.Params[txtEstimatedTimeOfDelivery.UniqueID];
                UcRequestHeader1.DisplayRequestHeader(Convert.ToInt32(ViewState["RequestId"]));

                BindData();

                if (!IsPostBack)
                {
                    if (Request.QueryString["Change"] == "update")
                    {
                        ViewState["Update"] = Request.QueryString["Change"].ToString();
                        BindDataUpdate();
                    }
                    else
                        ViewState["Update"] = "Normal";
                }
            }
        }
        else {
            divOrderCancelConfirm.Style.Add("display", "block");
        }
    }

    private void BindDataUpdate()
    {
        //BindData();
        logger.Debug("Getting old Quotation");
        try
        {
            Cls_Request objRequest = null;
            objRequest = new Cls_Request();
            objRequest.RequestId = Convert.ToInt32(ViewState["RequestId"].ToString());
            objRequest.QuotationID = Convert.ToInt32(ViewState["QuotationID"].ToString());
            DataTable dtReq = objRequest.GetOldQuotation();

            foreach (GridViewRow dr in gvMakeDetails.Rows)
            {
                Label lbl1 = (Label)dr.FindControl("lblMake");
                TextBox txt1 = (TextBox)dr.FindControl("txtValue1");
                TextBox txt2 = (TextBox)dr.FindControl("txtValue2");
                TextBox txtAcc = new TextBox();
                int chk = 0, chk1 = 0;
                double quoteValue = 0;
                for (int i = 0; i < dtReq.Rows.Count; i++)
                {
                    quoteValue = 0;
                    if (lbl1.Text.ToString() == dtReq.Rows[i]["AccName"].ToString())
                    {
                        quoteValue = Convert.ToDouble(dtReq.Rows[i]["QuoteValue"]);
                        if (dtReq.Rows[i]["OptionID"].ToString() == "1")
                        {

                            if (quoteValue > 0)
                                txt1.Text = Convert.ToString(quoteValue);
                            else
                                txt1.Text = String.Empty;

                            if (lbl1.Text.ToString() == "AddA1")
                            {
                                chk = 1;
                                txtAcc = (TextBox)dr.FindControl("txtAdd1");
                                txtAcc.Text = dtReq.Rows[i]["DAcc"].ToString();
                                txtAcc.ReadOnly = true;
                            }

                        }
                        if (dtReq.Rows[i]["OptionID"].ToString() == "2")
                        {
                            if (quoteValue > 0)
                                txt2.Text = Convert.ToString(quoteValue);
                            else
                                txt2.Text = String.Empty;

                            if (lbl1.Text.ToString() == "AddA2")
                            {
                                chk1 = 1;
                                txtAcc = (TextBox)dr.FindControl("txtAdd2");
                                txtAcc.Text = dtReq.Rows[i]["DAcc"].ToString();
                                txtAcc.ReadOnly = true;
                            }
                        }

                    }
                }
                if (chk == 0 && lbl1.Text.ToString() == "AddA1")
                {
                    dr.Enabled = false;
                }
                if (chk1 == 0 && lbl1.Text.ToString() == "AddA2")
                {
                    dr.Enabled = false;
                }

            }

            txtEstimatedTimeOfDelivery.Text = dtReq.Rows[0]["EstiDate"].ToString();
            ddlMonthBuild.SelectedValue = Convert.ToDateTime(dtReq.Rows[0]["BuildDate"].ToString()).Month.ToString();
            ddlYearBuild.SelectedValue = Convert.ToDateTime(dtReq.Rows[0]["BuildDate"].ToString()).Year.ToString(); ;
            ddlComplianceMonth.SelectedValue = Convert.ToDateTime(dtReq.Rows[0]["ComplianceDate"].ToString()).Month.ToString();
            ddlComplianceYear.SelectedValue = Convert.ToDateTime(dtReq.Rows[0]["ComplianceDate"].ToString()).Year.ToString(); ; ;

            string DNotes = dtReq.Rows[0]["DealerNotes"].ToString();
            DNotes = DNotes.Replace("^", "\n");
            txtDealerNotes.Text = DNotes;
            if (Convert.ToInt32(dtReq.Rows[0]["Stock"]) == 1)
                RadioButtonList1.SelectedIndex = 0;
            else
                RadioButtonList1.SelectedIndex = 1;
            trUpdate.Visible = true;

            // added on 22 may 2012
            if (Convert.ToInt32(dtReq.Rows[0]["IsBonus"]) == 1)
            {
                chkBonus.Checked = true;
                tdExpDate.Style.Add("display", "block");
                txtBonusExpire.Text = Convert.ToDateTime(dtReq.Rows[0]["BonusExpDate"]).ToString("MM/dd/yyyy");
            }
        }
        catch (Exception ex)
        {
            logger.Error("BindDataUpdate Error - " + ex.Message);
        }
        finally
        {
        }
    }

    private void addDate()
    {
        txtEstimatedTimeOfDelivery.Text = "";
        ddlComplianceMonth.SelectedIndex = 0;
        ddlMonthBuild.SelectedIndex = 0;

        //ddlYearBuild.Items.Add("");
        ddlYearBuild.Items.Add("-Year-");
        for (int i = (System.DateTime.Now.Year) - 10; i <= (System.DateTime.Now.Year) + 2; i++)
        {
            //ddlYearBuild.Items.Add(i.ToString());
            ddlYearBuild.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }

        //ddlComplianceYear.Items.Add("");
        ddlComplianceYear.Items.Add("-Year-");
        for (int i = (System.DateTime.Now.Year) - 10; i <= (System.DateTime.Now.Year) + 2; i++)
        {
            //ddlComplianceYear.Items.Add(i.ToString());
            ddlComplianceYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    protected void gvMakeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int val1tabindex = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgActive = ((Image)e.Row.FindControl("imgActive"));
            try
            {
                //For Text Box Tab Index
                TextBox txtValue1 = (TextBox)e.Row.FindControl("txtValue1");
                TextBox txtValue2 = (TextBox)e.Row.FindControl("txtValue2");

                if (!DataBinder.Eval(e.Row.DataItem, "Key").Equals("Total-On Road Cost (Inclusive of GST)"))
                {
                    if (txtValue1 != null)
                    {
                        int tab = e.Row.RowIndex + 1;
                        txtValue1.TabIndex = Convert.ToInt16(tab.ToString());
                    }
                    if (txtValue2 != null)
                    {
                        txtValue2.TabIndex = Convert.ToInt16(txtValTabIndex.ToString());
                        txtValTabIndex = txtValTabIndex + 1;
                    }
                }
                if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("Total-On Road Cost (Inclusive of GST)"))
                {
                    txtValue1.TabIndex = Convert.ToInt16("-1");
                    txtValue2.TabIndex = Convert.ToInt16("-1");
                    //txtValue1.ReadOnly = true;
                    //txtValue2.ReadOnly = true;
                    e.Row.Style.Value = "font-weight:bold";
                }
                else if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("Sub Total") || DataBinder.Eval(e.Row.DataItem, "Key").Equals("Sub Total -"))
                {
                    ((Label)e.Row.FindControl("lblMake")).Style.Value = "padding-left: 5px";
                    imgActive.Visible = true;
                    e.Row.CssClass = "gridactiverow";
                    e.Row.Style.Value = "font-weight:bold";
                }
                //by manoj on 10 mar 11 for add acc by dealer
                if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("AddA1") || DataBinder.Eval(e.Row.DataItem, "Key").Equals("AddA2"))
                {
                    // ((Label)e.Row.FindControl("lblMake")).Text=String.Empty;
                    ((Label)e.Row.FindControl("lblMake")).Style.Value = "padding-left: 0px; Display:none;";

                    TextBox txt1 = new TextBox();
                    if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("AddA1"))
                    {
                        txt1.ID = "txtAdd1";
                        txt1.Style.Value = "Width:300px";
                        GridViewRow gv = (GridViewRow)e.Row;
                        gv.Cells[0].Controls.Add(txt1);
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("AddA2"))
                    {
                        txt1.ID = "txtAdd2";
                        txt1.Style.Value = "Width:300px";
                        GridViewRow gv = (GridViewRow)e.Row;
                        gv.Cells[0].Controls.Add(txt1);
                    }
                }
                if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("Additional Accessories") || DataBinder.Eval(e.Row.DataItem, "Key").Equals("Fixed Charges"))
                {
                    ((Label)e.Row.FindControl("lblMake")).Style.Value = "padding-left: 5px";
                    //for (int i = 2; i <= e.Row.Controls.Count; i++)
                    //{
                    //}
                    e.Row.CssClass = "gridactiverow";
                    imgActive.Visible = true;
                    //if (e.Row.Controls[1] != null)
                    //{
                    e.Row.Controls[1].Visible = false;
                    //}
                    //if (e.Row.Controls[2] != null)
                    //{
                    e.Row.Controls[2].Visible = false;
                    //}
                    e.Row.Cells[0].ColumnSpan = gvMakeDetails.Columns.Count;
                    //((Label)e.Row.Cells[0].FindControl("lblSpecification")).Style.Value = "padding-right: 5px";
                    //
                    //e.Row.Cells[2].Visible = true;
                    //if (e.Row.Controls[3] != null)
                    //{
                    if (e.Row.Controls.Count > 3)
                    {
                        e.Row.Controls[3].Visible = false;
                    }
                    //}
                    //if (e.Row.Controls[4] != null)
                    //{
                    if (e.Row.Controls.Count > 4)
                    {
                        e.Row.Controls[4].Visible = false;
                    }
                    //}
                    //if (e.Row.Controls[5] != null)
                    //{
                    if (e.Row.Controls.Count > 5)
                    {
                        e.Row.Controls[5].Visible = false;
                    }
                    //}
                }

                if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("GST ( LCT if applicable)"))
                {
                    ((Label)e.Row.FindControl("lblMake")).Style.Value = "padding-left: 0px; Display:none;";
                    ((Label)e.Row.FindControl("lblMake_1")).Text = "GST <span style='font-size:11px'>(LCT if applicable)</span>";
                    ((Label)e.Row.FindControl("lblMake_1")).Visible = true;
                    ((ImageButton)e.Row.FindControl("imgbtnLCTExempt")).Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("Recommended Retail Price Exc GST") || DataBinder.Eval(e.Row.DataItem, "Key").Equals("Total-On Road Cost (Inclusive of GST)"))
                {
                    ((Label)e.Row.FindControl("lblMake")).Style.Value = "padding-left: 5px";
                    e.Row.CssClass = "gridactiverow";
                    imgActive.Visible = true;
                    //TextBox txtValue1 = (TextBox)e.Row.FindControl("txtValue1");
                    //if (txtValue1 != null)
                    //{
                    //    txtValue1.TabIndex = Convert.ToInt16(e.Row.RowIndex.ToString());
                    //}
                    //TextBox txtValue2 = (TextBox)e.Row.FindControl("txtValue2");
                    //if (txtValue2 != null)
                    //{
                    //    txtValue2.TabIndex = Convert.ToInt16(txtValTabIndex.ToString());
                    //    txtValTabIndex = txtValTabIndex + 1;
                    //}
                }
            }
            catch (Exception ex)
            { }
        }

    }

    protected void gvMakeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMakeDetails.PageIndex = e.NewPageIndex;
        BindPagingData();
    }

    private void BindPagingData()
    {
        Cls_Request objRequest = new Cls_Request();
        objRequest.RequestId = Convert.ToInt32(ViewState["RequestId"].ToString());
        DataTable dtReq = objRequest.GetDataForQuotation();

        gvMakeDetails.DataSource = dtReq;
        gvMakeDetails.DataBind();
    }

    private void BindData()
    {

        Cls_Request objRequest = null;
        objRequest = new Cls_Request();
        objRequest.RequestId = Convert.ToInt32(ViewState["RequestId"].ToString());
        DataTable dtReq = objRequest.GetDataForQuotation();

        #region Commented
        //string strMake = "";
        //string strModel = "";
        //string strSeries = "";
        //StringBuilder MakeModelSeries = new StringBuilder();


        //if (dtReqHeader.Rows.Count > 0)
        //{
        //    strMake = dtReqHeader.Rows[0]["Make"].ToString();
        //    strModel = dtReqHeader.Rows[0]["Model"].ToString();
        //    strSeries = dtReqHeader.Rows[0]["Series"].ToString();
        //}
        //MakeModelSeries.Append(strMake);
        //if (strModel != "")
        //{
        //    MakeModelSeries.Append("," + strModel);
        //}
        //if (strSeries != "")
        //{
        //    MakeModelSeries.Append("," + strSeries);
        //}

        //DataTable dt = new DataTable();
        //dt.Columns.Add("Header");
        //dt.Columns.Add("Details");

        //DataRow dRow = null;
        //dRow = dt.NewRow();
        //dRow["Header"] = "Make,Model,Series";
        //dRow["Details"] = MakeModelSeries.ToString();
        //dt.Rows.Add(dRow);

        ////DataList1.DataSource = dt;
        ////DataList1.DataBind();

        //DataTable dt1 = new DataTable();
        //dt1.Columns.Add("Header");
        //dt1.Columns.Add("Details");

        //DataRow dRow1 = null;

        ////DataList2.RepeatColumns = 1;

        //DataTable dtReqParams = objRequest.GetRequestParameters();
        //if (dtReqParams.Rows.Count > 0)
        //{
        //    foreach (DataRow drParam in dtReqParams.Rows)
        //    {
        //        dRow1 = dt1.NewRow();
        //        dRow1["Header"] = drParam["Parameter"].ToString();
        //        if (drParam["ParamValue"].ToString() == "")
        //        {
        //            dRow1["Details"] = "-";
        //        }
        //        else
        //        {
        //            dRow1["Details"] = drParam["ParamValue"].ToString();
        //        }

        //        dt1.Rows.Add(dRow1);
        //    }
        //}

        ////DataList2.DataSource = dt1;
        ////DataList2.DataBind();
        #endregion

        DataTable dtReqHeader = objRequest.GetRequestHeaderInfo();
        if (dtReqHeader.Rows[0]["suburb"].ToString() == null || dtReqHeader.Rows[0]["suburb"].ToString() == "")
            lblSub1.Text = "--";
        else
            lblSub1.Text = dtReqHeader.Rows[0]["suburb"].ToString();

        if (dtReqHeader.Rows[0]["pcode"].ToString() == null || dtReqHeader.Rows[0]["pcode"].ToString() == "")
            lblPCode1.Text = "--";
        else
            lblPCode1.Text = dtReqHeader.Rows[0]["pcode"].ToString();

        ConfigValues objConfig = new ConfigValues();

        int optionLimit = Convert.ToInt32(objConfig.GetValue(Cls_Constants.NO_OF_OPTIONS_IN_QUOTATION));
        for (int i = 1; i < optionLimit + 1; i++)
        {
            TemplateField tf = new TemplateField();
            tf.ItemTemplate = new GridViewTextboxTemplate(DataControlRowType.DataRow, "Value1", "String", "txtValue" + i, i);
            tf.HeaderTemplate = new GridViewTextboxTemplate(DataControlRowType.Header, i.ToString(), "String", "", i);
            //tf.FooterTemplate = new GridViewTextboxTemplate(DataControlRowType.Footer, i.ToString(), "String", "txtDealerNotes" + i, i);

            tf.ItemTemplate.InstantiateIn(gvMakeDetails);
            tf.HeaderTemplate.InstantiateIn(gvMakeDetails);
            // tf.FooterTemplate.InstantiateIn(gvMakeDetails);

            gvMakeDetails.Columns.Add(tf);
        }
        txtValTabIndex = dtReq.Rows.Count;

        gvMakeDetails.DataSource = dtReq;
        gvMakeDetails.DataBind();

        String ConsultantNotes = "";
        int cnt = 0;
        //by manoj on 9 apr 2011
        if (dtReqHeader.Rows[0]["OrderTaken"].ToString() != String.Empty)
        {
            cnt++;
            ConsultantNotes = "<b style='color:Red; text-decoration:blink;'>" + dtReqHeader.Rows[0]["OrderTaken"].ToString() + "</b><br/>";
        }
        if (dtReqHeader.Rows[0]["Urgent"].ToString() != String.Empty)
        {
            cnt++;
            ConsultantNotes += dtReqHeader.Rows[0]["Urgent"].ToString() + "<br/>";
        }
        if (dtReqHeader.Rows[0]["BuildYear"].ToString() != String.Empty)
        {
            cnt++;
            ConsultantNotes += dtReqHeader.Rows[0]["BuildYear"].ToString() + "<br/>";
        }
        if (dtReqHeader.Rows[0]["ConsultantNotes"].ToString() != String.Empty)
        {
            cnt++;
            ConsultantNotes += dtReqHeader.Rows[0]["ConsultantNotes"].ToString();
        }
        if (cnt == 0)
            ConsultantNotes = "--";

        lblConsultantNotes1.Text = ConsultantNotes;

    }


    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            logger.Debug("Create new quotation Starts");
            Page.Validate();

            if (Page.IsValid)
            {
                // return;
                DataTable dtDetailsData = new DataTable();
                String ColName = "Value", ID = "";
                Cls_Quotation objQuotation = new Cls_Quotation();

                dtDetailsData.Columns.Add("ID");
                // dtDetailsData.Columns.Add("OptionID");
                dtDetailsData.Columns.Add("IsAccessory");
                dtDetailsData.Columns.Add("IsChargeType");
                dtDetailsData.Columns.Add("IsAdditionalAccessory");

                for (int i = 1; i < gvMakeDetails.Columns.Count; i++)
                {
                    dtDetailsData.Columns.Add(ColName + i.ToString());
                }

                for (int i = 0; i < gvMakeDetails.Rows.Count; i++)
                {
                    DataRow dRow = dtDetailsData.NewRow();
                    dRow[0] = ((HiddenField)gvMakeDetails.Rows[i].Cells[0].FindControl("hdfID")).Value.ToString();
                    dRow[1] = ((HiddenField)gvMakeDetails.Rows[i].Cells[0].FindControl("hdfIsAccessory")).Value.ToString();
                    dRow[2] = ((HiddenField)gvMakeDetails.Rows[i].Cells[0].FindControl("hdfIsChargeType")).Value.ToString();
                    dRow[3] = ((HiddenField)gvMakeDetails.Rows[i].Cells[0].FindControl("hdfIsAdditionAccessory")).Value.ToString();

                    for (int col = 1; col < gvMakeDetails.Columns.Count; col++)
                    {
                        dRow[col + 3] = ((TextBox)gvMakeDetails.Rows[i].Cells[0].FindControl("txtValue" + col.ToString())).Text.ToString();

                    }
                    dtDetailsData.Rows.Add(dRow);

                    // by manoj on 11 mar 2011 for dealer acc
                    Label lbl = (Label)gvMakeDetails.Rows[i].Cells[0].FindControl("lblMake");
                    if (lbl.Text.ToString() == "AddA1")
                    {
                        TextBox txt1 = (TextBox)gvMakeDetails.Rows[i].Cells[0].FindControl("txtAdd1");
                        objQuotation.AddAcc1 = txt1.Text;
                    }
                    else if (lbl.Text.ToString() == "AddA2")
                    {
                        TextBox txt1 = (TextBox)gvMakeDetails.Rows[i].Cells[0].FindControl("txtAdd2");
                        objQuotation.AddAcc2 = txt1.Text;
                    }
                }
                int Default = 0;
                objQuotation.RequestID = Convert.ToInt32(ViewState["RequestId"].ToString());
                objQuotation.Date = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                //objQuotation.Order = 0;

                //if (!string.IsNullOrEmpty(txtExStock.Text))
                //{
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    objQuotation.ExStock = 1;
                    objQuotation.Order = 0;
                }
                else
                {
                    objQuotation.ExStock = 0;
                    objQuotation.Order = 1;
                }
                //}
                objQuotation.EstimatedDeleveryDates = txtEstimatedTimeOfDelivery.Text;
                objQuotation.DealerNotes = txtDealerNotes.Text;


                string BuildDt = null;
                string ComplianceDt = null;

                if (ddlComplianceMonth.SelectedIndex <= 9)
                    ComplianceDt = "0" + ddlComplianceMonth.SelectedIndex.ToString();
                else
                    ComplianceDt = ddlComplianceMonth.SelectedIndex.ToString();
                ComplianceDt = Convert.ToString(ComplianceDt + "/" + ddlComplianceYear.SelectedItem);
                objQuotation.ComplianceDate = ComplianceDt;


                if (ddlMonthBuild.SelectedIndex <= 9)
                    BuildDt = "0" + ddlMonthBuild.SelectedIndex.ToString();
                else
                    BuildDt = ddlMonthBuild.SelectedIndex.ToString();

                BuildDt = Convert.ToString(BuildDt + "/" + ddlYearBuild.SelectedItem);
                objQuotation.Builddate = BuildDt;

                objQuotation.QuotationDetailsDataTable = dtDetailsData;
                objQuotation.UserID = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
                // added on 11 may 12 to show all quote of multi franc. dealer
                objQuotation.DealerID = Convert.ToInt32(ViewState["DID"]);
                //addedon 22 may 2012
                objQuotation.IsBonus = (chkBonus.Checked ? 1 : 0);
                objQuotation.BonusExpDate = (chkBonus.Checked ? txtBonusExpire.Text.Trim() : null);
                //end 22 may 2012

                int intResult = 0;
                //by manoj on 13 apr 2011 for keeping the quote version 
                // logger.Debug("Update QryString = " + Convert.ToString(ViewState["Update"]));
                if (Convert.ToString(ViewState["Update"]) == "update")
                {
                    logger.Debug("Revise version Starts");
                    string DNotes = objQuotation.DealerNotes;
                    DNotes = DNotes.Replace("\n", "^");
                    objQuotation.DealerNotes = DNotes;
                    // DateTime date1 = Convert.ToDateTime(objQuotation.Date);
                    //logger.Debug("Date");
                    //logger.Debug("Date - " + objQuotation.Date);
                    //logger.Debug("Date 1 - " + Convert.ToDateTime(objQuotation.Date).ToString("dd MMM yyyy HH:ss:mm"));
                    objQuotation.DealerNotes += "^" + txtUpNotes.Text + " [ Version Updated On " + objQuotation.Date + " ] ";
                    objQuotation.RequestID = Convert.ToInt32(ViewState["RequestId"].ToString());
                    objQuotation.QuotationID = Convert.ToInt32(ViewState["QuotationID"].ToString());
                    //logger.Debug("Date End");

                    intResult = objQuotation.UpdateQuotationData("update");

                    if (intResult > 0)
                    {
                        lblMsg.Text = "Quotation saved successfully";
                        sendMail(objQuotation.RequestID, objQuotation.UserID, "Version");
                        Response.Redirect("ViewDealersQuotation.aspx");
                    }
                    else
                        lblMsg.Text = "Error in saving Quotation.. Please try again.";

                    logger.Debug("Revise version Ends");
                }//end
                else
                {
                    intResult = objQuotation.AddQuotationData("new");

                    if (intResult > 0)
                    {
                        lblMsg.Text = "Quotation saved successfully";
                        sendMail(objQuotation.RequestID, objQuotation.UserID, "Original");
                        Response.Redirect("ViewDealersQuotation.aspx");
                    }
                    else
                        lblMsg.Text = "Error in saving Quotation.. Please try again.";
                }
            }
        }
        catch (Exception ex)
        {
            logger.Debug("Error - " + ex.Message);
        }
        finally
        {
            logger.Debug("Create new quotation Ends");
        }
    }

    /// <summary>
    /// Function to send mail to Consultant about Quotation submitted.
    /// 09 Dec 2010. Manoj
    /// </summary>

    private void sendMail(int RequestID, int UserID, string mode)
    {
        logger.Debug("Sending mail start for Quotation Submit");
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        Cls_Quotation objQuotation = new Cls_Quotation();
        StringBuilder str = new StringBuilder();
        // string DName="";
        try
        {
            int ID;

            Cls_Request objRequest = null;
            objRequest = new Cls_Request();

            objRequest.RequestId = RequestID;
            ID = objRequest.GetQuotationID("dealer");

            objQuotation.RequestID = RequestID;
            objQuotation.UserID = UserID;

            objQuotation.QuotationID = ID;
            DataTable dtQuotHeader = objQuotation.GetQuotationHeaders();

            // str.Append("welcome");
            str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear ");
            str.Append(dtQuotHeader.Rows[0]["Name"].ToString());

            if (mode == "Version")
                str.Append("<br /><br />You received one Revised Quotation from the Dealer ");
            else if (mode == "Original")
                str.Append("<br /><br />You received one Quotation from the Dealer ");

            str.Append(dtQuotHeader.Rows[0]["DealerName"].ToString() + " on " + System.DateTime.UtcNow.Date.ToString("MMMM dd, yyyy"));
            str.Append(" for " + Convert.ToString(dtQuotHeader.Rows[0]["Make"]) + " - " + Convert.ToString(dtQuotHeader.Rows[0]["Model"]) + " in the state " + Convert.ToString(dtQuotHeader.Rows[0]["state"]) + " - " + Convert.ToString(dtQuotHeader.Rows[0]["suburb"]) + ".</p>");


            //add email sender and receiver.
            objEmailHelper.EmailFromID = dtQuotHeader.Rows[0]["Email"].ToString();
            objEmailHelper.EmailToID = dtQuotHeader.Rows[0]["CunsMail"].ToString();

            #region HTML Design for sending mail
            /*
        //start Table
        str.Append("<table style='width: 95%; font:normal normal normal 13px Tahoma;' align='center'>");

        //Make,Model,Series
        str.Append("<tr style='border: solid 1px #acacac;'><td align='center' style='color: White; background-color: #0A73A2; font-weight: bold;'>Make,Model,Series</td></tr>");

        str.Append("<tr><td align='center' style='border: solid 1px #acacac;'>");

        DataTable dtData = objRequest.GetRequestHeaderInfo();
        
        if (dtData.Rows.Count > 0)
        {
            str.Append(dtData.Rows[0]["Make"].ToString());
            if (dtData.Rows[0]["Model"].ToString() != "")
                str.Append("," + dtData.Rows[0]["Model"].ToString());
            if (dtData.Rows[0]["Series"].ToString() != "")
                str.Append("," + dtData.Rows[0]["Series"].ToString());
        }
        str.Append("</td></tr>");

        str.Append("<tr><td style='color:White; background-color: #0A73A2; font-weight: bold; border: solid 1px #acacac;'>Request Parameter</td></tr>");
        str.Append("<tr style='border: solid 1px #acacac;'><td width='100%' align='right' style='border: solid 1px #acacac;'><table width='85%'>");

        dtData = null;
        dtData = objRequest.GetRequestParameters();
        if (dtData.Rows.Count > 0)
        {
            foreach (DataRow drParam in dtData.Rows)
            {
                //dRow1 = dt1.NewRow();
                str.Append("<tr><td align='left' style='font-size:13px; width: 50%; padding-left: 10px; background-color: #eaeaea; font-weight: bold;'>");
                str.Append(drParam["Parameter"].ToString());
                str.Append("</td>");

                str.Append("<td style='padding-left: 10px; font-size:13px;' align='left'>");
                if (drParam["ParamValue"].ToString() == "")
                    str.Append("-");
                else
                    str.Append(drParam["ParamValue"].ToString());
                str.Append("</td></tr>");
           }
        }
        str.Append("</table></td></tr>");

        //Dealer &  Consultant Info.

        str.Append("<tr><td><table width='100%' style='border: solid 1px #acacac;'>");

        str.Append("<tr><td><table width='100%'>");
        str.Append("<tr style='color: White; background-color: #0A73A2; font-weight: bold; font-size: 13px'>");
        str.Append("<td style='width: 35%'>Dealer Name </td><td style='width: 35%'>Email</td><td style='width: 30%'>Phone no.</td></tr>");
        str.Append("<tr style='font-size:13px'><td>" + dtQuotHeader.Rows[0]["DealerName"].ToString() + "</td><td> " + dtQuotHeader.Rows[0]["Email"].ToString() + "</td><td>" + dtQuotHeader.Rows[0]["Phone"].ToString() + "</td></tr>");
        str.Append("</table></td></tr>");

        str.Append("<tr><td><table width='100%'>");
        str.Append("<tr style='color: White; background-color: #0A73A2; font-weight: bold; font-size: 13px'>");
        str.Append("<td style='width: 35%'>Consultant Name </td><td style='width: 35%'>Consultant Notes</td><td style='width: 30%'></td></tr>");
        str.Append("<tr style='font-size:13px'><td>" + dtQuotHeader.Rows[0]["Name"].ToString() + "</td><td> " + dtQuotHeader.Rows[0]["ConsultantNotes"].ToString() + "</td><td> </td></tr>");
        str.Append("</table></td></tr>");

        str.Append("<tr><td><table width='100%'>");
        str.Append("<tr style='color: White; background-color: #0A73A2; font-weight: bold; font-size: 13px'>");
        str.Append("<td style='width: 35%'>Estimated Delivery Date</td><td style='width: 35%'>Quotation Submitted Date</td><td style='width: 30%'>Compliance Date</td></tr>");
        str.Append("<tr style='font-size:13px'><td>" + dtQuotHeader.Rows[0]["EstimatedDeliveryDate"].ToString() + "</td><td> " + dtQuotHeader.Rows[0]["Date"].ToString() + "</td><td>" + dtQuotHeader.Rows[0]["ComplianceDate"].ToString() + "</td></tr>");
        str.Append("</table></td></tr>");

        str.Append("<tr><td><table width='100%'>");
        str.Append("<tr style='color: White; background-color: #0A73A2; font-weight: bold; font-size: 13px'>");
        str.Append("<td style='width: 35%'>Ex Stock</td><td style='width: 35%'>Order</td><td style='width: 30%'>Dealer Notes</td></tr>");
        str.Append("<tr style='font-size:13px'><td>" + ((Int32.Parse((dtQuotHeader.Rows[0]["ExStock"].ToString()).Substring(0, (dtQuotHeader.Rows[0]["ExStock"].ToString()).IndexOf('.'))) == 0) ? "No" : "Yes") + "</td><td> " + ((Int32.Parse((dtQuotHeader.Rows[0]["Order"].ToString()).Substring(0, (dtQuotHeader.Rows[0]["Order"].ToString()).IndexOf('.'))) == 0) ? "No" : "Yes") + "</td><td>" + dtQuotHeader.Rows[0]["DealerNotes"].ToString() + "</td></tr>");
        str.Append("</table></td></tr>");

        str.Append("</table></td></tr>");

        dtData=null;
        dtData = objQuotation.GetPerticularQuotation();

        str.Append("<tr><td style='width: 100%'><table width='100%' style=' font-size:13px; border: solid 1px #acacac;'><tr style='color: White; background-color: #0A73A2; font-weight: bold; border: solid 1px #acacac;'><td style='width: 60%'>Description</td><td style='width: 20%'>Option1</td><td style='width: 20%'>Option2</td></tr>");

        for (int i = 0; i < dtData.Rows.Count; i++)
        {

            if (dtData.Rows[i]["Value1"].ToString() != "" && dtData.Rows[i]["Value1"].ToString() != null && dtData.Rows[i]["Value2"].ToString() != "" && dtData.Rows[i]["Value2"].ToString() != null)
            {
                if (dtData.Rows[i]["Key"].ToString().Equals("Recommended Retail Price Exc GST") || dtData.Rows[i]["Key"].ToString().Equals("Total-On Road Cost (Inclusive of GST)"))
                {
                    str.Append("<tr><td style='width: 60%; padding:0 0 0 7px; background-color:#E6F5FF'>");
                    str.Append(dtData.Rows[i]["Key"].ToString() + " </td>");
                    str.Append("<td style='width: 20%; padding:0 0 0 8px; background-color:#E6F5FF'>$ " + Int32.Parse((dtData.Rows[i]["Value1"].ToString()).Substring(0, (dtData.Rows[i]["Value1"].ToString()).IndexOf('.'))) + "</td><td style='width: 20%; padding:0 0 0 8px; background-color:#E6F5FF'>$ " + Int32.Parse((dtData.Rows[i]["Value2"].ToString()).Substring(0, (dtData.Rows[i]["Value2"].ToString()).IndexOf('.'))) + "</td></tr>");
                }
                else
                {
                    str.Append("<tr><td style='width: 60%; padding:0 0 0 8px;'>");
                    str.Append(dtData.Rows[i]["Key"].ToString() + " </td>");
                    str.Append("<td style='width: 20%; padding:0 0 0 8px;'>$ " + Int32.Parse((dtData.Rows[i]["Value1"].ToString()).Substring(0, (dtData.Rows[i]["Value1"].ToString()).IndexOf('.'))) + "</td><td style='width: 20%; padding:0 0 0 8px;'>$ " + Int32.Parse((dtData.Rows[i]["Value2"].ToString()).Substring(0, (dtData.Rows[i]["Value2"].ToString()).IndexOf('.'))) + "</td></tr>");
                }
            }
            else
            {
                str.Append("<tr><td colspan='3' style='width: 60%; background-color:#E6F5FF; padding:0 0 0 8px;'>");
                str.Append(dtData.Rows[i]["Key"].ToString() + " </td></tr>");
               // str.Append("<td style='width: 20%; background-color:#D9f1ff '>" + dtData.Rows[i]["Value1"].ToString() + "</td><td style='width: 20%;'>" + dtData.Rows[i]["Value2"].ToString() + "</td></tr>");
            }
                
        }

        str.Append("</table></td></tr></table>");
  */
            #endregion


            string link = ConfigurationManager.AppSettings["DummyPageUrl1"];
            str.Append("<p style='font: normal normal normal 12px Tahoma;'><a href='");
            str.Append(link);
            str.Append("'>Click here</a>");
            str.Append(" to check the Quotation</p>");

            objEmailHelper.EmailBody = str.ToString();
            //objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
            //objEmailHelper.EmailFromID = "manoj.mahagaonkar@mechsoftgroup.com";
            objEmailHelper.EmailSubject = "Received Quotation From Dealer";

            if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
                objEmailHelper.SendEmail();

            dtQuotHeader.Dispose();
            //dtData.Dispose();
        }
        catch (Exception ex)
        {
            logger.Debug("Error while submitting quote - " + ex.Message);
        }
        finally
        {
            logger.Debug("Sending mail ends for Quotation Submit");
        }

    }

    protected void gvMakeDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnDefault_Click(object sender, EventArgs e)
    {

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewRecivedQuoteRequests.aspx", false);
    }
}
