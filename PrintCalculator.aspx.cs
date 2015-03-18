using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PrintCalculator : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDeparture_1.Text = Convert.ToString(Request.QueryString["Departure"]);
            lblArrival_1.Text = Convert.ToString(Request.QueryString["Arrival"]);
            if (lblArrival_1.Text.ToUpper() == "QLD")
            {
                lblCylinder.Visible = true;
                lblCylinder.Text = "Cylinder: " + Convert.ToString(Request.QueryString["Cylinder"]);
            }
            else if (lblArrival_1.Text.ToUpper() == "ACT")
            {
                lblCylinder.Visible = true;
                lblCylinder.Text = "Green Stars: " + Convert.ToString(Request.QueryString["Stars"]);
            }
            else
                lblCylinder.Visible = false;

            lblPP_1.Text = Convert.ToString(Request.QueryString["PP"]);

            calculate();
        }
    }

    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void calculate()
    {
        Cls_Calculator objCalculator = new Cls_Calculator();
        string FromState = Convert.ToString(lblDeparture_1.Text).ToUpper();
        string ToState = Convert.ToString(lblArrival_1.Text).ToUpper();
        double PurchasePrice = Convert.ToDouble(lblPP_1.Text);
        double OnRoadPrice = 0;
        DataTable dt = null;
        try
        {
            objCalculator.FromStateID = Convert.ToInt32(Request.QueryString["DepartureValue"]);
            objCalculator.ToStateID = Convert.ToInt32(Request.QueryString["ArrivalValue"]);
            dt = objCalculator.GetCharges();

            // Switch used to calculate onroad price of vehicle for different state
            switch (Convert.ToString(lblArrival_1.Text).ToUpper())
            {
                case "NSW":
                    OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 45000) > 0 ? (PurchasePrice - 45000) * 0.015 : 0);
                    break;

                case "VIC":
                    OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 57466) > 0 ? (PurchasePrice - 57466) * 0.02 : 0);
                    break;

                case "ACT":
                    // OnRoadPrice = (PurchasePrice * 0.03) + ((PurchasePrice - 45000) > 0 ? (PurchasePrice - 45000) * 0.015 : 0);
                    int GreenStars = Convert.ToInt32(Request.QueryString["StarValue"]);
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
                    int Cylinder = Convert.ToInt32(Request.QueryString["CylinderValue"]);
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
}
