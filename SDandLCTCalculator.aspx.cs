using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using log4net;

public partial class SDandLCTCalculator : System.Web.UI.Page
{

    #region Variable
    static ILog logger = LogManager.GetLogger(typeof(SDandLCTCalculator));
    DataTable dt;
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)Master.FindControl("lblHeader")).Text = "Stamp Duty, Luxury Car Tax and Invoice Amount Calculator";
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    #endregion

    #region Methods

    public void BindGrid()
    {
        Cls_CompletedQuoatationReportHelper objReport = new Cls_CompletedQuoatationReportHelper();
        try
        {

            dt = objReport.GetStates();
            dt.Rows.RemoveAt(5);

            gvSDandLctCalc.DataSource = dt;
            gvSDandLctCalc.DataBind();
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

    #endregion

    #region Events

    protected void gvSDandLctCalc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            String StateIds = "3,5,7";
            HiddenField hdfStateID = ((HiddenField)e.Row.FindControl("hdfStateID"));
            if (StateIds.Contains(Convert.ToString(hdfStateID.Value)))
            {
                Label lblState = ((Label)e.Row.FindControl("lblState"));
                lblState.Text = lblState.Text + " <b><span style='color:Red; font-size:18px;'>*</span><b/>";
            }
            if (Convert.ToInt32(hdfStateID.Value) == 3)
            {
                DropDownList ddlCylinders = (DropDownList)e.Row.FindControl("ddlCylinders");
                ddlCylinders.Visible = true;
            }
        }
    }

    protected void txtPurchasePrice_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
        HiddenField hdfStateID = (HiddenField)gr.FindControl("hdfStateID");
        TextBox txtPurchasePrice = (TextBox)gr.FindControl("txtPurchasePrice");
        TextBox txtDifference = (TextBox)gr.FindControl("txtDifference");

        Label lblStampDutyPayable = (Label)gr.FindControl("lblStampDutyPayable");
        Label lblLuxuryCarTax = (Label)gr.FindControl("lblLuxuryCarTax");
        Label lblAmtInvoice = (Label)gr.FindControl("lblAmtInvoice");

        string PurchasePrice_1, Difference_1;
        Int64 PurchasePrice, Difference;

        try
        {
            PurchasePrice_1 = (txtPurchasePrice.Text.Trim().Equals(String.Empty) ? "0" : txtPurchasePrice.Text);
            Difference_1 = (txtDifference.Text.Trim().Equals(String.Empty) ? "0" : txtDifference.Text);
            PurchasePrice = Convert.ToInt64(PurchasePrice_1);
            Difference = Convert.ToInt64(Difference_1);

            double StampDutyPayable = 0, LuxuryCarTax = 0, AmounttoInvoice = 0;

            switch (Convert.ToInt32(hdfStateID.Value))
            {
                //For ACT
                case 1:
                    StampDutyPayable = (PurchasePrice > 45000) ? (PurchasePrice - 45000) * 0.05 + 1350 : PurchasePrice * 0.03;
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.35 : (PurchasePrice < 59133 && PurchasePrice > 45000) ? Difference / 1.05 : Difference / 1.03;
                    break;

                //For NSW
                case 2:
                    StampDutyPayable = (PurchasePrice > 45000) ? (PurchasePrice - 45000) * 0.05 + 1350 : PurchasePrice * 0.03;
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.35 : (PurchasePrice < 59133 && PurchasePrice > 45000) ? Difference / 1.05 : Difference / 1.03;
                    break;

                //For QLD
                case 3:
                    double Cylinders = Convert.ToDouble(((DropDownList)gr.FindControl("ddlCylinders")).SelectedValue);
                    double O5 = (Cylinders == 0) ? 0 : Cylinders;
                    StampDutyPayable = PurchasePrice * 0.02 * O5;
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.3 : Difference;
                    break;

                //For VIC
                case 4:
                    StampDutyPayable = (PurchasePrice > 57000) ? PurchasePrice * 0.05 : PurchasePrice * 0.03;
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.35 : Difference / 1.03;
                    break;

                //FOR SA
                case 5:
                    StampDutyPayable = (PurchasePrice > 3000) ? (PurchasePrice - 3000) * 0.04 + 60 : 0;
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.3 : Difference;
                    break;

                //For WA
                case 7:
                    double H6 = 2.75 + (PurchasePrice - 20000) / 6666.666;
                    StampDutyPayable = (PurchasePrice == 0) ? 0 : (PurchasePrice < 45000) ? (PurchasePrice * H6) / 100 : PurchasePrice * 0.065;
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.3 : Difference;
                    break;

                //For TAS
                case 8:
                    StampDutyPayable = (PurchasePrice < 35000) ? PurchasePrice * 0.03 : (PurchasePrice > 35000 && PurchasePrice < 40000) ? 1050 + 0.11 * (PurchasePrice - 35000) : 1600 + 0.04 * (PurchasePrice - 45000);
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.34 : (PurchasePrice < 35000) ? Difference / 1.03 : (PurchasePrice > 41000) ? Difference / 1.04 : -1;
                    break;
            }

            //LuxuryCarTax formula is same for all State
            //change on 2 july 2013 From 59133 to 60316.00
            LuxuryCarTax = (PurchasePrice > 60316) ? (PurchasePrice - 60316) * 0.33 / 1.1 : 0;

            // Assigning the value to lable
            lblStampDutyPayable.Text = "$ " + Convert.ToString(Math.Round(StampDutyPayable));
            lblLuxuryCarTax.Text = "$ " + Convert.ToString(Math.Round(LuxuryCarTax));
            lblAmtInvoice.Text = "$ " + Convert.ToString(Math.Round(AmounttoInvoice));
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void txtDifference_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
        HiddenField hdfStateID = (HiddenField)gr.FindControl("hdfStateID");
        TextBox txtPurchasePrice = (TextBox)gr.FindControl("txtPurchasePrice");
        TextBox txtDifference = (TextBox)gr.FindControl("txtDifference");

        Label lblAmtInvoice = (Label)gr.FindControl("lblAmtInvoice");

        Int64 PurchasePrice, Difference;
        string PurchasePrice_1, Difference_1;
        try
        {
            PurchasePrice_1 = (txtPurchasePrice.Text.Trim().Equals(String.Empty) ? "0" : txtPurchasePrice.Text);
            Difference_1 = (txtDifference.Text.Trim().Equals(String.Empty) ? "0" : txtDifference.Text);
            PurchasePrice = Convert.ToInt64(PurchasePrice_1);
            Difference = Convert.ToInt64(Difference_1);

            double AmounttoInvoice = 0;

            switch (Convert.ToInt32(hdfStateID.Value))
            {
                //For ACT
                case 1:
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.35 : (PurchasePrice < 59133 && PurchasePrice > 45000) ? Difference / 1.05 : Difference / 1.03;
                    break;

                //For NSW
                case 2:
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.35 : (PurchasePrice < 59133 && PurchasePrice > 45000) ? Difference / 1.05 : Difference / 1.03;
                    break;

                //For QLD
                case 3:
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.3 : Difference;
                    break;

                //For VIC
                case 4:
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.35 : Difference / 1.03;
                    break;

                //FOR SA
                case 5:
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.3 : Difference;
                    break;

                //For WA
                case 7:
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.3 : Difference;
                    break;

                //For TAS
                case 8:
                    AmounttoInvoice = (PurchasePrice > 59133) ? Difference / 1.34 : (PurchasePrice < 35000) ? Difference / 1.03 : (PurchasePrice > 41000) ? Difference / 1.04 : -1;
                    break;
            }

            // Assigning the value to lable
            lblAmtInvoice.Text = "$ " + Convert.ToString(Math.Round(AmounttoInvoice));
        }
        catch (Exception ex)
        {
            logger.Error("txtDifference_TextChanged error - " + ex.Message);
        }
    }

    protected void ddlCylinders_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtPurchasePrice_TextChanged(sender, e);
        }
        catch (Exception ex)
        {
            logger.Error("ddlCylinders_SelectedIndexChanged Error - " + ex.Message);
        }

    }

    #endregion

}
