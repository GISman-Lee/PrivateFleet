using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Mechsoft.GeneralUtilities;
using System.Data;

public partial class QuoteRequest_1 : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(QuoteRequest_1));
    DataTable dt;


    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        logger.Error("Create Quote_1 Load Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {

            //Set page header text
            UcRequestParameters1.BindParameters();
            if (!IsPostBack)
            {
                TextBox1.Attributes.Add("onkeypress", "return maxLength(event,this);");
                //CleanUp();
                Label lblHeader = (Label)Master.FindControl("lblHeader");
                if (lblHeader != null)
                    lblHeader.Text = "Create Quote Request";

                //Fill make dropdown
                FillMake();
                UcRequestParameters1.FillAdditionalAccessories();

                if (Convert.ToInt32(Request.QueryString["from"]) == 2)
                {
                    if (Session["Make_Model_Series"] != null)
                    {
                        ddlMake.SelectedValue = Convert.ToString(Session["Make_Model_Series"]).Split('^')[0];
                        ddlMake_SelectedIndexChanged(sender, e);
                        ddlModel.SelectedValue = Convert.ToString(Session["Make_Model_Series"]).Split('^')[2];
                        txtSeries.Text = Convert.ToString(Session["Make_Model_Series"]).Split('^')[4];
                        Session.Remove("Make_Model_Series");
                    }
                    if (Session["ConsultantNotes"] != null)
                    {
                        TextBox1.Text = Convert.ToString(Session["ConsultantNotes"]);
                        Session.Remove("ConsultantNotes");
                    }
                    if (Session["chkBox"] != null)
                    {
                        chkOrderTaken.Checked = Convert.ToBoolean(Convert.ToInt32(Convert.ToString(Session["chkBox"]).Split(',')[0]));
                        chkUrgent.Checked = Convert.ToBoolean(Convert.ToInt32(Convert.ToString(Session["chkBox"]).Split(',')[1]));
                        chkBuid.Checked = Convert.ToBoolean(Convert.ToInt32(Convert.ToString(Session["chkBox"]).Split(',')[2]));
                        Session.Remove("chkBox");
                    }
                    if (Session["dtParameters"] != null)
                    {
                        DataTable d1T = (DataTable)Session["dtParameters"];
                        GridView gv = (GridView)UcRequestParameters1.FindControl("gvParameters");
                        int cnt = 1;
                        foreach (DataRow dr in ((DataTable)Session["dtParameters"]).Rows)
                        {
                            foreach (GridViewRow gr in gv.Rows)
                            {
                                Label lbl = (Label)gr.FindControl("lblAccessory");

                                if (lbl.Text == Convert.ToString(dr["AccessoryName"]))
                                {
                                    TextBox txt = (TextBox)gr.FindControl("txtValue" + cnt);
                                    txt.Text = Convert.ToString(dr["Specification"]);
                                    cnt++;
                                }
                            }
                        }
                    }
                    if (Session["SELECT_ACC"] != null)
                    {
                        DataTable dttemp = (DataTable)Session["SELECT_ACC"];
                        GridView gv1 = (GridView)UcRequestParameters1.FindControl("gvAccessories");
                        gv1.DataSource = (DataTable)Session["SELECT_ACC"];
                        gv1.DataBind();

                    }
                   
                }
                Session.Remove("PCode_Suburb");
                Session.Remove("dtAllDealers");
                Session.Remove("DEALER_SELECTED");
            }

            //Bind parameters
            QuoteRequest1.DefaultButton = "btnNextStep";
            chkBuid.Text = " Must be " + DateTime.Now.Year.ToString() + " Build & Complied";

        }
        catch (Exception ex)
        {
            logger.Error("Page_Load For QuoteRequest_1 Event : " + ex.Message);
        }
        finally
        {
            logger.Error("Create Quote_1 Load Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Method to fill make dropdown
    /// </summary>
    private void FillMake()
    {
        logger.Debug("FillMake Method Start");
        logger.Error("Fill Make Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        Cls_MakeHelper objMake = new Cls_MakeHelper();

        try
        {
            //get all active make
            if (Cache["MAKES"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadMake();

            dt = Cache["MAKES"] as DataTable;

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
        catch (Exception)
        {
            throw;
        }
        finally
        {
            dt = null;
            logger.Error("Fill Make Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            logger.Debug("FillMake Method End");
        }
    }
    /// <summary>
    /// Method to fill models dropdown
    /// </summary>
    private void FillModels()
    {
        logger.Debug("FillModels Method Start");
        logger.Error("Fill Models Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {
            if (Cache["MODELS"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadModel();

            dt = Cache["MODELS"] as DataTable;

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
        catch (Exception)
        {
            throw;
        }
        finally
        {
            dt = null;
            logger.Error("Fill Models Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            logger.Debug("FillModels Method End");
        }
    }
    #endregion

    #region Drop Down Events
    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Error("DDL Make Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
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
            logger.Error("Ddl Male ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }
    #endregion

    #region Button Events
    protected void btnNextStep_Click(object sender, ImageClickEventArgs e)
    {
        logger.Error("Step 1 Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {
            Session["Make_Model_Series"] = ddlMake.SelectedValue + "^" + ddlMake.SelectedItem + "^" + ddlModel.SelectedValue + "^" + ddlModel.SelectedItem + "^" + txtSeries.Text;
            //Session["Model"] = ddlModel.SelectedValue;
            //Session["Series"] = txtSeries.Text;
            Session["ConsultantNotes"] = TextBox1.Text;
            Session["chkBox"] = null;
            string chkStr = "";
            if (chkOrderTaken.Checked)
                chkStr = "1";
            else
                chkStr = "0";

            if (chkUrgent.Checked)
                chkStr += ",1";
            else
                chkStr += ",0";

            if (chkBuid.Checked)
                chkStr += ",1";
            else
                chkStr += ",0";
            Session["chkBox"] = chkStr;

            UcRequestParameters1.MethodForSelectParam();   //may need to use session
            UcRequestParameters1.UpdateDataTable();     //may need to use session

            // Session.Abandon();
            //Session.Remove("chkBox");
            //string s = Convert.ToString(Session["chkBox"]);
            Response.Redirect("QuoteRequest_2.aspx");
        }
        catch (Exception ex)
        {
            logger.Error("btnNextStep_Click Event : " + ex.Message);
        }
        finally
        {
            logger.Error("Step 1 Ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

    }
    #endregion

}
