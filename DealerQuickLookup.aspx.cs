using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mechsoft.GeneralUtilities;
using System.Data;
using log4net;
using Mechsoft.FleetDeal;

public partial class DealerQuickLookup : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(DealerQuickLookup));
    DataTable dtNew = null;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtQuickPCode.Text = "";
            Label lblHeader = (Label)Master.FindControl("lblHeader");
            lblHeader.Text = "Dealer Quick Lookup";

            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvDealerDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());

            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "TotalPoints1";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;

            FillMake();

            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "FinalPoints";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;

            //btnSendBulkEnquiry.Enabled = false;

        }
        pnlQuick.DefaultButton = "btnSearchDealers";
    }

    #endregion

    #region Methods

    private void FillMake()
    {
        logger.Debug("FillMake Method Start");
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
                ddlQuickMake.Items.Clear();

                //fill make dropdown
                ddlQuickMake.DataSource = dt;
                ddlQuickMake.DataTextField = "Make";
                ddlQuickMake.DataValueField = "id";
                ddlQuickMake.DataBind();
            }

            //insert default item in make dropdown
            ddlQuickMake.Items.Insert(0, new ListItem("-Select Make-", "-Select-"));
        }
        catch (Exception ex)
        {
            logger.Debug("Error : " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("FillMake Method End");
        }
    }

    public void DealerDetails()
    {
        Cls_Dealer objDealer = new Cls_Dealer();
        DataTable dt = null;
        try
        {
            objDealer.PostalCode = txtQuickPCode.Text;
            objDealer.MakeID = Convert.ToInt32(ddlQuickMake.SelectedValue);

            dt = objDealer.SearchDealersForMakeInCities();

            ViewState["dtDealers"] = dt;
            if (dt.Rows.Count > 0)
            {
                pnlDealerDetails.Visible = true;
            }

            //dtNew = new DataTable();
            //dtNew = dt.Copy();//(DataTable)ViewState["dtDealers"];

            DataColumn dcFinalPoints = new DataColumn("FinalPoints");
            dcFinalPoints.DataType = typeof(Double);
            dt.Columns.Add(dcFinalPoints);
            //dtNew.Columns.Add(dcFinalPoints);
            //dtNew.Columns[0].AllowDBNull = true;
            //dtNew.Columns[7].AllowDBNull = true;
            //dtNew.Columns[11].AllowDBNull = true;
            //dtNew.Columns[12].AllowDBNull = true;
            //dtNew.Columns[14].AllowDBNull = true;
            //dtNew.Columns[15].AllowDBNull = true;

            Int32 i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                #region Added by Archana : On 16 April 2012
                bool IsOutsideDealer = Convert.ToBoolean((Convert.ToString(dr["IsOutsideDealer"]).ToLower()));
                bool IsHotDealer = !string.IsNullOrEmpty(Convert.ToString(dr["IsHotDealer"])) ? Convert.ToBoolean(Convert.ToString(dr["IsHotDealer"])) : false;
                Double FinalPoints = 1, _totRating = 0;

                if (!String.IsNullOrEmpty(Convert.ToString(dr["Rating"])))
                {
                    _totRating = Math.Round(Convert.ToDouble(Convert.ToString(dr["Rating"])), 1);
                }
                Double kms = !String.IsNullOrEmpty(Convert.ToString(dr["kms"])) ? Convert.ToDouble(Convert.ToString(dr["kms"])) : 0;

                Double TotalPoints = Convert.ToDouble(Convert.ToString(dr["TotalPoints"]));

                if (kms > 100)
                {
                    FinalPoints = (0.75 * FinalPoints);
                }
                if (_totRating > 8 && Convert.ToInt32(Convert.ToString(dr["Total"])) >= 5)
                {
                    FinalPoints = (1.5 * FinalPoints);
                }
                if (_totRating < 5 && Convert.ToInt32(Convert.ToString(dr["Total"])) >= 5)
                {
                    FinalPoints = (0.5 * FinalPoints);
                }

                FinalPoints = IsHotDealer ? 1.5 * FinalPoints : FinalPoints;

                FinalPoints = IsOutsideDealer ? 0.6 * FinalPoints : FinalPoints;

                FinalPoints = TotalPoints * FinalPoints;

                // if (dtNew != null)
                //  {
                dt.Rows[i]["FinalPoints"] = FinalPoints;
                // }
                i++;
                #endregion
            }

            // DataView dvFinal = dtNew.DefaultView;
            DataView dvFinal = dt.DefaultView;
            dvFinal.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            DataTable dtDealersDetails = dvFinal.ToTable();
            gvDealerDetails.DataSource = dtDealersDetails;
            gvDealerDetails.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("btnSearchDealers_Click Event : " + ex.Message);
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

    public void hidePopupDiv()
    {
        divpopID.Visible = false;
    }

    #endregion

    #region Events

    protected void btnSearchDealers_Click(object sender, ImageClickEventArgs e)
    {
        DealerDetails();
    }

    protected void gvDealerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnSelect = (ImageButton)e.Row.FindControl("btnSelect");

                //for rating of dealer
                Label lblRating = (Label)e.Row.FindControl("lblRating");

                if (lblRating.Text != String.Empty && lblRating.Text != null)
                {
                    string tot = Convert.ToString(Math.Round(Convert.ToDouble(lblRating.Text), 1)) + "<sub>(" + Convert.ToString(((HiddenField)e.Row.FindControl("hdfTot")).Value) + ")</sub>";
                    lblRating.Text = tot;
                }
                else
                {
                    lblRating.Text = "--";
                    lblRating.ToolTip = "Yet not Rated";
                }

                string showbtn = DataBinder.Eval(e.Row.DataItem, "ShowSelectButton").ToString();
                if (showbtn.ToLower().ToString() == "true")
                {
                    btnSelect.Enabled = true;
                    btnSelect.ImageUrl = "~/Images/Select_dealer.gif";
                }
                else
                {
                    btnSelect.Enabled = false;
                    btnSelect.ImageUrl = "~/Images/Select_dealer_disabled.gif";
                }

                if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[12].ToString().ToLower() == "true")
                {
                    // e.Row.CssClass = "datalistHotDealer";
                    e.Row.Cells[0].CssClass = "temp"; // add css for hot dealer to add Flame image
                }
                else if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[24].ToString().ToLower() == "true")
                {
                    e.Row.Cells[0].CssClass = "temp1";
                }
                if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[15].ToString().ToLower() == "true")
                {
                    e.Row.CssClass = "datalistOutSideDealer";
                }
                else
                {
                    e.Row.CssClass = "datalistNormalDealer";
                }


                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelect");
                chk.Attributes.Add("onclick", "javascript:EnableBulkEnq('" + chk.ClientID + "')");
                //chk.Attributes.Add("onclick", "javascript:SelectAll();");
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvDealerDetails_RowDataBound_Event :" + ex.Message);
        }
    }

    protected void gvDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDealerDetails.PageIndex = e.NewPageIndex;
            gvDealerDetails.DataSource = (DataTable)ViewState["dtDealers"];
            gvDealerDetails.DataBind();
            // BindDealers((DataTable)ViewState["dtDealers"]);
        }
        catch (Exception ex)
        {
            logger.Error("gvDealerDetails_PageIndexChanging Event :" + ex.Message);
        }
    }

    protected void gvDealerDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();

        DealerDetails();

    }
    
    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDealerDetails.PageIndex = 0;
        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {

            gvDealerDetails.DataSource = (DataTable)ViewState["dtDealers"];
            gvDealerDetails.PageSize = gvDealerDetails.PageCount * gvDealerDetails.Rows.Count;
            gvDealerDetails.DataBind();
        }
        else
        {
            gvDealerDetails.DataSource = (DataTable)ViewState["dtDealers"];
            gvDealerDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvDealerDetails.DataBind();
        }
    }
    
    protected void btnSendBulkEnquiry_Click(object sender, EventArgs e)
    {
        ConfigValues objConfigue = new ConfigValues();
        objConfigue.Key = "NO_OF_DEALERS_TO_SEND_MAIL";
        int LimitDealerCnt = Convert.ToInt16(objConfigue.GetValue(objConfigue.Key));
        int count = 0;
        Page page = HttpContext.Current.Handler as Page;


        foreach (GridViewRow gr in gvDealerDetails.Rows)
        {

            CheckBox chk = (CheckBox)gr.FindControl("chkSelect");
            if (chk.Checked)
            {
                count++;
            }
        }
        if (count < 1)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Please select atleast one dealer to send mail.');", true);
            return;
        }
        string str = "Total number of dealers should not exceeds " + LimitDealerCnt.ToString();
        if (count > LimitDealerCnt)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + str + "');", true);
            return;
        }
        else
        {
            btnSendBulkEnquiry.Enabled = true;
            DataTable dt = new DataTable();
            DataColumn dcID = new DataColumn("DealerID");
            DataColumn dcEmailID = new DataColumn("DealerEmailID");
            dt.Columns.Add(dcID);
            dt.Columns.Add(dcEmailID);
            DataRow dr;

            foreach (GridViewRow gr in gvDealerDetails.Rows)
            {
                CheckBox chk = (CheckBox)gr.FindControl("chkSelect");
                if (chk.Checked)
                {
                    dr = dt.NewRow();
                    dr["DealerID"] = (((HiddenField)gr.FindControl("hfDealerID")).Value).ToString();
                    dr["DealerEmailID"] = (((Label)gr.FindControl("lblEmail")).Text).ToString();
                    dt.Rows.Add(dr);
                }
            }

            Session["DealerInfo"] = dt;
            divpopID.Visible = true;
            UCEnquiry.ShowHideDivs();
        }
    }
    #endregion

}
