
/// Filename    : ServiceFeeMstr.aspx.cs
/// Author      : Kalpana @ MechSoftGroup.com.
/// Date        : 04-11-2011
/// Purpose     : Code for Handling Service Fee
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


public partial class ServiceFeeMstr : System.Web.UI.Page
{
    #region Variables
    static ILog logger = LogManager.GetLogger(typeof(ServiceFeeMstr));
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)Master.FindControl("lblHeader")).Text = "Handling Fee Master";

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
    /// Description  : Method to bind all service fee
    /// </summary>
    public void BindData()
    {
        Cls_ServiceFeeMaster objSF = new Cls_ServiceFeeMaster();
        DataTable dt = new DataTable();
        dt = objSF.getServiceFee();
        //  gv.DataSource = dt;
        //  gv.DataBind();

        gvServiceFee.DataSource = dt;
        gvServiceFee.DataBind();
    }
    #endregion

    #region Events
    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 04-11- 2011
    /// Description  : Method to edit row of service fee
    /// </summary>
    protected void gvServiceFee_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            //set new edit index for edit mode
            gvServiceFee.EditIndex = e.NewEditIndex;

            //bind grid data
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvServiceFee_RowEditing" + ex.Message);
        }
    }

    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 04-11- 2011
    /// Description  : Method to update row of service fee
    /// </summary>
    protected void gvServiceFee_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Cls_ServiceFeeMaster objFee = new Cls_ServiceFeeMaster();

        try
        {
            int intStateId = Convert.ToInt32(gvServiceFee.DataKeys[e.RowIndex].Values["ID"]);
            TextBox txtEditFee = (TextBox)gvServiceFee.Rows[e.RowIndex].FindControl("txtEditFee");
            TextBox intEditRegoCTP = (TextBox)gvServiceFee.Rows[e.RowIndex].FindControl("txtEditRegoCTP");

            //state id
            objFee.Id = intStateId;
            objFee.fee = Convert.ToDouble(txtEditFee.Text.Trim());
            objFee.regoCTP = Convert.ToInt16(intEditRegoCTP.Text.Trim());

            //update service fee
            bool result = objFee.UpdateFee();

            if (result)
                lblMsg.Text = "Role updated successfully";
            else
                lblMsg.Text = "Error Occured.. Please try again";

            //make grid non-editable
            gvServiceFee.EditIndex = -1;

            //bind grid data
            BindData();


        }
        catch (Exception ex)
        {
            logger.Error("imgbtnCancel_Click err" + ex.Message);
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
            //System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            //if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    int a = 0;
            //}

            //Make grid non-editable
            gvServiceFee.EditIndex = -1;

            //bind grid data
            BindData();
            lblMsg.Text = "";

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

    protected void gvServiceFee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "gridactiverow";

                //Convert.ToDouble(d.ToString("0.000000000000E00"));
                //labelValue.Text = val.ToString("#0.###");
                //Label txtEditACT = (Label)(e.Row.FindControl("txtEditACT"));

                //  Label lblValue = (Label)(e.Row.FindControl("lblValue"));
                //  lblValue.Text = string.Format("{0:F2}", Convert.ToDouble(lblValue.Text));


            }
        }
        catch (Exception ex)
        {
            logger.Error("Update Click" + ex.Message);
        }

    }

    #endregion
}
