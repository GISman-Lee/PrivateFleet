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
using AccessControlUnit;
using log4net;
using System.Text;
using System.Windows.Forms;
using System.Threading;

public partial class VDT_Customer_Welcome : System.Web.UI.Page
{

    #region Private Variables

    ILog logger = LogManager.GetLogger(typeof(VDT_Customer_Welcome));
    Cls_VDTWelcome objCls_VDTWelcome = new Cls_VDTWelcome();
    bool chk;
    Thread tr1 = null;
    #endregion

    #region Page_Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (Session["id"] == null)
            {
                Response.Redirect("vdtLogin.aspx");
                return;
            }
            if (Convert.ToString(Session["id"]) == "")
            {
                Response.Redirect("vdtLogin.aspx");
                return;
            }

            if (!Page.IsPostBack)
            {
                lblmsg.Text = "";
                chk = false;
                lblusername.Text = Convert.ToString(Convert.ToString(Session["usernmae"]));
                objCls_VDTWelcome.id = Convert.ToInt32(Session["id"]);
                ds = objCls_VDTWelcome.getVDT_DealerStatusByCustomerid();

                dt = ds.Tables[0].Copy();
                grdDealerStatus.DataSource = dt;
                grdDealerStatus.DataBind();
                ViewState["_VDTDealer"] = dt;

                lblDealerName.Text = "";

                lblmobile.Text = "";
                lblVehicleComissionNo.Text = "";

                lblCompany.Text = "";
                lblCustomerHeading.Text = "";
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //make request to deealer button visible false if admin 
                        // unmark the customer after dealer make status as car delivered
                        if (Convert.ToInt32(dt.Rows[0]["Unmark"]) >= 1)
                        {
                            btnSend.Visible = false;
                        }
                        //end
                        lblusername.Text = Convert.ToString(dt.Rows[0]["fullname"]);
                        lblCustomerHeading.Text = Convert.ToString(dt.Rows[0]["fullname"]);
                        if (lblusername.Text.Trim() == "")
                        {
                            lblusername.Text = "---";
                            lblCustomerHeading.Text = "---";
                        }
                        lblDealerName.Text = Convert.ToString(dt.Rows[0]["Name"]);

                        if (lblDealerName.Text.Trim() == "")
                        {
                            lblDealerName.Text = "---";
                        }
                        hypEmail.NavigateUrl = "mailto:" + Convert.ToString(dt.Rows[0]["Email"]);
                        hypEmail.Text = Convert.ToString(dt.Rows[0]["Email"]);
                        if (hypEmail.Text.Trim() == "")
                        {
                            hypEmail.Text = "---";
                            hypEmail.NavigateUrl = "#";
                        }

                        lblmobile.Text = Convert.ToString(dt.Rows[0]["mobile"]);
                        if (lblmobile.Text.Trim() == "")
                        {
                            lblmobile.Text = "---";
                        }

                        lblVechicleName.Text = Convert.ToString(dt.Rows[0]["make"]);
                        if (lblVechicleName.Text.Trim() == "")
                        {
                            lblVechicleName.Text = "---";
                        }

                        lblVechicleName.Text = lblVechicleName.Text + " - " + Convert.ToString(dt.Rows[0]["Model"]);
                        if (lblVechicleName.Text.Trim() == "")
                        {
                            lblVechicleName.Text = lblVechicleName.Text + " ( --- )";
                        }
                        lblCompany.Text = Convert.ToString(dt.Rows[0]["company"]);
                        if (lblCompany.Text.Trim() == "")
                        {
                            lblCompany.Text = "---";
                        }

                        lblstate.Text = Convert.ToString(dt.Rows[0]["state"]);
                        if (lblstate.Text.Trim() == "")
                        {
                            lblstate.Text = "---";
                        }
                        lblphone.Text = Convert.ToString(dt.Rows[0]["phone"]);
                        if (lblphone.Text.Trim() == "")
                        {
                            lblphone.Text = "---";
                        }


                        lblVehicleComissionNo.Text = Convert.ToString(dt.Rows[0]["VehicleCommissionNo"]);
                        if (lblVehicleComissionNo.Text.Trim() == "")
                        {
                            lblVehicleComissionNo.Text = "---";
                        }

                        lblStockNo.Text = Convert.ToString(dt.Rows[0]["StockNo"]);
                        if (lblStockNo.Text.Trim() == "")
                        {
                            lblStockNo.Text = "---";
                        }

                    }
                    ImageButton2.OnClientClick = "";
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load : " + Convert.ToString(ex.Message));
        }
    }

    #endregion

    #region Events

    public void imgbtnLogOut_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            Response.Redirect("vdtLogin.aspx");
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnLogOut_Click : " + Convert.ToString(ex.Message));
        }
    }

    public void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            Cls_VDTWelcome objCls_VDTWelcome1 = new Cls_VDTWelcome();
            objCls_VDTWelcome1.id = Convert.ToInt32(Session["id"]);

            DataSet dsData = new DataSet();
            dsData = objCls_VDTWelcome1.getVDT_DealerStatusByCustomerid();
            if (dsData.Tables[1] != null)
            {
                if (dsData.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(dsData.Tables[1].Rows[0]["block"]) == "1")
                    {
                        lblMessageForModal.Text = "<span style='font-weight:bold;'>There should be " + Convert.ToString(dsData.Tables[1].Rows[0]["blockhour"]) + " hour’s difference between two requests.</span>";
                        lblMessageForModal.Text += "<br /><br />Please contact the dealer directly or Private Fleet if you still have queries outstanding";
                        msgpop.Style.Add("display", "block");
                        pnlmodal.Visible = true;
                        pnlClientReq.Visible = false;
                    }
                    else if (Convert.ToString(dsData.Tables[1].Rows[0]["block"]) != "1")
                    {
                        SaveVDTCustomerRequestDetails();
                    }
                }
                else
                {
                    SaveVDTCustomerRequestDetails();
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnSend_Click : " + Convert.ToString(ex.Message));
        }
    }

    public void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            msgpop.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            logger.Error("btnClose_Click : " + Convert.ToString(ex.Message));
        }
    }

    public void btnTempSend_Click(object sender, EventArgs e)
    {
        try
        {
            Page page = HttpContext.Current.Handler as Page;
            divConfirm.Visible = true;
            if (!txtDesc.Text.Equals(String.Empty))
            {
                pnlSend.Visible = true;
                modalSendMail.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Please enter the text to send.');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnTempSend_Click : " + Convert.ToString(ex.Message));
        }
    }

    public void btnConfirm_Click(object sende, EventArgs e)
    {
        try
        {
            //=================================================================
            //===== Commented By chetan on 23-01-2014
            //objCls_VDTWelcome.description = Convert.ToString(txtDesc.Text.Trim());
            //DataTable dt = (DataTable)ViewState["_VDTDealer"];
            //objCls_VDTWelcome.SendRequestTOPFforHELP(dt);

            //objCls_VDTWelcome.CustomerID = Convert.ToInt32(Session["id"]);
            //objCls_VDTWelcome.DealerID = Convert.ToInt32(dt.Rows[0]["id"]);
            //objCls_VDTWelcome.description = Convert.ToString(txtDesc.Text.Trim());
            //objCls_VDTWelcome.RequestToID = "Admin";
            //objCls_VDTWelcome.Save_VDTCustomerRequestDetails();
           
            tr1 = new Thread(MailForHelp);
            tr1.Start();
            //================================================================
            pnlSend.Visible = false;
            divConfirm.Visible = false;
            modalSendMail.Hide();
            lblmsg.Text = "Email send to Private Fleet Successfully.";
            lblmsg.CssClass = "dbresult";
            tr1.Join(100);
        }
        catch (Exception ex)
        {
            logger.Error("btnConfirm_Click" + Convert.ToString(ex.Message));
        }
    }
    public void MailForHelp()
    {
        DataTable dt=null;
        try
        {
            objCls_VDTWelcome.description = Convert.ToString(txtDesc.Text.Trim());
            dt = (DataTable)ViewState["_VDTDealer"];
            objCls_VDTWelcome.SendRequestTOPFforHELP(dt);

            objCls_VDTWelcome.CustomerID = Convert.ToInt32(Session["id"]);
            objCls_VDTWelcome.DealerID = Convert.ToInt32(dt.Rows[0]["id"]);
            objCls_VDTWelcome.description = Convert.ToString(txtDesc.Text.Trim());
            objCls_VDTWelcome.RequestToID = "Admin";
            objCls_VDTWelcome.Save_VDTCustomerRequestDetails();
        }
        catch (Exception ex)
        {
            logger.Error("btnConfirm_Click" + Convert.ToString(ex.Message));
        }
        finally { 
            dt = null; 
        }
    }
    public void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            divConfirm.Visible = false;
            pnlSend.Visible = true;
            modalSendMail.Show();
        }
        catch (Exception ex)
        {
            logger.Error("btnEdit_Click" + Convert.ToString(ex.Message));
        }
    }

    public void imgBtnConfirm_Click(object sende, EventArgs e)
    {
        try
        {
            divConfirm.Visible = false;
            pnlSend.Visible = true;
            modalSendMail.Show();
        }
        catch (Exception ex)
        {
            logger.Error("imgBtnConfirm_Click" + Convert.ToString(ex.Message));
        }
    }

    public void btnhelp_Click(object sende, EventArgs e)
    {
        try
        {
            Cls_VDTWelcome objCls_VDTWelcome1 = new Cls_VDTWelcome();
            objCls_VDTWelcome1.id = Convert.ToInt32(Session["id"]);

            DataSet dsData = new DataSet();
            dsData = objCls_VDTWelcome1.getVDT_DealerStatusByCustomerid();

            if (dsData.Tables[2] != null)
            {
                if (dsData.Tables[2].Rows.Count > 0)
                {
                    if (Convert.ToString(dsData.Tables[2].Rows[0]["block"]) == "1")
                    {
                        lblMessageForModal.Text = "There should be " + Convert.ToString(dsData.Tables[2].Rows[0]["blockhour"]) + " hour’s difference between two requests.";
                        lblMessageForModal.Font.Bold = true;
                        msgpop.Style.Add("display", "block");
                    }
                    else
                    {
                        ShowMailSendPopUp();
                    }
                }
                else
                {
                    ShowMailSendPopUp();
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnhelp_Click" + Convert.ToString(ex.Message));
        }
    }

    public void btnCancels_Click(object sender, EventArgs e)
    {
        try
        {
            pnlSend.Visible = false;
            divConfirm.Visible = false;
            modalSendMail.Enabled = false;
            modalSendMail.Hide();
        }
        catch (Exception ex)
        {
            logger.Error("btnCancels_Click" + Convert.ToString(ex.Message));
        }
    }

    // 4 mar 2013 - add customer comment to Delivery update status request.
    public void imgbtnClientReq_Click(object sender, EventArgs e)
    {
       // DataTable dt;
        try
        {
            tr1 = new Thread(SendMailToDealer);
            tr1.Start();
            msgpop.Style.Add("display", "none");
            tr1.Join(100);
            #region Commented By Chetan
            //dt = (DataTable)ViewState["_VDTDealer"];

            //objCls_VDTWelcome.CustomerID = Convert.ToInt32(Session["id"]);
            //objCls_VDTWelcome.DealerID = Convert.ToInt32(dt.Rows[0]["id"]);
            //objCls_VDTWelcome.description = "";
            //objCls_VDTWelcome.clientComment = txtEmailMsg.Text.Trim();
            //objCls_VDTWelcome.RequestToID = "Dealer";
            //objCls_VDTWelcome.SendUpdateRequestTODealer(dt);
            //objCls_VDTWelcome.Save_VDTCustomerRequestDetails();

            //pnlmodal.Visible = true;
            //pnlClientReq.Visible = false;
            #endregion
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnClientReq_Click error : " + Convert.ToString(ex.Message));
        }        
    }
    /// <summary>
    /// Change By chetan for multiple click by customer
    /// on date :- 23-01-2014
    /// </summary>
    /// ========================================================
    public void SendMailToDealer()
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["_VDTDealer"];

            objCls_VDTWelcome.CustomerID = Convert.ToInt32(Session["id"]);
            objCls_VDTWelcome.DealerID = Convert.ToInt32(dt.Rows[0]["id"]);
            objCls_VDTWelcome.description = "";
            objCls_VDTWelcome.clientComment = txtEmailMsg.Text.Trim();
            objCls_VDTWelcome.RequestToID = "Dealer";
            objCls_VDTWelcome.SendUpdateRequestTODealer(dt);
            objCls_VDTWelcome.Save_VDTCustomerRequestDetails();

            pnlmodal.Visible = true;
            pnlClientReq.Visible = false;
        }
        catch (Exception ex) {
            logger.Error("Mail Sending thread SendMailToDealer error : " + Convert.ToString(ex.Message));
        }
        finally
        {
            dt = null;
        }
    }
    //======================================================================
    public void imgbtnCancel_Click(object sender, EventArgs e)
    {
        pnlmodal.Visible = false;
        pnlClientReq.Visible = true;
        msgpop.Style.Add("display", "none");
    }

    #endregion

    #region Methods

    private void ShowMailSendPopUp()
    {
        try
        {
            txtDesc.Text = "";
            divConfirm.Visible = false;
            pnlSend.Visible = true;
            modalSendMail.Enabled = true;
            modalSendMail.Show();
        }
        catch (Exception ex)
        {
            logger.Error("ShowMailSendPoopUp" + Convert.ToString(ex.Message));
        }
    }

    private void SaveVDTCustomerRequestDetails()
    {
        DataTable dt = null;
        try
        {
            dt = (DataTable)ViewState["_VDTDealer"];

            // on 4 mar 2013
            //objCls_VDTWelcome.CustomerID = Convert.ToInt32(Session["id"]);
            //objCls_VDTWelcome.DealerID = Convert.ToInt32(dt.Rows[0]["id"]);
            //objCls_VDTWelcome.description = "";
            //objCls_VDTWelcome.RequestToID = "Dealer";
            //objCls_VDTWelcome.SendUpdateRequestTODealer(dt);
            //objCls_VDTWelcome.Save_VDTCustomerRequestDetails();

            lblmsg.Text = "Email send to " + Convert.ToString(dt.Rows[0]["name"]) + " Dealer Successfuly.";
            lblMessageForModal.Text = "A request for an update has been sent directly to the supplying dealer. If you are having any difficulty in getting a satisfactory response please feel free to contact the dealer directly through the email/phone numbers above or by contacting Private Fleet";

            lblmsg.CssClass = "dbresult";
            msgpop.Style.Add("display", "block");
            chk = true;
        }
        catch (Exception ex)
        {
            logger.Error("SaveVDTCustomerRequestDetails : " + Convert.ToString(ex.Message));
        }
        finally
        {
            dt = null;
        }
    }
    #endregion
}
