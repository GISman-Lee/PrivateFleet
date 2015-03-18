using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mechsoft.GeneralUtilities;
using log4net;
using System.Text;
using System.Web.Mail;


public partial class PrintPageQuoteComparison : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(PrintPageQuoteComparison));
    int RowCounter = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {


                if (Request.QueryString["ReqID"] != null && Request.QueryString["ReqID"] != string.Empty)
                {
                    int RequestId = Convert.ToInt32(Request.QueryString["ReqID"]);

                    ViewState["RequestID"] = RequestId;

                    UcRequestHeader1.DisplayRequestHeader(RequestId);

                    Cls_UserMaster objUserMaster = new Cls_UserMaster();
                    objUserMaster.RequestID = RequestId;
                    DataTable dtDealerInfo = objUserMaster.GetDealerInfo();
                    gvDealerInfo.DataSource = dtDealerInfo;
                    gvDealerInfo.DataBind();

                    ViewState["DealerInfo1"] = dtDealerInfo;
                }
            }
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("Page load event : " + ex.Message);
        }
    }
    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }

    #region "Methods"

    private void BindData()
    {
        logger.Debug("Quote Comparison Data Bind Starts");
        DataTable dtQuotes = new DataTable();
        DataTable dt = null;
        DataRow row = null;

        Cls_QuoteComparison objCompare = new Cls_QuoteComparison();
        Cls_Request objRequest = new Cls_Request();
        Cls_ChargeType objCharge = new Cls_ChargeType();

        try
        {
            //clear gridview columns
            gvMakeDetails.Columns.Clear();

            //if request id is not null 
            if (ViewState["RequestID"] != null)
            {
                #region "Actual Data Fetching Logic"

                dtQuotes.Columns.Add("Key");
                dtQuotes.Columns.Add("Description");

                //get request dealers and options
                objCompare.RequestId = Convert.ToInt32(ViewState["RequestID"]);

                DataTable dtDealers = objCompare.GetDealersAndOptions();
                ViewState["dtDealers"] = dtDealers;

                if (dtDealers.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDealers.Rows.Count; i++)
                    {
                        string strColumn = dtDealers.Rows[i]["DealerName"].ToString().Trim() + "-" + dtDealers.Rows[i]["OptionID"].ToString().Trim();
                        dtQuotes.Columns.Add(strColumn);
                    }
                }


                //get quote comparison information for fixed charges
                if (Session[Cls_Constants.LOGGED_IN_USERID] != null)
                    objCompare.ConsultantId = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
                else
                {
                    Response.Redirect("index.aspx");
                }

                ////
                DataSet ds = objCompare.GetQuoteComparisonInfo();
                ViewState["dsQuoteCompare"] = ds;

                #region "Additional Accessories"
                logger.Debug("Data bind for Additional Accessories Starts");
                row = dtQuotes.NewRow();
                row["Key"] = -9999;
                row["Description"] = "Additional Accessories";

                dtQuotes.Rows.Add(row);

                //get request additional accessories
                objRequest.RequestId = Convert.ToInt32(ViewState["RequestID"]);
                DataTable dtAccessories = objRequest.GetRequestAccessories();

                if (dtAccessories.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAccessories.Rows)
                    {
                        row = dtQuotes.NewRow();
                        row["Key"] = 0 - Convert.ToInt64(dr["ID"]);
                        row["Description"] = Convert.ToString(dr["AccessoryName"]);
                        dtQuotes.Rows.Add(row);
                    }
                }

                //by manoj 12 mar 2011 for dealer acc
                dt = ds.Tables[3];
                int j = 0;
                foreach (DataRow drow in dt.Rows)
                {

                    objRequest.QuotationID = Convert.ToInt32(drow["ID"]);

                    DataTable dtAccessoriesD = objRequest.GetAccessoriesForDealer();

                    if (dtAccessoriesD.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtAccessoriesD.Rows)
                        {
                            row = dtQuotes.NewRow();
                            string id = Convert.ToString(dr["ID"]);
                            row["Key"] = 0 - Convert.ToInt64(id);
                            // row["Key"] = 0 - Convert.ToInt32(dr["ID"]);
                            row["Description"] = dr["AccessoryName"].ToString();
                            dtQuotes.Rows.Add(row);

                        }
                    }
                }

                //for total cost of accessories
                double sum = 0;
                string colName = "";
                DataRow rowTemp = dtQuotes.NewRow();
                rowTemp["Key"] = 0 - 9;
                rowTemp["Description"] = "Total Cost of Accessories";
                dtQuotes.Rows.Add(rowTemp);

                //assign value 0 to all dealers by default
                if (dtDealers.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDealers.Rows.Count; i++)
                    {
                        string strColumn = dtDealers.Rows[i]["DealerName"].ToString().Trim() + "-" + dtDealers.Rows[i]["OptionID"].ToString().Trim();

                        dtQuotes.PrimaryKey = new DataColumn[] { dtQuotes.Columns["Key"] };
                        DataRow dr12 = dtQuotes.Rows.Find(-9);
                        if (dr12 != null)
                        {
                            dr12.BeginEdit();
                            if (!strColumn.Equals(String.Empty))
                            {
                                string strQutval = String.Format("{0:c}", Convert.ToDouble(sum));
                                strQutval = strQutval.Substring(1, strQutval.Length - 1);
                                strQutval = strQutval.Replace(",", "");
                                dr12[strColumn] = strQutval;
                            }
                            dr12.EndEdit();

                        }
                    }
                }
                //end


                //get quote comparison information for additional accessories
                // Cahnge by manoj on 12 mar 2011 for dealer acc

                if (ds.Tables[1] != null)
                {
                    for (int i = 1; i < ds.Tables.Count - 1; i++)
                    {
                        dt = ds.Tables[i];

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow drow in dt.Rows)
                            {
                                #region Update values in dtQuotes datatable
                                string strColumnName = drow["DealerName"].ToString().Trim() + "-" + drow["OptionID"].ToString().Trim();

                                //by manoj for tot acc cost
                                if (colName != strColumnName)
                                {
                                    dtQuotes.PrimaryKey = new DataColumn[] { dtQuotes.Columns["Key"] };
                                    DataRow drTemp12 = dtQuotes.Rows.Find(-9);
                                    if (drTemp12 != null)
                                    {
                                        drTemp12.BeginEdit();
                                        if (!colName.Equals(String.Empty))
                                        {
                                            string strQutval = String.Format("{0:c}", Convert.ToDouble(sum));
                                            strQutval = strQutval.Substring(1, strQutval.Length - 1);
                                            strQutval = strQutval.Replace(",", "");
                                            drTemp12[colName] = strQutval;
                                        }


                                        drTemp12.EndEdit();

                                        if (Convert.ToString(drTemp12[strColumnName]).Equals(String.Empty))
                                            sum = 0;
                                        else
                                            sum = Convert.ToDouble(drTemp12[strColumnName]);
                                    }
                                    colName = strColumnName;
                                }
                                //end

                                Int64 intId = 0 - Convert.ToInt64(drow["AccessoryID"]);

                                //find the row to be updated
                                dtQuotes.PrimaryKey = new DataColumn[] { dtQuotes.Columns["Key"] };
                                DataRow dr = dtQuotes.Rows.Find(intId);

                                if (dr != null)
                                {
                                    dr.BeginEdit();
                                    if (dtQuotes.Columns.Contains(strColumnName))
                                    {
                                        string strQutval = drow["QuoteValue"].ToString();
                                        strQutval = strQutval.Substring(1);
                                        //strQutval = String.Format("{0:c}", Convert.ToDouble(strQutval));
                                        // strQutval = strQutval.Substring(0, strQutval.IndexOf('.'));

                                        dr[strColumnName] = strQutval;
                                        sum += Convert.ToDouble(strQutval); // sum for tot acc cost
                                        //dr[strColumnName] = drow["QuoteValue"].ToString();
                                    }
                                    dr.EndEdit();

                                }
                                #endregion
                            }
                            //for total acc cost
                            dtQuotes.PrimaryKey = new DataColumn[] { dtQuotes.Columns["Key"] };
                            DataRow dr12 = dtQuotes.Rows.Find(-9);
                            if (dr12 != null)
                            {
                                dr12.BeginEdit();
                                if (!colName.Equals(String.Empty))
                                {
                                    string strQutval = String.Format("{0:c}", Convert.ToDouble(sum));
                                    strQutval = strQutval.Substring(1, strQutval.Length - 1);
                                    strQutval = strQutval.Replace(",", "");
                                    dr12[colName] = strQutval;
                                }
                                dr12.EndEdit();
                            }
                            //end
                        }
                        //end
                    }
                }
                logger.Debug("Data bind for Additional Accessories Ends");
                #endregion

                #region "Fixed Charges"
                logger.Debug("Data bind for Fixed Charges Starts");
                row = dtQuotes.NewRow();
                row["Key"] = -99999;
                row["Description"] = "Fixed Charges";
                dtQuotes.Rows.Add(row);

                //get fixed charges
                DataTable dtCharges = objCharge.GetAllChargeTypes();

                if (dtCharges.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCharges.Rows)
                    {
                        row = dtQuotes.NewRow();
                        row["Key"] = Convert.ToInt64(dr["ID"]);
                        row["Description"] = dr["Type"].ToString();
                        dtQuotes.Rows.Add(row);
                    }
                }

                if (ds.Tables[0] != null)
                {
                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow drow in dt.Rows)
                        {
                            #region Update values in dtQuotes datatable
                            Int64 intId = Convert.ToInt64(drow["ChargeTypeID"]);
                            string strColumnName = drow["DealerName"].ToString().Trim() + "-" + drow["OptionID"].ToString().Trim();

                            //set the primary key in datatable
                            dtQuotes.PrimaryKey = new DataColumn[] { dtQuotes.Columns["Key"] };

                            //find the row to be updated
                            DataRow dr = dtQuotes.Rows.Find(intId);

                            if (dr != null)
                            {
                                dr.BeginEdit();
                                if (dtQuotes.Columns.Contains(strColumnName))
                                {
                                    string strQutval = drow["QuoteValue"].ToString();
                                    strQutval = strQutval.Substring(1);
                                    // strQutval = String.Format("{0:c}", Convert.ToDouble(strQutval));
                                    //int a = strQutval.IndexOf('.');
                                    // strQutval = strQutval.Substring(0, strQutval.IndexOf('.'));
                                    dr[strColumnName] = strQutval;

                                    // dr[strColumnName] = drow["QuoteValue"].ToString();

                                }
                                dr.EndEdit();
                            }

                            #endregion
                        }
                    }
                }
                logger.Debug("Data bind for Fixed Charges Ends");
                #endregion



                #endregion

                #region "Add Template Fields dynamically in Gridview"
                logger.Debug("Add Template Fields dynamically in Gridview Starts");
                String[] col = new String[2];
                dt = ds.Tables[0];
                DataTable distinctQuotationIDs = dt.DefaultView.ToTable(true, "QuotationID");
                //col[1] = "DealerName";
                //col[0] = "OptionID";
                DataTable distinctOptionIDs = dt.DefaultView.ToTable(true, "OptionID");
                DataTable distinctDealerIds = dt.DefaultView.ToTable(true, "DealerID");


                col = new String[8];
                col[1] = "DealerID";
                col[0] = "DealerNotes";
                col[2] = "EstimatedDeleveryDates";
                col[3] = "DealerName";
                col[4] = "BuildDate";
                col[5] = "ComplianceDate";
                col[6] = "IsBonus";
                col[7] = "BonusExpDate";
                DataTable distinctDealerNotes = dtDealers.DefaultView.ToTable(true, col);
                DataTable distinctEstimatedDeleveryDates = dtDealers.DefaultView.ToTable(true, col);
                //add columns for dealer notes
                if (distinctDealerNotes.Rows.Count > 0)
                {
                    for (int i = 0; i < distinctDealerNotes.Rows.Count; i++)
                    {
                        string strColumn = "DealerNotes" + i.ToString();
                        dtQuotes.Columns.Add(strColumn);
                        dtQuotes.Columns[strColumn].DefaultValue = distinctDealerNotes.Rows[i]["DealerNotes"].ToString();
                    }
                }


                string OptionId = "optionId", DealerNotesDataBindingExpression = "DealerNotes";

                int ColToExclude = 0, OptionCounter = 1, QuotationIdcnt = 0;
                Boolean CreateDealerNotesLable = false;

                int hide = 0;

                foreach (DataColumn DC in dtQuotes.Columns)
                {
                    CreateDealerNotesLable = false;
                    hide = 0;
                    ColToExclude++;

                    if (ColToExclude == 2)
                    {
                        TemplateField tf = new TemplateField();
                        tf.ItemTemplate = new GridViewQuoteComparisionTemplate(DataControlRowType.DataRow, DC.ColumnName, "", false, "");
                        tf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                        tf.HeaderTemplate = new GridViewQuoteComparisionTemplate(DataControlRowType.Header, "Description", "", false, "");
                        tf.ItemTemplate.InstantiateIn(gvMakeDetails);
                        tf.HeaderTemplate.InstantiateIn(gvMakeDetails);
                        gvMakeDetails.Columns.Add(tf);

                    }



                    if (ColToExclude > 2 && (!DC.ColumnName.Contains("DealerNotes")))
                    {
                        //DataView dv = distinctOptionIDs.DefaultView;
                        //dv.RowFilter = "DealerName = '" + DC.ColumnName.Substring(0, DC.ColumnName.IndexOf('-')) + "'";


                        //if (temp != DC.ColumnName.Substring(0, DC.ColumnName.IndexOf('-')))
                        //{
                        //    temp = DC.ColumnName.Substring(0, DC.ColumnName.IndexOf('-'));
                        //    cnt = dv.Count;

                        //}



                        DealerNotesDataBindingExpression = "";
                        if (OptionCounter > distinctOptionIDs.Rows.Count)
                        {
                            OptionCounter = 1;
                            CreateDealerNotesLable = true;
                            QuotationIdcnt++;

                            if (distinctDealerNotes.Rows[QuotationIdcnt]["DealerNotes"].ToString() == String.Empty || distinctDealerNotes.Rows[QuotationIdcnt]["DealerNotes"].ToString() == null)
                                DealerNotesDataBindingExpression = "-";
                            else
                                DealerNotesDataBindingExpression = distinctDealerNotes.Rows[QuotationIdcnt]["DealerNotes"].ToString();

                        }
                        if (OptionCounter == 1)
                        {
                            CreateDealerNotesLable = true;
                            DealerNotesDataBindingExpression = distinctDealerNotes.Rows[QuotationIdcnt][0].ToString();
                        }

                        //by manoj on 22 mar 2011 for hiding the column having 0 quote value
                        foreach (DataRow drtemp in dtQuotes.Rows)
                        {
                            if (drtemp["Description"].Equals("Total-On Road Cost (Inclusive of GST)"))
                            {
                                if (drtemp[DC.ColumnName].Equals("0.00"))
                                {
                                    hide = 1;
                                    OptionCounter++;

                                }
                            }
                        }

                        if (hide == 0)
                        {
                            TemplateField tf = new TemplateField();
                            tf.ItemTemplate = new GridViewQuoteComparisionTemplate(DataControlRowType.DataRow, DC.ColumnName, "", false, "");
                            tf.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                            tf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                            tf.HeaderTemplate = new GridViewQuoteComparisionTemplate(DataControlRowType.Header, "Option" + OptionCounter.ToString(), "", false, "");
                            string Attribute = distinctOptionIDs.Rows[OptionCounter - 1][0].ToString() + "," + distinctQuotationIDs.Rows[QuotationIdcnt][0].ToString() + "," + ViewState["RequestID"].ToString() + "," + distinctDealerIds.Rows[QuotationIdcnt][0].ToString();

                            tf.ItemTemplate.InstantiateIn(gvMakeDetails);
                            tf.HeaderTemplate.InstantiateIn(gvMakeDetails);
                            if (dtQuotes.Rows[10][DC.ColumnName].ToString() != "$0.00")
                            {
                                tf.FooterTemplate = new GridViewQuoteComparisionTemplate(DataControlRowType.Footer, DC.ColumnName, Attribute, CreateDealerNotesLable, DealerNotesDataBindingExpression);
                                tf.FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                                tf.FooterTemplate.InstantiateIn(gvMakeDetails);

                            }

                            OptionCounter++;
                            gvMakeDetails.Columns.Add(tf);
                        }


                        //TemplateField tf = new TemplateField();
                        //tf.ItemTemplate = new GridViewQuoteComparisionTemplate(DataControlRowType.DataRow, DC.ColumnName, "", false, "");
                        //tf.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        //tf.HeaderTemplate = new GridViewQuoteComparisionTemplate(DataControlRowType.Header, "Option" + OptionCounter.ToString(), "", false, "");
                        //string Attribute = distinctOptionIDs.Rows[OptionCounter - 1][0].ToString() + "," + distinctQuotationIDs.Rows[QuotationIdcnt][0].ToString() + "," + ViewState["RequestID"].ToString() + "," + distinctDealerIds.Rows[QuotationIdcnt][0].ToString();


                        //tf.ItemTemplate.InstantiateIn(gvMakeDetails);
                        //tf.HeaderTemplate.InstantiateIn(gvMakeDetails);
                        //if (dtQuotes.Rows[10][DC.ColumnName].ToString() != "$0.00")
                        //{
                        //    tf.FooterTemplate = new GridViewQuoteComparisionTemplate(DataControlRowType.Footer, DC.ColumnName, Attribute, CreateDealerNotesLable, DealerNotesDataBindingExpression);
                        //    tf.FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                        //    tf.FooterTemplate.InstantiateIn(gvMakeDetails);

                        //}

                        //OptionCounter++;
                        //gvMakeDetails.Columns.Add(tf);

                    }


                }
                logger.Debug("Add Template Fields dynamically in Gridview Ends");
                #endregion

                ViewState["DistinctDealerNotes"] = distinctDealerNotes;

                // by manoj on 14 Mar 2011 For dealer Acc.
                ViewState["distinctEstimatedDeleveryDates"] = distinctEstimatedDeleveryDates;
                DataRow[] tempRow = dtQuotes.Select("Description='Total-On Road Cost (Inclusive of GST)'");
                foreach (DataRow thisRow in tempRow)
                {
                    DataRow dr = dtQuotes.NewRow();
                    for (int i = 0; i < dtQuotes.Columns.Count; i++)
                    { dr[i] = thisRow[i]; }
                    thisRow.Delete();
                    dtQuotes.Rows.Add(dr);
                }
                DataRow dr1 = dtQuotes.NewRow();
                dr1["Key"] = "-9999999";
                dr1["Description"] = "Extra";
                dtQuotes.Rows.Add(dr1);

                //by manoj 24 may 11 
                foreach (DataRow drTemp in dtQuotes.Rows)
                {
                    dr1 = dtQuotes.NewRow();
                    if (Convert.ToString(drTemp["Description"]) == "Recommended Retail Price Exc GST")
                    {
                        for (int i = 0; i < drTemp.ItemArray.Length; i++)
                        {
                            dr1[i] = drTemp.ItemArray[i];
                        }
                        dtQuotes.Rows.Remove(drTemp);
                        dtQuotes.Rows.InsertAt(dr1, 0);
                        break;
                    }
                }
                //end

                ViewState[Cls_Constants.NOOFROWSINQUOTE] = dtQuotes.Rows.Count.ToString();
                //bind grid data

                gvMakeDetails.DataSource = dtQuotes;
                gvMakeDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Debug("Quote Comparison Data Bind Error: " + ex.Message);
            // throw;
        }
        finally
        {
            logger.Debug("Quote Comparison Data Bind Ends");
            objCompare = null;
        }
    }


    #endregion

    #region "Events"

    // by manoj on 16 Mar 2011 for daler reminder
    protected void gvDealerInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int min = 0;
        ConfigValues objConfig = new ConfigValues();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            try
            {
                min = Convert.ToInt32(objConfig.GetValue(Cls_Constants.MINUTES_TO_REMIND_DEALER));
                DateTime dtnow = System.DateTime.Now;
                TimeSpan ts = new TimeSpan(0, -min, 00);
                DateTime dtNew = dtnow.Add(ts);
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRemind");



                if (((HiddenField)e.Row.FindControl("QoutationID")).Value.ToString() == "")
                {
                    // int QId = Convert.ToInt32(((HiddenField)e.Row.FindControl("QoutaionExist")).Value);
                    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Reminder").ToString()) == 1)
                    {

                        lnk.Enabled = false;
                        lnk.ForeColor = System.Drawing.Color.FromName("Gray");
                        lnk.Text = "Quotation Pending <br/> Reminded";


                    }
                    else
                    {
                        if (dtNew <= Convert.ToDateTime(((HiddenField)e.Row.FindControl("LastDate")).Value))
                        {
                            lnk.Enabled = false;
                            lnk.ForeColor = System.Drawing.Color.FromName("Gray");

                        }
                    }
                    e.Row.Style.Add("Background-Color", "#FF9F9F");


                }
                else
                {
                    lnk.Enabled = false;
                    lnk.ForeColor = System.Drawing.Color.FromName("Gray");
                    lnk.Text = "Qoutation Received";
                    e.Row.Style.Add("Background-Color", "#AEFFAE");
                }

            }
            catch (Exception ex)
            {

            }
        }
    }

    // by manoj on 16 Mar 2011 for daler reminder
    //protected void gvDealerInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "RemindDealer")
    //    {

    //        Cls_UserMaster objUserMaster = new Cls_UserMaster();
    //        DataTable dtTemp = (DataTable)ViewState["DealerInfo1"];
    //        DateTime LastTemindDateTime = System.DateTime.Now;
    //        int DealerId = 0, RequestId = 0;
    //        string Email = "";
    //        try
    //        {
    //            DealerId = Convert.ToInt32(e.CommandArgument);
    //            objUserMaster.DealerID = DealerId;

    //            for (int i = 0; i < dtTemp.Rows.Count; i++)
    //            {
    //                if (Convert.ToInt32(dtTemp.Rows[i]["DealerID"].ToString()) == Convert.ToInt32(e.CommandArgument))
    //                {
    //                    LastTemindDateTime = Convert.ToDateTime(dtTemp.Rows[i]["LastRemindDateTime"].ToString());
    //                    RequestId = Convert.ToInt32(dtTemp.Rows[i]["RequestId"].ToString());
    //                    objUserMaster.RequestID = RequestId;
    //                    objUserMaster.Reminder = Convert.ToInt32(dtTemp.Rows[i]["Reminder"].ToString()) + 1;
    //                    Email = dtTemp.Rows[i]["Email"].ToString();
    //                }
    //            }

    //            DateTime dtnow = System.DateTime.Now;
    //            TimeSpan ts = new TimeSpan(-3, 00, 00);
    //            DateTime dtNew = dtnow.Add(ts);


    //            int result = objUserMaster.updateReminder();
    //            if (result > 0)
    //            {
    //                SendMail(Email, DealerId, RequestId);
    //            }

    //            gvDealerInfo.DataSource = objUserMaster.GetDealerInfo();
    //            gvDealerInfo.DataBind();

    //        }
    //        catch
    //        {
    //        }
    //        finally
    //        {
    //            objUserMaster = null;
    //            dtTemp = null;
    //        }

    //    }
    //}

    protected void gvMakeDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["dtDealers"] != null)
            {
                DataTable dtDealers = (DataTable)ViewState["dtDealers"];
                DataView dv = null;

                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell Cell_Header = new TableCell();
                Cell_Header.Text = "";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 1;
                HeaderRow.Cells.Add(Cell_Header);

                DataTable tempdt = dtDealers.DefaultView.ToTable(true, "DealerName", "Company", "Email", "Fax", "Phone");

                foreach (DataRow dr in tempdt.Rows)
                {
                    Cell_Header = new TableCell();
                    //Cell_Header.Text = "<html><body>" + dr["DealerName"].ToString() + "<br/>Company : " + dr["Company"].ToString() + "<br/>Email :"+ dr["Email"].ToString() + "<br/>Fax : " + dr["Fax"].ToString() + "<br/> Phone : " + dr["Phone"].ToString() + " </body></html>";
                    Cell_Header.Text = dr["DealerName"].ToString();
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.Style.Add("Color", "White");
                    dv = dtDealers.DefaultView;
                    dv.RowFilter = "DealerName = '" + dr["DealerName"].ToString() + "' and Value>0.00";

                    Cell_Header.ColumnSpan = dv.ToTable().Rows.Count;
                    HeaderRow.Cells.Add(Cell_Header);
                }

                gvMakeDetails.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            RowCounter++;
            if (RowCounter == Convert.ToInt16(ViewState[Cls_Constants.NOOFROWSINQUOTE].ToString()))
            {
                if (ViewState["dtDealers"] != null)
                {
                    DataTable dtDealers = (DataTable)ViewState["dtDealers"];
                    DataView dv = null;

                    //to add dealer notes
                    GridView HeaderGrid = (GridView)sender;
                    GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Insert);
                    TableCell Cell_Header = new TableCell();
                    Cell_Header.Text = "Dealer Notes";
                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header.ColumnSpan = 1;
                    HeaderRow.Cells.Add(Cell_Header);

                    //add estimated delivery date
                    GridView HeaderGrid1 = (GridView)sender;
                    GridViewRow HeaderRow1 = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Insert);
                    TableCell Cell_Header1 = new TableCell();
                    Cell_Header1.Text = "Estimated Delivery Dates";
                    Cell_Header1.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header1.ColumnSpan = 1;
                    HeaderRow1.Cells.Add(Cell_Header1);
                    HeaderRow1.CssClass = "gridactiverow";
                    HeaderRow1.BackColor = System.Drawing.Color.FromName("#B9E9FF");

                    //add BuilDate
                    GridView HeaderGrid2 = (GridView)sender;
                    GridViewRow HeaderRow2 = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Insert);
                    TableCell Cell_Header2 = new TableCell();
                    Cell_Header2.Text = "Build Date";
                    Cell_Header2.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header2.ColumnSpan = 1;
                    HeaderRow2.Cells.Add(Cell_Header2);
                    HeaderRow2.CssClass = "gridactiverow";
                    HeaderRow2.BackColor = System.Drawing.Color.FromName("#B9E9FF");

                    //add ComplianceDate 
                    GridView HeaderGrid3 = (GridView)sender;
                    GridViewRow HeaderRow3 = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Insert);
                    TableCell Cell_Header3 = new TableCell();
                    Cell_Header3.Text = "Compliance Date";
                    Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header3.ColumnSpan = 1;
                    HeaderRow3.Cells.Add(Cell_Header3);
                    HeaderRow3.CssClass = "gridactiverow";
                    HeaderRow3.BackColor = System.Drawing.Color.FromName("#B9E9FF");

                    //add Manufacturer Bonus 
                    GridView HeaderGrid4 = (GridView)sender;
                    GridViewRow HeaderRow4 = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Insert);
                    TableCell Cell_Header4 = new TableCell();
                    Cell_Header4.Text = "Manufacturer Bonus";
                    Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                    Cell_Header4.ColumnSpan = 1;
                    HeaderRow4.Cells.Add(Cell_Header4);
                    HeaderRow4.CssClass = "gridactiverow";
                    HeaderRow4.BackColor = System.Drawing.Color.FromName("#B9E9FF");


                    DataTable tempdt1 = (DataTable)ViewState["distinctEstimatedDeleveryDates"];
                    foreach (DataRow dr1 in tempdt1.Rows)
                    {
                        Cell_Header1 = new TableCell();
                        Cell_Header1.Text = dr1["EstimatedDeleveryDates"].ToString();
                        Cell_Header1.HorizontalAlign = HorizontalAlign.Center;

                        dv = dtDealers.DefaultView;
                        dv.RowFilter = "DealerName = '" + dr1["DealerName"].ToString() + "' and Value>0.00";

                        Cell_Header1.ColumnSpan = dv.ToTable().Rows.Count;
                        HeaderRow1.Cells.Add(Cell_Header1);


                        Cell_Header = new TableCell();
                        if (dr1["DealerNotes"].ToString() == String.Empty)
                            Cell_Header.Text = "-";
                        else
                            Cell_Header.Text = dr1["DealerNotes"].ToString();
                        Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                        Cell_Header.ColumnSpan = dv.ToTable().Rows.Count; ;
                        HeaderRow.Cells.Add(Cell_Header);

                        //add BuildDate
                        Cell_Header2 = new TableCell();
                        Cell_Header2.Text = (dr1["BuildDate"].ToString()).Substring(3);
                        Cell_Header2.HorizontalAlign = HorizontalAlign.Center;
                        Cell_Header2.ColumnSpan = dv.ToTable().Rows.Count; ;
                        HeaderRow2.Cells.Add(Cell_Header2);

                        //add ComplianceDate 
                        Cell_Header3 = new TableCell();
                        Cell_Header3.Text = (dr1["ComplianceDate"].ToString()).Substring(3);
                        Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
                        Cell_Header3.ColumnSpan = dv.ToTable().Rows.Count; ;
                        HeaderRow3.Cells.Add(Cell_Header3);

                        //add Manufacturer Bonus 
                        Cell_Header4 = new TableCell();
                        if (Convert.ToInt32(dr1["IsBonus"]) == 1)
                        {
                            if (String.IsNullOrEmpty(Convert.ToString(dr1["BonusExpDate"])))
                                Cell_Header4.Text = "Applicable ( no Expiry Date)";
                            else
                                Cell_Header4.Text = "Applicable ( upto " + (Convert.ToString(dr1["BonusExpDate"])) + " )";
                        }
                        else
                            Cell_Header4.Text = "N/A";
                        Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                        if (dv.ToTable().Rows.Count > 0)
                        {
                            Cell_Header4.ColumnSpan = dv.ToTable().Rows.Count; ;
                            HeaderRow4.Cells.Add(Cell_Header4);
                        }

                    }


                    DataTable tempdt = (DataTable)ViewState["DistinctDealerNotes"]; //dtDealers.DefaultView.ToTable(true, "DealerNotes");


                    gvMakeDetails.Controls[0].Controls.AddAt((RowCounter + 1), HeaderRow);
                    gvMakeDetails.Controls[0].Controls.AddAt((RowCounter + 2), HeaderRow1);
                    gvMakeDetails.Controls[0].Controls.AddAt((RowCounter + 3), HeaderRow2);
                    gvMakeDetails.Controls[0].Controls.AddAt((RowCounter + 4), HeaderRow3);
                    gvMakeDetails.Controls[0].Controls.AddAt((RowCounter + 5), HeaderRow4);

                    RowCounter = 0;
                }
            }
        }
    }
    protected void gvMakeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvMakeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "gvNormalRow";

                if (DataBinder.Eval(e.Row.DataItem, "Description").Equals("Additional Accessories")
                    || DataBinder.Eval(e.Row.DataItem, "Description").Equals("Fixed Charges"))
                {
                    e.Row.CssClass = "gridactiverow";
                    e.Row.Cells[0].ColumnSpan = gvMakeDetails.Columns.Count;
                    for (int i = 1; i < e.Row.Controls.Count; i++)
                    {
                        e.Row.Cells[i].Visible = false;

                    }
                }

                if (DataBinder.Eval(e.Row.DataItem, "Description").Equals("Recommended Retail Price Exc GST")
                    || DataBinder.Eval(e.Row.DataItem, "Description").Equals("Total-On Road Cost (Inclusive of GST)"))
                {
                    e.Row.CssClass = "gridactiverow";
                    if (DataBinder.Eval(e.Row.DataItem, "Description").Equals("Total-On Road Cost (Inclusive of GST)"))
                    {
                        e.Row.Style.Value = "Font-Weight:bold";
                    }

                }

                //byt manoj for deleting empty row
                if (DataBinder.Eval(e.Row.DataItem, "Description").Equals("Extra"))
                {
                    e.Row.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            logger.Error("gvMakeDetails_RowDataBound Event : " + ex.Message);
        }

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    ((Button)e.Row.FindControl("btnShortList")).CommandArgument = DataBinder.Eval(e.Row.DataItem, "CommandArgument").ToString();
        //}
    }

    //protected void btnSendMail_Click(object sender, ImageClickEventArgs e)
    //{
    //}

    #endregion

}
