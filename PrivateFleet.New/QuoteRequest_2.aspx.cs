using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;

public partial class QuoteRequest_2 : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(QuoteRequest_2));
    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        logger.Error("Create Quote_2 Load Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {

            lblMsg.Text = String.Empty;
            //Set page header text
            if (!IsPostBack)
            {
                txtPCode.Attributes.Add("onkeypress", "return isNumberKey(event,this);");

                Label lblHeader = (Label)Master.FindControl("lblHeader");
                if (lblHeader != null)
                    lblHeader.Text = "Create Quote Request";

                if (Convert.ToInt32(Request.QueryString["from"]) == 3)
                {
                    if (Session["PCode_Suburb"] != null)
                    {
                        txtPCode.Text = Convert.ToString(Session["PCode_Suburb"]).Split('^')[0];
                        txtPCode_TextChanged(sender, e);
                        ddlLocation.SelectedValue =Convert.ToString(Session["PCode_Suburb"]).Split('^')[1];
                        btnSearchDealers_Click(null,null);
                        Session.Remove("PCode_Suburb");
                    }
                    if (Session["DEALER_SELECTED"] != null)
                    {
                        
                        GridView gv = (GridView)UcDealerSelection1.FindControl("gvDealerDetails");
                        GridView gv1 = (GridView)UcDealerSelection1.FindControl("gvSelectedDealers");

                        foreach (DataRow dr in ((DataTable)Session["DEALER_SELECTED"]).Rows)
                        {
                            foreach (GridViewRow gr in gv.Rows)
                            {
                                if (Convert.ToString(dr["ID"])== (gr.FindControl("hfDealerID") as HiddenField).Value.ToString())
                                {
                                    ImageButton btn = (ImageButton)gr.FindControl("btnSelect");
                                    btn.ImageUrl = "~/Images/Select_dealer_disabled.gif";
                                    btn.Enabled = false;
                                }
                            }
                        }
                        UcDealerSelection1.FindControl("tblSelectedDealers").Visible = true;
                        gv1.DataSource = (DataTable)Session["DEALER_SELECTED"];
                        gv1.DataBind();
                        
                    }

                }
            }
            QuoteRequest2.DefaultButton = "btnSearchDealers";


        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Quote Request_2 Event : " + ex.Message);
        }
        finally
        {
            logger.Error("Create Quote_2 Load Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }
    #endregion

    #region text events
    protected void txtPCode_TextChanged(object sender, EventArgs e)
    {
        logger.Error("PCode Change Start=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        ddlLocation.Items.Clear();
        ddlLocation.Items.Add("-Select Suburb-");
        GridView gvTemp = (GridView)UcDealerSelection1.FindControl("gvDealerDetails");
        gvTemp.DataSource = null;
        gvTemp.DataBind();
        getSuburb();
        logger.Error("PCode Change end=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
    }


    #endregion

    #region Methods
    private void getSuburb()
    {
        Cls_CustomerMaster objCustomer = new Cls_CustomerMaster();
        int PostalCode = 0;
        if (!(String.IsNullOrEmpty(txtPCode.Text)))
            PostalCode = Convert.ToInt32(txtPCode.Text);
        objCustomer.PostalCode = PostalCode.ToString();
        logger.Error("Get suburb DB start=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        DataTable dtSuburbs = objCustomer.GetSuburbsOfThePostalCode();
        logger.Error("Get suburb DB end=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));

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

    private bool ValidateDealerSelection()
    {
        logger.Error("ValidateDealerSelection Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
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
        logger.Error("ValidateDealerSelection ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        return true;

    }
    #endregion

    #region Button Events
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("QuoteRequest_1.aspx?from=2");
    }
    protected void btnSearchDealers_Click(object sender, ImageClickEventArgs e)
    {
        logger.Error("Search Dealer Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        Cls_Dealer objDealer = new Cls_Dealer();
        DataTable dt = null;

        try
        {
            #region search dealers in selected cities
            objDealer.PostalCode = txtPCode.Text;
            objDealer.MakeID = Convert.ToInt32(Convert.ToString(Session["Make_Model_Series"]).Split('^')[0]);

            logger.Error("Get data from DB Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            dt = objDealer.SearchDealersForMakeInCities();
            logger.Error("Get data from DB ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            #endregion

            UcDealerSelection1.ClearDataTable();
            UcDealerSelection1.Visible = true;
            UcDealerSelection1.dtDealers = dt;
            UcDealerSelection1.BindDealers(dt);

            if (sender != null)
            {
                GridView gvTemp = (GridView)UcDealerSelection1.FindControl("gvSelectedDealers");
                gvTemp.DataSource = null;
                gvTemp.DataBind();
                Session.Remove("DEALER_SELECTED");
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnSearchDealers_Click Event : " + ex.Message);
        }
        finally
        {
            logger.Error("Search Dealer Ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }
    protected void btnCreateRequest_Click(object sender, ImageClickEventArgs e)
    {
        #region "Check selected dealer limit"
        if (!ValidateDealerSelection())
            return;
        #endregion
        Session["PCode_Suburb"] = txtPCode.Text + "^" + ddlLocation.SelectedValue + "^" + ddlLocation.SelectedItem;
        Response.Redirect("QuoteRequest_3.aspx");
    }

    #endregion
}
