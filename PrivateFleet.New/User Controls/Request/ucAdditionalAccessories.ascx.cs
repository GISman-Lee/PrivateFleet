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
using Mechsoft.FleetDeal;
using log4net;

public partial class User_Controls_Request_ucAdditionalAccessories : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_Request_ucAdditionalAccessories));
    static DataTable dtSelectedAcc = null;//Static
    static DataTable dtSelectedAccClone = null;//Static

    #region "Variables and Properties"
    private int _seriesId;
    /// <summary>
    /// Unique Identifier for Series
    /// </summary>
    public int SeriesId
    {
        get { return _seriesId; }
        set { _seriesId = value; }
    }

    private DataTable _dtAccessories;
    public DataTable dtAccessories
    {
        get
        {
            if (dtSelectedAcc != null)
                _dtAccessories = dtSelectedAcc;
            return _dtAccessories;
        }
        set
        {
            _dtAccessories = value;
        }
    }
    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BuildDataTable();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event : " + ex.Message);
        }
    }
    #endregion

    #region "Methods"
    /// <summary>
    /// Fill additional accessories dropdown
    /// </summary>
    public void FillAdditionalAccessories()
    {
        logger.Debug("FillAdditionalAccessories Method Start");
        Cls_Accessories objHelper = new Cls_Accessories();
        DataTable dt = null;
        ViewState["SeriesID"] = SeriesId;
        Boolean tblChanged = false;
        try
        {
            if (ViewState["dtAccessories"] == null || (!ViewState["SeriesID"].ToString().Equals(Session["SeriesId"].ToString())))
            {
                //get accessories other than series default accessories
                dt = objHelper.GetAdditionalAccessoriesForSeries();

                ViewState["dtAccessories"] = dt;

                Session["SeriesID"] = ViewState["SeriesID"].ToString();

                ClearSelectionOfNonAssocitatedElements();

            }
            else
            {
                dt = (DataTable)ViewState["dtAccessories"];
            }

            //clear accessories dropdown
            ddlAccessories.Items.Clear();

            //bind accessories to dropdown
            ddlAccessories.DataSource = dt;
            ddlAccessories.DataTextField = "Accessory";
            ddlAccessories.DataValueField = "ID";
            ddlAccessories.DataBind();

            //insert default item in dropdown
            ddlAccessories.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlAccessories.Items.Add(new ListItem("Add New Accessory", "Add New"));
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("FillAdditionalAccessories Method End");
        }
    }

    private void ClearSelectionOfNonAssocitatedElements()
    {
        if (ViewState["dtAccessories"] != null)
        {
            try
            {
                int Count = dtSelectedAcc.Rows.Count;
                String ValueToCompare = null;
                DataTable dt = (DataTable)ViewState["dtAccessories"];
                if (dtSelectedAcc.Rows.Count > 0)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        ValueToCompare = dtSelectedAcc.Rows[i]["ID"].ToString();
                        Boolean IsFound = false;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (Convert.ToBoolean(dtSelectedAcc.Rows[i]["IsDBDriven"]))
                            {
                                if (ValueToCompare.Equals(dt.Rows[j]["ID"].ToString()))
                                {
                                    IsFound = true;
                                }
                            }

                        }
                        if (Convert.ToBoolean(dtSelectedAcc.Rows[i]["IsDBDriven"]))
                        {
                            if (!IsFound)
                            {
                                dtSelectedAcc.Rows.RemoveAt(i);
                            }
                        }
                    }
                }
            }

            catch (Exception Ex)
            {
            }
            BindAdditionalAccessories();
        }
    }

    /// <summary>
    /// create datatable for selected additional accessories
    /// </summary>
    private void BuildDataTable()
    {
        dtSelectedAcc = new DataTable();
        logger.Debug("Method Start : BuildDataTable");

        try
        {
            dtSelectedAcc.Columns.Add("ID");
            dtSelectedAcc.Columns.Add("AccessoryName");
            dtSelectedAcc.Columns.Add("Specification");
            dtSelectedAcc.Columns.Add("IsDBDriven");
            dtSelectedAcc.Columns[1].Unique = true;
            ClearConstraintsOfDataTable(dtSelectedAcc);

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End : BuildDataTable");
        }
    }

    private void ClearConstraintsOfDataTable(DataTable dtSelectedAcc)
    {
        dtSelectedAcc.Columns["IsDBDriven"].AllowDBNull = true;
        dtSelectedAcc.Columns["AccessoryName"].AllowDBNull = true;
        dtSelectedAcc.Columns["Specification"].AllowDBNull = true;
        dtSelectedAcc.Columns[0].AllowDBNull = true;
    }

    /// <summary>
    /// Method to bind addtional accessories to grid
    /// </summary>
    private void BindAdditionalAccessories()
    {
        logger.Debug("Method Start : BindAdditionalAccessories");

        try
        {
            //bind selected additional accessories to grid
            gvAccessories.DataSource = dtSelectedAcc;
            gvAccessories.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End : BindAdditionalAccessories");
        }
    }
    #endregion

    #region "Events"
    protected void ibtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlAccessories.SelectedValue.Equals("Add New"))
        {


            gvAccessories.ShowFooter = true;
            if (dtSelectedAcc.Rows.Count == 0)
            {
                dtSelectedAccClone = dtSelectedAcc.Copy();
                ClearConstraintsOfDataTable(dtSelectedAccClone);
                dtSelectedAccClone.Rows.Add(dtSelectedAccClone.NewRow());
                gvAccessories.DataSource = dtSelectedAccClone;
                gvAccessories.DataBind();
                gvAccessories.Rows[0].Visible = false;
            }
            else
            {
                UpdateDataTable();
                BindAdditionalAccessories();
                ddlAccessories.SelectedIndex = 0;
            }
            Page.Validate("VGNewAccessory");
        }
        else
        {
            if (ddlAccessories.SelectedIndex > 0)
            {
                DataTable dt = null;
                try
                {
                    if (ddlAccessories.SelectedValue != "0")
                    {
                        #region "Insert accessory in grid"
                        //create new datatable row
                        DataRow dr = dtSelectedAcc.NewRow();

                        dr["ID"] = ddlAccessories.SelectedValue.ToString();
                        dr["AccessoryName"] = ddlAccessories.SelectedItem.Text;
                        dr["Specification"] = "";
                        dr["IsDBDriven"] = "true";

                        //add row to datatable

                        dtSelectedAcc.Rows.Add(dr);


                        //if (gvAccessories.Rows.Count > 0)
                        UpdateDataTable();


                        //bind additional acceessories to grid
                        gvAccessories.ShowFooter = false;
                        BindAdditionalAccessories();

                        UpdateDataTable();

                        dt = (DataTable)ViewState["dtAccessories"];
                        dt.PrimaryKey = new DataColumn[] { dt.Columns["Accessory"] };

                        dt.Rows.Remove(dt.Rows.Find(ddlAccessories.SelectedItem.Text));
                        ViewState["dtAccessories"] = dt;


                        FillAdditionalAccessories();



                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("ibtnAdd_Click Event : " + ex.Message);
                }
            }
            else
            {
                // UpdateDataTable();
            }
        }
    }

    private void UpdateDataTable()
    {
        for (int i = 0; i < gvAccessories.Rows.Count; i++)
        {
            dtSelectedAcc.Rows[i].BeginEdit();
            ((DataRow)dtSelectedAcc.Rows[i])[0] = (gvAccessories.Rows[i].FindControl("hdfID") as HiddenField).Value.ToString();
            ((DataRow)dtSelectedAcc.Rows[i])[2] = (gvAccessories.Rows[i].FindControl("txtSpec") as TextBox).Text;
            dtSelectedAcc.EndInit();
            dtSelectedAcc.AcceptChanges();
        }
    }
    protected void gvAccessories_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            //Remove accessory from selected accessories list
            if (e.CommandName == "RemoveAccessory")
            {
                #region "Remove accessory from grid"
                LinkButton lnkRemove = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkRemove.Parent.Parent;
                Label lblAccessory = (Label)gvRow.FindControl("lblAccessory");
                HiddenField hdfIsDBDriven = (HiddenField)gvRow.FindControl("hdfIsDBDriven");
                HiddenField hdfID = (HiddenField)gvRow.FindControl("hdfID");

                if (Convert.ToBoolean(hdfIsDBDriven.Value))
                {

                    //if (gvAccessories.Rows.Count > 0)
                    UpdateDataTable();


                    //set the primary key in datatable
                    dtSelectedAcc.PrimaryKey = new DataColumn[] { dtSelectedAcc.Columns["AccessoryName"] };

                    //find row in datatable
                    DataRow drAccessory = dtSelectedAcc.Rows.Find(lblAccessory.Text);

                    //remove accessory record from datatable
                    dtSelectedAcc.Rows.Remove(drAccessory);



                    // if (gvAccessories.Rows.Count > 0)


                    //bind additional acceessories to grid
                    BindAdditionalAccessories();



                    DataTable dt = (DataTable)ViewState["dtAccessories"];

                    DataRow dRow = dt.NewRow();
                    dRow[0] = hdfID.Value.ToString();
                    dRow[1] = lblAccessory.Text;
                    dt.Rows.Add(dRow);

                    DataView dv = dt.DefaultView;
                    dv.Sort = "Accessory ASC";
                    dt = dv.ToTable();
                    ViewState["dtAccessories"] = dt;


                    //refill accessories dropdown
                    FillAdditionalAccessories();

                #endregion
                }
                else
                {
                    UpdateDataTable();
                    dtSelectedAcc.PrimaryKey = new DataColumn[] { dtSelectedAcc.Columns["AccessoryName"] };

                    //find row in datatable
                    DataRow drAccessory = dtSelectedAcc.Rows.Find(lblAccessory.Text);

                    //remove accessory record from datatable
                    dtSelectedAcc.Rows.Remove(drAccessory);


                    // if (gvAccessories.Rows.Count > 0)


                    //bind additional acceessories to grid
                    gvAccessories.ShowFooter = false;
                    BindAdditionalAccessories();



                }
            }

        }
        catch (Exception ex)
        {
            logger.Error("gvAccessories_RowCommand Event : " + ex.Message);
        }
    }
    #endregion

    protected void ibtnSave_Click(object sender, ImageClickEventArgs e)
    {
        UpdateDataTable();
    }
    protected void ibtnSaveAddtional_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate("VGNewAccessory");

        if (Page.IsValid)
        {
            DataRow dRow = dtSelectedAcc.NewRow();
            dRow[0] = "0";
            dRow[1] = ((TextBox)gvAccessories.FooterRow.FindControl("txtAddAccessory")).Text;
            dRow[2] = ((TextBox)gvAccessories.FooterRow.FindControl("txtAddSpec")).Text;
            dRow[3] = "false";

            dtSelectedAcc.Rows.Add(dRow);

            gvAccessories.ShowFooter = false;
            BindAdditionalAccessories();
        }
    }
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        gvAccessories.ShowFooter = false;
        BindAdditionalAccessories();

        ddlAccessories.SelectedIndex = 0;
    }
}
