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


public partial class User_Controls_Request_ucRequestParameters : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_Request_ucRequestParameters));
    DataTable dtParams = null;
    //static DataTable dtSelectedAcc = null;
    //static DataTable dtSelectedAccClone = null;

    #region "Variables and Properties"

    string[] AccDetails = new string[6];
    private DataTable _dtParameters;
    public DataTable dtParameters
    {
        get
        {
            dtParams = (DataTable)ViewState["DTPARAMS"];
            if (ViewState["dtParameters"] != null)
                _dtParameters = dtParams;// (DataTable)ViewState["dtParameters"];
            return _dtParameters;

        }
        set
        {
            _dtParameters = value;
            // ViewState["dtParameters"] = _dtParameters;
        }
    }
    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        logger.Debug("UC request parameter page load Method Start =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {
            lblMsgAcc.Visible = false;
            lblMsgAcc_1.Visible = false;
            if (!IsPostBack)
            {
                if (Convert.ToString(Request.QueryString["moveto"]) != "incomplete")
                {
                    BuildDataTable();
                    BuildDataTable1();
                    BindAdditionalAccTable();
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event : " + ex.Message);
        }
        finally
        {
            logger.Debug("UC request parameter page load Method ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }
    #endregion

    #region "Methods"
    /// <summary>
    /// create datatable for selected additional accessories
    /// </summary>
    public void BuildDataTable()
    {
        dtParams = new DataTable();
        logger.Debug("Method Start : BuildDataTable");

        try
        {
            DataColumn colString = new DataColumn("ID");
            //colString.DataType = System.Type.GetType("System.String");
            dtParams.Columns.Add(colString);
            dtParams.Columns.Add("AccessoryName");
            dtParams.Columns.Add("Specification");
            dtParams.Columns.Add("IsDBDriven");
            dtParams.Columns[1].Unique = true;
            ClearConstraintsOfDataTable(dtParams);
            ViewState["DTPARAMS"] = dtParams;

        }
        catch (Exception ex)
        {
            logger.Error("Method Error : BuildDataTable - " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("Method End : BuildDataTable");
        }
    }

    private void ClearConstraintsOfDataTable(DataTable dtParams)
    {
        dtParams.Columns["IsDBDriven"].AllowDBNull = true;
        dtParams.Columns["AccessoryName"].AllowDBNull = true;
        dtParams.Columns["Specification"].AllowDBNull = true;
        dtParams.Columns[0].AllowDBNull = true;
        ViewState["DTPARAMS"] = dtParams;
    }

    /// <summary>
    /// Method to bind parameters to grid
    /// </summary>
    public void BindParameters()
    {
        logger.Debug("UC request parameter Bind parameter Method ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        logger.Debug("Method Start : BindParameters");

        try
        {

            TemplateField TempFeild = new TemplateField();
            TempFeild.HeaderTemplate = new gvTemlateCreatorForAutoCompleteExtender(DataControlRowType.Header, "Value", "Specification");
            TempFeild.ItemTemplate = new gvTemlateCreatorForAutoCompleteExtender(DataControlRowType.DataRow, "Value", "Specification");
            TempFeild.HeaderTemplate.InstantiateIn(gvParameters);
            TempFeild.ItemTemplate.InstantiateIn(gvParameters);

            gvParameters.Columns.Add(TempFeild);



            DataTable dt = null;
            if (ViewState["dtParameters"] == null)
            {
                Cls_Accessories objAccessory = new Cls_Accessories();
                logger.Debug("UC request parameter get active parameter DB starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                dt = objAccessory.GetActiveParameters();
                logger.Debug("UC request parameter get active parameter DB ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                ViewState["dtParameters"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["dtParameters"];
            }
            foreach (DataRow drTemp in dt.Rows)
            {
                if (drTemp["accessoryname"].Equals("Body Type"))
                {
                    drTemp["accessoryname"] = "<strong>Body Type </strong>(eg 3-Door Hatch, Wagon, 7-Seater etc)";

                }
                else if (drTemp["accessoryname"].Equals("Registration Type"))
                {
                    drTemp["accessoryname"] = "<strong>Registration Type </strong> (eg Private, Pensioner etc)";
                }
                else if (drTemp["accessoryname"].Equals("Transmission") || drTemp["accessoryname"].Equals("Colour") || drTemp["accessoryname"].Equals("Fuel Type"))
                {
                    drTemp["accessoryname"] = "<strong>" + drTemp["accessoryname"] + "</strong>";
                }
            }

            //bind selected additional accessories to grid
            gvParameters.DataSource = dt;
            gvParameters.DataBind();


        }
        catch (Exception ex)
        {
            logger.Error("BindParameters Method : " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("UC request parameter Bind parameter Method ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            logger.Debug("Method End : BindParameters");
        }
    }

    public void MethodForSelectParam()
    {
        try
        {
            dtParams = (DataTable)ViewState["DTPARAMS"];
            if (dtParams != null)
            {
                dtParams.Rows.Clear();
            }
            int i = 1;
            foreach (GridViewRow gvRow in gvParameters.Rows)
            {
                TextBox txt = (TextBox)gvRow.FindControl("txtValue" + i);
                Label lbl = (Label)gvRow.FindControl("lblAccessory");
                HiddenField hdfID = (HiddenField)gvRow.FindControl("hdfID");
                if (dtParams != null)
                {
                    DataRow dr = dtParams.NewRow();

                    if (hdfID != null)
                        dr["ID"] = hdfID.Value;

                    if (lbl != null)
                        dr["AccessoryName"] = lbl.Text;

                    if (txt != null)
                        dr["Specification"] = txt.Text;

                    dr["IsDBDriven"] = "True";
                    dtParams.Rows.Add(dr);
                    ViewState["DTPARAMS"] = dtParams;
                    i++;
                }
            }


            ViewState["dtParameters"] = dtParams;


        }
        catch (Exception ex)
        {
            logger.Error("ibtnSave_Click Event : " + ex.Message);
        }
    }
    #endregion

    #region "Events"
    //protected void ibtnSave_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        dtParams.Rows.Clear();
    //        int i = 1;
    //        foreach (GridViewRow gvRow in gvParameters.Rows)
    //        {
    //            TextBox txt = (TextBox)gvRow.FindControl("txtValue" + i);
    //            Label lbl = (Label)gvRow.FindControl("lblAccessory");
    //            HiddenField hdfID = (HiddenField)gvRow.FindControl("hdfID");
    //            DataRow dr = dtParams.NewRow();

    //            if (hdfID != null)
    //                dr["ID"] = hdfID.Value;

    //            if (lbl != null)
    //                dr["AccessoryName"] = lbl.Text;

    //            if (txt != null)
    //                dr["Specification"] = txt.Text;

    //            dr["IsDBDriven"] = "True";
    //            dtParams.Rows.Add(dr);
    //            i++;

    //        }
    //        ViewState["dtParameters"] = dtParams;


    //    }
    //    catch (Exception ex)
    //    {
    //        logger.Error("ibtnSave_Click Event : " + ex.Message);
    //    }
    //}
    #endregion

    protected void gvParameters_DataBound(object sender, EventArgs e)
    {

    }

    protected void gvParameters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtValue1 = (TextBox)e.Row.FindControl("txtValue1");
            if (txtValue1 != null)
            {
                txtValue1.CssClass = "txtBold";
            }
            TextBox txtValue2 = (TextBox)e.Row.FindControl("txtValue2");
            if (txtValue2 != null)
            {
                txtValue2.CssClass = "txtBold";
            }
            TextBox txtValue3 = (TextBox)e.Row.FindControl("txtValue3");
            if (txtValue3 != null)
            {
                txtValue3.CssClass = "txtBold";
            }
            TextBox txtValue4 = (TextBox)e.Row.FindControl("txtValue4");
            if (txtValue4 != null)
            {
                txtValue4.CssClass = "txtBold";
            }
            //by manoj on 11 mar 2011 for adding extra parameter fual type
            TextBox txtValue5 = (TextBox)e.Row.FindControl("txtValue5");
            if (txtValue5 != null)
            {
                txtValue5.CssClass = "txtBold";
            }
        }
    }

    #region Merged events of Additional accesories user control
    //declare and initialize logger object
    // static ILog logger = LogManager.GetLogger(typeof(User_Controls_Request_ucAdditionalAccessories));
    public DataTable dtSelectedAcc = null;
    public DataTable dtSelectedAccClone = null;

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

    private int _modelId;
    public int ModelId
    {
        get { return _modelId; }
        set { _modelId = value; }
    }

    private DataTable _dtAccessories;
    public DataTable dtAccessories
    {
        get
        {
            dtSelectedAcc = (DataTable)ViewState["SELECT_ACC"];
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

    #region "Methods"
    /// <summary>
    /// Fill additional accessories dropdown
    /// </summary>
    public void FillAdditionalAccessories()
    {
        logger.Debug("FillAdditionalAccessories Method Start");
        Cls_Accessories objHelper = new Cls_Accessories();
        Cls_Request objRequest = new Cls_Request();

        DataTable dt = null;

        try
        {
            if (Cache["ACCESSORIES"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadMasterAccessories();

            if (ViewState["dtAccessories"] != null)
                dt = (DataTable)ViewState["dtAccessories"];
            else
            {
                // dt = Cache["ACCESSORIES"] as DataTable;
                Cls_Accessories accessories = new Cls_Accessories();
                dt = accessories.GetAdditionalAccessoriesForSeries();
                ViewState["dtAccessories"] = dt;
            }

            //clear accessories dropdown
            ddlAccessories.Items.Clear();

            if (dt != null)
            {
                //bind accessories to dropdown
                ddlAccessories.DataSource = dt;
                ddlAccessories.DataTextField = "Accessory";
                ddlAccessories.DataValueField = "ID";
                ddlAccessories.DataBind();
            }

            //insert default item in dropdown
            ddlAccessories.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlAccessories.Items.Insert(1, new ListItem("Add New Accessory", "Add New"));
            //ddlAccessories.Items.Add(new ListItem("Add New Accessory", "Add New"));
        }
        catch (Exception ex)
        {
            logger.Error("FillAdditionalAccessories Method Error =" + ex.Message);
            throw;
        }
        finally
        {
        }
    }

    private void ClearSelectionOfNonAssocitatedElements()
    {
        if (ViewState["dtAccessories"] != null)
        {
            try
            {
                dtSelectedAcc = (DataTable)ViewState["SELECT_ACC"];
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
                                ViewState["SELECT_ACC"] = dtSelectedAcc;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                logger.Error("Method Error : " + Ex.Message);
            }
            BindAdditionalAccessories();
        }
    }

    /// <summary>
    /// create datatable for selected additional accessories
    /// </summary>
    public void BuildDataTable1()
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
            ClearConstraintsOfDataTable1(dtSelectedAcc);

            //ViewState["SELECT_ACC"] = dtSelectedAcc;

        }
        catch (Exception ex)
        {
            logger.Error("Method Error: BuildDataTable - " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("Method End : BuildDataTable");
        }
    }

    private void ClearConstraintsOfDataTable1(DataTable dtSelectedAcc)
    {
        dtSelectedAcc.Columns["IsDBDriven"].AllowDBNull = true;
        dtSelectedAcc.Columns["AccessoryName"].AllowDBNull = true;
        dtSelectedAcc.Columns["Specification"].AllowDBNull = true;
        dtSelectedAcc.Columns[0].AllowDBNull = true;
        ViewState["SELECT_ACC"] = dtSelectedAcc;
    }

    /// <summary>
    /// Method to bind addtional accessories to grid
    /// </summary>
    public void BindAdditionalAccessories()
    {
        logger.Debug("Method Start : BindAdditionalAccessories");

        try
        {
            dtSelectedAcc = (DataTable)ViewState["SELECT_ACC"];
            //bind selected additional accessories to grid

            gvAccessories.DataSource = dtSelectedAcc;
            gvAccessories.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error("Method Error : BindAdditionalAccessories - " + ex.Message);
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
        logger.Debug("UC request parameter add new acc starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        dtSelectedAcc = (DataTable)ViewState["SELECT_ACC"];
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

                ViewState["SELECT_ACCCLONE"] = dtSelectedAccClone;
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

                        ViewState["SELECT_ACC"] = dtSelectedAcc;


                        //if (gvAccessories.Rows.Count > 0)
                        UpdateDataTable();


                        //bind additional acceessories to grid
                        gvAccessories.ShowFooter = false;
                        BindAdditionalAccessories();



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
        logger.Debug("UC request parameter add new acc ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
    }

    public void UpdateDataTable()
    {
        dtSelectedAcc = (DataTable)ViewState["SELECT_ACC"];
        for (int i = 0; i < gvAccessories.Rows.Count; i++)
        {
            dtSelectedAcc.Rows[i].BeginEdit();
            ((DataRow)dtSelectedAcc.Rows[i])[0] = (gvAccessories.Rows[i].FindControl("hdfID") as HiddenField).Value.ToString();
            ((DataRow)dtSelectedAcc.Rows[i])[2] = (gvAccessories.Rows[i].FindControl("txtSpec") as TextBox).Text;
            dtSelectedAcc.EndInit();
            dtSelectedAcc.AcceptChanges();
            ViewState["SELECT_ACC"] = dtSelectedAcc;
        }

    }
    protected void gvAccessories_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            dtSelectedAcc = (DataTable)ViewState["SELECT_ACC"];
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

                    ViewState["SELECT_ACC"] = dtSelectedAcc;

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

                    ViewState["SELECT_ACC"] = dtSelectedAcc;

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

    protected void ibtnSave1_Click(object sender, ImageClickEventArgs e)
    {
        UpdateDataTable();

    }
    #endregion

    // To show additional acc table with default three rows
    public void BindAdditionalAccTable()
    {
        // to add default 3 row for add new
        DataTable dtParams1 = new DataTable();
        dtParams1.Columns.Add("ID");
        dtParams1.Columns.Add("AccessoryName");
        dtParams1.Columns.Add("Specification");
        dtParams1.Columns.Add("IsDBDriven");

        //dtSelectedAccClone = dtSelectedAcc.Clone();
        //ClearConstraintsOfDataTable(dtSelectedAccClone);
        //dtSelectedAccClone.Columns["AccessoryName"].Unique = false;
        dtParams1.Rows.Add(dtParams1.NewRow());
        dtParams1.Rows.Add(dtParams1.NewRow());
        dtParams1.Rows.Add(dtParams1.NewRow());
        gvAddAcc.DataSource = dtParams1;
        gvAddAcc.DataBind();
        //end  
    }

    public void ddlAccessories_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //FUnctionForIbtnAdd();
            dtSelectedAcc = (DataTable)ViewState["SELECT_ACC"];
            // gvAccessories.ShowFooter = true;
            if (ddlAccessories.SelectedValue.Equals("Add New"))
            {
                //  gvAccessories.ShowFooter = true;


                lblMsgAcc.Visible = false;
                gvAddAcc.Visible = true;

                BindAdditionalAccTable();

                //old commented by manoj
                //if (dtSelectedAcc.Rows.Count == 0)
                //{
                //    dtSelectedAccClone = dtSelectedAcc.Copy();
                //    ClearConstraintsOfDataTable(dtSelectedAccClone);
                //    dtSelectedAccClone.Rows.Add(dtSelectedAccClone.NewRow());
                //    gvAccessories.DataSource = dtSelectedAccClone;
                //    gvAccessories.DataBind();
                //    gvAccessories.Rows[0].Visible = false;

                //   // ViewState["SELECT_ACCCLONE"] = dtSelectedAccClone;
                //}
                //else
                //{
                //    UpdateDataTable();
                //    BindAdditionalAccessories();
                //    ddlAccessories.SelectedIndex = 0;
                //}
                ddlAccessories.SelectedIndex = 0;
                Page.Validate("VGNewAccessory");
            }
            else
            {
                // gvAccessories.ShowFooter = false;
                gvAddAcc.Visible = true;
                gvAddAcc.DataSource = null;
                gvAddAcc.DataBind();
                gvAddAcc.Visible = false;
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

                            ViewState["SELECT_ACC"] = dtSelectedAcc;

                            //if (gvAccessories.Rows.Count > 0)
                            UpdateDataTable();


                            //bind additional acceessories to grid
                            //gvAccessories.ShowFooter = false;
                            BindAdditionalAccessories();



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
        catch (Exception ex)
        {
            logger.Error("ibtnAdd_Click Event : " + ex.Message);
            throw ex;
        }
    }
    protected void gvAddAcc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "AddSubmit")
            {
                ImageButton lnkRemove = (ImageButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkRemove.Parent.Parent;
                logger.Debug("UC request parameter save add. acc starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                Cls_Accessories objHelper = new Cls_Accessories();
                //Page.Validate("VGNewAccessory");
                dtSelectedAcc = (DataTable)ViewState["SELECT_ACC"];
                if (((TextBox)gvRow.FindControl("txtAddAccessory")).Text.Equals(String.Empty))
                {
                    lblMsgAcc.Visible = true;
                }
                else
                {
                    if (Page.IsValid)
                    {

                        objHelper.AddNewAccessory(((TextBox)gvRow.FindControl("txtAddAccessory")).Text);
                        int ID = objHelper.GetId(((TextBox)gvRow.FindControl("txtAddAccessory")).Text);
                        DataRow dRow = dtSelectedAcc.NewRow();
                        dRow[0] = ID;
                        dRow[1] = ((TextBox)gvRow.FindControl("txtAddAccessory")).Text;
                        dRow[2] = ((TextBox)gvRow.FindControl("txtAddSpec")).Text;
                        dRow[3] = "True";

                        dtSelectedAcc.Rows.Add(dRow);
                        ViewState["SELECT_ACC"] = dtSelectedAcc;


                        BindAdditionalAccessories();

                        gvRow.Visible = false;
                        removeHeading();

                    }
                    logger.Debug("UC request parameter save add. acc ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                }
            }
            else if (e.CommandName == "AddCancel")
            {
                ImageButton lnkRemove = (ImageButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkRemove.Parent.Parent;
                gvRow.Visible = false;

                removeHeading();

            }

        }
        catch (Exception ex)
        {
            logger.Error("gvAddAcc_RowCommand error=" + ex.Message);
            throw ex;
        }
    }

    public void removeHeading()
    {
        int cnt = 0;
        foreach (GridViewRow gvr in ((GridView)FindControl("gvAddAcc")).Rows)
        {
            if (!gvr.Visible)
                cnt++;
        }
        if (cnt == 3)
        {
            ((GridView)FindControl("gvAddAcc")).DataSource = null;
            ((GridView)FindControl("gvAddAcc")).DataBind();
        }

    }

    // on 2 jul 2012 for add moredealer to quote request after quote creation
    public DataTable UpdateAddAccViewState(DataTable dtTemp)
    {
        DataTable dt1 = new DataTable();
        DataTable dtDllAcc = new DataTable();
        try
        {
            //if (dtTemp != null)
            //    ViewState["SELECT_ACC"] = dtTemp;

            dt1.Columns.Add("ID");
            dt1.Columns.Add("AccessoryName");
            dt1.Columns.Add("Specification");
            dt1.Columns.Add("IsDBDriven");
            dt1.Columns[1].Unique = true;
            ClearConstraintsOfDataTable1(dt1);

            dtDllAcc = null;
            dtDllAcc = (DataTable)ViewState["dtAccessories"];

            DataRow dr = null;
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                dr = null;
                dr = dt1.NewRow();
                dr["ID"] = dtTemp.Rows[i]["ID"];
                dr["AccessoryName"] = dtTemp.Rows[i]["accessoryname"];
                dr["Specification"] = dtTemp.Rows[i]["Specification"];
                dr["IsDBDriven"] = "true";

                dt1.Rows.Add(dr);

                //Remove selected acc from drop downlist
                dtDllAcc.PrimaryKey = new DataColumn[] { dtDllAcc.Columns["Accessory"] };
                if (dtDllAcc.Rows.Find(dtTemp.Rows[i]["accessoryname"]) != null)
                {
                    dtDllAcc.Rows.Remove(dtDllAcc.Rows.Find(dtTemp.Rows[i]["accessoryname"]));
                    ViewState["dtAccessories"] = dtDllAcc;
                    FillAdditionalAccessories();
                }
                //end
            }
            ViewState["SELECT_ACC"] = dt1;
        }
        catch (Exception ex)
        {
        }
        return dt1;
    }//end
}
