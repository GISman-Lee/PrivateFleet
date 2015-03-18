using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Text;
using System.Data;
using Mechsoft.GeneralUtilities;
using System.Xml;

public partial class QuoteRequest_3 : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(QuoteRequest_3));
    DataTable dtParameters;
    DataTable dtAccessories;

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayRequestHeader();
        }
    }
    #endregion

    #region Methods
    public void DisplayRequestHeader()
    {
        logger.Error("DisplayRequestHeader Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        Cls_Request objRequest = new Cls_Request();
        try
        {
            string strMake = "";
            string strModel = "";
            //string strSeries = "";

            DataList1.RepeatColumns = 1;

            strMake = Convert.ToString(Convert.ToString(Session["Make_Model_Series"]).Split('^')[1]);

            if (Convert.ToString(Session["Make_Model_Series"]).Split('^')[2] != "0")
                strModel = Convert.ToString(Convert.ToString(Session["Make_Model_Series"]).Split('^')[3]);

            //strSeries = dtReqHeader.Rows[0]["Series"].ToString();

            StringBuilder MakeModelSeries = new StringBuilder();
            MakeModelSeries.Append(strMake);
            if (strModel != "")
            {
                MakeModelSeries.Append("," + strModel);
            }
            if (!Convert.ToString(Session["Make_Model_Series"]).Split('^')[4].Equals(String.Empty))
            {
                MakeModelSeries.Append("," + Convert.ToString(Session["Make_Model_Series"]).Split('^')[4].Trim());
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
            foreach (DataRow gr1 in ((DataTable)Session["dtParameters"]).Rows)
            {
                //Label lbl1 = (Label)gr1.FindControl("lblAccessory");
                //TextBox txt1 = (TextBox)gr1.FindControl("txtValue" + cnt);
                dRow1 = dt1.NewRow();
                dRow1["Header"] = Convert.ToString(gr1["AccessoryName"]);
                if (Convert.ToString(gr1["Specification"]) == "")
                {
                    dRow1["Details"] = "-";
                }
                else
                {
                    dRow1["Details"] = Convert.ToString(gr1["Specification"]);
                }
                //dRow1["Details"] = drParam["ParamValue"].ToString();
                dt1.Rows.Add(dRow1);
                cnt++;
            }


            DataList2.DataSource = dt1;
            DataList2.DataBind();


            if (Session["SELECT_ACC"] != null && ((DataTable)Session["SELECT_ACC"]).Rows.Count > 0)
            {
                trAddAcc.Visible = true;

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("Header");
                dt2.Columns.Add("Details");

                DataRow dRow2 = null;

                DataList3.RepeatColumns = 1;
                cnt = 1;
                foreach (DataRow gr1 in ((DataTable)Session["SELECT_ACC"]).Rows)
                {
                    //Label lbl1 = (Label)gr1.FindControl("lblAccessory");
                    //TextBox txt1 = (TextBox)gr1.FindControl("txtSpec");
                    dRow2 = dt2.NewRow();
                    dRow2["Header"] = Convert.ToString(gr1["AccessoryName"]);
                    if (Convert.ToString(gr1["Specification"]) == "")
                    {
                        dRow2["Details"] = "-";
                    }
                    else
                    {
                        dRow2["Details"] = Convert.ToString(gr1["Specification"]);
                    }
                    //dRow1["Details"] = drParam["ParamValue"].ToString();
                    dt2.Rows.Add(dRow2);
                    cnt++;
                }


                DataList3.DataSource = dt2;
                DataList3.DataBind();
            }
            cnt = 0;
            if (Session["chkBox"] != null)
            {
                if (Convert.ToString(Session["chkBox"]).Split(',')[0] != "0")
                {
                    trConsNotes.Visible = true;
                    cnt++;
                    lit.Text = "<b><span style='text-decoration:blink; color:red;'>Order Taken</span></b>";
                }
                if (Convert.ToString(Session["chkBox"]).Split(',')[1] != "0")
                {
                    trConsNotes.Visible = true;
                    if (cnt == 0)
                        lit.Text = "Some flexibility depending on delivery.Urgently required";
                    else
                        lit.Text += "<br/>Some flexibility depending on delivery.Urgently required";
                    cnt++;

                }
                if (Convert.ToString(Session["chkBox"]).Split(',')[2] != "0")
                {
                    trConsNotes.Visible = true;
                    if (cnt == 0)
                        lit.Text = "Must be 2011 Build & Complied";
                    else
                        lit.Text += "<br/>Must be " + System.DateTime.Now.Date.Year + " Build & Complied";
                    cnt++;
                }

            }
            if (!Convert.ToString(Session["ConsultantNotes"]).Equals(String.Empty))
            {
                trConsNotes.Visible = true;
                if (cnt == 0)
                    lit.Text = Convert.ToString(Session["ConsultantNotes"]);
                else
                    lit.Text += "<br/>" + Convert.ToString(Session["ConsultantNotes"]);
            }

            gvSelectedDealers1.DataSource = (DataTable)Session["DEALER_SELECTED"];
            gvSelectedDealers1.DataBind();

        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            logger.Error("DisplayRequestHeader ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }

    private void CreateRequest()
    {
        logger.Error("CreateRequest Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        Cls_Request objRequest = new Cls_Request();

        try
        {
         
            if (Session["Make_Model_Series"] == null || Session["ConsultantNotes"] == null || Session["chkBox"] == null || Session["dtParameters"] == null|| Session["SELECT_ACC"]==null || Session["PCode_Suburb"] == null || Session["DEALER_SELECTED"] == null)
            {
                //string strTemp = "<script type='text/javascript'>alert('Error in Quotation Please try again')</script>";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "xx", "<script type='text/javascript'>alert('Error in Quotation Please try again');</script>", false);
                RemoveSessions();
                Response.Redirect("QuoteRequest_1.aspx");
            }

            dtParameters = null;
            dtAccessories = null;
            // ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Hi')", true);
            dtParameters = (DataTable)Session["dtParameters"];
            dtAccessories = (DataTable)Session["SELECT_ACC"];
            DataRow Suburb = dtParameters.NewRow();
            Suburb["ID"] = "5";
            Suburb["AccessoryName"] = "Deliver To";
            if (Convert.ToString(Session["PCode_Suburb"]).Split('^')[2] == "-Select Suburb-" || Convert.ToString(Session["PCode_Suburb"]).Split('^')[2] == "- Select Location -")
            {
                Suburb["Specification"] = "";
            }
            else
            {
                Suburb["Specification"] = Convert.ToString(Session["PCode_Suburb"]).Split('^')[2];
            }
            Suburb["IsDBDriven"] = "True";
            dtParameters.Rows.Add(Suburb);
            //save quote request details to database

            //Suburb id
            //objRequest.SuburbID = Convert.ToInt32(ddlLocation.SelectedValue.ToString());
            if (Convert.ToString(Session["PCode_Suburb"]).Split('^')[2] == "- Select Location -" || Convert.ToString(Session["PCode_Suburb"]).Split('^')[2] == "-Select Suburb-")
                objRequest.SuburbID = 0;
            else
                objRequest.SuburbID = Convert.ToInt32(Convert.ToString(Session["PCode_Suburb"]).Split('^')[1]);


            //MakedID
            objRequest.ID = Convert.ToInt32(Convert.ToString(Session["Make_Model_Series"]).Split('^')[0]);

            if (Convert.ToInt32(Convert.ToString(Session["Make_Model_Series"]).Split('^')[2]) > 0)
                objRequest.ModelID = Convert.ToInt32(Convert.ToString(Session["Make_Model_Series"]).Split('^')[2]);

            //if (ddlSeries.SelectedIndex > 0)
            //    objRequest.SeriesId = Convert.ToInt32(ddlSeries.SelectedValue);

            objRequest.ConsultantId = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            objRequest.ConsultantNotes = Convert.ToString(Session["ConsultantNotes"]);
            string selectedDealerIds = "";
            foreach (DataRow dr in ((DataTable)Session["DEALER_SELECTED"]).Rows)
                selectedDealerIds += dr["ID"].ToString() + ",";
            objRequest.DealerIds = selectedDealerIds;

            //by manoj on 8 apr 11
            objRequest.Series = Convert.ToString(Session["Make_Model_Series"]).Split('^')[4].Trim();
            objRequest.OrderTaken = Convert.ToInt32(Convert.ToString(Session["chkBox"]).Split(',')[0]);
            objRequest.Urgent = Convert.ToInt32(Convert.ToString(Session["chkBox"]).Split(',')[1]);
            objRequest.BuildYear = Convert.ToInt32(Convert.ToString(Session["chkBox"]).Split(',')[2]);
            //end

            ClearConstraintsOfDataTable(dtParameters);
            ClearConstraintsOfDataTable(dtAccessories);

            //xml document for additional accessories and parameters
            if (dtAccessories.Rows.Count > 0)
                dtAccessories.Merge(dtParameters);
            else
                dtAccessories.Merge(dtParameters);

            objRequest.XmlDocument = ConvertDataTableToXML(dtAccessories).InnerXml;
            objRequest.PCode = Convert.ToString(Session["PCode_Suburb"]).Split('^')[0].Trim();
            //save request
            logger.Error("Save to DB Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            int result = objRequest.SaveQuoteRequest();
            logger.Error("Save to DB Ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            if (result > 0)
            {
                CleanUp();
                logger.Error("Email Sending Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                sendMail(result);
                logger.Error("Email Sending ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                Response.Redirect("ViewSentRequests.aspx", true);
            }
            else
                lblMsg.Text = "Error in saving Quote Request.. Please try again.";

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in saving Quote Request.. Please try again.";
            logger.Error("btnCreateRequest_Click Event : " + ex.Message);
        }
        finally
        {
            logger.Error("CreateRequest Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            objRequest = null;
            RemoveSessions();
        }
    }

    public void RemoveSessions()
    {
        //remove session of QR_1
        Session.Remove("Make_Model_Series");
        Session.Remove("ConsultantNotes");
        Session.Remove("chkBox");
        Session.Remove("dtParameters");
        Session.Remove("SELECT_ACC");
       // Session.Remove("dtAccessories");

        //remove Session of QR8_2
        Session.Remove("PCode_Suburb");
        Session.Remove("dtAllDealers");
        Session.Remove("DEALER_SELECTED");
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

    private void CleanUp()
    {
        dtAccessories.Rows.Clear();
        dtParameters.Rows.Clear();
    }

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

            for (int i = 0; i < dtDealerInfo.Rows.Count; i++)
            {
                StringBuilder str = new StringBuilder();
                //if (i == 0)
                MailTo = dtDealerInfo.Rows[i]["Email"].ToString();
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
                #endregion

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

        }
        catch (Exception ex)
        {
            logger.Debug("Error While sending QR mail - " + ex.Message);
        }
        finally
        {
            logger.Debug("Sending mail Ends for new Quote Request");
        }


    }
    #endregion

    #region Button Events
    protected void imgbutPre_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("QuoteRequest_2.aspx?from=3");
    }
    protected void imgbutCreate_Click(object sender, ImageClickEventArgs e)
    {
        CreateRequest();
    }
    #endregion

}
