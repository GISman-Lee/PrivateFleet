/// Filename    : ServiceFeeMstr.aspx.cs
/// Author      : Kalpana @ MechSoftGroup.com.
/// Date        : 04-11-2011
/// Purpose     : Code for Freight Cost Details
/// History     : 
/// -----------------------------------------------------------------------------------------
/// Sr.No.		Date                    Author		        Comments
/// -----------------------------------------------------------------------------------------
///   1.		04-11-2011	           Kalpana		        Intial Version
///==================================================================================================*/
///</history>
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;
using System.Drawing;

public partial class FreightCostdetails : System.Web.UI.Page
{
    #region Variables
    static ILog logger = LogManager.GetLogger(typeof(FreightCostdetails));
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)Master.FindControl("lblHeader")).Text = "Freight Cost Master";
        if (!IsPostBack)
        {
            lblMsg.Text = "";

            BindData();


        }
    }
    #region Methods
    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 04-11- 2011
    /// Description  : Method to bind freight details
    /// </summary>
    public void BindData()
    {
        Cls_FreightCost objFC = new Cls_FreightCost();
        DataTable dt = new DataTable();
        dt = objFC.getFreightCost();


        gvFreightCost.DataSource = dt;
        gvFreightCost.DataBind();
    }
    #endregion

    #region Events
    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 04-11- 2011
    /// Description  : Method to edit row of freight details
    /// </summary>
    protected void gvFreightCost_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            //set new edit index for edit mode
            gvFreightCost.EditIndex = e.NewEditIndex;

            //bind grid data
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvFreightCost_RowEditing" + ex.Message);
        }
    }

    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 04-11- 2011
    /// Description  : Method to update row of freight details
    /// </summary>
    protected void gvFreightCost_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Cls_FreightCost objFC = new Cls_FreightCost();
        try
        {
            int intFrmId = Convert.ToInt32(gvFreightCost.DataKeys[e.RowIndex].Values["fromSID"]);
            TextBox txtEditACT = (TextBox)gvFreightCost.Rows[e.RowIndex].FindControl("txtEditACT");
            TextBox txtEditNSW = (TextBox)gvFreightCost.Rows[e.RowIndex].FindControl("txtEditNSW");
            TextBox txtEditQLD = (TextBox)gvFreightCost.Rows[e.RowIndex].FindControl("txtEditQLD");
            TextBox txtEditSA = (TextBox)gvFreightCost.Rows[e.RowIndex].FindControl("txtEditSA");
            TextBox txtEditTSA = (TextBox)gvFreightCost.Rows[e.RowIndex].FindControl("txtEditTAS");
            TextBox txtEditVIC = (TextBox)gvFreightCost.Rows[e.RowIndex].FindControl("txtEditVIC");
            TextBox txtEditWA = (TextBox)gvFreightCost.Rows[e.RowIndex].FindControl("txtEditWA");

            //state id
            objFC.SId = intFrmId;
            objFC.ACTcost = Convert.ToDouble(txtEditACT.Text.Trim());
            objFC.NSWcost = Convert.ToDouble(txtEditNSW.Text.Trim());
            objFC.QLDcost = Convert.ToDouble(txtEditQLD.Text.Trim());
            objFC.SAcost = Convert.ToDouble(txtEditSA.Text.Trim());
            objFC.TAScost = Convert.ToDouble(txtEditTSA.Text.Trim());
            objFC.VICcost = Convert.ToDouble(txtEditVIC.Text.Trim());
            objFC.WAcost = Convert.ToDouble(txtEditWA.Text.Trim());

            //update service fee
            bool result = objFC.UpdateFreightCost();

            if (result)
                lblMsg.Text = "Role updated successfully";
            else
                lblMsg.Text = "Error Occured.. Please try again";

            //make grid non-editable
            gvFreightCost.EditIndex = -1;

            //bind grid data
            BindData();



        }
        catch (Exception ex)
        {
            logger.Error("imgBtnUpdate err" + ex.Message);
        }

    }

    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 04-11- 2011
    /// Description  : Method to cancel the changes
    /// </summary>
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Make grid non-editable
            gvFreightCost.EditIndex = -1;

            //bind grid data
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("Cancle click" + ex.Message);
        }
    }


    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 07-11- 2011
    /// Description  : Method to get data upto two values after decimal
    /// </summary>
    protected void gvFreightCost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "gridactiverow";

                #region Commented
                //Label lblACT = (Label)(e.Row.FindControl("lblACT"));
                //lblACT.Text = string.Format("{0:F2}", Convert.ToDouble(lblACT.Text));

                //Label lblNSW = (Label)(e.Row.FindControl("lblNSW"));
                //lblNSW.Text = string.Format("{0:F2}", Convert.ToDouble(lblNSW.Text));

                //Label lblQLD = (Label)(e.Row.FindControl("lblQLD"));
                //lblQLD.Text = string.Format("{0:F2}", Convert.ToDouble(lblQLD.Text));

                //Label lblSA = (Label)(e.Row.FindControl("lblSA"));
                //lblSA.Text = string.Format("{0:F2}", Convert.ToDouble(lblSA.Text));

                //Label lblTAS = (Label)(e.Row.FindControl("lblTAS"));
                //lblTAS.Text = string.Format("{0:F2}", Convert.ToDouble(lblTAS.Text));

                //Label lblVIC = (Label)(e.Row.FindControl("lblVIC"));
                //lblVIC.Text = string.Format("{0:F2}", Convert.ToDouble(lblVIC.Text));

                //Label lblWA = (Label)(e.Row.FindControl("lblWA"));
                //lblWA.Text = string.Format("{0:F2}", Convert.ToDouble(lblWA.Text));
                #endregion

                //by manoj
                int icell = e.Row.Cells.Count;
                for (int i = 1; i < icell; i++)
                {

                    if (e.Row.RowIndex == this.gvFreightCost.EditIndex)
                    {
                        TextBox txt = (TextBox)e.Row.Cells[i].Controls[1];
                        txt.Text = string.Format("{0:F2}", Convert.ToDouble(txt.Text));
                    }
                    else
                    {
                        Label lbl = (Label)e.Row.Cells[i].Controls[1];
                        lbl.Text = string.Format("{0:F2}", Convert.ToDouble(lbl.Text));

                        if (Convert.ToDouble(lbl.Text) == Convert.ToDouble(0))
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Update Click" + ex.Message);
        }

    }
    #endregion
}
