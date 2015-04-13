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
using log4net;
using Mechsoft.GeneralUtilities;

public partial class User_Controls_ucHotDealerSelection : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_ucHotDealerSelection));

    PagedDataSource pds = new PagedDataSource();

    #region "Properties"
    public int CurrentPage
    {

        get
        {
            if (this.ViewState["CurrentPage"] == null)
                return 0;
            else
                return Convert.ToInt16(this.ViewState["CurrentPage"].ToString());
        }

        set
        {
            this.ViewState["CurrentPage"] = value;
        }

    }

    private DataTable _dtDealers;
    public DataTable dtDealers
    {
        get
        {
            if (this.ViewState["dtDealers"] != null)
                return (DataTable)this.ViewState["dtDealers"];
            else
                return _dtDealers;
        }
        set
        {
            _dtDealers = value;
            this.ViewState["dtDealers"] = _dtDealers;
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
                // BindAllDealersGrids();  
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            }

        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event : " + ex.Message);
        }
    }

    #endregion

    #region Methods

    private void BindAllMakesNormalDealers()
    {
        Cls_Dealer objDealer = new Cls_Dealer();
        DataTable dt = new DataTable();
        try
        {
            dt = dtDealers;
            DataView dv = dt.DefaultView;
            // filter to remove hot dealers
            dv.RowFilter = "IsHotDealer = false AND IsColdDealer = false";
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            //dt=dt.Select("IsHotDealer = true");
            if (dt.Rows.Count > 0)
            {
                trShowND.Visible = true;
                gvNormalDealerDetails.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue);

            }
            else
                trShowND.Visible = false;

            ViewState["gvNormalDealer"] = dt;
            gvNormalDealerDetails.DataSource = dt;//dtDealers;//objDealer.GetAllMakesNormalDealers();
            gvNormalDealerDetails.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("Bind Normal dealer err- " + ex.Message);
        }


    }

    private void BindAllMakesHotDealers()
    {
        Cls_Dealer objDealer = new Cls_Dealer();
        //DataTable dtAllMakesNormalDalers = objDealer.GetAllMakesHotDealers();
        DataTable dt = new DataTable();

        try
        {
            dt = dtDealers;
            DataView dv = dt.DefaultView;
            //filter to remove hot dealers
            dv.RowFilter = "IsHotDealer = true AND IsColdDealer = false";
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            //dt=dt.Select("IsHotDealer = true");
            if (dt.Rows.Count > 0)
            {
                trShowHD.Visible = true;
                gvHotDealerDetails.PageSize = Convert.ToInt32(ddl_NoRecords1.SelectedValue);
            }
            else
                trShowHD.Visible = false;

            ViewState["gvHotDealer"] = dt;
            gvHotDealerDetails.DataSource = dt;// dtDealers;//dtAllMakesNormalDalers;
            gvHotDealerDetails.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error("Bind Normal dealer err- " + ex.Message);
        }

    }

    //9 jul 12
    private void BindAllMakesColdDealers()
    {
        Cls_Dealer objDealer = new Cls_Dealer();
        DataTable dt = new DataTable();

        try
        {
            dt = dtDealers;
            DataView dv = dt.DefaultView;
            //filter to remove hot dealers
            dv.RowFilter = "IsColdDealer = true and IsHotDealer = false";
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            //dt=dt.Select("IsHotDealer = true");
            if (dt.Rows.Count > 0)
            {
                trShowHD.Visible = true;
                gvColdDealer.PageSize = Convert.ToInt32(ddlColdCount.SelectedValue);
            }
            else
                trShowHD.Visible = false;

            ViewState["gvColdDealer"] = dt;
            gvColdDealer.DataSource = dt;// dtDealers;//dtAllMakesNormalDalers;
            gvColdDealer.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error("Bind Normal dealer err- " + ex.Message);
        }
    }

    /// <summary>
    /// Method to display normal dealers from point system
    /// </summary>
    private void BindNormalDealers(DataTable dt)
    {
        logger.Debug("Method Start : BindNormalDealers");
        tblNormalDealers.Visible = true;

        Cls_Dealer objDealer = new Cls_Dealer();
        try
        {
            if (dt == null)
            {
                //get dealers
                dt = objDealer.GetAllDealers();
            }

            DataView dv = dt.DefaultView;
            //filter to remove hot dealers
            dv.RowFilter = "IsHotDealer = false";
            dt = dv.ToTable();

            //set paging parameters
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            pds.CurrentPageIndex = CurrentPage;
            lnkbtnNext.Enabled = !pds.IsLastPage;
            lnkbtnPrevious.Enabled = !pds.IsFirstPage;

            if (pds.Count > 0)
            {
                dlNormalDealers.Visible = true;
                tblEmptyNormal.Visible = false;
                trPaging.Visible = true;

                //bind dealers to datalist
                dlNormalDealers.DataSource = pds;
                dlNormalDealers.DataBind();
            }
            else
            {
                dlNormalDealers.Visible = false;
                tblEmptyNormal.Visible = true;
                trPaging.Visible = false;
            }

            PerformPaging();
            //gvNormalDealerDetails.DataSource = dt;
            //gvNormalDealerDetails.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objDealer = null;
            logger.Debug("Method End : BindNormalDealers");
        }
    }

    /// <summary>
    /// Method to display hot dealers from point system
    /// </summary>
    public void BindHotDealers(DataTable dt)
    {
        logger.Debug("Method Start : BindHotDealers");

        Cls_Dealer objDealer = new Cls_Dealer();
        try
        {
            if (dt == null)
            {
                //get hot dealers
                dt = objDealer.GetHotDealers();
            }

            DataView dv = dt.DefaultView;
            //filter to remove hot dealers
            dv.RowFilter = "IsHotDealer = true";
            dt = dv.ToTable();

            if (dt.Rows.Count > 0)
            {
                dlHotDealers.Visible = true;
                tblEmptyHot.Visible = false;

                //bind hot dealers to data list
                dlHotDealers.DataSource = dt;
                dlHotDealers.DataBind();
            }
            else
            {
                dlHotDealers.Visible = false;
                tblEmptyHot.Visible = true;
            }
            //gvHotDealerDetails.DataSource = dt;
            //gvHotDealerDetails.DataBind();

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objDealer = null;
            logger.Debug("Method End : BindHotDealers");
        }
    }

    /// <summary>
    /// Public method to dispaly normal dealers and hot dealers
    /// </summary>
    public void BindDealers()
    {
        try
        {
            //BindNormalDealers(_dtDealers);
            //BindHotDealers(_dtDealers);
            BindAllDealersGrids();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Method to do the paging of datalist items
    /// </summary>
    private void PerformPaging()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");
            for (int i = 0; i < pds.PageCount; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            dlPaging.DataSource = dt;
            dlPaging.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("PerformPaging Method : " + ex.Message);
            throw;
        }
        finally
        {
            dt = null;
        }
    }

    private void EnableSelectDealerButton()
    {

    }

    // on 6 jul 12 by manoj
    private void DefineSortDirection()
    {
        if (ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] != null)
        {
            if (ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString() == Cls_Constants.VIEWSTATE_ASC)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
            }
            else
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            }

        }
    }

    #endregion

    #region "Events"

    //by manoj on 17 Jan 2012
    protected void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvNormalDealerDetails.PageIndex = 0;
        if (ddl_NoRecords2.SelectedValue.ToString() == "All")
        {
            //For view 1
            gvNormalDealerDetails.PageSize = gvNormalDealerDetails.PageCount * gvNormalDealerDetails.Rows.Count;
            gvNormalDealerDetails.DataSource = (DataTable)ViewState["gvNormalDealer"];
            gvNormalDealerDetails.DataBind();
        }
        else
        {
            //for view 1
            gvNormalDealerDetails.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
            gvNormalDealerDetails.DataSource = (DataTable)ViewState["gvNormalDealer"];
            gvNormalDealerDetails.DataBind();
        }
    }

    protected void ddl_NoRecords1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvHotDealerDetails.PageIndex = 0;
        if (ddl_NoRecords1.SelectedValue.ToString() == "All")
        {
            //For view 2
            gvHotDealerDetails.PageSize = gvHotDealerDetails.PageCount * gvHotDealerDetails.Rows.Count;
            gvHotDealerDetails.DataSource = (DataTable)ViewState["gvHotDealer"];
            gvHotDealerDetails.DataBind();
        }
        else
        {
            //for view 2
            gvHotDealerDetails.PageSize = Convert.ToInt32(ddl_NoRecords1.SelectedValue.ToString());
            gvHotDealerDetails.DataSource = (DataTable)ViewState["gvHotDealer"];
            gvHotDealerDetails.DataBind();
        }
    }

    protected void ddlColdCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvColdDealer.PageIndex = 0;
        if (ddlColdCount.SelectedValue.ToString() == "All")
        {
            //For view 2
            gvColdDealer.PageSize = gvColdDealer.PageCount * gvColdDealer.Rows.Count;
            gvColdDealer.DataSource = (DataTable)ViewState["gvColdDealer"];
            gvColdDealer.DataBind();
        }
        else
        {
            //for view 2
            gvColdDealer.PageSize = Convert.ToInt32(ddlColdCount.SelectedValue.ToString());
            gvColdDealer.DataSource = (DataTable)ViewState["gvColdDealer"];
            gvHotDealerDetails.DataBind();
        }
    }

    //end

    protected void datalist1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        Cls_Dealer objDealer = new Cls_Dealer();
        try
        {
            ImageButton btnSelect = (ImageButton)e.CommandSource;
            DataListItem dlItem = (DataListItem)btnSelect.Parent;
            int intDealerId = Convert.ToInt32(((HiddenField)dlItem.FindControl("hfDealerID")).Value);

            objDealer.DealerID = intDealerId;
            objDealer.IsHotDealer = true;
            objDealer.MarkDealerAsHotOrNormal();

            if (ViewState["dtDealers"] != null)
            {
                //set the primary key in datatable
                DataTable tempdt = (DataTable)ViewState["dtDealers"];
                tempdt.Columns["IsHotDealer"].ReadOnly = false;
                tempdt.PrimaryKey = new DataColumn[] { tempdt.Columns["ID"] };

                //find row in datatable
                DataRow drDealer = tempdt.Rows.Find(intDealerId);

                if (drDealer != null)
                {
                    drDealer.BeginEdit();
                    drDealer["IsHotDealer"] = true;
                    drDealer.EndEdit();
                }
                dtDealers = tempdt;
            }
            BindDealers();

            //#region check if dealer is already selected or not

            ////set the primary key in datatable
            //dtSelectedDealers.PrimaryKey = new DataColumn[] { dtSelectedDealers.Columns["ID"] };

            ////find row in datatable
            //DataRow drDealer = dtSelectedDealers.Rows.Find(intDealerId);
            //#endregion

            ////If dealer is not selected
            //if (drDealer == null)
            //{
            //    //Insert selected dealer details in datatable
            //    DataRow dr = dtSelectedDealers.NewRow();

            //    dr["ID"] = intDealerId;
            //    dr["Name"] = ((Label)dlItem.FindControl("NameLabel")).Text;
            //    dr["Phone"] = ((Label)dlItem.FindControl("PhoneLabel")).Text;
            //    dr["Email"] = ((Label)dlItem.FindControl("EmailLabel")).Text;
            //    dr["Fax"] = ((Label)dlItem.FindControl("FaxLabel")).Text;
            //    dr["IsHotDealer"] = ((HiddenField)dlItem.FindControl("hdfIsHotDealer")).Value;

            //    //add row in datatable
            //    dtSelectedDealers.Rows.Add(dr);
            //}

            //btnSelect.Enabled = false;
            //btnSelect.ImageUrl = "~/Images/Select_dealer_disabled.gif";
        }
        catch (Exception ex)
        {
            logger.Error("datalist_ItemCommand Event : " + ex.Message);
        }
    }

    protected void datalist2_ItemCommand(object source, DataListCommandEventArgs e)
    {
        Cls_Dealer objDealer = new Cls_Dealer();
        try
        {
            ImageButton btnSelect = (ImageButton)e.CommandSource;
            DataListItem dlItem = (DataListItem)btnSelect.Parent;
            int intDealerId = Convert.ToInt32(((HiddenField)dlItem.FindControl("hfDealerID")).Value);

            objDealer.DealerID = intDealerId;
            objDealer.IsHotDealer = false;
            objDealer.MarkDealerAsHotOrNormal();

            if (ViewState["dtDealers"] != null)
            {
                //set the primary key in datatable
                DataTable tempdt = (DataTable)ViewState["dtDealers"];

                tempdt.PrimaryKey = new DataColumn[] { tempdt.Columns["ID"] };

                //find row in datatable
                DataRow drDealer = tempdt.Rows.Find(intDealerId);

                if (drDealer != null)
                {
                    drDealer.BeginEdit();
                    drDealer["IsHotDealer"] = false;
                    drDealer.EndEdit();
                }
                dtDealers = tempdt;
            }
            BindDealers();
        }
        catch (Exception ex)
        {
            logger.Error("datalist_ItemCommand Event : " + ex.Message);
        }
    }

    #region "Paging Events"

    protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("lnkbtnPaging"))
            {
                CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
                BindNormalDealers(_dtDealers);
            }
        }
        catch (Exception ex)
        {
            logger.Error("lnkbtnPrevious_Click Event : " + ex.Message);
        }
    }

    protected void lnkbtnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentPage -= 1;
            BindNormalDealers(_dtDealers);
        }
        catch (Exception ex)
        {
            logger.Error("lnkbtnPrevious_Click Event : " + ex.Message);
        }
    }

    protected void lnkbtnNext_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentPage += 1;
            BindNormalDealers(_dtDealers);
        }
        catch (Exception ex)
        {
            logger.Error("lnkbtnNext_Click Event : " + ex.Message);
        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CurrentPage = 0;
            BindNormalDealers(_dtDealers);
        }
        catch (Exception ex)
        {
            logger.Error("ddlPageSize_SelectedIndexChanged Event : " + ex.Message);
        }
    }

    protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
        {
            lnkbtnPage.Enabled = false;
            lnkbtnPage.Font.Bold = true;
        }
    }

    #endregion

    protected void btnSelect_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnHotDealers_Click(object sender, ImageClickEventArgs e)
    {

    }


    private void BindAllDealersGrids()
    {

        BindAllMakesNormalDealers();
        BindAllMakesHotDealers();
        //on 9 jul 12
        BindAllMakesColdDealers();
    }


    protected void gvHotDealerDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvHotDealers_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("MakeDealerNormal"))
        {
            int RowID = Convert.ToInt16(e.CommandArgument.ToString());
            RowID = RowID - (gvHotDealerDetails.PageSize * gvHotDealerDetails.PageIndex);
            int intDealerId = Convert.ToInt16(gvHotDealerDetails.DataKeys[RowID][0].ToString());
            Cls_Dealer objDealer = new Cls_Dealer();
            objDealer.DealerID = intDealerId;
            objDealer.IsHotDealer = false;
            objDealer.IsColdDealer = false;
            objDealer.MarkDealerAsHotOrNormal();
            //BindAllDealersGrids();
            if (ViewState["dtDealers"] != null)
            {
                //set the primary key in datatable
                DataTable tempdt = (DataTable)ViewState["dtDealers"];
                tempdt.Columns["IsHotDealer"].ReadOnly = false;
                tempdt.Columns["IsColdDealer"].ReadOnly = false;
                tempdt.PrimaryKey = new DataColumn[] { tempdt.Columns["ID"] };

                //find row in datatable
                DataRow drDealer = tempdt.Rows.Find(intDealerId);

                if (drDealer != null)
                {
                    drDealer.BeginEdit();
                    drDealer["IsHotDealer"] = false;
                    drDealer["IsColdDealer"] = false;
                    drDealer.EndEdit();
                }
                dtDealers = tempdt;
            }

        }
        else if (e.CommandName.Equals("MakeDealerCold"))
        {
            int RowID = Convert.ToInt16(e.CommandArgument.ToString());
            RowID = RowID - (gvHotDealerDetails.PageSize * (gvHotDealerDetails.PageIndex));
            int intDealerId = Convert.ToInt16(gvHotDealerDetails.DataKeys[RowID][0].ToString());
            Cls_Dealer objDealer = new Cls_Dealer();
            objDealer.DealerID = intDealerId;
            objDealer.IsHotDealer = false;
            objDealer.IsColdDealer = true;
            objDealer.MarkDealerAsHotOrNormal();
            //BindAllDealersGrids();
            if (ViewState["dtDealers"] != null)
            {
                //set the primary key in datatable
                DataTable tempdt = (DataTable)ViewState["dtDealers"];
                tempdt.Columns["IsHotDealer"].ReadOnly = false;
                tempdt.Columns["IsColdDealer"].ReadOnly = false;
                tempdt.PrimaryKey = new DataColumn[] { tempdt.Columns["ID"] };

                //find row in datatable
                DataRow drDealer = tempdt.Rows.Find(intDealerId);

                if (drDealer != null)
                {
                    drDealer.BeginEdit();
                    drDealer["IsHotDealer"] = false;
                    drDealer["IsColdDealer"] = true;
                    drDealer.EndEdit();
                }
                dtDealers = tempdt;
            }

        }
        BindDealers();
        DealersTabContainer.ActiveTab = HotDealers;
    }

    protected void gvHotDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHotDealerDetails.PageIndex = e.NewPageIndex;
        //DataTable dt = new DataTable();
        //dt = (DataTable)ViewState["dtDealers"];
        //DataView dv = dt.DefaultView;
        ////filter to remove hot dealers
        //dv.RowFilter = "IsHotDealer = true";
        //dt = dv.ToTable();
        BindAllMakesHotDealers();
    }

    protected void gvHotDealerDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        // BindData(objCourseMaster);
        BindDealers();
    }


    protected void gvNormalDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNormalDealerDetails.PageIndex = e.NewPageIndex;
        //DataTable dt = new DataTable();
        //dt = (DataTable)ViewState["dtDealers"];
        //DataView dv = dt.DefaultView;
        //filter to remove hot dealers
        //dv.RowFilter = "IsHotDealer = false";
        //dt = dv.ToTable();
        BindAllMakesNormalDealers();
    }

    protected void gvNormalDealerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("MakeHotDealer"))
        {
            int RowID = Convert.ToInt16(e.CommandArgument.ToString());
            RowID = RowID - (gvNormalDealerDetails.PageSize * (gvNormalDealerDetails.PageIndex));
            int intDealerId = Convert.ToInt16(gvNormalDealerDetails.DataKeys[RowID][0].ToString());
            Cls_Dealer objDealer = new Cls_Dealer();
            objDealer.DealerID = intDealerId;
            objDealer.IsHotDealer = true;
            objDealer.IsColdDealer = false;
            objDealer.MarkDealerAsHotOrNormal();
            //BindAllDealersGrids();
            if (ViewState["dtDealers"] != null)
            {
                //set the primary key in datatable
                DataTable tempdt = (DataTable)ViewState["dtDealers"];
                tempdt.Columns["IsHotDealer"].ReadOnly = false;
                tempdt.Columns["IsColdDealer"].ReadOnly = false;
                tempdt.PrimaryKey = new DataColumn[] { tempdt.Columns["ID"] };

                //find row in datatable
                DataRow drDealer = tempdt.Rows.Find(intDealerId);

                if (drDealer != null)
                {
                    drDealer.BeginEdit();
                    drDealer["IsHotDealer"] = true;
                    drDealer["IsColdDealer"] = false;
                    drDealer.EndEdit();
                }
                dtDealers = tempdt;
            }

        }
        else if (e.CommandName.Equals("MakeDealerCold"))
        {
            int RowID = Convert.ToInt16(e.CommandArgument.ToString());
            RowID = RowID - (gvNormalDealerDetails.PageSize * (gvNormalDealerDetails.PageIndex));
            int intDealerId = Convert.ToInt16(gvNormalDealerDetails.DataKeys[RowID][0].ToString());
            Cls_Dealer objDealer = new Cls_Dealer();
            objDealer.DealerID = intDealerId;
            objDealer.IsHotDealer = false;
            objDealer.IsColdDealer = true;
            objDealer.MarkDealerAsHotOrNormal();
            //BindAllDealersGrids();
            if (ViewState["dtDealers"] != null)
            {
                //set the primary key in datatable
                DataTable tempdt = (DataTable)ViewState["dtDealers"];
                tempdt.Columns["IsColdDealer"].ReadOnly = false;
                tempdt.Columns["IsHotDealer"].ReadOnly = false;
                tempdt.PrimaryKey = new DataColumn[] { tempdt.Columns["ID"] };

                //find row in datatable
                DataRow drDealer = tempdt.Rows.Find(intDealerId);

                if (drDealer != null)
                {
                    drDealer.BeginEdit();
                    drDealer["IsHotDealer"] = false;
                    drDealer["IsColdDealer"] = true;
                    drDealer.EndEdit();
                }
                dtDealers = tempdt;
            }

        }
        BindDealers();
        DealersTabContainer.ActiveTab = NormalDealers;
    }

    protected void gvNormalDealerDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        // BindData(objCourseMaster);
        BindDealers();
    }


    //on 9 jul 12
    protected void gvColdDealer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHotDealerDetails.PageIndex = e.NewPageIndex;
        BindAllMakesColdDealers();
    }

    protected void gvColdDealer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("MakeDealerNormal"))
        {
            int RowID = Convert.ToInt16(e.CommandArgument.ToString());
            RowID = RowID - (gvColdDealer.PageSize * gvColdDealer.PageIndex);
            int intDealerId = Convert.ToInt16(gvColdDealer.DataKeys[RowID][0].ToString());
            Cls_Dealer objDealer = new Cls_Dealer();
            objDealer.DealerID = intDealerId;
            objDealer.IsHotDealer = false;
            objDealer.IsColdDealer = false;
            objDealer.MarkDealerAsHotOrNormal();
            //BindAllDealersGrids();
            if (ViewState["dtDealers"] != null)
            {
                //set the primary key in datatable
                DataTable tempdt = (DataTable)ViewState["dtDealers"];
                tempdt.Columns["IsHotDealer"].ReadOnly = false;
                tempdt.Columns["IsColdDealer"].ReadOnly = false;
                tempdt.PrimaryKey = new DataColumn[] { tempdt.Columns["ID"] };

                //find row in datatable
                DataRow drDealer = tempdt.Rows.Find(intDealerId);

                if (drDealer != null)
                {
                    drDealer.BeginEdit();
                    drDealer["IsHotDealer"] = false;
                    drDealer["IsColdDealer"] = false;
                    drDealer.EndEdit();
                }
                dtDealers = tempdt;
            }

        }
        else if (e.CommandName.Equals("MakeDealerHot"))
        {
            int RowID = Convert.ToInt16(e.CommandArgument.ToString());
            RowID = RowID - (gvColdDealer.PageSize * (gvColdDealer.PageIndex));
            int intDealerId = Convert.ToInt16(gvColdDealer.DataKeys[RowID][0].ToString());
            Cls_Dealer objDealer = new Cls_Dealer();
            objDealer.DealerID = intDealerId;
            objDealer.IsHotDealer = true;
            objDealer.IsColdDealer = false;
            objDealer.MarkDealerAsHotOrNormal();
            //BindAllDealersGrids();
            if (ViewState["dtDealers"] != null)
            {
                //set the primary key in datatable
                DataTable tempdt = (DataTable)ViewState["dtDealers"];
                tempdt.Columns["IsHotDealer"].ReadOnly = false;
                tempdt.Columns["IsColdDealer"].ReadOnly = false;
                tempdt.PrimaryKey = new DataColumn[] { tempdt.Columns["ID"] };

                //find row in datatable
                DataRow drDealer = tempdt.Rows.Find(intDealerId);

                if (drDealer != null)
                {
                    drDealer.BeginEdit();
                    drDealer["IsHotDealer"] = true;
                    drDealer["IsColdDealer"] = false;
                    drDealer.EndEdit();
                }
                dtDealers = tempdt;
            }

        }
        BindDealers();
        DealersTabContainer.ActiveTab = tabPanelColdDealer;
    }

    protected void gvColdDealer_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        // BindData(objCourseMaster);
        BindDealers();
    }
    //end

    #endregion
}
