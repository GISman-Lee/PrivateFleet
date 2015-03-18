using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;

public partial class Calculator : System.Web.UI.Page
{
    #region Variable
    static ILog logger = LogManager.GetLogger(typeof(Calculator));
    DataTable dt;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)Master.FindControl("lblHeader")).Text = "Calculator";
        if (!IsPostBack)
        {
            FillState();
        }
    }

    public void FillState()
    {
        Cls_CompletedQuoatationReportHelper objReport = new Cls_CompletedQuoatationReportHelper();
        try
        {

            dt = objReport.GetStates();

            dt.Rows.RemoveAt(5);

            ddlDeparture.DataSource = dt;
            ddlDeparture.DataBind();
            ddlDeparture.Items.Insert(0, new ListItem("-Select State-", "0"));

            ddlArrival.DataSource = dt;
            ddlArrival.DataBind();
            ddlArrival.Items.Insert(0, new ListItem("-Select State-", "0"));



        }
        catch (Exception ex)
        {
            logger.Error("Fill State Err - " + ex.Message);
        }
        finally
        {
            objReport = null;
            dt = null;
        }
    }

    protected void ddlArrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ddlArrival.SelectedItem).ToUpper() == "QLD")
        {
            ddlCylinders.Visible = true;
            ddlGreenStar.Visible = false;
            ddlGreenStar.SelectedValue = "-1";
            hlink.Visible = false;
        }
        else if (Convert.ToString(ddlArrival.SelectedItem).ToUpper() == "ACT")
        {
            ddlGreenStar.Visible = true;
            ddlCylinders.Visible = false;
            ddlCylinders.SelectedValue = "-1";
            hlink.Visible = true;
        }
        else
        {
            ddlCylinders.SelectedValue = "-1";
            ddlCylinders.Visible = false;
            ddlGreenStar.SelectedValue = "-1";
            ddlGreenStar.Visible = false;
            hlink.Visible = false;
        }

    }

    protected void imgCalculate_Click(object sender, ImageClickEventArgs e)
    {
        pnlCalculator.Visible = true;
        Cls_Calculator objCalculator = new Cls_Calculator();
        string FromState = Convert.ToString(ddlDeparture.SelectedItem).ToUpper();
        string ToState = Convert.ToString(ddlArrival.SelectedItem).ToUpper();
        double PurchasePrice = Convert.ToDouble(txtPP.Text);
        double OnRoadPrice = 0;
        try
        {
            objCalculator.FromStateID = Convert.ToInt32(ddlDeparture.SelectedValue);
            objCalculator.ToStateID = Convert.ToInt32(ddlArrival.SelectedValue);
            dt = objCalculator.GetCharges();

            // Switch used to calculate onroad price of vehicle for different state
            switch (Convert.ToString(ddlArrival.SelectedItem).ToUpper())
            {
                case "NSW":
                    OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 45000) > 0 ? (PurchasePrice - 45000) * 0.015 : 0);
                    break;

                case "VIC":
                    //OnRoadPrice = if(PurchasePrice<59133) then (PurchasePrice*0.03) Else PurchasePrice*0.05 [[In technical Terms- (PurchasePrice * 0.03) + ((PurchasePrice - 59133) > 0 ? (PurchasePrice - 59133) * 0.02 : 0);]]
                    //Commented on 19 May 14 from 59133 to 60316 Threshold By: Ayyaj 
                    //OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 59133) > 0 ? (PurchasePrice - 59133) * 0.02 : 0);
                    //Changed on 6 Dec 2013 from 57466 to 59113 
                    //OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 59113) > 0 ? (PurchasePrice - 59113) * 0.02 : 0);
                    // Changed on 6 Dec 2013 from 57466 to 59113 
                    //OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 57466) > 0 ? (PurchasePrice - 57466) * 0.02 : 0);
                    // Changed on 22 Jun 12 from 5% to 3% 
                    //  OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 57009) > 0 ? (PurchasePrice - 57009) * 0.03 : 0);
                    // Changed on 19 May 14 from 59133 to 60316 Threshold By: Ayyaj 
                    //OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 60316) > 0 ? (PurchasePrice - 60316) * 0.02 : 0);
                    // Changed on 05 June 14  By: Ayyaj 
                    //OnRoadPrice = (PurchasePrice > 60316) ? PurchasePrice * 0.05 : PurchasePrice * 0.03;
                    // Changed on 01 July 14  By: Ayyaj threshould 60316 
                    OnRoadPrice = (PurchasePrice > 61884) ? PurchasePrice * 0.05 : PurchasePrice * 0.03;
                    break;

                case "ACT":
                    // OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 45000) > 0 ? (PurchasePrice - 45000) * 0.015 : 0);
                    int GreenStars = Convert.ToInt32(ddlGreenStar.SelectedValue);
                    if (GreenStars == 0)
                        OnRoadPrice = 0;
                    else if (GreenStars == 2)
                        OnRoadPrice = (PurchasePrice * 0.02) + ((PurchasePrice - 45000) > 0 ? (PurchasePrice - 45000) * 0.02 : 0);
                    else if (GreenStars == 3)
                        OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 45000) > 0 ? (PurchasePrice - 45000) * 0.02 : 0);
                    else if (GreenStars == 4)
                        OnRoadPrice = (PurchasePrice * 0.04) + ((PurchasePrice - 45000) > 0 ? (PurchasePrice - 45000) * 0.02 : 0);
                    break;

                case "QLD":
                    int Cylinder = Convert.ToInt32(ddlCylinders.SelectedValue);
                    if (Cylinder == 0)
                        OnRoadPrice = PurchasePrice * 0.02;
                    else if (Cylinder == 4)
                        OnRoadPrice = PurchasePrice * 0.03;
                    else if (Cylinder == 6)
                        OnRoadPrice = PurchasePrice * 0.035;
                    else if (Cylinder == 8)
                        OnRoadPrice = PurchasePrice * 0.04;
                    break;

                case "WA":
                    if (PurchasePrice <= 25000)
                        OnRoadPrice = PurchasePrice * 0.275;
                    else if (PurchasePrice <= 50000)
                        OnRoadPrice = ((((PurchasePrice - 25000) / 6666) + 2.75) / 100) * PurchasePrice;
                    else
                        OnRoadPrice = PurchasePrice * 0.065;
                    break;

                case "TAS":
                    if (PurchasePrice > 40000)
                        OnRoadPrice = PurchasePrice * 0.04;
                    else
                        OnRoadPrice = PurchasePrice * 0.03;
                    if (PurchasePrice < 40000 && PurchasePrice > 35000)
                        OnRoadPrice = OnRoadPrice + ((PurchasePrice - 35000) * 0.11);
                    break;

                case "SA":
                    OnRoadPrice = PurchasePrice * 0.04;
                    break;

                default:
                    break;
            }

            lblRegoCTP_ans.Text = Convert.ToString(dt.Rows[0]["RegoCTP"]);
            lblStampDuty_ans.Text = Convert.ToString(OnRoadPrice);

            OnRoadPrice = OnRoadPrice + Convert.ToDouble(dt.Rows[0]["RegoCTP"]);

            lblfCharges_ans.Text = Convert.ToString(dt.Rows[0]["FreightCost"]);
            lblHandlingFees_ans.Text = Convert.ToString(dt.Rows[0]["HandlingFee"]);
            lblTotal_ans.Text = string.Format("{0:N2}", (Convert.ToDouble(dt.Rows[0]["FreightCost"]) + Convert.ToDouble(dt.Rows[0]["HandlingFee"]) + OnRoadPrice));

            

        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }

    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string strWindowParams = "menubar=no,scrollbars=yes,status=no,toolbar=no,resizable=yes,left=200,top=20,width=700,height=450";
        string strSCRIPT = "window.open('PrintCalculator.aspx?Departure=" + ddlDeparture.SelectedItem + "&DepartureValue=" + ddlDeparture.SelectedValue + " &Arrival=" + ddlArrival.SelectedItem + "&ArrivalValue=" + ddlArrival.SelectedValue + " &Cylinder=" + ddlCylinders.SelectedItem + "&CylinderValue=" + ddlCylinders.SelectedValue + " &PP=" + txtPP.Text + " &Stars=" + ddlGreenStar.SelectedItem + " &StarValue=" + ddlGreenStar.SelectedValue + "','my_win','" + strWindowParams + "')";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "_deo", strSCRIPT, true);

    }
}
