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
using log4net;
using Mechsoft.FleetDeal;
using Mechsoft.GeneralUtilities;
using System.Xml;
using System.IO;
using System.Text;
using System.Web.Mail;
using System.Data.SqlClient;

public partial class QuoteRequestOld : System.Web.UI.Page
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(QuoteRequestOld));

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        logger.Debug("Create Quote Load Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {
            lblMsg.Text = String.Empty;
            UcRequestParameters1.BindParameters();
            //Set page header text
            if (!IsPostBack)
            {
                txtPCode.Attributes.Add("onkeypress", "return isNumberKey(event,this);");
                TextBox1.Attributes.Add("onkeypress", "return maxLength(event,this);");
                //CleanUp();
                Label lblHeader = (Label)Master.FindControl("lblHeader");
                if (lblHeader != null)
                    lblHeader.Text = "Create Quote Request";

                //Fill make dropdown
                FillMake();

                //Fill Master Accessories
                //UcRequestParameters1.SeriesId = 0;
                //UcRequestParameters1.ModelId = 0;
                UcRequestParameters1.FillAdditionalAccessories();


                #region added on 30 June 2012 by manoj
                if (Convert.ToString(Request.QueryString["moveto"]) == "addDealer")
                {
                    Cls_Request objRequest = new Cls_Request();
                    DataSet dsReqDetails = new DataSet();
                    GridView gv = null;
                    ViewState["RequestID"] = Convert.ToInt32(Request.QueryString["RequestID"]);
                    objRequest.RequestId = Convert.ToInt32(ViewState["RequestID"]);
                    dsReqDetails = objRequest.GetAllRequestInfo();
                    DataTable dtTemp = new DataTable();
                    DataTable dtTemp_1 = new DataTable();

                    dtTemp = dsReqDetails.Tables[0];
                    dtTemp_1 = dsReqDetails.Tables[1];


                    //1st view  Binding
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        ViewState["dtQuoteInfo_New"] = dtTemp;

                        ddlMake.SelectedValue = Convert.ToString(dtTemp.Rows[0]["MakeID"]);
                        ddlMake_SelectedIndexChanged(null, null);
                        ddlModel.SelectedValue = Convert.ToString(dtTemp.Rows[0]["ModelID"]);
                        txtSeries.Text = Convert.ToString(dtTemp.Rows[0]["Series_1"]);
                        chkOrderTaken.Checked = Convert.ToBoolean(dtTemp.Rows[0]["IsOrderTaken"]);
                        chkUrgent.Checked = Convert.ToBoolean(dtTemp.Rows[0]["IsUrgent"]);
                        chkBuid.Checked = Convert.ToBoolean(dtTemp.Rows[0]["IsCurrentYear"]);
                        TextBox1.Text = Convert.ToString(dtTemp.Rows[0]["ConsultantNotes"]);

                        if (Convert.ToString(dtTemp.Rows[0]["IsQRSend"]) == null || Convert.ToString(dtTemp.Rows[0]["IsQRSend"]).Equals(String.Empty))
                            lblQRSend.Text = "QR Details not Send to any Customer.";
                        else if (Convert.ToInt32(dtTemp.Rows[0]["IsQRSend"]) == 0)
                            lblQRSend.Text = "QR Details not Send to any Customer.";
                        else
                            lblQRSend.Text = "QR Details Send to Customer on " + Convert.ToString(dtTemp.Rows[0]["QRDate"]) + " date.";

                        // Bind Additional Acc.
                        DataView dv = dtTemp_1.DefaultView;
                        dv.RowFilter = "IsParameter=0";
                        dtTemp = null;
                        dtTemp = dv.ToTable();

                        UcRequestParameters1.BuildDataTable1();
                        gv = (GridView)UcRequestParameters1.FindControl("gvAccessories");
                        gv.DataSource = UcRequestParameters1.UpdateAddAccViewState(dtTemp);
                        gv.DataBind();

                        // Bind Request Parameters
                        gv = null;
                        gv = (GridView)UcRequestParameters1.FindControl("gvParameters");

                        dv = dtTemp_1.DefaultView;
                        dv.RowFilter = "IsParameter=1";
                        dtTemp = null;
                        dtTemp = dv.ToTable();
                        int j = 1;
                        foreach (GridViewRow gvr in gv.Rows)
                        {
                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                string accName = Convert.ToString(dtTemp.Rows[i]["accessoryname"]);
                                if (((Label)gvr.FindControl("lblAccessory")).Text.Contains(accName))
                                {
                                    ((TextBox)gvr.FindControl("txtValue" + j)).Text = Convert.ToString(dtTemp.Rows[i]["Specification"]);
                                    ((HiddenField)gvr.FindControl("hdfID")).Value = Convert.ToString(dtTemp.Rows[i]["ID"]);
                                }
                            }
                            j++;
                        }

                        UcRequestParameters1.BuildDataTable();
                        btnNextStep_Click(null, null);
                        // end

                        //2nd view starts
                        btnPrevious.Visible = false;
                        dtTemp = dsReqDetails.Tables[0];
                        txtPCode.Text = Convert.ToString(dtTemp.Rows[0]["PCode"]);
                        txtPCode_TextChanged(null, null);
                        ddlLocation.SelectedValue = Convert.ToString(dtTemp.Rows[0]["SuburbID"]);

                        UcDealerSelection1.setGridSortParams();
                        btnSearchDealers_Click(null, null);
                        txtPCode.Enabled = false;
                        ddlLocation.Enabled = false;
                        btnSearchDealers.Enabled = false;
                        dtTemp = null;
                        dtTemp = dsReqDetails.Tables[2];

                        //Bind Dealer Details for Search
                        gv = (GridView)UcDealerSelection1.FindControl("gvDealerDetails");
                        foreach (GridViewRow gvr in gv.Rows)
                        {
                            int intSelectedID = Convert.ToInt32(gv.DataKeys[gvr.RowIndex].Values["ID"]);
                            ImageButton btnSelect = (ImageButton)gvr.FindControl("btnSelect");
                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dtTemp.Rows[i]["ID"]) == intSelectedID)
                                {
                                    btnSelect.ImageUrl = "~/Images/Select_dealer_disabled.gif";
                                    btnSelect.Enabled = false;
                                }
                            }
                        }

                        //bind Already Selected Dealers
                        gv = null;
                        gv = (GridView)UcDealerSelection1.FindControl("gvSelectedDealers_pre");
                        gv.DataSource = dtTemp;
                        gv.DataBind();
                        ((HtmlTable)UcDealerSelection1.FindControl("tblSelectedDealers_pre")).Visible = true;
                        //end
                        //3rd view
                        trQRSend.Visible = true;
                        imgbutCreate.Visible = false;
                        imgbtnAddDealer.Visible = true;
                        //end
                    }
                }
                #endregion end
            }

            //Bind parameters

            QuoteRequest_1.DefaultButton = "btnSearchDealers";
            PanView1.DefaultButton = "btnNextStep";
            chkBuid.Text = " Must be " + DateTime.Now.Year.ToString() + " Build & Complied";

        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Event : " + ex.Message);
        }
        finally
        {
            logger.Debug("Create Quote Load Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }
    #endregion

    #region "Events"
    protected void lnkSelection_Click(object sender, EventArgs e)
    {
        mvQuoteRequest.SetActiveView(viewDealerSelection);
    }

    protected void lnkRequest_Click(object sender, EventArgs e)
    {
        mvQuoteRequest.SetActiveView(viewCreateRequest);
    }

    protected void btnNextStep_Click(object sender, ImageClickEventArgs e)
    {
        //logger.Debug("Step 1 Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        Page page = HttpContext.Current.Handler as Page;
        try
        {
            int tempChk = 0;
            GridView gvTemp = (GridView)UcRequestParameters1.FindControl("gvAddAcc");

            foreach (GridViewRow gvr in gvTemp.Rows)
            {
                TextBox txtAddAccessory = (TextBox)gvr.FindControl("txtAddAccessory");
                TextBox txtAddSpec = (TextBox)gvr.FindControl("txtAddSpec");
                //                ImageButton ibtnSaveAddtional = (ImageButton)gvr.FindControl("ibtnSaveAddtional");

                if (gvr.Visible && (!txtAddAccessory.Text.Equals(String.Empty) || !txtAddSpec.Text.Equals(String.Empty)))
                {
                    //if (tempChk == 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "confirm('Additional Accessories were not saved. Do you want to save it?') ;", true);
                    //}
                    tempChk = 1;
                    //UcRequestParameters1.gvAddAcc_RowCommand(ibtnSaveAddtional, null);

                }
            }

            if (tempChk == 0)
            {
                UcRequestParameters1.MethodForSelectParam();
                UcRequestParameters1.UpdateDataTable();

                mvQuoteRequest.SetActiveView(viewDealerSelection);
                txtPCode.Text = "";
                ddlLocation.Items.Clear();
                ddlLocation.Items.Insert(0, new ListItem("-Select Suburb-", "0"));


                GridView gvtemp = (GridView)UcDealerSelection1.FindControl("gvSelectedDealers");
                gvtemp.DataBind();
                UcDealerSelection1.FindControl("tblSelectedDealers").Visible = false;
                UcDealerSelection1.FindControl("lblRowsToDisplay").Visible = false;
                UcDealerSelection1.FindControl("ddl_NoRecords").Visible = false;

                GridView gv1 = (GridView)UcDealerSelection1.FindControl("gvDealerDetails");
                gv1.DataBind();

                GridView gvAddAcc = (GridView)UcRequestParameters1.FindControl("gvAddAcc");
                gvAddAcc.DataBind();
                gvAddAcc.Visible = false;
            }
            else
            {
                ((Label)UcRequestParameters1.FindControl("lblMsgAcc_1")).Visible = true;
            }


        }
        catch (Exception ex)
        {
            logger.Error("btnNextStep_Click Event : " + ex.Message);
        }
        finally
        {
            //   logger.Debug("Step 1 Ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        logger.Debug("btnPrevious_Click starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {
            mvQuoteRequest.SetActiveView(viewCreateRequest);
            GridView gvAddAcc = (GridView)UcRequestParameters1.FindControl("gvAddAcc");
            gvAddAcc.Visible = true;
            UcRequestParameters1.BindAdditionalAccTable();
        }
        catch (Exception ex)
        {
            logger.Error("btnPrevious_Click Error=" + ex.Message);
        }
        logger.Debug("btnPrevious_Click ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        logger.Debug("btnBack_Click starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        mvQuoteRequest.SetActiveView(viewCreateRequest);
        logger.Debug("btnBack_Click ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
    }

    public void DisplayRequestHeader()
    {
        logger.Debug("DisplayRequestHeader Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        Cls_Request objRequest = new Cls_Request();
        try
        {
            string strMake = "";
            string strModel = "";
            //string strSeries = "";

            DataList1.RepeatColumns = 1;

            strMake = Convert.ToString(ddlMake.SelectedItem);

            if (ddlModel.SelectedValue != "0")
                strModel = Convert.ToString(ddlModel.SelectedItem);

            //strSeries = dtReqHeader.Rows[0]["Series"].ToString();

            StringBuilder MakeModelSeries = new StringBuilder();
            MakeModelSeries.Append(strMake);
            if (strModel != "")
            {
                MakeModelSeries.Append("," + strModel);
            }
            if (!txtSeries.Text.Equals(String.Empty))
            {
                MakeModelSeries.Append("," + txtSeries.Text.Trim());
            }
            //if (strSeries != "")
            //{
            //    MakeModelSeries.Append("," + strSeries);
            //}



            DataTable dt = new DataTable();
            dt.Columns.Add("Header");
            dt.Columns.Add("Details");

            DataRow dRow = null;
            dRow = dt.NewRow();
            dRow["Header"] = "Make,Model,Series";
            dRow["Details"] = MakeModelSeries.ToString();
            dt.Rows.Add(dRow);

            DataList1.DataSource = dt;
            DataList1.DataBind();

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Header");
            dt1.Columns.Add("Details");

            DataRow dRow1 = null;

            DataList2.RepeatColumns = 1;
            int cnt = 1;
            foreach (GridViewRow gr1 in ((GridView)UcRequestParameters1.FindControl("gvParameters")).Rows)
            {
                Label lbl1 = (Label)gr1.FindControl("lblAccessory");
                TextBox txt1 = (TextBox)gr1.FindControl("txtValue" + cnt);
                dRow1 = dt1.NewRow();
                dRow1["Header"] = Convert.ToString(lbl1.Text);
                if (Convert.ToString(txt1.Text) == "")
                {
                    dRow1["Details"] = "-";
                }
                else
                {
                    dRow1["Details"] = Convert.ToString(txt1.Text);
                }
                //dRow1["Details"] = drParam["ParamValue"].ToString();
                dt1.Rows.Add(dRow1);
                cnt++;
            }


            DataList2.DataSource = dt1;
            DataList2.DataBind();
            ViewState["ReqParams_New"] = dt1;


            if (((GridView)UcRequestParameters1.FindControl("gvAccessories")).Rows.Count > 0)
            {
                trAddAcc.Visible = true;

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("Header");
                dt2.Columns.Add("Details");

                DataRow dRow2 = null;

                DataList3.RepeatColumns = 1;
                cnt = 1;
                foreach (GridViewRow gr1 in ((GridView)UcRequestParameters1.FindControl("gvAccessories")).Rows)
                {
                    Label lbl1 = (Label)gr1.FindControl("lblAccessory");
                    TextBox txt1 = (TextBox)gr1.FindControl("txtSpec");
                    dRow2 = dt2.NewRow();
                    dRow2["Header"] = Convert.ToString(lbl1.Text);
                    if (Convert.ToString(txt1.Text) == "")
                    {
                        dRow2["Details"] = "-";
                    }
                    else
                    {
                        dRow2["Details"] = Convert.ToString(txt1.Text);
                    }
                    //dRow1["Details"] = drParam["ParamValue"].ToString();
                    dt2.Rows.Add(dRow2);
                    cnt++;
                }
                DataList3.DataSource = dt2;
                DataList3.DataBind();
                ViewState["AddAcc_New"] = dt2;
            }
            cnt = 0;
            if (chkOrderTaken.Checked)
            {
                trConsNotes.Visible = true;
                cnt++;
                lit.Text = "<b><span style='text-decoration:blink; color:red;'>Order Taken</span></b>";
            }
            if (chkUrgent.Checked)
            {
                trConsNotes.Visible = true;
                if (cnt == 0)
                    lit.Text = "Some flexibility depending on delivery.Urgently required";
                else
                    lit.Text += "<br/>Some flexibility depending on delivery.Urgently required";

                cnt++;
            }
            if (chkBuid.Checked)
            {
                trConsNotes.Visible = true;
                if (cnt == 0)
                    lit.Text = "Must be 2011 Build & Complied";
                else
                    lit.Text += "<br/>Must be " + System.DateTime.Now.Date.Year + " Build & Complied";
            }
            if (!TextBox1.Text.Equals(String.Empty))
            {
                if (cnt == 0)
                    lit.Text = TextBox1.Text;
                else
                    lit.Text += "<br/>" + TextBox1.Text;
            }
        }
        catch (Exception ex)
        {
            logger.Error("DisplayRequestHeader Error=" + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("DisplayRequestHeader ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }

    protected void btnCreateRequest_Click(object sender, ImageClickEventArgs e)
    {
        #region "Check selected dealer limit"
        if (ddlLocation.Items.Count == 1 && Convert.ToString(ddlLocation.SelectedItem) == "- No Loaction Found -")
        {
            GridView gvTemp = (GridView)UcDealerSelection1.FindControl("gvSelectedDealers");
            gvTemp.DataSource = null;
            gvTemp.DataBind();
            UcDealerSelection1.BuildDataTable();

        }

        if (!ValidateDealerSelection())
            return;
        #endregion

        mvQuoteRequest.SetActiveView(view1);
        //int cnt = 0;
        //lblLastMsg.Text = "Sent this Quote Request to the";
        //foreach (GridViewRow gr in gvSelectedDealers1.Rows)
        //{
        //    if (cnt == 0)
        //        lblLastMsg.Text += " Dealer " + Convert.ToString(gr.Cells[0].Text);
        //    else
        //        lblLastMsg.Text += ", " + Convert.ToString(gr.Cells[0].Text);
        //    cnt++;
        //}
        DisplayRequestHeader();
        // lblLastMsg += " "+gvSelectedDealers1;


    }

    private int CreateRequest()
    {
        logger.Debug("CreateRequest Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        Cls_Request objRequest = new Cls_Request();

        try
        {
            #region "Check selected dealer limit"
            if (!ValidateDealerSelection())
                return 0;
            #endregion

            // ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Hi')", true);

            DataRow Suburb = UcRequestParameters1.dtParameters.NewRow();
            Suburb["ID"] = "5";
            Suburb["AccessoryName"] = "Deliver To";
            if (ddlLocation.SelectedItem.ToString() == "-Select Suburb-" || ddlLocation.SelectedItem.ToString() == "- Select Location -")
            {
                Suburb["Specification"] = "";
            }
            else
            {
                Suburb["Specification"] = ddlLocation.SelectedItem.ToString();
            }
            Suburb["IsDBDriven"] = "True";
            UcRequestParameters1.dtParameters.Rows.Add(Suburb);
            //save quote request details to database

            //Suburb id
            //objRequest.SuburbID = Convert.ToInt32(ddlLocation.SelectedValue.ToString());
            if (ddlLocation.SelectedItem.ToString() == "- Select Location -" || ddlLocation.SelectedItem.ToString() == "-Select Suburb-")
                objRequest.SuburbID = 0;
            else
                objRequest.SuburbID = Convert.ToInt32(ddlLocation.SelectedValue.ToString());


            //MakedID
            objRequest.ID = Convert.ToInt32(ddlMake.SelectedValue.ToString());

            if (ddlModel.SelectedIndex > 0)
                objRequest.ModelID = Convert.ToInt32(ddlModel.SelectedValue.ToString());

            if (ddlSeries.SelectedIndex > 0)
                objRequest.SeriesId = Convert.ToInt32(ddlSeries.SelectedValue);

            objRequest.ConsultantId = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            objRequest.ConsultantNotes = TextBox1.Text;
            objRequest.DealerIds = UcDealerSelection1.SelectedDealerIds;

            //by manoj on 8 apr 11
            objRequest.Series = txtSeries.Text.Trim();
            if (chkOrderTaken.Checked)
                objRequest.OrderTaken = 1;
            else
                objRequest.OrderTaken = 0;

            if (chkUrgent.Checked)
                objRequest.Urgent = 1;
            else
                objRequest.Urgent = 0;

            if (chkBuid.Checked)
                objRequest.BuildYear = 1;
            else
                objRequest.BuildYear = 0;
            //end

            ClearConstraintsOfDataTable(UcRequestParameters1.dtParameters);
            ClearConstraintsOfDataTable(UcRequestParameters1.dtAccessories);

            //xml document for additional accessories and parameters
            if (UcRequestParameters1.dtAccessories.Rows.Count > 0)
                UcRequestParameters1.dtAccessories.Merge(UcRequestParameters1.dtParameters);
            else
                UcRequestParameters1.dtAccessories.Merge(UcRequestParameters1.dtParameters);

            objRequest.XmlDocument = ConvertDataTableToXML(UcRequestParameters1.dtAccessories).InnerXml;
            objRequest.PCode = txtPCode.Text.Trim().ToString();
            //save request
            logger.Debug("Save to DB Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            int result = objRequest.SaveQuoteRequest();
            logger.Debug("Save to DB Ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            logger.Debug("Req ID - " + result);
            if (result > 0)
            {
                CleanUp();
                sendMail(result);

            }
            else
                lblMsg.Text = "Error in saving Quote Request.. Please try again.";
            return result;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in saving Quote Request.. Please try again.";
            logger.Error("btnCreateRequest_Click Event : " + ex.Message);
            return 0;
        }
        finally
        {
            logger.Debug("CreateRequest Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            objRequest = null;
        }

    }

    /// <summary>
    /// Function to send mail to Dealers about Quotation Request.
    /// 09 Dec 2010. Manoj
    /// </summary>

    private void sendMail(int ID)
    {
        logger.Debug("Sending mail start for new Quote Request");

        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        Cls_UserMaster objUserMaster = new Cls_UserMaster();
        Cls_Request objRequest = new Cls_Request();
        string ConsultantNotes = "-", MakeModelSeries = "";

        try
        {
            //ID = objRequest.GetQuotationID("consultant");
            objRequest.RequestId = ID;
            DataTable dt = objRequest.GetRequestHeaderInfo();

            objUserMaster.RequestID = ID;
            objUserMaster.ConsultantID = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            DataTable dtConsultantInfo1 = objUserMaster.GetConsultantBasicInfo();
            DataTable dtDealerInfo = objUserMaster.GetDealerInfo();
            ConsultantNotes = dt.Rows[0]["ConsultantNotes"].ToString();
            string MailTo = "";
            StringBuilder str;
            for (int i = 0; i < dtDealerInfo.Rows.Count; i++)
            {
                str = new StringBuilder();
                //if (i == 0)
                MailTo = dtDealerInfo.Rows[i]["Email"].ToString();
                if (!String.IsNullOrEmpty(Convert.ToString(dtDealerInfo.Rows[i]["SecondaryEmail"])))
                    MailTo += " ; " + Convert.ToString(dtDealerInfo.Rows[i]["SecondaryEmail"]);
                //else
                // MailTo += "," + dtDealerInfo.Rows[i][1].ToString();

                str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + dtDealerInfo.Rows[i]["Dealer Name"].ToString() + "<br /><br />Please be advised that we have a client ready to buy a new ");

                MakeModelSeries = dt.Rows[0]["Make"].ToString();

                if (dt.Rows[0]["Model"].ToString() != "")
                    MakeModelSeries += " " + dt.Rows[0]["Model"].ToString();

                //if (dt.Rows[0]["Series"].ToString() != "")
                //    MakeModelSeries += "," + dt.Rows[0]["Series"].ToString();
                //str.Append(MakeModelSeries + "</p>");

                str.Append(MakeModelSeries + " and are seeking pricing and availability.<br /><br />Would you please log in and complete the quote request at <a href='http://quotes.privatefleet.com.au/'>http://quotes.privatefleet.com.au</a><br /><br />");
                str.Append("If you have any queries, please contact me via email <a href='mailto:" + dtConsultantInfo1.Rows[1][1].ToString() + "'>" + dtConsultantInfo1.Rows[1][1].ToString() + "</a> or on (02) 9411 6777 ext " + dtConsultantInfo1.Rows[2][1].ToString());
                str.Append("<br /><br />Kind Regards<br /><br />" + dtConsultantInfo1.Rows[0][1].ToString());
                str.Append("<br /><a href='http://www.privatefleet.com.au/'>www.privatefleet.com.au</a><br /><br />");
                str.Append("Please note:  This system is in beta.  Please report any bugs, suggestions or other feedback to ");
                str.Append("<a href='mailto:davidlye@privatefleet.com.au'>davidlye@privatefleet.com.au</a></p>");

                #region Html Tabl
                /*  //Start Html Table 
        //Request Parameter & Header
        str.Append("<table width='95%' align='center' style='font: normal normal normal 12px Tahoma;'>");
        str.Append("<tr><td align='center' style='border:solid 1px #acacac; color:White; background-color:#0A73A2; font-weight:bold;'>Make,Model,Series</td></tr><tr>");
        str.Append("<td align='center' style='border:solid 1px #acacac;'>");
        str.Append(MakeModelSeries+"</td></tr>");
        str.Append("<tr style ='color:White; border:solid 1px #acacac; background-color :#0A73A2; font-weight :bold; Width:100%;'><td align='left' bgcolor=''>Request Parameter</td></tr>");
        str.Append("<tr><td align='right' style='border:solid 1px #acacac;'><table width='85%' style='font-size:13px'>");

        dt = null;
        dt = objRequest.GetRequestParameters();

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow drParam in dt.Rows)
            {
                  str.Append("<tr><td align='left' style ='padding-left :10px; width :50%; background-color :#eaeaea; font-weight :bold;' >");
                  str.Append(drParam["Parameter"].ToString() + "</td>");
                
                  if (drParam["ParamValue"].ToString() == "")
                      str.Append("<td style='padding-left :10px'  align='left'>-</td></tr>");
                  else
                      str.Append("<td style='padding-left :10px'  align='left'>" + drParam["ParamValue"].ToString() + "</td></tr>");
            }
        }
        str.Append(" </table> </td> </tr>");

          
        // Consultant Info
        str.Append("<tr style ='color:White; background-color:#0A73A2; font-weight:bold; Width:100%; border:solid 1px #acacac;'><td align='left'>Consultant</td></tr>");
        str.Append("<tr><td align='right' style='border:solid 1px #acacac;'><table width='85%' style='font-size:13px'>");
        
        for (int i = 0; i < dtConsultantInfo1.Rows.Count; i++)
        {
            str.Append("<tr><td align='left' style ='padding-left :10px; width :50%; background-color :#eaeaea; font-weight :bold;' >");
            str.Append(dtConsultantInfo1.Rows[i][0].ToString() + "</td>");
                
            str.Append("<td style='padding-left :10px'  align='left'>");
            str.Append(dtConsultantInfo1.Rows[i][1].ToString() + "</td></tr>");
        }
        str.Append(" </table> </td> </tr>");
       
         
        //Dealer Info 
        str.Append("<tr style ='color:White; background-color :#0A73A2; font-weight :bold; Width:100%; border:solid 1px #acacac;'><td align='left' bgcolor=''> Dealer</td> </tr>");
        str.Append(" <tr><td align='right' style='border: solid 1px #acacac;'><table style='width:85%; background-color:White; border:solid 1px #acacac; font-size:13px'>");
        
        str.Append("<tr align='center' style='background-color:#eaeaea; font-weight:bold; Color:Black'>");
        str.Append("<td>Dealer Name </td><td>Email </td><td>Phone </td></tr>");
         
        
        for(int i=0; i<dtDealerInfo.Rows.Count; i++)
        {
            str.Append("<tr align='center'><td>" + dtDealerInfo.Rows[i][0].ToString() + "</td>");
            str.Append("<td>" + dtDealerInfo.Rows[i][1].ToString() + "</td>");

            str.Append("<td>");   
            if(dtDealerInfo.Rows[i][2].ToString()=="")
                str.Append("-"); 
            else
                str.Append(dtDealerInfo.Rows[i][2].ToString()); 
            str.Append("</td></tr>");
       }
       str.Append("</table></td></tr>");

                
       //Accessories 
       str.Append(" <tr style ='color:White; background-color :#0A73A2; font-weight :bold; Width:100%; border:solid 1px #acacac;'><td align='left'> Accessories</td></tr>  ");

       dt = null;
       dt = objRequest.GetRequestAccessories();
       
       if(dt.Rows.Count==0)
       {
           str.Append("<tr><td style='font-size:13px; border:solid 1px #acacac;'>No Accessories</td></tr>");
       }
       else
       {
           str.Append("<tr><td><table style='width:100%; border:solid 1px #acacac; font-size:13px'><tr><td>Accessory</td><td>Specification</td></tr>");    
            for(int i=0; i<dt.Rows.Count; i++)
            {
                str.Append("<tr><td>" + dt.Rows[i][1] + "</td>");
                str.Append("<td>" + dt.Rows[i][2] + "</td></tr>");
            }
            str.Append("</table></td></tr>");
       }
      

       //Fixed Charges
       str.Append(" <tr style ='color:White; background-color :#0A73A2; font-weight :bold; Width:100%; border:solid 1px #acacac;'><td align='left'> Fixed Charges </td></tr> ");

       Cls_ChargeType objChargeType = new Cls_ChargeType();
       //get fixed charge types
       dt = objChargeType.GetAllChargeTypes();

       str.Append(" <tr><td><table align='left' style='width:100%; border-color:#acacac; border-style:solid ;  border-width :1px; font-size:13px'>");

       for (int i = 0; i < dt.Rows.Count; i++)
       {
           if (dt.Rows[i][2].ToString().ToLower()=="true")
           {
               str.Append(" <tr><td style='padding-left :8px'>" + dt.Rows[i][1].ToString() + "</td></tr>");
           }
       }
       str.Append("</table></td></tr>");

       //Consultant Notes 
       str.Append(" <tr style ='color:White; background-color :#0A73A2; font-weight :bold; Width:100%; font-size :13.7px; border:solid 1px #acacac;'><td align='left' bgcolor=''> Consultant notes</td></tr> ");
       str.Append("<tr style='border:solid 1px #acacac;'><td style='border: solid 1px #acacac;'>");
       str.Append(ConsultantNotes + "</td></tr></table>");
        */
                #endregion;

                //string link = ConfigurationManager.AppSettings["DummyPageUrl1"];
                //str.Append("<p style='font: normal normal normal 12px Tahoma;'><a href='" + link + "'>Click here</a> to Submit the Quotation</p> ");

                objEmailHelper.EmailBody = str.ToString();

                objEmailHelper.EmailToID = MailTo;
                objEmailHelper.EmailFromID = dtConsultantInfo1.Rows[1][1].ToString();
                // objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
                // objEmailHelper.EmailFromID = "manoj.mahagaonkar@mechsoftgroup.com";
                objEmailHelper.EmailSubject = "New Quote Request for " + MakeModelSeries + " from " + dtConsultantInfo1.Rows[0][1].ToString();

                logger.Debug("QR To -" + MailTo);
                logger.Debug("QR From -" + dtConsultantInfo1.Rows[1][1].ToString());

                if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
                    objEmailHelper.SendEmail();

                dtConsultantInfo1.Dispose();
                dtDealerInfo.Dispose();
                dt.Dispose();
            }

            #region---------------sending Mail To Consultant---------------------------------
            ///************************************Sending Mail To Consultatn*****************/
            str = new StringBuilder();
            DataTable dtDealerDetails = new DataTable();
            DataColumn dcName = new DataColumn("Dealer Name");
            DataColumn dcState = new DataColumn("State");
            DataColumn dcPOSTCODE = new DataColumn("Post Code");
            DataColumn dcMobile = new DataColumn("Phone");
            dtDealerDetails.Columns.Add(dcName);
            dtDealerDetails.Columns.Add(dcState);
            dtDealerDetails.Columns.Add(dcPOSTCODE);
            dtDealerDetails.Columns.Add(dcMobile);
            DataRow drDetails;

            foreach (DataRow drDealer in dtDealerInfo.Rows)
            {
                drDetails = dtDealerDetails.NewRow();
                drDetails["Dealer Name"] = drDealer["Dealer Name"];
                drDetails["State"] = drDealer["State"];
                drDetails["Post Code"] = drDealer["PostCode"];
                drDetails["Phone"] = drDealer["Phone"];
                dtDealerDetails.Rows.Add(drDetails);
            }

            StringBuilder strDealer = new StringBuilder();
            strDealer.Append(" " + "<br>");
            strDealer.Append("<table border=\"1\" style=\"font:10pt Verdana;border:solid 1px black;\"><tr><td> Dealer Name</td>" + "<td>Dealer State</td>" + "<td>Dealer Postal Code</td><td>Phone</td></tr></br>");
            foreach (DataRow dr1 in dtDealerDetails.Rows)
            {
                strDealer = strDealer.Append("<tr><td> '" + dr1[0].ToString() + "'</td><td>'" + dr1[1].ToString() + "'</td><td>'" + dr1[2].ToString() + "'</td><td>" + dr1[3].ToString() + "</td></tr></br>");
            }
            strDealer = strDealer.Append("</table>");

            DataRow dr = dtConsultantInfo1.Rows[1];
            MailTo = dtConsultantInfo1.Rows[1]["Details"].ToString();  //dr["Details"].ToString();
            dr = dtConsultantInfo1.Rows[0];
            string ConsultantName = dr["Details"].ToString();
            str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + ConsultantName + "<br /><br />This mail is to inform you the details of dealer to whom quotation request is sent for ");
            MakeModelSeries = dt.Rows[0]["Make"].ToString();

            if (dt.Rows[0]["Model"].ToString() != "")
                MakeModelSeries += " " + dt.Rows[0]["Model"].ToString();
            //str.Append(MakeModelSeries + " and are seeking pricing and availability.<br /><br />Would you please log in and complete the quote request at <a href='http://quotes.privatefleet.com.au/'>http://quotes.privatefleet.com.au</a><br /><br />");
            str.Append(MakeModelSeries + "<br /><br />Would you please log in to see the quote request at <a href='http://quotes.privatefleet.com.au/'>http://quotes.privatefleet.com.au</a><br /><br />");
            str.Append("<strong>Information of Dealers is as follows : </strong> " + strDealer);
            //str.Append("<br/>If you have any queries, please contact me via email <a href='mailto:" + dtConsultantInfo1.Rows[1][1].ToString() + "'>" + dtConsultantInfo1.Rows[1][1].ToString() + "</a> or on (02) 9411 6777 ext " + dtConsultantInfo1.Rows[2][1].ToString());
            str.Append("<br /><br />Kind Regards<br /><br />" + dtConsultantInfo1.Rows[0][1].ToString());
            str.Append("<br /><a href='http://www.privatefleet.com.au/'>www.privatefleet.com.au</a><br /><br />");
            str.Append("Please note:  This system is in beta.  Please report any bugs, suggestions or other feedback to ");
            str.Append("<a href='mailto:davidlye@privatefleet.com.au'>davidlye@privatefleet.com.au</a></p>");
            objEmailHelper.EmailBody = str.ToString();

            objEmailHelper.EmailToID = MailTo;
            //objEmailHelper.EmailFromID = dtConsultantInfo1.Rows[1][1].ToString();
            objEmailHelper.EmailFromID = ConfigurationManager.AppSettings["EmailFromID"];
            objEmailHelper.EmailSubject = "New Quote Request for " + MakeModelSeries + " from " + dtConsultantInfo1.Rows[0][1].ToString();

            logger.Debug("QR To -" + MailTo);
            logger.Debug("QR From -" + dtConsultantInfo1.Rows[1][1].ToString());

            if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
                objEmailHelper.SendEmail();
            #endregion

        }
        catch (Exception ex)
        {
            logger.Error("Error While sending QR mail - " + ex.Message);
        }
        finally
        {
            logger.Debug("Sending mail Ends for new Quote Request");
        }
    }

    #endregion

    #region "Methods"
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

    /// <summary>
    /// Method to clear constraints of a datatable
    /// </summary>
    /// <param name="dt"></param>
    public void ClearConstraintsOfDataTable(DataTable dt)
    {
        if (dt.Columns.Count > 0)
        {
            foreach (DataColumn DC in dt.Columns)
            {
                DC.AllowDBNull = true;
                DC.ReadOnly = false;
                //    DC.Unique = false;
                DC.Table.Constraints.Clear();
            }
        }
    }

    private bool ValidateDealerSelection()
    {
        logger.Debug("ValidateDealerSelection Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        ConfigValues objConfig = new ConfigValues();
        if (objConfig.GetValue(Cls_Constants.CHECK_DEALER_LIMIT) == "1")
        {
            int intNormalDealerLimit = Convert.ToInt32(objConfig.GetValue(Cls_Constants.NO_OF_NORMAL_DEALERS));
            int intHotDealerLimit = Convert.ToInt32(objConfig.GetValue(Cls_Constants.NO_OF_HOT_DEALERS));
            int intNormalDealerSelected = UcDealerSelection1.NoOfNormalDealers;
            int intHotDealerSelected = UcDealerSelection1.NoOfHotDealers;
            int intTotalDealerSelected = UcDealerSelection1.TotalSelectedDealer;
            int intNormalDealerAvailable = UcDealerSelection1.noOfNormalDealersInSearch;
            int intHotDealerAvailable = UcDealerSelection1.noOfHotDealersInSearch;
            int intTotalDealerAvailable = UcDealerSelection1.TotalSearchDealer;
            if (intTotalDealerSelected <= 0)
            {
                lblMsg.Text = "There are no dealers selected, Please select dealers to proceed!!!";
                return false;
            }
            else
            {
                if ((intHotDealerAvailable >= intHotDealerLimit) && (intHotDealerAvailable != 0))
                {
                    if (intHotDealerSelected < intHotDealerLimit)
                    {
                        lblMsg.Text = "Please select" + intHotDealerLimit + "hot dealers to proceed!!!";
                        return false;
                    }
                }

                if ((intNormalDealerAvailable >= intNormalDealerLimit) && (intNormalDealerAvailable != 0))
                {
                    if (intNormalDealerSelected < intNormalDealerLimit)
                    {
                        lblMsg.Text = "Please select" + intNormalDealerLimit + "normal dealers to proceed!!!";
                        return false;
                    }
                }
            }
        }
        logger.Debug("ValidateDealerSelection ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        return true;

    }

    private void CleanUp()
    {
        UcRequestParameters1.dtAccessories.Rows.Clear();
        UcRequestParameters1.dtParameters.Rows.Clear();
        ViewState["dtParameters"] = null;
    }
    #endregion

    #region "Create Request Methods"

    /// <summary>
    /// Method to fill make dropdown
    /// </summary>
    private void FillMake()
    {
        logger.Debug("FillMake Method Start");
        logger.Debug("Fill Make Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        Cls_MakeHelper objMake = new Cls_MakeHelper();
        try
        {
            //get all active make
            if (Cache["MAKES"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadMake();

            DataTable dt = Cache["MAKES"] as DataTable;

            if (dt != null)
            {
                //clear make dropdown
                ddlMake.Items.Clear();

                //fill make dropdown
                ddlMake.DataSource = dt;
                ddlMake.DataTextField = "Make";
                ddlMake.DataValueField = "id";
                ddlMake.DataBind();
            }

            //insert default item in make dropdown
            ddlMake.Items.Insert(0, new ListItem("-Select Make-", "0"));
        }
        catch (Exception ex)
        {
            logger.Error("Fill Make Error =" + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("Fill Make Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            logger.Debug("FillMake Method End");
        }
    }

    /// <summary>
    /// Method to fill models dropdown
    /// </summary>
    private void FillModels()
    {
        logger.Debug("FillModels Method Start");
        logger.Debug("Fill Models Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {
            if (Cache["MODELS"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadModel();

            DataTable dt = Cache["MODELS"] as DataTable;

            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format(@"MakeId={0}", ddlMake.SelectedValue);

            //clear models dropdown
            ddlModel.Items.Clear();

            if (dt != null)
            {
                //fill models dropdown
                ddlModel.DataSource = dv.ToTable();
                ddlModel.DataTextField = "Model";
                ddlModel.DataValueField = "id";
                ddlModel.DataBind();
            }

            //insert default item in models dropdown
            ddlModel.Items.Insert(0, new ListItem("-Select Model-", "0"));
        }
        catch (Exception ex)
        {
            logger.Error("Fill Models Error =" + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("Fill Models Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            logger.Debug("FillModels Method End");
        }
    }

    /// <summary>
    /// Method to fill series dropdown
    /// </summary>
    private void FillSeries()
    {
        logger.Debug("FillSeries Method Start");
        //Cls_SeriesMaster objSeries = new Cls_SeriesMaster();
        try
        {
            //get series
            //objSeries.ModelID = Convert.ToInt32(ddlModel.SelectedValue);
            //DataTable dt = objSeries.GetSeriesOfModel();

            if (Cache["SERIES"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadSeries();

            DataTable dt = Cache["SERIES"] as DataTable;

            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format(@"ModelId={0}", ddlModel.SelectedValue);

            //clear series dropdown
            ddlSeries.Items.Clear();

            if (dt != null)
            {
                //fill series dropdown
                ddlSeries.DataSource = dt;
                ddlSeries.DataTextField = "Series";
                ddlSeries.DataValueField = "id";
                ddlSeries.DataBind();
            }

            //insert default item in series dropdown
            ddlSeries.Items.Insert(0, new ListItem("-Select Series-", "-Select-"));

        }
        catch (Exception ex)
        {
            logger.Error("FillSeries Method Error - " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("FillSeries Method End");
        }
    }

    #endregion

    #region "Create Request Events"
    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Debug("DDL Make Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {
            //Fill models dropdown
            FillModels();
            ddlSeries.Items.Clear();

        }
        catch (Exception ex)
        {
            logger.Error("ddlMake_SelectedIndexChanged Event : " + ex.Message);
        }
        finally
        {
            logger.Debug("Ddl Male ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }
    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Fill series dropdown
            FillSeries();
        }
        catch (Exception ex)
        {
            logger.Error("ddlModel_SelectedIndexChanged Event : " + ex.Message);
        }
    }
    #endregion

    protected void btnSearchDealers_Click(object sender, ImageClickEventArgs e)
    {
        logger.Debug("Search Dealer Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        Cls_Dealer objDealer = new Cls_Dealer();
        DataTable dt = null;
        try
        {
            //Validation
            //if (ddlMake.SelectedValue == "-Select-")
            //{
            //    lblMsgOp.Text = "Please Select Make";
            //    mvQuoteRequest.SetActiveView(viewCreateRequest);
            //    return;
            //}

            #region search dealers in selected cities
            objDealer.PostalCode = txtPCode.Text;
            objDealer.MakeID = Convert.ToInt32(ddlMake.SelectedValue);

            logger.Debug("Get data from DB Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            dt = objDealer.SearchDealersForMakeInCities();
            logger.Debug("Get data from DB ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            #endregion

            UcDealerSelection1.ClearDataTable();
            UcDealerSelection1.Visible = true;
            UcDealerSelection1.dtDealers = dt;
            UcDealerSelection1.BindDealers(dt);


        }
        catch (Exception ex)
        {
            logger.Error("btnSearchDealers_Click Event : " + ex.Message);
        }
        finally
        {
            logger.Debug("Search Dealer Ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }

    protected void txtPCode_TextChanged(object sender, EventArgs e)
    {
        logger.Debug("PCode Change Start=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {
            ddlLocation.Items.Clear();
            ddlLocation.Items.Add("-Select Suburb-");
            GridView gvTemp = (GridView)UcDealerSelection1.FindControl("gvDealerDetails");
            gvTemp.DataSource = null;
            gvTemp.DataBind();


            lbtGetLocations_Click(sender, e);

        }
        catch (Exception ex)
        {
            logger.Error("PCode Change Error = " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("PCode Change end=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }

    protected void lbtGetLocations_Click(object sender, EventArgs e)
    {
        Cls_CustomerMaster objCustomer = new Cls_CustomerMaster();
        int PostalCode = 0;
        if (!(String.IsNullOrEmpty(txtPCode.Text)))
            PostalCode = Convert.ToInt32(txtPCode.Text);
        objCustomer.PostalCode = PostalCode.ToString();
        logger.Debug("Get suburb DB start=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        DataTable dtSuburbs = objCustomer.GetSuburbsOfThePostalCode();
        logger.Debug("Get suburb DB end=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));

        ddlLocation.Items.Clear();
        ddlLocation.DataSource = dtSuburbs;
        ddlLocation.DataValueField = "ID";
        ddlLocation.DataTextField = "Suburb";
        ddlLocation.DataBind();

        if (String.IsNullOrEmpty(txtPCode.Text))
        {
            ddlLocation.Items.Insert(0, new ListItem("- Please Enter Postal Code First -", "-Select-"));
            return;
        }
        if (ddlLocation.Items.Count == 0)
            ddlLocation.Items.Insert(0, new ListItem("- No Loaction Found -", "0"));
        else
            ddlLocation.Items.Insert(0, new ListItem("- Select Location -", "0"));
    }

    protected void imgbutPre_Click(object sender, ImageClickEventArgs e)
    {
        mvQuoteRequest.SetActiveView(viewDealerSelection);
    }

    protected void imgbutCreate_Click(object sender, ImageClickEventArgs e)
    {
        divOrderCancelConfirm.Style.Add("display", "block");
        // CreateRequest();
    }


    // on 3rd july 12 for adding more dealer to previous quote
    protected void imgbtnAddDealer_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtTemp_new = (DataTable)ViewState["dtQuoteInfo_New"];
        try
        {
            if (Convert.ToInt32(dtTemp_new.Rows[0]["IsQRSend"]) == 0)
            {
                ViewState["AddMoreDealer"] = "Add";
                divOrderCancelConfirm.Style.Add("display", "block");
            }
            else
            {
                addDealer();
                Response.Redirect("ViewSentRequests.aspx", true);
            }

        }
        catch (Exception ex)
        {
            logger.Error("imgbtnAddDealer_Click err -" + ex.Message);
        }
        finally
        {
            dtTemp_new = null;
        }
    }

    private int addDealer()
    {
        Cls_Request objRequest = new Cls_Request();

        try
        {
            objRequest.DealerIds = UcDealerSelection1.SelectedDealerIds;
            objRequest.RequestId = Convert.ToInt32(ViewState["RequestID"]);
            int result = objRequest.addMoreDealer();

            if (result > 0)
            {
                sendMailToAddDealer(Convert.ToInt32(ViewState["RequestID"]), UcDealerSelection1.SelectedDealerIds);

            }
            else
                lblMsg.Text = "Error while sending this quote request to another dealer. Please try again";
        }
        catch (Exception ex)
        {
            logger.Error("Add dealer err -" + ex.Message);
        }
        finally
        {
            objRequest = null;
        }
        return Convert.ToInt32(ViewState["RequestID"]);

    }

    protected void imgbtnSendCustomerMail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divOrderCancelConfirm.Style.Add("display", "none");
            int RequestID = 0;
            if (Convert.ToString(ViewState["AddMoreDealer"]) != "Add")
            {
                RequestID = CreateRequest();
            }
            else if (Convert.ToString(ViewState["AddMoreDealer"]) == "Add")
            {
                RequestID = addDealer();
            }
            sendEmailToCustomer(txtCustomerEmail.Text.Trim(), RequestID);
            Response.Redirect("ViewSentRequests.aspx", true);
        }
        catch (Exception ex)
        {

        }
    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            divOrderCancelConfirm.Style.Add("display", "none");
            int RequestID = 0;
            if (Convert.ToString(ViewState["AddMoreDealer"]) != "Add")
            {
                RequestID = CreateRequest();
            }
            else if (Convert.ToString(ViewState["AddMoreDealer"]) == "Add")
            {
                RequestID = addDealer();
            }
            Response.Redirect("ViewSentRequests.aspx", true);
        }
        catch (Exception ex)
        {

        }
    }

    //DataTable dt = objRequest.GetRequestHeaderInfo();
    private void sendEmailToCustomer(string CustEmail, int RequestID)
    {
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        Cls_Request objRequest = new Cls_Request();
        Cls_UserMaster objUserMaster = new Cls_UserMaster();
        string makeModelSeries = "";
        try
        {
            DataTable dt = new DataTable();
            objRequest.RequestId = RequestID;
            dt = objRequest.GetRequestHeaderInfo();

            objUserMaster.RequestID = RequestID;
            objUserMaster.ConsultantID = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            DataTable dtConsultantInfo1 = objUserMaster.GetConsultantBasicInfo();
            DataTable dtDealerInfo = objUserMaster.GetDealerInfo();
            string ConsultantNotes = dt.Rows[0]["ConsultantNotes"].ToString();


            makeModelSeries += Convert.ToString(ddlMake.SelectedItem).Trim();
            if (ddlModel.SelectedValue != "0")
                makeModelSeries += " - " + Convert.ToString(ddlModel.SelectedItem).Trim();
            if (!txtSeries.Text.Trim().Equals(String.Empty))
                makeModelSeries += " - " + txtSeries.Text.Trim();

            StringBuilder str = new StringBuilder();
            str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear Customer <br /><br />This email is to confirm that ");
            str.Append(Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]) + " has submitted the following quote request to multiple dealerships for pricing.");

            #region Html Tabl
            //Start Html Table 
            //Request Parameter & Header
            str.Append("<br/><br/><table width='95%' align='center' style='font: normal normal normal 12px Tahoma;'>");
            str.Append("<tr><td align='center' style='border:solid 1px #acacac; color:White; background-color:#0A73A2; font-weight:bold;'>Make,Model,Series</td></tr><tr>");
            str.Append("<td align='center' style='border:solid 1px #acacac; font: normal normal bold 18px Tahoma;'>");
            str.Append(makeModelSeries + "</td></tr>");
            str.Append("<tr style ='color:White; border:solid 1px #acacac; background-color :#0A73A2; font-weight :bold; Width:100%;'><td align='left' bgcolor=''>Request Parameter</td></tr>");
            str.Append("<tr><td align='right' style='border:solid 1px #acacac;'><table width='85%' style='font-size:13px'>");

            dt = null;
            dt = objRequest.GetRequestParameters();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow drParam in dt.Rows)
                {
                    str.Append("<tr><td align='left' style ='padding-left :10px; width :50%; background-color :#eaeaea; font-weight :bold;' >");
                    str.Append(drParam["Parameter"].ToString() + "</td>");

                    if (drParam["ParamValue"].ToString() == "")
                        str.Append("<td style='padding-left :10px'  align='left'>-</td></tr>");
                    else
                        str.Append("<td style='padding-left :10px'  align='left'>" + drParam["ParamValue"].ToString() + "</td></tr>");
                }
            }
            str.Append(" </table> </td> </tr>");

            //Suburb
            str.Append("<tr> <td>");
            str.Append("<table width='100%' align='center' style='font: normal normal normal 12px Tahoma; border:solid 1px #acacac;'>");
            str.Append("<tr><td width='20%'>Suburb :</td><td width='30%'>" + Convert.ToString(ddlLocation.SelectedItem).Trim() + "</td>");
            str.Append("<td width='20%'>Postal Code :</td><td width='30%'>" + Convert.ToString(txtPCode.Text.Trim()) + "</td>");
            str.Append("</tr></table> </td> </tr>");

            // Consultant Info
            str.Append("<tr style ='color:White; background-color:#0A73A2; font-weight:bold; Width:100%; border:solid 1px #acacac;'><td align='left'>Consultant</td></tr>");
            str.Append("<tr><td align='right' style='border:solid 1px #acacac;'><table width='85%' style='font-size:13px'>");

            for (int i = 0; i < dtConsultantInfo1.Rows.Count; i++)
            {
                str.Append("<tr><td align='left' style ='padding-left :10px; width :50%; background-color :#eaeaea; font-weight :bold;' >");
                str.Append(dtConsultantInfo1.Rows[i][0].ToString() + "</td>");

                str.Append("<td style='padding-left :10px'  align='left'>");
                str.Append(dtConsultantInfo1.Rows[i][1].ToString() + "</td></tr>");
            }
            str.Append(" </table> </td> </tr>");

            //Accessories 
            str.Append(" <tr style ='color:White; background-color :#0A73A2; font-weight :bold; Width:100%; border:solid 1px #acacac;'><td align='left'> Accessories</td></tr>  ");

            dt = null;
            dt = objRequest.GetRequestAccessories();

            if (dt.Rows.Count == 0)
            {
                str.Append("<tr><td style='font-size:13px; border:solid 1px #acacac;'>No Accessories</td></tr>");
            }
            else
            {
                // str.Append("<tr><td><table style='width:100%; border:solid 1px #acacac; font-size:13px'><tr><td>Accessory</td><td>Specification</td></tr>");

                str.Append("<tr><td><table style='width:100%; border:solid 1px #acacac; font-size:13px'><tr><td>Private Fleet Member Pack (inc free roadside assistance)</td><td></td></tr>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str.Append("<tr><td>" + dt.Rows[i][1] + "</td>");
                    str.Append("<td>" + dt.Rows[i][2] + "</td></tr>");
                }
                str.Append("</table></td></tr>");
            }


            //Fixed Charges
            str.Append(" <tr style ='color:White; background-color :#0A73A2; font-weight :bold; Width:100%; border:solid 1px #acacac;'><td align='left'> Fixed Charges </td></tr> ");

            Cls_ChargeType objChargeType = new Cls_ChargeType();
            //get fixed charge types
            dt = objChargeType.GetAllChargeTypes();

            str.Append(" <tr><td><table align='left' style='width:100%; border-color:#acacac; border-style:solid ;  border-width :1px; font-size:13px'>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString().ToLower() == "true")
                {
                    str.Append(" <tr><td style='padding-left :8px'>" + dt.Rows[i][1].ToString() + "</td></tr>");
                }
            }
            str.Append("</table></td></tr></table>");

            ////Consultant Notes 
            //str.Append(" <tr style ='color:White; background-color :#0A73A2; font-weight :bold; Width:100%; font-size :13.7px; border:solid 1px #acacac;'><td align='left' bgcolor=''> Consultant notes</td></tr> ");
            //str.Append("<tr style='border:solid 1px #acacac;'><td style='border: solid 1px #acacac;'>");
            //str.Append(ConsultantNotes + "</td></tr></table>");

            #endregion

            #region Comment on 24 July 12
            //str.Append("<br/><br/>" + makeModelSeries);

            //DataTable dt = (DataTable)ViewState["ReqParams_New"];
            //DataTable dt1 = (DataTable)ViewState["AddAcc_New"];

            //str.Append("<br/><br/>Body Type: " + Convert.ToString(dt.Rows[0]["Details"]));
            //str.Append("<br/>Registration Type: " + Convert.ToString(dt.Rows[1]["Details"]));
            //str.Append("<br/>Transmission: " + Convert.ToString(dt.Rows[2]["Details"]));
            //str.Append("<br/>Colour: " + Convert.ToString(dt.Rows[3]["Details"]));
            //str.Append("<br/>Fuel Type: " + Convert.ToString(dt.Rows[4]["Details"]));
            //str.Append("<br/><br/>With options being<br/>");
            //for (int i = 0; i < dt1.Rows.Count; i++)
            //{
            //    str.Append("<br/>" + Convert.ToString(dt1.Rows[i]["Header"]) + " - " + Convert.ToString(dt1.Rows[i]["Details"]));
            //}

            //str.Append("<br/><br/>For delivery to " + Convert.ToString(ddlLocation.SelectedItem).Trim() + " (" + txtPCode.Text.Trim() + ")");
            #endregion

            str.Append("<br/><br/>This information is just for your reference.  You do not need to do anything further at this point. However if there is something that is not to your requirements, please contact your consultant, ");
            string ext = Convert.ToString(Session[Cls_Constants.PHONE]);
            if (!ext.Equals(String.Empty) && ext.Contains("ext"))
            {
                ext = ext.Substring(ext.IndexOf("ext") + 4);
                ext = "ext " + ext;
            }
            str.Append(Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]) + " asap on 1300 303 181 " + ext);
            str.Append(" or by email on <a href='mailto:" + Convert.ToString(Session[Cls_Constants.FromEmailID]) + "'>" + Convert.ToString(Session[Cls_Constants.FromEmailID]) + "</a>");
            str.Append("<br/><br/>Best Regards");
            str.Append("<br/><br/>Private Fleet");
            str.Append("<br/>1300 303 181");
            str.Append("<br/><a href='http://www.privatefleet.com.au/'>www.privatefleet.com.au</a></p>");

            objEmailHelper.EmailBody = str.ToString();

            objEmailHelper.EmailToID = CustEmail;
            //*Commented On: 10 Sept 2014, By: Ayyaj, Desc:Catherine To Consultant*/
            //objEmailHelper.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
            objEmailHelper.EmailFromID = "Private Fleet Tender Dept <" + Convert.ToString(Session[Cls_Constants.FromEmailID]) + ">"; ;
            // objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
            objEmailHelper.EmailSubject = "Private Fleet Tender Update";

            logger.Debug("QR To -" + objEmailHelper.EmailToID);
            logger.Debug("QR From -" + objEmailHelper.EmailFromID);

            if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
            {
                objEmailHelper.SendEmail();
                objRequest.CustomerEmail = CustEmail;
                objRequest.RequestId = RequestID;
                int res1 = objRequest.UpdateCustomerEmail();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Send QR temp. to customer err - " + ex.Message + " :: " + ex.StackTrace);
        }
        finally
        {


        }
    }

    public void sendMailToAddDealer(int ID, String DealerIDs)
    {
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        Cls_UserMaster objUserMaster = new Cls_UserMaster();
        Cls_Request objRequest = new Cls_Request();
        string ConsultantNotes = "-", MakeModelSeries = "";

        try
        {
            //ID = objRequest.GetQuotationID("consultant");
            objRequest.RequestId = ID;
            DataTable dt = objRequest.GetRequestHeaderInfo();

            objUserMaster.RequestID = ID;
            objUserMaster.ConsultantID = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            DataTable dtConsultantInfo1 = objUserMaster.GetConsultantBasicInfo();
            DataTable dtDealerInfo = objUserMaster.GetDealerInfo();
            ConsultantNotes = dt.Rows[0]["ConsultantNotes"].ToString();
            string MailTo = "";
            StringBuilder str;

            for (int i = 0; i < dtDealerInfo.Rows.Count; i++)
            {
                if (!DealerIDs.Contains(Convert.ToString(dtDealerInfo.Rows[i]["DealerID"])))
                {
                    continue;
                }
                str = new StringBuilder();
                MailTo = dtDealerInfo.Rows[i]["Email"].ToString();
                if (!String.IsNullOrEmpty(Convert.ToString(dtDealerInfo.Rows[i]["SecondaryEmail"])))
                    MailTo += " ; " + Convert.ToString(dtDealerInfo.Rows[i]["SecondaryEmail"]);

                str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + dtDealerInfo.Rows[i]["Dealer Name"].ToString() + "<br /><br />Please be advised that we have a client ready to buy a new ");

                MakeModelSeries = dt.Rows[0]["Make"].ToString();

                if (dt.Rows[0]["Model"].ToString() != "")
                    MakeModelSeries += " " + dt.Rows[0]["Model"].ToString();

                str.Append(MakeModelSeries + " and are seeking pricing and availability.<br /><br />Would you please log in and complete the quote request at <a href='http://quotes.privatefleet.com.au/'>http://quotes.privatefleet.com.au</a><br /><br />");
                str.Append("If you have any queries, please contact me via email <a href='mailto:" + dtConsultantInfo1.Rows[1][1].ToString() + "'>" + dtConsultantInfo1.Rows[1][1].ToString() + "</a> or on (02) 9411 6777 ext " + dtConsultantInfo1.Rows[2][1].ToString());
                str.Append("<br /><br />Kind Regards<br /><br />" + dtConsultantInfo1.Rows[0][1].ToString());
                str.Append("<br /><a href='http://www.privatefleet.com.au/'>www.privatefleet.com.au</a><br /><br />");
                str.Append("Please note:  This system is in beta.  Please report any bugs, suggestions or other feedback to ");
                str.Append("<a href='mailto:davidlye@privatefleet.com.au'>davidlye@privatefleet.com.au</a></p>");

                objEmailHelper.EmailBody = str.ToString();

                objEmailHelper.EmailToID = MailTo;
                objEmailHelper.EmailFromID = dtConsultantInfo1.Rows[1][1].ToString();
                // objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
                objEmailHelper.EmailSubject = "New Quote Request for " + MakeModelSeries + " from " + dtConsultantInfo1.Rows[0][1].ToString();

                logger.Debug("QR To -" + MailTo);
                logger.Debug("QR From -" + dtConsultantInfo1.Rows[1][1].ToString());

                if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
                    objEmailHelper.SendEmail();

                dtConsultantInfo1.Dispose();
                dtDealerInfo.Dispose();
                dt.Dispose();
            }

            #region---------------sending Mail To Consultant---------------------------------
            ///************************************Sending Mail To Consultatn*****************/
            str = new StringBuilder();
            DataTable dtDealerDetails = new DataTable();
            DataColumn dcName = new DataColumn("Dealer Name");
            DataColumn dcState = new DataColumn("State");
            DataColumn dcPOSTCODE = new DataColumn("Post Code");
            DataColumn dcMobile = new DataColumn("Phone");
            dtDealerDetails.Columns.Add(dcName);
            dtDealerDetails.Columns.Add(dcState);
            dtDealerDetails.Columns.Add(dcPOSTCODE);
            dtDealerDetails.Columns.Add(dcMobile);
            DataRow drDetails;

            foreach (DataRow drDealer in dtDealerInfo.Rows)
            {
                if (!DealerIDs.Contains(Convert.ToString(drDealer["DealerID"])))
                {
                    continue;
                }
                drDetails = dtDealerDetails.NewRow();
                drDetails["Dealer Name"] = drDealer["Dealer Name"];
                drDetails["State"] = drDealer["State"];
                drDetails["Post Code"] = drDealer["PostCode"];
                drDetails["Phone"] = drDealer["Phone"];
                dtDealerDetails.Rows.Add(drDetails);
            }

            StringBuilder strDealer = new StringBuilder();
            strDealer.Append(" " + "<br>");
            strDealer.Append("<table border=\"1\" style=\"font:10pt Verdana;border:solid 1px black;\"><tr><td> Dealer Name</td>" + "<td>Dealer State</td>" + "<td>Dealer Postal Code</td><td>Phone</td></tr></br>");
            foreach (DataRow dr1 in dtDealerDetails.Rows)
            {
                strDealer = strDealer.Append("<tr><td> '" + dr1[0].ToString() + "'</td><td>'" + dr1[1].ToString() + "'</td><td>'" + dr1[2].ToString() + "'</td><td>" + dr1[3].ToString() + "</td></tr></br>");
            }
            strDealer = strDealer.Append("</table>");

            DataRow dr = dtConsultantInfo1.Rows[1];
            MailTo = dtConsultantInfo1.Rows[1]["Details"].ToString();  //dr["Details"].ToString();
            dr = dtConsultantInfo1.Rows[0];
            string ConsultantName = dr["Details"].ToString();
            str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + ConsultantName + "<br /><br />This mail is to inform you the details of dealer to whom quotation request is sent for ");
            MakeModelSeries = dt.Rows[0]["Make"].ToString();

            if (dt.Rows[0]["Model"].ToString() != "")
                MakeModelSeries += " " + dt.Rows[0]["Model"].ToString();

            str.Append(MakeModelSeries + "<br /><br />Would you please log in to see the quote request at <a href='http://quotes.privatefleet.com.au/'>http://quotes.privatefleet.com.au</a><br /><br />");
            str.Append("<strong>Information of Dealers is as follows : </strong> " + strDealer);
            str.Append("<br /><br />Kind Regards<br /><br />" + dtConsultantInfo1.Rows[0][1].ToString());
            str.Append("<br /><a href='http://www.privatefleet.com.au/'>www.privatefleet.com.au</a><br /><br />");
            str.Append("Please note:  This system is in beta.  Please report any bugs, suggestions or other feedback to ");
            str.Append("<a href='mailto:davidlye@privatefleet.com.au'>davidlye@privatefleet.com.au</a></p>");
            objEmailHelper.EmailBody = str.ToString();

            objEmailHelper.EmailToID = MailTo;
            objEmailHelper.EmailFromID = ConfigurationManager.AppSettings["EmailFromID"];
            objEmailHelper.EmailSubject = "New Quote Request for " + MakeModelSeries + " from " + dtConsultantInfo1.Rows[0][1].ToString();
            // objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";

            logger.Debug("QR To -" + MailTo);
            logger.Debug("QR From -" + dtConsultantInfo1.Rows[1][1].ToString());

            if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
                objEmailHelper.SendEmail();
            #endregion
        }
        catch (Exception ex)
        { }
    }
    //end
}
