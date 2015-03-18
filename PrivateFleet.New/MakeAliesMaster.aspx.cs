using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Mechsoft.GeneralUtilities;

public partial class MakeAliesMaster : System.Web.UI.Page
{
    Cls_Alies objAlies = new Cls_Alies();
    protected void Page_Load(object sender, EventArgs e)
    {
       Label lblHeader = (Label)Master.FindControl("lblHeader");
       lblHeader.Text = "Make Alies";
        //((Label)Master.FindControl("lblHeader")).Text = "Handling Fee Master";

        if (!IsPostBack)
        {
            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvMakeAlies.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());

            ddl_NoRecords.Visible = false;
            lblRowsToDisplay.Visible = false;
        }

        if (lblHeader != null)
           // lblHeader.Text = "Completed quotation Report";
        BindData();
        
    }


    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            objAlies.Make = Convert.ToInt32(((DropDownList)gvMakeAlies.FooterRow.FindControl("ddlMakes")).SelectedValue.ToString());
            objAlies.Alies = ((TextBox)gvMakeAlies.FooterRow.FindControl("txtAlies")).Text.ToString();

            int result = objAlies.AddAlies();

            if (result == 1)
                lblResult.Text = "Alies Added Successfully";
            else
                lblResult.Text = " Alies Addition Failed";
            BindData();
        }
        catch (Exception ex)
        { }
    }
    public void BindData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objAlies.GetAlies();
            ViewState["dt"] = dt;

            if (dt.Rows.Count == 0)
            {
                ddl_NoRecords.Visible = false;
                lblRowsToDisplay.Visible = false;
            }
            else
            {
                ddl_NoRecords.Visible = true;
                lblRowsToDisplay.Visible = true;
            }


            gvMakeAlies.DataSource = dt;
            gvMakeAlies.DataBind();
            BindDropDrown(((DropDownList)gvMakeAlies.FooterRow.FindControl("ddlMakes")), "0");
        }
        catch (Exception ex)
        { }
    }

    private void BindDropDrown(DropDownList ddlBind, string p)
    {

        DataTable dt = new DataTable();
        dt = objAlies.GetMake();
        ddlBind.DataSource = dt;
        ddlBind.DataTextField = "Make";
        ddlBind.DataValueField = "ID";
        ddlBind.DataBind();
        ddlBind.Items.Insert(0, "-Select-");
    }
    protected void gvMakeAlies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMakeAlies.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gvMakeAlies_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gvMakeAlies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "gridactiverow";
        }
    }


    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {

            gvMakeAlies.DataSource = (DataTable)ViewState["dt"];
            gvMakeAlies.PageSize = gvMakeAlies.PageCount * gvMakeAlies.Rows.Count;
            gvMakeAlies.DataBind();

        }
        else
        {
            gvMakeAlies.DataSource = (DataTable)ViewState["dt"];
            gvMakeAlies.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvMakeAlies.DataBind();

        }
    }
}
